using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GerenciadorPAK
{
    public static class CommandLine
    {
        private static readonly string TempDir = Path.Combine(Path.GetTempPath(), "GerenciadorSBPAKTemp");
        private static readonly string TempAppPath = Path.Combine(TempDir, "SBPacker.exe");
        private static readonly string TempInput = Path.Combine(TempDir, "input.txt");
        private static string sbPackerSourcePath = "";

        public static void SetSBPackerPath(string path)
        {
            sbPackerSourcePath = path;
        }

        public static string[] GetFileNameList(string packPath)
        {
            Directory.SetCurrentDirectory(Application.StartupPath);
            GenerateApp();

            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = TempAppPath,
                    Arguments = $"EN -l \"{packPath}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            p.Start();

            Thread.Sleep(1000);

            var o = p.StandardOutput;
            // Skip header lines
            o.ReadLine();
            o.ReadLine();
            o.ReadLine();
            o.ReadLine();

            var l = new List<string>();
            int n = 0;
            while (true)
            {
                if (p.WaitForExit(500))
                    break;

                while (!o.EndOfStream)
                {
                    l.Add(o.ReadLine());
                }

                if (n == 20)
                {
                    p.Kill();
                    throw new TimeoutException();
                }
                n++;
            }

            while (!o.EndOfStream)
            {
                l.Add(o.ReadLine());
            }

            RemoveApp();
            return l.ToArray();
        }

        public static void PackFileInList(string source, string packPath, string[] list)
        {
            source = GetAbsolutePath(source, Application.StartupPath);
            packPath = GetAbsolutePath(packPath, Application.StartupPath);
            Directory.SetCurrentDirectory(source);
            GenerateApp();

            using (var i = new StreamWriter(TempInput, false, Encoding.Default))
            {
                foreach (string line in list)
                {
                    if (line.EndsWith(".bik") && !line.StartsWith("*"))
                    {
                        i.WriteLine("*" + line);
                    }
                    else
                    {
                        i.WriteLine(line);
                    }
                }
            }

            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = TempAppPath,
                    Arguments = $"EN -a \"{TempInput}\" \"{packPath}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            p.Start();

            Thread.Sleep(1000);

            var o = p.StandardOutput;
            // Skip header lines
            o.ReadLine();
            o.ReadLine();
            o.ReadLine();
            o.ReadLine();

            var err = new StringBuilder();
            int n = 0;
            while (true)
            {
                if (p.WaitForExit(500))
                    break;

                while (!o.EndOfStream)
                {
                    string line = o.ReadLine()?.Trim();
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.StartsWith("Arquivo lista inválido"))
                            throw new Exception("Invalid file list");
                        if (line.StartsWith("AVISO arquivo não existe:"))
                            err.AppendLine(line.Substring("AVISO arquivo não existe:".Length) + " - File not found");
                        if (line.StartsWith("Não há arquivos para criar. Pasta com arquivos vazios não são permitidos"))
                            err.AppendLine("No files to create. Empty folders are not allowed");
                    }
                }

                if (n == 20)
                {
                    p.Kill();
                    throw new TimeoutException();
                }
                n++;
            }

            while (!o.EndOfStream)
            {
                string line = o.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(line))
                {
                    if (line.StartsWith("Arquivo lista inválido"))
                        throw new Exception("Invalid file list");
                    if (line.StartsWith("AVISO arquivo não existe:"))
                        err.AppendLine(line.Substring("AVISO arquivo não existe:".Length) + " - File not found");
                    if (line.StartsWith("Não há arquivos para criar. Pasta com arquivos vazios não são permitidos"))
                        err.AppendLine("No files to create. Empty folders are not allowed");
                }
            }

            RemoveApp();

            if (err.Length > 0)
            {
                throw new Exception(err.ToString());
            }
        }

        private static void GenerateApp()
        {
            if (!Directory.Exists(TempDir))
                Directory.CreateDirectory(TempDir);

            string sourcePath = !string.IsNullOrEmpty(sbPackerSourcePath) ? sbPackerSourcePath : Path.Combine(Application.StartupPath, "SBPacker.exe");
            if (File.Exists(sourcePath))
            {
                File.Copy(sourcePath, TempAppPath, true);
            }
            else
            {
                throw new FileNotFoundException($"SBPacker.exe not found at: {sourcePath}");
            }
        }

        private static void RemoveApp()
        {
            try
            {
                if (Directory.Exists(TempDir))
                {
                    Directory.Delete(TempDir, true);
                }
            }
            catch
            {
                // Ignore cleanup errors
            }
        }

        private static string GetAbsolutePath(string path, string basePath)
        {
            if (Path.IsPathRooted(path))
                return path;
            return Path.GetFullPath(Path.Combine(basePath, path));
        }
    }
}