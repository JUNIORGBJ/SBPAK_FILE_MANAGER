using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using FRCCoreFile;

namespace GerenciadorPAK
{
    public partial class Form1 : Form
    {
        private string title;
        private string debugTip = "Erro\nDetalhes";
        private readonly string[] recentFiles = new string[10];
        private string sbPackerPath = "";

        private PAK pf;
        private FileDB pfCurDirDB;
        private bool pfClosed = true;
        private readonly string tempDir = Path.Combine(Path.GetTempPath(), "GerenciadorPAKTemp");
        private int fileListViewMajorCompareeIndex = -1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Load language preference first
                LoadLanguagePreference();
                
                // Apply language settings
                ApplyLanguage();
                
                // Set title after language is applied
                title = this.Text;
                
                // Extrai o SBPacker.exe para a pasta temporária
                ExtractSBPacker();
                
                // Configura o caminho do SBPacker.exe para o CommandLine
                CommandLine.SetSBPackerPath(sbPackerPath);

                var imageList = new ImageList();
                imageList.Images.Add(SystemIcons.Application.ToBitmap());
                imageList.Images.Add(SystemIcons.WinLogo.ToBitmap());
                imageList.ColorDepth = ColorDepth.Depth32Bit;
                fileListView.SmallImageList = imageList;

                RefreshRecent();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inicializar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!pfClosed)
                pf?.Close();
            Directory.SetCurrentDirectory(Application.StartupPath);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Directory.Exists(tempDir))
            {
                try
                {
                    Directory.Delete(tempDir, true);
                }
                catch { }
            }
        }



        private void ShowError(Exception ex)
        {
            var result = MessageBox.Show($"{debugTip}\n\n{ex}", title, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (result == DialogResult.Yes)
            {
                Clipboard.SetText($"Erro\n\n{ex}");
            }
        }

        private void ExtractSBPacker()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "GerenciadorPAK.SBPacker.exe";
                
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        var tempPath = Path.GetTempPath();
                        sbPackerPath = Path.Combine(tempPath, "SBPacker.exe");
                        
                        // Só extrai se o arquivo não existir ou for diferente
                        if (!File.Exists(sbPackerPath) || new FileInfo(sbPackerPath).Length != stream.Length)
                        {
                            using (var fileStream = File.Create(sbPackerPath))
                            {
                                stream.CopyTo(fileStream);
                            }
                        }
                    }
                    else
                    {
                        // Fallback para o arquivo local se o recurso não for encontrado
                        sbPackerPath = Path.Combine(Application.StartupPath, "SBPacker.exe");
                    }
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro, usa o arquivo local
                sbPackerPath = Path.Combine(Application.StartupPath, "SBPacker.exe");
                ShowError(ex);
            }
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RefreshList()
        {
            if (pfCurDirDB == null) return;

            var sorter = new List<ListViewItem>();
            string fileMask = maskTextBox.Text;
            int n = 0;

            foreach (var f in pfCurDirDB.SubFileDB)
            {
                ListViewItem item = null;
                switch (f.Type)
                {
                    case FileDB.FileType.File:
                        if (IsMatchFileMask(f.Name, fileMask))
                        {
                            item = new ListViewItem(new[] { f.Name, f.Length.ToString(), f.Address.ToString(), GetExtendedFileName(f.Name), n.ToString(), "1" }, 0);
                        }
                        break;
                    case FileDB.FileType.Directory:
                        item = new ListViewItem(new[] { f.Name, "", f.Address.ToString().TrimStart('0'), "", n.ToString(), "0" }, 1);
                        break;
                }
                if (item != null) sorter.Add(item);
                n++;
            }

            if (fileListViewMajorCompareeIndex != -1)
                sorter.Sort(Comparison);

            fileListView.Items.Clear();
            if (pfCurDirDB.ParentFileDB != null)
            {
                fileListView.Items.Add(new ListViewItem(new[] { "..", "", "", "", "-1", "0" }, 1));
            }
            fileListView.Items.AddRange(sorter.ToArray());
        }

        private int Comparison(ListViewItem x, ListViewItem y)
        {
            if (int.Parse(x.SubItems[5].Text) < int.Parse(y.SubItems[5].Text)) return -1;
            if (int.Parse(x.SubItems[5].Text) > int.Parse(y.SubItems[5].Text)) return 1;

            switch (fileListViewMajorCompareeIndex)
            {
                case 0:
                case 3:
                    return string.Compare(x.SubItems[fileListViewMajorCompareeIndex].Text, y.SubItems[fileListViewMajorCompareeIndex].Text, StringComparison.InvariantCultureIgnoreCase);
                case 1:
                case 2:
                    if (int.Parse(x.SubItems[5].Text) != 0)
                    {
                        int xVal = int.Parse(x.SubItems[fileListViewMajorCompareeIndex].Text);
                        int yVal = int.Parse(y.SubItems[fileListViewMajorCompareeIndex].Text);
                        if (xVal < yVal) return -1;
                        if (xVal > yVal) return 1;
                    }
                    break;
            }

            return string.Compare(x.SubItems[0].Text, y.SubItems[0].Text, StringComparison.InvariantCultureIgnoreCase);
        }

        public void DoOpenPAK(string pakPath)
        {
            try
            {
                if (!pfClosed) pf?.Close();
                pf = PAK.Open(pakPath);
                pfCurDirDB = pf.Root;
                pathTextBox.Text = pfCurDirDB.Name + "\\";
                pathTextBox.Text = pathTextBox.Text.TrimStart('\\');
                RefreshList();
                AddRecent(pakPath + ",2");
                pfClosed = false;
                this.Text = title + " - " + pakPath;
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        public void DoClose()
        {
            if (!pfClosed) pf?.Close();
            pfCurDirDB = null;
            fileListView.Items.Clear();
            pathTextBox.Text = "";
            pfClosed = true;
            this.Text = title;
        }

        private void RefreshRecent()
        {
            // Implementation for recent files menu
        }

        private void AddRecent(string path)
        {
            // Implementation for adding recent files
        }

        private bool IsMatchFileMask(string fileName, string mask)
        {
            if (mask == "*.*" || mask == "*")
                return true;
            return fileName.EndsWith(mask.Replace("*", ""));
        }

        private string GetExtendedFileName(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        // Event Handlers
        private void OpenPAKFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos PAK (*.pak)|*.pak|Todos os arquivos (*.*)|*.*";
                openFileDialog.Title = GetText("OpenPakFileTitle");
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DoOpenPAK(openFileDialog.FileName);
                }
            }
        }

        private void CreatePAKFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var createForm = new CreatePAKForm();
            createForm.ShowDialog();
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoClose();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetText("AboutMessage"), GetText("AboutTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FileListView_ItemActivate(object sender, EventArgs e)
        {
            if (fileListView.SelectedItems.Count == 0) return;

            var selectedItem = fileListView.SelectedItems[0];
            int index = int.Parse(selectedItem.SubItems[4].Text);

            if (index == -1) // Parent directory
            {
                if (pfCurDirDB.ParentFileDB != null)
                {
                    pfCurDirDB = pfCurDirDB.ParentFileDB;
                    pathTextBox.Text = pfCurDirDB.Name + "\\";
                    pathTextBox.Text = pathTextBox.Text.TrimStart('\\');
                    RefreshList();
                }
            }
            else if (index >= 0 && index < pfCurDirDB.SubFileDB.Count)
            {
                var fileDB = pfCurDirDB.SubFileDB[index];
                if (fileDB.Type == FileDB.FileType.Directory)
                {
                    pfCurDirDB = fileDB;
                    pathTextBox.Text = pfCurDirDB.Name + "\\";
                    pathTextBox.Text = pathTextBox.Text.TrimStart('\\');
                    RefreshList();
                }
            }
        }

        private void FileListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            fileListViewMajorCompareeIndex = e.Column;
            RefreshList();
        }

        private void MaskTextBox_TextChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private async void ExtractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileListView.SelectedItems.Count == 0) return;

            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Selecione o diretório de destino";
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Show progress bar and disable controls
                        extractProgressBar.Visible = true;
                        extractProgressBar.Style = ProgressBarStyle.Continuous;
                        extractProgressBar.Minimum = 0;
                        extractProgressBar.Maximum = fileListView.SelectedItems.Count;
                        extractProgressBar.Value = 0;
                        
                        // Disable menu and context menu during extraction
                        mainMenu.Enabled = false;
                        contextMenu.Enabled = false;
                        fileListView.Enabled = false;

                        int extractedCount = 0;
                        int totalFiles = fileListView.SelectedItems.Count;
                        
                        await Task.Run(() =>
                        {
                            foreach (ListViewItem selectedItem in fileListView.SelectedItems)
                            {
                                int index = int.Parse(selectedItem.SubItems[4].Text);
                                
                                if (index >= 0 && index < pfCurDirDB.SubFileDB.Count)
                                {
                                    var fileDB = pfCurDirDB.SubFileDB[index];
                                    pf.Extract(fileDB, folderBrowserDialog.SelectedPath, "*");
                                    extractedCount++;
                                    
                                    // Update progress bar on UI thread
                                    this.Invoke(new Action(() =>
                                    {
                                        extractProgressBar.Value = extractedCount;
                                    }));
                                }
                            }
                        });
                        
                        // Hide progress bar and re-enable controls
                        extractProgressBar.Visible = false;
                        mainMenu.Enabled = true;
                        contextMenu.Enabled = true;
                        fileListView.Enabled = true;
                        
                        if (extractedCount > 0)
                        {
                            ShowMessage(string.Format(GetText("ExtractionComplete"), extractedCount));
                        }
                    }
                    catch (Exception ex)
                    {
                        // Hide progress bar and re-enable controls in case of error
                        extractProgressBar.Visible = false;
                        mainMenu.Enabled = true;
                        contextMenu.Enabled = true;
                        fileListView.Enabled = true;
                        
                        ShowError(ex);
                    }
                }
            }
        }

        #region Language Management

        public enum Language
        {
            Portuguese,
            English
        }

        private Language currentLanguage = Language.Portuguese;

        private static class LanguageTexts
        {
            public static readonly Dictionary<Language, Dictionary<string, string>> Texts = new Dictionary<Language, Dictionary<string, string>>
            {
                [Language.Portuguese] = new Dictionary<string, string>
                {
                    ["Title"] = "Gerenciador de arquivos SBPAK v1.0",
                    ["File"] = "&Arquivo",
                    ["OpenPAK"] = "&Abrir arquivo PAK",
                    ["CreatePAK"] = "&Criar arquivo PAK",
                    ["Close"] = "&Fechar",
                    ["Exit"] = "&Sair",
                    ["Language"] = "&Idioma",
                    ["Portuguese"] = "Português (BR)",
                    ["English"] = "English (US)",
                    ["About"] = "&Sobre",
                    ["FileName"] = "Nome do arquivo",
                    ["FileSize"] = "Tamanho",
                    ["Offset"] = "Offset",
                    ["FileType"] = "Tipo",
                    ["Extract"] = "&Extrair",
                    ["Error"] = "Erro",
                    ["Details"] = "Detalhes",
                    ["ExtractionComplete"] = "Extração concluída com sucesso! {0} arquivo(s) extraído(s).",
                    ["AboutMessage"] = "Gerenciador de arquivos SBPAK\nVersão 1.0\n\nDesenvolvido por gBj\n\nEste software foi desenvolvido para extrair e criar arquivos PAK do jogo Desperados (Helldorado).\r\nEle oferece uma interface gráfica (GUI) amigável para o utilitário SBPacker.exe, originalmente criado pela Spellbound Entertainment.\r\nOs direitos autorais do SBPacker.exe pertencem ao Nobilis Group e à Spellbound Entertainment.",
                ["AboutTitle"] = "Sobre",
                ["OpenPakFileTitle"] = "Abrir arquivo PAK"
                },
                [Language.English] = new Dictionary<string, string>
                {
                    ["Title"] = "SBPAK File Manager v1.0",
                    ["File"] = "&File",
                    ["OpenPAK"] = "&Open PAK file",
                    ["CreatePAK"] = "&Create PAK file",
                    ["Close"] = "&Close",
                    ["Exit"] = "E&xit",
                    ["Language"] = "&Language",
                    ["Portuguese"] = "Português (BR)",
                    ["English"] = "English (US)",
                    ["About"] = "&About",
                    ["FileName"] = "File name",
                    ["FileSize"] = "Size",
                    ["Offset"] = "Offset",
                    ["FileType"] = "Type",
                    ["Extract"] = "&Extract",
                    ["Error"] = "Error",
                    ["Details"] = "Details",
                    ["ExtractionComplete"] = "Extraction completed successfully! {0} file(s) extracted.",
                    ["AboutMessage"] = "SBPAK File Manager\nVersion 1.0\n\nDeveloped by gBj\n\nThis software was created to extract and generate PAK files from the game Desperados (Helldorado).\r\nIt provides a user-friendly graphical interface (GUI) for the utility SBPacker.exe, originally developed by Spellbound Entertainment.\r\nNobilis Group and Spellbound Entertainment own the copyright of SBPacker.exe.",
                ["AboutTitle"] = "About",
                ["OpenPakFileTitle"] = "Open PAK file"
                }
            };
        }

        private string GetText(string key)
        {
            if (LanguageTexts.Texts.ContainsKey(currentLanguage) && LanguageTexts.Texts[currentLanguage].ContainsKey(key))
            {
                return LanguageTexts.Texts[currentLanguage][key];
            }
            return key; // Fallback to key if translation not found
        }

        private void ApplyLanguage()
        {
            // Update form title
            this.Text = GetText("Title");
            
            // Update menu items
            fileToolStripMenuItem.Text = GetText("File");
            openPAKFileToolStripMenuItem.Text = GetText("OpenPAK");
            createPAKFileToolStripMenuItem.Text = GetText("CreatePAK");
            closeToolStripMenuItem.Text = GetText("Close");
            exitToolStripMenuItem.Text = GetText("Exit");
            languageToolStripMenuItem.Text = GetText("Language");
            portugueseToolStripMenuItem.Text = GetText("Portuguese");
            englishToolStripMenuItem.Text = GetText("English");
            aboutToolStripMenuItem.Text = GetText("About");
            aboutToolStripMenuItem1.Text = GetText("About");
            
            // Update column headers
            fileNameColumn.Text = GetText("FileName");
            fileLengthColumn.Text = GetText("FileSize");
            offsetColumn.Text = GetText("Offset");
            fileTypeColumn.Text = GetText("FileType");
            
            // Update context menu
            extractToolStripMenuItem.Text = GetText("Extract");
            
            // Update debug tip
            debugTip = GetText("Error") + "\n" + GetText("Details");
        }

        private void PortugueseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentLanguage = Language.Portuguese;
            ApplyLanguage();
            SaveLanguagePreference();
        }

        private void EnglishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentLanguage = Language.English;
            ApplyLanguage();
            SaveLanguagePreference();
        }

        private void SaveLanguagePreference()
        {
            try
            {
                Properties.Settings.Default.Language = currentLanguage.ToString();
                Properties.Settings.Default.Save();
            }
            catch
            {
                // Ignore errors when saving preferences
            }
        }

        private void LoadLanguagePreference()
        {
            try
            {
                string savedLanguage = Properties.Settings.Default.Language;
                if (Enum.TryParse<Language>(savedLanguage, out Language language))
                {
                    currentLanguage = language;
                }
            }
            catch
            {
                // Use default language if loading fails
                currentLanguage = Language.Portuguese;
            }
        }

        #endregion
    }
}
