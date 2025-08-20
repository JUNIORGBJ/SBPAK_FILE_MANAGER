namespace GerenciadorPAK
{
    partial class CreatePAKForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pakFileLabel = new Label();
            pakFileTextBox = new TextBox();
            pakFileButton = new Button();
            sourceDirectoryLabel = new Label();
            sourceDirectoryTextBox = new TextBox();
            sourceDirectoryButton = new Button();
            buttonStart = new Button();
            buttonCancel = new Button();
            progressBar = new ProgressBar();
            SuspendLayout();
            // 
            // pakFileLabel
            // 
            pakFileLabel.AutoSize = true;
            pakFileLabel.Font = new Font("Segoe UI", 9F);
            pakFileLabel.ForeColor = Color.White;
            pakFileLabel.Location = new Point(13, 19);
            pakFileLabel.Name = "pakFileLabel";
            pakFileLabel.Size = new Size(76, 15);
            pakFileLabel.TabIndex = 0;
            pakFileLabel.Text = "Arquivo PAK:";
            // 
            // pakFileTextBox
            // 
            pakFileTextBox.BackColor = Color.FromArgb(51, 51, 55);
            pakFileTextBox.BorderStyle = BorderStyle.FixedSingle;
            pakFileTextBox.Font = new Font("Segoe UI", 9F);
            pakFileTextBox.ForeColor = Color.White;
            pakFileTextBox.Location = new Point(13, 38);
            pakFileTextBox.Name = "pakFileTextBox";
            pakFileTextBox.Size = new Size(350, 23);
            pakFileTextBox.TabIndex = 1;
            // 
            // pakFileButton
            // 
            pakFileButton.BackColor = Color.FromArgb(0, 122, 204);
            pakFileButton.FlatAppearance.BorderSize = 0;
            pakFileButton.FlatStyle = FlatStyle.Flat;
            pakFileButton.Font = new Font("Segoe UI", 9F);
            pakFileButton.ForeColor = Color.White;
            pakFileButton.Location = new Point(372, 36);
            pakFileButton.Name = "pakFileButton";
            pakFileButton.Size = new Size(70, 26);
            pakFileButton.TabIndex = 2;
            pakFileButton.Text = "Procurar...";
            pakFileButton.UseVisualStyleBackColor = false;
            pakFileButton.Click += PakFileButton_Click;
            // 
            // sourceDirectoryLabel
            // 
            sourceDirectoryLabel.AutoSize = true;
            sourceDirectoryLabel.Font = new Font("Segoe UI", 9F);
            sourceDirectoryLabel.ForeColor = Color.White;
            sourceDirectoryLabel.Location = new Point(13, 80);
            sourceDirectoryLabel.Name = "sourceDirectoryLabel";
            sourceDirectoryLabel.Size = new Size(113, 15);
            sourceDirectoryLabel.TabIndex = 3;
            sourceDirectoryLabel.Text = "Diretório de origem:";
            // 
            // sourceDirectoryTextBox
            // 
            sourceDirectoryTextBox.BackColor = Color.FromArgb(51, 51, 55);
            sourceDirectoryTextBox.BorderStyle = BorderStyle.FixedSingle;
            sourceDirectoryTextBox.Font = new Font("Segoe UI", 9F);
            sourceDirectoryTextBox.ForeColor = Color.White;
            sourceDirectoryTextBox.Location = new Point(13, 98);
            sourceDirectoryTextBox.Name = "sourceDirectoryTextBox";
            sourceDirectoryTextBox.Size = new Size(350, 23);
            sourceDirectoryTextBox.TabIndex = 4;
            // 
            // sourceDirectoryButton
            // 
            sourceDirectoryButton.BackColor = Color.FromArgb(0, 122, 204);
            sourceDirectoryButton.FlatAppearance.BorderSize = 0;
            sourceDirectoryButton.FlatStyle = FlatStyle.Flat;
            sourceDirectoryButton.Font = new Font("Segoe UI", 9F);
            sourceDirectoryButton.ForeColor = Color.White;
            sourceDirectoryButton.Location = new Point(372, 97);
            sourceDirectoryButton.Name = "sourceDirectoryButton";
            sourceDirectoryButton.Size = new Size(70, 26);
            sourceDirectoryButton.TabIndex = 5;
            sourceDirectoryButton.Text = "Procurar...";
            sourceDirectoryButton.UseVisualStyleBackColor = false;
            sourceDirectoryButton.Click += SourceDirectoryButton_Click;
            // 
            // buttonStart
            // 
            buttonStart.BackColor = Color.FromArgb(0, 122, 204);
            buttonStart.FlatAppearance.BorderSize = 0;
            buttonStart.FlatStyle = FlatStyle.Flat;
            buttonStart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonStart.ForeColor = Color.White;
            buttonStart.Location = new Point(298, 144);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(70, 33);
            buttonStart.TabIndex = 9;
            buttonStart.Text = "&Iniciar";
            buttonStart.UseVisualStyleBackColor = false;
            buttonStart.Click += ButtonStart_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = Color.FromArgb(68, 68, 68);
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.FlatAppearance.BorderSize = 0;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Segoe UI", 9F);
            buttonCancel.ForeColor = Color.White;
            buttonCancel.Location = new Point(372, 144);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(70, 33);
            buttonCancel.TabIndex = 10;
            buttonCancel.Text = "&Cancelar";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // progressBar
            // 
            progressBar.BackColor = Color.FromArgb(51, 51, 55);
            progressBar.ForeColor = Color.FromArgb(0, 122, 204);
            progressBar.Location = new Point(13, 144);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(276, 33);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 11;
            progressBar.Visible = false;
            // 
            // CreatePAKForm
            // 
            AcceptButton = buttonStart;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 45, 48);
            CancelButton = buttonCancel;
            ClientSize = new Size(455, 195);
            Controls.Add(buttonCancel);
            Controls.Add(buttonStart);
            Controls.Add(progressBar);
            Controls.Add(sourceDirectoryButton);
            Controls.Add(sourceDirectoryTextBox);
            Controls.Add(sourceDirectoryLabel);
            Controls.Add(pakFileButton);
            Controls.Add(pakFileTextBox);
            Controls.Add(pakFileLabel);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CreatePAKForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Criar arquivo PAK";
            FormClosed += CreatePAKForm_FormClosed;
            Load += CreatePAKForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label pakFileLabel;
        private System.Windows.Forms.TextBox pakFileTextBox;
        private System.Windows.Forms.Button pakFileButton;
        private System.Windows.Forms.Label sourceDirectoryLabel;
        private System.Windows.Forms.TextBox sourceDirectoryTextBox;
        private System.Windows.Forms.Button sourceDirectoryButton;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}