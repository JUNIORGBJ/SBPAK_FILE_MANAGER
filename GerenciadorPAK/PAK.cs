using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FRCCoreFile;

namespace GerenciadorPAK
{
    public class PAK : StreamEx
    {
        internal const int Key = unchecked((int)0xB6B6B6B6);
        private const byte KeyByte = 0xB6;

        private FileDB rootValue;

        private PAK(string path, FileMode fileMode, FileAccess access, FileShare share)
            : base(path, fileMode, access, share)
        {
        }

        public FileDB Root => rootValue;

        public static PAK Open(string path)
        {
            PAK pf = null;
            try
            {
                string[] fileName = CommandLine.GetFileNameList(path);

                pf = new PAK(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                if (pf.ReadSimpleString(11) != "SBPAK V 1.0")
                    throw new InvalidDataException();

                pf.Position = 36;
                int num = pf.ReadInt32() ^ Key;
                pf.Position += 4;
                int dataPosition = pf.ReadInt32() ^ Key;
                pf.Position += 4;

                pf.rootValue = new FileDB("", FileDB.FileType.Directory, 0, 0);
                var fileDBList = new List<FileDB>();
                for (int n = 0; n < num; n++)
                {
                    var f = new FileDB(pf);
                    f.Address += dataPosition;
                    fileDBList.Add(f);
                }
                fileDBList.Sort(Compare);

                if (fileName.Length != num)
                    throw new InvalidDataException();

                for (int n = 0; n < num; n++)
                {
                    fileDBList[n].Name = fileName[n];
                    fileDBList[n].Decode = !fileName[n].EndsWith(".bik");
                    pf.PushFileToDir(fileDBList[n], pf.rootValue);
                }

                return pf;
            }
            catch
            {
                try
                {
                    pf?.Close();
                }
                catch { }
                throw;
            }
        }

        private void PushFileToDir(FileDB f, FileDB d)
        {
            string dir = "";
            if (f.Name.Contains("\\"))
            {
                string fileName = f.Name;
                dir = PopFirstDir(ref fileName);
                f.Name = fileName;
            }

            if (string.IsNullOrEmpty(dir))
            {
                d.SubFileDB.Add(f);
                f.ParentFileDB = d;
            }
            else
            {
                if (d.SubFileDBRef == null)
                    d.SubFileDBRef = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

                if (!d.SubFileDBRef.ContainsKey(dir))
                {
                    var dirDB = FileDB.CreateDirectory(dir);
                    d.SubFileDBRef.Add(dirDB.Name, d.SubFileDB.Count);
                    d.SubFileDB.Add(dirDB);
                    dirDB.ParentFileDB = d;
                }
                PushFileToDir(f, d.SubFileDB[d.SubFileDBRef[dir]]);
            }
        }

        private static string PopFirstDir(ref string path)
        {
            if (string.IsNullOrEmpty(path))
                return "";

            int nameS = path.IndexOf('\\');
            if (nameS < 0)
            {
                string ret = path;
                path = "";
                return ret;
            }
            else
            {
                string ret = path.Substring(0, nameS);
                path = path.Substring(nameS + 1);
                return ret;
            }
        }

        private static int Compare(FileDB left, FileDB right)
        {
            if (left.Address < right.Address) return -1;
            if (left.Address > right.Address) return 1;
            return 0;
        }

        private static string ReadFixedString(StreamEx s, int count)
        {
            var c = new char[count];
            for (int n = 0; n < count; n++)
            {
                c[n] = (char)(s.ReadByte() ^ KeyByte);
            }
            return new string(c);
        }

        public FileDB TryGetFileDB(string path)
        {
            string p = path;
            FileDB ret = Root;
            string d = PopFirstDir(ref p);
            if (string.IsNullOrEmpty(d))
                return ret;
            if (d != ret.Name)
                return null;

            while (ret != null)
            {
                d = PopFirstDir(ref p);
                if (string.IsNullOrEmpty(d))
                    return ret;

                for (int n = 0; n < ret.SubFileDB.Count; n++)
                {
                    if (string.Equals(d, ret.SubFileDB[n].Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        ret = ret.SubFileDB[n];
                        goto ContinueWhile;
                    }
                }
                return null;

                ContinueWhile:;
            }
            return null;
        }

        public void Extract(FileDB file, string directory, string mask = "*.*")
        {
            string dir = directory.Trim().TrimEnd('\\');
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            switch (file.Type)
            {
                case FileDB.FileType.File:
                    if (IsMatchFileMask(file.Name, mask))
                    {
                        Position = file.Address;
                        using (var t = new StreamEx(GetPath(dir, file.Name), FileMode.Create))
                        {
                            if (file.Decode)
                            {
                                for (int n = 0; n < file.Length; n++)
                                {
                                    t.WriteByte((byte)(BaseStream.ReadByte() ^ KeyByte));
                                }
                            }
                            else
                            {
                                for (int n = 0; n < file.Length; n++)
                                {
                                    t.WriteByte((byte)BaseStream.ReadByte());
                                }
                            }
                        }
                    }
                    break;
                case FileDB.FileType.Directory:
                    foreach (var fileDB in file.SubFileDB)
                    {
                        Extract(fileDB, GetPath(dir, file.Name), mask);
                    }
                    break;
            }
        }

        public static void Create(string source, string path)
        {
            var fileQueue = new Queue<string>();
            ImportDirectory(source, source, fileQueue);
            CommandLine.PackFileInList(source, path, fileQueue.ToArray());
        }

        public static void CreateWithListFile(string source, string path, string listFilePath)
        {
            var fileQueue = new Queue<string>();
            using (var listFile = new StreamReader(listFilePath, Encoding.Default))
            {
                while (!listFile.EndOfStream)
                {
                    string line = listFile.ReadLine()?.Trim();
                    if (!string.IsNullOrEmpty(line))
                        fileQueue.Enqueue(line);
                }
            }
            CommandLine.PackFileInList(source, path, fileQueue.ToArray());
        }

        private static void ImportDirectory(string dir, string source, Queue<string> fileQueue)
        {
            foreach (string f in Directory.GetFiles(dir))
            {
                fileQueue.Enqueue(GetRelativePath(f, source));
            }
            foreach (string d in Directory.GetDirectories(dir))
            {
                ImportDirectory(d, source, fileQueue);
            }
        }

        // Helper methods that would need to be implemented or imported from FRCCoreFile
        private static bool IsMatchFileMask(string fileName, string mask)
        {
            // Simple implementation - could be enhanced
            if (mask == "*.*" || mask == "*")
                return true;
            // Add more sophisticated pattern matching if needed
            return fileName.EndsWith(mask.Replace("*", ""));
        }

        private static string GetPath(string dir, string fileName)
        {
            if (string.IsNullOrEmpty(dir))
                return fileName;
            return Path.Combine(dir, fileName);
        }

        private static string GetRelativePath(string fullPath, string basePath)
        {
            if (fullPath.StartsWith(basePath))
            {
                return fullPath.Substring(basePath.Length).TrimStart('\\', '/');
            }
            return fullPath;
        }
    }

    public class FileDB
    {
        public string Name { get; set; }
        public FileType Type { get; set; }
        public int Length { get; set; }
        public int Address { get; set; }
        public int Unknown { get; set; }
        public bool Decode { get; set; } = false;

        public const int DBLength = 48;
        public FileDB ParentFileDB { get; set; }
        public List<FileDB> SubFileDB { get; set; } = new List<FileDB>();
        public Dictionary<string, int> SubFileDBRef { get; set; }

        public FileDB(string name, FileType type, int length, int address)
        {
            if (!string.IsNullOrEmpty(name))
                Name = name;
            Type = type;
            Length = length;
            Address = address;
        }

        public FileDB(PAK s)
        {
            Length = s.ReadInt32() ^ PAK.Key;
            Address = s.ReadInt32() ^ PAK.Key;
            Name = (s.ReadInt32() ^ PAK.Key).ToString("000");
            Unknown = s.ReadInt32() ^ PAK.Key;
            Type = FileType.File;
        }

        public enum FileType : int
        {
            File = 0,
            Directory = 1,
            DirectoryEnd = 255
        }

        public static FileDB CreateFile(string name, int length, int address)
        {
            return new FileDB(name, FileType.File, length, address);
        }

        public static FileDB CreateDirectory(string name)
        {
            return new FileDB(name, FileType.Directory, unchecked((int)0xFFFFFFFF), 0);
        }

        public static FileDB CreateDirectoryEnd()
        {
            return new FileDB(null, FileType.DirectoryEnd, unchecked((int)0xFFFFFFFF), unchecked((int)0xFFFFFFFF));
        }
    }
}