namespace CipherBox
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Lock = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Unlock = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_UnlockTo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Zip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ZipAndLock = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Unzip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_UnzipTo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_View = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_StatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_FolderSize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_EncryptInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Verify = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_PasswordGenerator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Lock = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Unlock = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_EncryptInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Verify = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Refresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Setting = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLocked = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLastModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Context_EncryptInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Context_Verify = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Context_Lock = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Context_Unlock = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Context_Zip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Context_ZipAndLock = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Context_Unzip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Context_RemoveRestriction = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Context_AddWatermark = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Context_RemoveWatermark = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File,
            this.toolStripMenuItem_Edit,
            this.toolStripMenuItem_View,
            this.toolStripMenuItem_Tools,
            this.toolStripMenuItem_Help});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(695, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem_File
            // 
            this.toolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Open,
            this.toolStripMenuItem_Exit});
            this.toolStripMenuItem_File.Name = "toolStripMenuItem_File";
            this.toolStripMenuItem_File.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem_File.Text = "File";
            // 
            // toolStripMenuItem_Open
            // 
            this.toolStripMenuItem_Open.Name = "toolStripMenuItem_Open";
            this.toolStripMenuItem_Open.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem_Open.Text = "Open Folder";
            this.toolStripMenuItem_Open.Click += new System.EventHandler(this.ToolStripMenuItem_Open_Click);
            // 
            // toolStripMenuItem_Exit
            // 
            this.toolStripMenuItem_Exit.Name = "toolStripMenuItem_Exit";
            this.toolStripMenuItem_Exit.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem_Exit.Text = "Exit";
            this.toolStripMenuItem_Exit.Click += new System.EventHandler(this.ToolStripMenuItem_Exit_Click);
            // 
            // toolStripMenuItem_Edit
            // 
            this.toolStripMenuItem_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Lock,
            this.toolStripMenuItem_Unlock,
            this.toolStripMenuItem_UnlockTo,
            this.toolStripSeparator1,
            this.toolStripMenuItem_Zip,
            this.toolStripMenuItem_ZipAndLock,
            this.toolStripMenuItem_Unzip,
            this.toolStripMenuItem_UnzipTo});
            this.toolStripMenuItem_Edit.Name = "toolStripMenuItem_Edit";
            this.toolStripMenuItem_Edit.Size = new System.Drawing.Size(39, 20);
            this.toolStripMenuItem_Edit.Text = "Edit";
            // 
            // toolStripMenuItem_Lock
            // 
            this.toolStripMenuItem_Lock.Name = "toolStripMenuItem_Lock";
            this.toolStripMenuItem_Lock.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem_Lock.Text = "Lock File(s)";
            this.toolStripMenuItem_Lock.Click += new System.EventHandler(this.ToolStripMenuItem_Lock_Click);
            // 
            // toolStripMenuItem_Unlock
            // 
            this.toolStripMenuItem_Unlock.Name = "toolStripMenuItem_Unlock";
            this.toolStripMenuItem_Unlock.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem_Unlock.Text = "Unlock File(s)";
            this.toolStripMenuItem_Unlock.Click += new System.EventHandler(this.ToolStripMenuItem_Unlock_Click);
            // 
            // toolStripMenuItem_UnlockTo
            // 
            this.toolStripMenuItem_UnlockTo.Enabled = false;
            this.toolStripMenuItem_UnlockTo.Name = "toolStripMenuItem_UnlockTo";
            this.toolStripMenuItem_UnlockTo.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem_UnlockTo.Text = "Unlock File to";
            this.toolStripMenuItem_UnlockTo.Click += new System.EventHandler(this.ToolStripMenuItem_UnlockTo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // toolStripMenuItem_Zip
            // 
            this.toolStripMenuItem_Zip.Name = "toolStripMenuItem_Zip";
            this.toolStripMenuItem_Zip.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem_Zip.Text = "Zip";
            this.toolStripMenuItem_Zip.Click += new System.EventHandler(this.ToolStripMenuItem_Zip_Click);
            // 
            // toolStripMenuItem_ZipAndLock
            // 
            this.toolStripMenuItem_ZipAndLock.Name = "toolStripMenuItem_ZipAndLock";
            this.toolStripMenuItem_ZipAndLock.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem_ZipAndLock.Text = "Zip and Lock";
            this.toolStripMenuItem_ZipAndLock.Click += new System.EventHandler(this.ToolStripMenuItem_ZipAndLock_Click);
            // 
            // toolStripMenuItem_Unzip
            // 
            this.toolStripMenuItem_Unzip.Name = "toolStripMenuItem_Unzip";
            this.toolStripMenuItem_Unzip.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem_Unzip.Text = "Unzip File";
            this.toolStripMenuItem_Unzip.Click += new System.EventHandler(this.ToolStripMenuItem_Unzip_Click);
            // 
            // toolStripMenuItem_UnzipTo
            // 
            this.toolStripMenuItem_UnzipTo.Name = "toolStripMenuItem_UnzipTo";
            this.toolStripMenuItem_UnzipTo.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem_UnzipTo.Text = "Unzip File to";
            this.toolStripMenuItem_UnzipTo.Click += new System.EventHandler(this.ToolStripMenuItem_UnzipTo_Click);
            // 
            // toolStripMenuItem_View
            // 
            this.toolStripMenuItem_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_StatusBar,
            this.toolStripMenuItem_FolderSize,
            this.toolStripMenuItem_Refresh});
            this.toolStripMenuItem_View.Name = "toolStripMenuItem_View";
            this.toolStripMenuItem_View.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem_View.Text = "View";
            // 
            // toolStripMenuItem_StatusBar
            // 
            this.toolStripMenuItem_StatusBar.Checked = true;
            this.toolStripMenuItem_StatusBar.CheckOnClick = true;
            this.toolStripMenuItem_StatusBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem_StatusBar.Name = "toolStripMenuItem_StatusBar";
            this.toolStripMenuItem_StatusBar.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItem_StatusBar.Text = "Status Bar";
            this.toolStripMenuItem_StatusBar.Click += new System.EventHandler(this.ToolStripMenuItem_StatusBar_Click);
            // 
            // toolStripMenuItem_FolderSize
            // 
            this.toolStripMenuItem_FolderSize.CheckOnClick = true;
            this.toolStripMenuItem_FolderSize.Name = "toolStripMenuItem_FolderSize";
            this.toolStripMenuItem_FolderSize.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItem_FolderSize.Text = "Folder Size";
            this.toolStripMenuItem_FolderSize.Click += new System.EventHandler(this.ToolStripMenuItem_FolderSize_Click);
            // 
            // toolStripMenuItem_Refresh
            // 
            this.toolStripMenuItem_Refresh.Name = "toolStripMenuItem_Refresh";
            this.toolStripMenuItem_Refresh.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItem_Refresh.Text = "Refresh";
            this.toolStripMenuItem_Refresh.Click += new System.EventHandler(this.ToolStripMenuItem_Refresh_Click);
            // 
            // toolStripMenuItem_Tools
            // 
            this.toolStripMenuItem_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_EncryptInfo,
            this.toolStripMenuItem_Verify,
            this.toolStripMenuItem_PasswordGenerator,
            this.toolStripMenuItem_Setting});
            this.toolStripMenuItem_Tools.Name = "toolStripMenuItem_Tools";
            this.toolStripMenuItem_Tools.Size = new System.Drawing.Size(48, 20);
            this.toolStripMenuItem_Tools.Text = "Tools";
            // 
            // toolStripMenuItem_EncryptInfo
            // 
            this.toolStripMenuItem_EncryptInfo.Name = "toolStripMenuItem_EncryptInfo";
            this.toolStripMenuItem_EncryptInfo.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem_EncryptInfo.Text = "Encryption Info";
            this.toolStripMenuItem_EncryptInfo.Click += new System.EventHandler(this.ToolStripMenuItem_EncryptionInfo_Click);
            // 
            // toolStripMenuItem_Verify
            // 
            this.toolStripMenuItem_Verify.Name = "toolStripMenuItem_Verify";
            this.toolStripMenuItem_Verify.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem_Verify.Text = "Verify Password";
            this.toolStripMenuItem_Verify.Click += new System.EventHandler(this.ToolStripMenuItem_Verify_Click);
            // 
            // toolStripMenuItem_PasswordGenerator
            // 
            this.toolStripMenuItem_PasswordGenerator.Enabled = false;
            this.toolStripMenuItem_PasswordGenerator.Name = "toolStripMenuItem_PasswordGenerator";
            this.toolStripMenuItem_PasswordGenerator.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem_PasswordGenerator.Text = "Password Generator";
            this.toolStripMenuItem_PasswordGenerator.Click += new System.EventHandler(this.ToolStripMenuItem_PasswordGenerator_Click);
            // 
            // toolStripMenuItem_Setting
            // 
            this.toolStripMenuItem_Setting.Name = "toolStripMenuItem_Setting";
            this.toolStripMenuItem_Setting.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem_Setting.Text = "Options";
            this.toolStripMenuItem_Setting.Click += new System.EventHandler(this.ToolStripMenuItem_Setting_Click);
            // 
            // toolStripMenuItem_Help
            // 
            this.toolStripMenuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_About});
            this.toolStripMenuItem_Help.Name = "toolStripMenuItem_Help";
            this.toolStripMenuItem_Help.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem_Help.Text = "Help";
            // 
            // toolStripMenuItem_About
            // 
            this.toolStripMenuItem_About.Name = "toolStripMenuItem_About";
            this.toolStripMenuItem_About.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem_About.Text = "About CipherBox";
            this.toolStripMenuItem_About.Click += new System.EventHandler(this.ToolStripMenuItem_About_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 468);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(695, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(60, 17);
            this.toolStripStatusLabel1.Text = "Welcome!";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Lock,
            this.toolStripButton_Unlock,
            this.toolStripButton_EncryptInfo,
            this.toolStripButton_Verify,
            this.toolStripButton_Refresh,
            this.toolStripButton_Setting});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(695, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_Lock
            // 
            this.toolStripButton_Lock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Lock.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Lock.Image")));
            this.toolStripButton_Lock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Lock.Name = "toolStripButton_Lock";
            this.toolStripButton_Lock.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Lock.Text = "Lock";
            this.toolStripButton_Lock.Click += new System.EventHandler(this.ToolStripButton_Lock_Click);
            // 
            // toolStripButton_Unlock
            // 
            this.toolStripButton_Unlock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Unlock.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Unlock.Image")));
            this.toolStripButton_Unlock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Unlock.Name = "toolStripButton_Unlock";
            this.toolStripButton_Unlock.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Unlock.Text = "Unlock";
            this.toolStripButton_Unlock.Click += new System.EventHandler(this.ToolStripButton_Unlock_Click);
            // 
            // toolStripButton_EncryptInfo
            // 
            this.toolStripButton_EncryptInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_EncryptInfo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_EncryptInfo.Image")));
            this.toolStripButton_EncryptInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_EncryptInfo.Name = "toolStripButton_EncryptInfo";
            this.toolStripButton_EncryptInfo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_EncryptInfo.Text = "EncryptInfo";
            this.toolStripButton_EncryptInfo.Click += new System.EventHandler(this.ToolStripButton_EncryptionInfo_Click);
            // 
            // toolStripButton_Verify
            // 
            this.toolStripButton_Verify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Verify.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Verify.Image")));
            this.toolStripButton_Verify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Verify.Name = "toolStripButton_Verify";
            this.toolStripButton_Verify.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Verify.Text = "Verify";
            this.toolStripButton_Verify.ToolTipText = "Verify Password";
            this.toolStripButton_Verify.Click += new System.EventHandler(this.ToolStripButton_Verify_Click);
            // 
            // toolStripButton_Refresh
            // 
            this.toolStripButton_Refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Refresh.Image")));
            this.toolStripButton_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Refresh.Name = "toolStripButton_Refresh";
            this.toolStripButton_Refresh.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Refresh.Text = "Refresh";
            this.toolStripButton_Refresh.Click += new System.EventHandler(this.ToolStripButton_Refresh_Click);
            // 
            // toolStripButton_Setting
            // 
            this.toolStripButton_Setting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Setting.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Setting.Image")));
            this.toolStripButton_Setting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Setting.Name = "toolStripButton_Setting";
            this.toolStripButton_Setting.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Setting.Text = "Setting";
            this.toolStripButton_Setting.Click += new System.EventHandler(this.ToolStripButton_Setting_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Size = new System.Drawing.Size(695, 419);
            this.splitContainer1.SplitterDistance = 231;
            this.splitContainer1.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 1;
            this.treeView1.Size = new System.Drawing.Size(231, 419);
            this.treeView1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icon_folder.png");
            this.imageList1.Images.SetKeyName(1, "icon_folder_open.png");
            this.imageList1.Images.SetKeyName(2, "icon_blank_file.ico");
            this.imageList1.Images.SetKeyName(3, "icon_word.png");
            this.imageList1.Images.SetKeyName(4, "icon_ppt.png");
            this.imageList1.Images.SetKeyName(5, "icon_excel.png");
            this.imageList1.Images.SetKeyName(6, "icon_pdf.png");
            this.imageList1.Images.SetKeyName(7, "icon_zip_13.png");
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colLocked,
            this.colType,
            this.colSize,
            this.colLastModified});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(460, 419);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            // 
            // colLocked
            // 
            this.colLocked.Text = "Locked";
            // 
            // colType
            // 
            this.colType.Text = "Type";
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            // 
            // colLastModified
            // 
            this.colLastModified.Text = "Last Modified";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Context_EncryptInfo,
            this.toolStripMenuItem_Context_Verify,
            this.toolStripMenuItem_Context_Lock,
            this.toolStripMenuItem_Context_Unlock,
            this.toolStripMenuItem_Context_Zip,
            this.toolStripMenuItem_Context_ZipAndLock,
            this.toolStripMenuItem_Context_Unzip,
            this.toolStripMenuItem_Context_RemoveRestriction,
            this.toolStripMenuItem_Context_AddWatermark,
            this.toolStripMenuItem_Context_RemoveWatermark});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(179, 246);
            // 
            // toolStripMenuItem_Context_EncryptInfo
            // 
            this.toolStripMenuItem_Context_EncryptInfo.Name = "toolStripMenuItem_Context_EncryptInfo";
            this.toolStripMenuItem_Context_EncryptInfo.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Context_EncryptInfo.Text = "Encryption Info";
            this.toolStripMenuItem_Context_EncryptInfo.Click += new System.EventHandler(this.ToolStripMenuItem_Context_EncryptInfo_Click);
            // 
            // toolStripMenuItem_Context_Verify
            // 
            this.toolStripMenuItem_Context_Verify.Name = "toolStripMenuItem_Context_Verify";
            this.toolStripMenuItem_Context_Verify.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Context_Verify.Text = "Verify Password";
            this.toolStripMenuItem_Context_Verify.Click += new System.EventHandler(this.ToolStripMenuItem_Context_Verify_Click);
            // 
            // toolStripMenuItem_Context_Lock
            // 
            this.toolStripMenuItem_Context_Lock.Name = "toolStripMenuItem_Context_Lock";
            this.toolStripMenuItem_Context_Lock.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Context_Lock.Text = "Lock";
            this.toolStripMenuItem_Context_Lock.Click += new System.EventHandler(this.ToolStripMenuItem_Context_Lock_Click);
            // 
            // toolStripMenuItem_Context_Unlock
            // 
            this.toolStripMenuItem_Context_Unlock.Name = "toolStripMenuItem_Context_Unlock";
            this.toolStripMenuItem_Context_Unlock.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Context_Unlock.Text = "Unlock";
            this.toolStripMenuItem_Context_Unlock.Click += new System.EventHandler(this.ToolStripMenuItem_Context_Unlock_Click);
            // 
            // toolStripMenuItem_Context_Zip
            // 
            this.toolStripMenuItem_Context_Zip.Name = "toolStripMenuItem_Context_Zip";
            this.toolStripMenuItem_Context_Zip.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Context_Zip.Text = "Zip";
            this.toolStripMenuItem_Context_Zip.Click += new System.EventHandler(this.ToolStripMenuItem_Context_Zip_Click);
            // 
            // toolStripMenuItem_Context_ZipAndLock
            // 
            this.toolStripMenuItem_Context_ZipAndLock.Name = "toolStripMenuItem_Context_ZipAndLock";
            this.toolStripMenuItem_Context_ZipAndLock.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Context_ZipAndLock.Text = "Zip and Lock";
            this.toolStripMenuItem_Context_ZipAndLock.Click += new System.EventHandler(this.ToolStripMenuItem_Context_ZipAndLock_Click);
            // 
            // toolStripMenuItem_Context_Unzip
            // 
            this.toolStripMenuItem_Context_Unzip.Name = "toolStripMenuItem_Context_Unzip";
            this.toolStripMenuItem_Context_Unzip.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Context_Unzip.Text = "Unzip";
            this.toolStripMenuItem_Context_Unzip.Click += new System.EventHandler(this.ToolStripMenuItem_Context_Unzip_Click);
            // 
            // toolStripMenuItem_Context_RemoveRestriction
            // 
            this.toolStripMenuItem_Context_RemoveRestriction.Name = "toolStripMenuItem_Context_RemoveRestriction";
            this.toolStripMenuItem_Context_RemoveRestriction.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Context_RemoveRestriction.Text = "Remove Restriction";
            this.toolStripMenuItem_Context_RemoveRestriction.Click += new System.EventHandler(this.ToolStripMenuItem_Context_RemoveRestriction_Click);
            // 
            // toolStripMenuItem_Context_AddWatermark
            // 
            this.toolStripMenuItem_Context_AddWatermark.Name = "toolStripMenuItem_Context_AddWatermark";
            this.toolStripMenuItem_Context_AddWatermark.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Context_AddWatermark.Text = "Add Watermark";
            this.toolStripMenuItem_Context_AddWatermark.Click += new System.EventHandler(this.ToolStripMenuItem_Context_AddWatermark_Click);
            // 
            // toolStripMenuItem_Context_RemoveWatermark
            // 
            this.toolStripMenuItem_Context_RemoveWatermark.Name = "toolStripMenuItem_Context_RemoveWatermark";
            this.toolStripMenuItem_Context_RemoveWatermark.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Context_RemoveWatermark.Text = "Remove Watermark";
            this.toolStripMenuItem_Context_RemoveWatermark.Click += new System.EventHandler(this.ToolStripMenuItem_Context_RemoveWatermark_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 490);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "CipherBox";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Edit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Tools;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Help;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_Lock;
        private System.Windows.Forms.ToolStripButton toolStripButton_Unlock;
        private System.Windows.Forms.ToolStripButton toolStripButton_EncryptInfo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colLastModified;
        private System.Windows.Forms.ColumnHeader colLocked;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Open;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Setting;
        private System.Windows.Forms.ToolStripButton toolStripButton_Refresh;
        private System.Windows.Forms.ToolStripButton toolStripButton_Setting;
        private System.Windows.Forms.ToolStripButton toolStripButton_Verify;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Lock;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Unlock;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_UnlockTo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Zip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ZipAndLock;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Unzip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_UnzipTo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_EncryptInfo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Verify;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_About;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_StatusBar;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_FolderSize;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_PasswordGenerator;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Refresh;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Exit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Context_EncryptInfo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Context_Verify;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Context_Lock;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Context_Unlock;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Context_Zip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Context_ZipAndLock;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Context_Unzip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Context_RemoveRestriction;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Context_AddWatermark;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Context_RemoveWatermark;
    }
}

