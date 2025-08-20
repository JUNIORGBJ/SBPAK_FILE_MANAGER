using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GerenciadorPAK
{
    public partial class CreatePAKForm : Form
    {
        // Language system
        private enum Language
        {
            Portuguese,
            English
        }

        private static class CreatePAKLanguageTexts
        {
            public static readonly Dictionary<string, string> Portuguese = new Dictionary<string, string>
            {
                // Form title and controls
                ["FormTitle"] = "Criar arquivo PAK",
                ["PakFileLabel"] = "Arquivo PAK:",
                ["SourceDirectoryLabel"] = "Diretório de origem:",
                ["BrowseButton"] = "Procurar...",
                ["StartButton"] = "&Iniciar",
                ["CancelButton"] = "&Cancelar",
                
                // Messages
                ["PleaseSpecifyPakFile"] = "Por favor, especifique o arquivo PAK de destino.",
                ["PleaseSpecifySourceDirectory"] = "Por favor, especifique o diretório de origem.",
                ["SourceDirectoryNotExists"] = "O diretório de origem não existe.",
                ["PakFileCreatedSuccessfully"] = "Arquivo PAK criado com sucesso!",
                ["ErrorCreatingPakFile"] = "Erro ao criar arquivo PAK:\n\n{0}",
                
                // Dialog titles and filters
                ["SavePakFileTitle"] = "Salvar arquivo PAK",
                ["PakFilesFilter"] = "Arquivos PAK (*.pak)|*.pak|Todos os arquivos (*.*)|*.*",
                ["SelectSourceDirectoryDescription"] = "Selecione o diretório de origem",
                
                // Common
                ["Error"] = "Erro",
                ["Success"] = "Sucesso"
            };

            public static readonly Dictionary<string, string> English = new Dictionary<string, string>
            {
                // Form title and controls
                ["FormTitle"] = "Create PAK file",
                ["PakFileLabel"] = "PAK File:",
                ["SourceDirectoryLabel"] = "Source directory:",
                ["BrowseButton"] = "Browse...",
                ["StartButton"] = "&Start",
                ["CancelButton"] = "&Cancel",
                
                // Messages
                ["PleaseSpecifyPakFile"] = "Please specify the destination PAK file.",
                ["PleaseSpecifySourceDirectory"] = "Please specify the source directory.",
                ["SourceDirectoryNotExists"] = "The source directory does not exist.",
                ["PakFileCreatedSuccessfully"] = "PAK file created successfully!",
                ["ErrorCreatingPakFile"] = "Error creating PAK file:\n\n{0}",
                
                // Dialog titles and filters
                ["SavePakFileTitle"] = "Save PAK file",
                ["PakFilesFilter"] = "PAK Files (*.pak)|*.pak|All files (*.*)|*.*",
                ["SelectSourceDirectoryDescription"] = "Select source directory",
                
                // Common
                ["Error"] = "Error",
                ["Success"] = "Success"
            };
        }

        private string GetText(string key)
        {
            var currentLanguage = GetCurrentLanguage();
            var texts = currentLanguage == Language.English ? CreatePAKLanguageTexts.English : CreatePAKLanguageTexts.Portuguese;
            return texts.ContainsKey(key) ? texts[key] : key;
        }

        private Language GetCurrentLanguage()
        {
            try
            {
                string savedLanguage = Properties.Settings.Default.Language;
                return savedLanguage == "English" ? Language.English : Language.Portuguese;
            }
            catch
            {
                return Language.Portuguese;
            }
        }

        private void ApplyLanguage()
        {
            // Form title
            this.Text = GetText("FormTitle");
            
            // Labels
            pakFileLabel.Text = GetText("PakFileLabel");
            sourceDirectoryLabel.Text = GetText("SourceDirectoryLabel");
            
            // Buttons
            pakFileButton.Text = GetText("BrowseButton");
            sourceDirectoryButton.Text = GetText("BrowseButton");
            buttonStart.Text = GetText("StartButton");
            buttonCancel.Text = GetText("CancelButton");
        }

        public CreatePAKForm()
        {
            InitializeComponent();
        }

        private void CreatePAKForm_Load(object sender, EventArgs e)
        {
            ApplyLanguage();
        }

        private void CreatePAKForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Re-enable and activate main form
            var mainForm = Application.OpenForms["Form1"] as Form1;
            if (mainForm != null)
            {
                mainForm.Enabled = true;
                mainForm.Activate();
            }
        }

        private async void ButtonStart_Click(object sender, EventArgs e)
        {
            try
            {
                string pakFile = pakFileTextBox.Text.Trim();
                string sourceDirectory = sourceDirectoryTextBox.Text.Trim();
                
                if (string.IsNullOrEmpty(pakFile))
                {
                    MessageBox.Show(GetText("PleaseSpecifyPakFile"), GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(sourceDirectory))
                {
                    MessageBox.Show(GetText("PleaseSpecifySourceDirectory"), GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!Directory.Exists(sourceDirectory))
                {
                    MessageBox.Show(GetText("SourceDirectoryNotExists"), GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Show progress bar and disable controls
                progressBar.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;
                progressBar.MarqueeAnimationSpeed = 30;
                buttonStart.Enabled = false;
                buttonCancel.Enabled = false;
                pakFileTextBox.Enabled = false;
                sourceDirectoryTextBox.Enabled = false;
                pakFileButton.Enabled = false;
                sourceDirectoryButton.Enabled = false;
                
                // Create PAK file asynchronously
                await Task.Run(() =>
                {
                    PAK.Create(sourceDirectory, pakFile);
                });

                // Hide progress bar and re-enable controls
                progressBar.Visible = false;
                buttonStart.Enabled = true;
                buttonCancel.Enabled = true;
                pakFileTextBox.Enabled = true;
                sourceDirectoryTextBox.Enabled = true;
                pakFileButton.Enabled = true;
                sourceDirectoryButton.Enabled = true;
                
                MessageBox.Show(GetText("PakFileCreatedSuccessfully"), GetText("Success"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open the created PAK file in main form
                var mainForm = Application.OpenForms["Form1"] as Form1;
                if (mainForm != null)
                {
                    mainForm.DoOpenPAK(pakFile);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                // Hide progress bar and re-enable controls in case of error
                progressBar.Visible = false;
                buttonStart.Enabled = true;
                buttonCancel.Enabled = true;
                pakFileTextBox.Enabled = true;
                sourceDirectoryTextBox.Enabled = true;
                pakFileButton.Enabled = true;
                sourceDirectoryButton.Enabled = true;
                
                MessageBox.Show(string.Format(GetText("ErrorCreatingPakFile"), ex.Message), GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PakFileButton_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = GetText("PakFilesFilter");
                saveFileDialog.Title = GetText("SavePakFileTitle");
                saveFileDialog.DefaultExt = "pak";
                
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pakFileTextBox.Text = saveFileDialog.FileName;
                }
            }
        }

        private void SourceDirectoryButton_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = GetText("SelectSourceDirectoryDescription");
                folderBrowserDialog.ShowNewFolderButton = false;
                
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    sourceDirectoryTextBox.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }
    }
}