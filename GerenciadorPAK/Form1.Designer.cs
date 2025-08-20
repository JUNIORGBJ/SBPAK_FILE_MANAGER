namespace GerenciadorPAK
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            mainMenu = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openPAKFileToolStripMenuItem = new ToolStripMenuItem();
            createPAKFileToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            closeToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            languageToolStripMenuItem = new ToolStripMenuItem();
            portugueseToolStripMenuItem = new ToolStripMenuItem();
            englishToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem1 = new ToolStripMenuItem();
            splitter1 = new Splitter();
            pathTextBox = new TextBox();
            splitter2 = new Splitter();
            maskTextBox = new TextBox();
            fileListView = new ListView();
            fileNameColumn = new ColumnHeader();
            fileLengthColumn = new ColumnHeader();
            offsetColumn = new ColumnHeader();
            fileTypeColumn = new ColumnHeader();
            contextMenu = new ContextMenuStrip(components);
            extractToolStripMenuItem = new ToolStripMenuItem();
            extractProgressBar = new ProgressBar();
            mainMenu.SuspendLayout();
            contextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenu
            // 
            mainMenu.BackColor = Color.FromArgb(37, 37, 38);
            mainMenu.ForeColor = Color.White;
            mainMenu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, languageToolStripMenuItem, aboutToolStripMenuItem });
            mainMenu.Location = new Point(0, 0);
            mainMenu.Name = "mainMenu";
            mainMenu.Padding = new Padding(5, 2, 0, 2);
            mainMenu.RenderMode = ToolStripRenderMode.Professional;
            mainMenu.Size = new Size(787, 24);
            mainMenu.TabIndex = 0;
            mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openPAKFileToolStripMenuItem, createPAKFileToolStripMenuItem, toolStripSeparator1, closeToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(61, 20);
            fileToolStripMenuItem.Text = "&Arquivo";
            // 
            // openPAKFileToolStripMenuItem
            // 
            openPAKFileToolStripMenuItem.Image = (Image)resources.GetObject("openPAKFileToolStripMenuItem.Image");
            openPAKFileToolStripMenuItem.ImageAlign = ContentAlignment.MiddleLeft;
            openPAKFileToolStripMenuItem.Name = "openPAKFileToolStripMenuItem";
            openPAKFileToolStripMenuItem.Size = new Size(167, 22);
            openPAKFileToolStripMenuItem.Text = "&Abrir arquivo PAK";
            openPAKFileToolStripMenuItem.Click += OpenPAKFileToolStripMenuItem_Click;
            // 
            // createPAKFileToolStripMenuItem
            // 
            createPAKFileToolStripMenuItem.Image = (Image)resources.GetObject("createPAKFileToolStripMenuItem.Image");
            createPAKFileToolStripMenuItem.Name = "createPAKFileToolStripMenuItem";
            createPAKFileToolStripMenuItem.Size = new Size(167, 22);
            createPAKFileToolStripMenuItem.Text = "&Criar arquivo PAK";
            createPAKFileToolStripMenuItem.Click += CreatePAKFileToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(164, 6);
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Image = (Image)resources.GetObject("closeToolStripMenuItem.Image");
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(167, 22);
            closeToolStripMenuItem.Text = "&Fechar";
            closeToolStripMenuItem.Click += CloseToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(164, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Image = (Image)resources.GetObject("exitToolStripMenuItem.Image");
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(167, 22);
            exitToolStripMenuItem.Text = "&Sair";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // languageToolStripMenuItem
            // 
            languageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { portugueseToolStripMenuItem, englishToolStripMenuItem });
            languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            languageToolStripMenuItem.Size = new Size(56, 20);
            languageToolStripMenuItem.Text = "&Idioma";
            // 
            // portugueseToolStripMenuItem
            // 
            portugueseToolStripMenuItem.Image = (Image)resources.GetObject("portugueseToolStripMenuItem.Image");
            portugueseToolStripMenuItem.Name = "portugueseToolStripMenuItem";
            portugueseToolStripMenuItem.Size = new Size(180, 22);
            portugueseToolStripMenuItem.Text = "Português (BR)";
            portugueseToolStripMenuItem.Click += PortugueseToolStripMenuItem_Click;
            // 
            // englishToolStripMenuItem
            // 
            englishToolStripMenuItem.Image = (Image)resources.GetObject("englishToolStripMenuItem.Image");
            englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            englishToolStripMenuItem.Size = new Size(180, 22);
            englishToolStripMenuItem.Text = "English (US)";
            englishToolStripMenuItem.Click += EnglishToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem1 });
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(49, 20);
            aboutToolStripMenuItem.Text = "&Sobre";
            // 
            // aboutToolStripMenuItem1
            // 
            aboutToolStripMenuItem1.Image = (Image)resources.GetObject("aboutToolStripMenuItem1.Image");
            aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            aboutToolStripMenuItem1.Size = new Size(104, 22);
            aboutToolStripMenuItem1.Text = "&Sobre";
            aboutToolStripMenuItem1.Click += AboutToolStripMenuItem1_Click;
            // 
            // splitter1
            // 
            splitter1.Dock = DockStyle.Top;
            splitter1.Location = new Point(0, 24);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(787, 3);
            splitter1.TabIndex = 1;
            splitter1.TabStop = false;
            splitter1.UseWaitCursor = true;
            // 
            // pathTextBox
            // 
            pathTextBox.BackColor = Color.FromArgb(51, 51, 55);
            pathTextBox.BorderStyle = BorderStyle.FixedSingle;
            pathTextBox.Dock = DockStyle.Top;
            pathTextBox.Font = new Font("Segoe UI", 9F);
            pathTextBox.ForeColor = Color.White;
            pathTextBox.Location = new Point(0, 27);
            pathTextBox.Name = "pathTextBox";
            pathTextBox.ReadOnly = true;
            pathTextBox.Size = new Size(787, 23);
            pathTextBox.TabIndex = 2;
            // 
            // splitter2
            // 
            splitter2.Dock = DockStyle.Bottom;
            splitter2.Location = new Point(0, 538);
            splitter2.Name = "splitter2";
            splitter2.Size = new Size(787, 3);
            splitter2.TabIndex = 3;
            splitter2.TabStop = false;
            // 
            // maskTextBox
            // 
            maskTextBox.BackColor = Color.FromArgb(51, 51, 55);
            maskTextBox.BorderStyle = BorderStyle.FixedSingle;
            maskTextBox.Dock = DockStyle.Top;
            maskTextBox.Font = new Font("Segoe UI", 9F);
            maskTextBox.ForeColor = Color.White;
            maskTextBox.Location = new Point(0, 50);
            maskTextBox.Name = "maskTextBox";
            maskTextBox.Size = new Size(787, 23);
            maskTextBox.TabIndex = 4;
            maskTextBox.Text = "*";
            maskTextBox.TextChanged += MaskTextBox_TextChanged;
            // 
            // fileListView
            // 
            fileListView.BackColor = Color.FromArgb(64, 64, 64);
            fileListView.BorderStyle = BorderStyle.None;
            fileListView.Columns.AddRange(new ColumnHeader[] { fileNameColumn, fileLengthColumn, offsetColumn, fileTypeColumn });
            fileListView.ContextMenuStrip = contextMenu;
            fileListView.Dock = DockStyle.Fill;
            fileListView.Font = new Font("Segoe UI", 9F);
            fileListView.ForeColor = Color.White;
            fileListView.FullRowSelect = true;
            fileListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            fileListView.ImeMode = ImeMode.NoControl;
            fileListView.Location = new Point(0, 73);
            fileListView.Name = "fileListView";
            fileListView.Size = new Size(787, 465);
            fileListView.TabIndex = 5;
            fileListView.UseCompatibleStateImageBehavior = false;
            fileListView.View = View.Details;
            fileListView.ColumnClick += FileListView_ColumnClick;
            fileListView.ItemActivate += FileListView_ItemActivate;
            // 
            // fileNameColumn
            // 
            fileNameColumn.Text = "Nome do arquivo";
            fileNameColumn.Width = 200;
            // 
            // fileLengthColumn
            // 
            fileLengthColumn.Text = "Tamanho";
            fileLengthColumn.Width = 100;
            // 
            // offsetColumn
            // 
            offsetColumn.Text = "Offset";
            offsetColumn.Width = 100;
            // 
            // fileTypeColumn
            // 
            fileTypeColumn.Text = "Tipo";
            fileTypeColumn.Width = 100;
            // 
            // contextMenu
            // 
            contextMenu.BackColor = Color.FromArgb(37, 37, 38);
            contextMenu.ForeColor = Color.White;
            contextMenu.Items.AddRange(new ToolStripItem[] { extractToolStripMenuItem });
            contextMenu.Name = "contextMenu";
            contextMenu.RenderMode = ToolStripRenderMode.Professional;
            contextMenu.Size = new Size(107, 26);
            // 
            // extractToolStripMenuItem
            // 
            extractToolStripMenuItem.Image = (Image)resources.GetObject("extractToolStripMenuItem.Image");
            extractToolStripMenuItem.Name = "extractToolStripMenuItem";
            extractToolStripMenuItem.Size = new Size(106, 22);
            extractToolStripMenuItem.Text = "&Extrair";
            extractToolStripMenuItem.Click += ExtractToolStripMenuItem_Click;
            // 
            // extractProgressBar
            // 
            extractProgressBar.BackColor = Color.FromArgb(51, 51, 55);
            extractProgressBar.ForeColor = Color.FromArgb(0, 122, 204);
            extractProgressBar.Location = new Point(12, 506);
            extractProgressBar.Name = "extractProgressBar";
            extractProgressBar.Size = new Size(763, 23);
            extractProgressBar.Style = ProgressBarStyle.Marquee;
            extractProgressBar.TabIndex = 6;
            extractProgressBar.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(787, 541);
            Controls.Add(extractProgressBar);
            Controls.Add(fileListView);
            Controls.Add(splitter2);
            Controls.Add(maskTextBox);
            Controls.Add(pathTextBox);
            Controls.Add(splitter1);
            Controls.Add(mainMenu);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = mainMenu;
            MaximizeBox = false;
            MinimumSize = new Size(527, 377);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gerenciador de arquivos SBPAK v1.0";
            FormClosing += Form1_FormClosing;
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            mainMenu.ResumeLayout(false);
            mainMenu.PerformLayout();
            contextMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPAKFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createPAKFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portugueseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.TextBox maskTextBox;
        private System.Windows.Forms.ListView fileListView;
        private System.Windows.Forms.ColumnHeader fileNameColumn;
        private System.Windows.Forms.ColumnHeader fileLengthColumn;
        private System.Windows.Forms.ColumnHeader offsetColumn;
        private System.Windows.Forms.ColumnHeader fileTypeColumn;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        private System.Windows.Forms.ProgressBar extractProgressBar;
    }
}
