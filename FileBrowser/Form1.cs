using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using CipherBox;
//using CipherBox.PasswordGeneration;


namespace CipherBox
{
    public partial class Form1 : Form
    {
        #region Initialization
        public CipherBoxConfig m_config = new CipherBoxConfig(null);

        //private PasswordGenerator m_pwdGenerator = null;

        public Form1()
        {
            InitializeComponent();

            this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseClick);
            this.listView1.MouseDoubleClick += new MouseEventHandler(this.ListView1_MouseDoubleClick);
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);

            this.listView1.MouseUp += new MouseEventHandler(this.listView1_MouseUp); // for adaptive context menu show only


            PopulateTreeView();
            PopulateListView(treeView1.SelectedNode);
        }
        #endregion

        #region populate tree and list views

        private int GetFileImageIndex(FileInfo fi)
        {
            string ftype = fi.Extension.ToUpper().TrimStart('.');
            if (ftype == "DOC" || ftype == "DOCX") return 3; // "Word";
            if (ftype == "PPT" || ftype == "PPTX") return 4; // "Slides";
            if (ftype == "XLS" || ftype == "XLSX" || ftype == "XLSB") return 5; // "Excel";
            if (ftype == "PDF") return 6; // "PDF";
            if (ftype == "ZIP" || ftype == "7Z" || ftype == "RAR") return 7; // "ZIP";

            return 2; // default "File";
        }



        /// <summary>
        /// populate tree view on the left window
        /// </summary>
        private void PopulateTreeView()
        {
            try
            {
                TreeNode rootNode;

                DirectoryInfo info = new DirectoryInfo(m_config.RootDirectory);
                if (info.Exists)
                {
                    rootNode = new TreeNode(info.Name);
                    rootNode.Tag = info;
                    GetDirectories(info.GetDirectories(), rootNode);

                    treeView1.Nodes.Clear();
                    treeView1.Nodes.Add(rootNode);
                    treeView1.SelectedNode = rootNode;
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// populate list view on the right window
        /// </summary>
        /// <param name="selectedNode"></param>
        private void PopulateListView(TreeNode selectedNode)
        {
            try
            {
                if (selectedNode == null) return;

                listView1.Items.Clear();
                DirectoryInfo nodeDirInfo = (DirectoryInfo)selectedNode.Tag;
                ListViewItem.ListViewSubItem[] subItems;
                ListViewItem item = null;

                try
                {
                    foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
                    {
                        string folder_date = dir.LastAccessTime.ToString("yyyy-MM-dd"); //ToShortDateString();
                        string folder_size = m_config.DisplayFolderSize ? GetSizeString(GetDirectorySize(dir, true)) : "";

                        item = new ListViewItem(dir.Name, 0);
                        subItems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, ""),
                        new ListViewItem.ListViewSubItem(item, "Folder"),
                        new ListViewItem.ListViewSubItem(item, folder_size),
                        new ListViewItem.ListViewSubItem(item, folder_date)
                    };
                        item.SubItems.AddRange(subItems);
                        listView1.Items.Add(item);
                    }
                }
                catch (UnauthorizedAccessException)
                { }

                try
                {
                    foreach (FileInfo file in nodeDirInfo.GetFiles())
                    {
                        string file_protected = (FileLocker.IsProtected(file.FullName) ? "Y" : "");
                        string file_size = file.Length.ToString();
                        string file_date = (file.LastWriteTime.Date == DateTime.Now.Date) ?
                            file.LastWriteTime.ToString("HH:mm:ss") : file.LastWriteTime.ToString("yyyy-MM-dd"); // ToShortTimeString() : ToShortDateString();
                        string file_type = FileLocker.GetFileType(file.FullName);

                        item = new ListViewItem(file.Name, GetFileImageIndex(file));
                        item.UseItemStyleForSubItems = false; // color setting works only if ListViewItem.UseItemStyleForSubItems = false
                        subItems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, file_protected) {ForeColor = Color.Green, Font = new Font(DefaultFont, FontStyle.Bold)},
                        new ListViewItem.ListViewSubItem(item, file_type), 
                        new ListViewItem.ListViewSubItem(item, file_size),
                        new ListViewItem.ListViewSubItem(item, file_date),
                    };

                        item.SubItems.AddRange(subItems);
                        listView1.Items.Add(item);
                    }
                }
                catch (UnauthorizedAccessException)
                { }

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region directory operations

        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                try
                {
                    aNode = new TreeNode(subDir.Name, 0, 0);
                    aNode.Tag = subDir;
                    aNode.ImageKey = "folder";

                    // traverse its sub-directories
                    subSubDirs = subDir.GetDirectories();
                    if (subSubDirs.Length != 0)
                    {
                        GetDirectories(subSubDirs, aNode);
                    }

                    nodeToAddTo.Nodes.Add(aNode);
                }
                catch (UnauthorizedAccessException)
                {
                    // ignore those symblic links
                }
            }
        }

        // requires: DotNet 2.0 only
        private static long GetDirectorySize(DirectoryInfo dInfo, bool includeSubDir)
        {
            long totalSize = 0;

            //DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] fi = dInfo.GetFiles();
            for (int i = 0; i < fi.Length; i++)
            {
                totalSize += fi.Length;
            }

            if (includeSubDir)
            {
                DirectoryInfo[] subdirs = dInfo.GetDirectories();
                for (int i = 0; i < subdirs.Length; i++)
                {
                    totalSize += GetDirectorySize(subdirs[i], true);
                }
            }

            return totalSize;
        }


        // compute directory size recursively
        // require: dotnet 4.0 and LINQ
        // .NET 4.0 introduces new methods to Enumerate Directory and Files.
        // All these methods return Enumerable Collections (IEnumerable<T>), which perform better than arrays. 
        // We will be using the DirectoryInfo.EnumerateDirectories and DirectoryInfo.EnumerateFiles
        // which returns an enumerable collection of Directory and File information respectively.
        /*
        static long GetDirectorySize_Net4(DirectoryInfo dInfo, bool includeSubDir)
        {
            // Enumerate all the files
            long totalSize = dInfo.EnumerateFiles()
                         .Sum(file => file.Length);

            // If Subdirectories are to be included
            if (includeSubDir)
            {
                // Enumerate all sub-directories
                totalSize += dInfo.EnumerateDirectories()
                         .Sum(dir => GetDirectorySize_Net4(dir, true));
            }
            return totalSize;
        }
        */

        // display of approximate size in string
        private static string GetSizeString(long bytes)
        {
            if (bytes < 1024)
                return string.Format("{0:N0} Bytes", bytes);
            else if (bytes < 1024 * 1024)
                return string.Format("{0:N0} KB", Math.Round((double)bytes / 1024, 3, MidpointRounding.ToEven));
            else if (bytes < 1024 * 1024 * 1024)
                return string.Format("{0:N0} MB", Math.Round((double)bytes / (1024 * 1024), 3, MidpointRounding.ToEven));
            else
                return string.Format("{0:N0} GB", Math.Round((double)bytes / (1024 * 1024 * 1024), 3, MidpointRounding.ToEven));
        }

        #endregion

        #region Choose folder
        // Menu-File-Open for choosing root directory
        private void ToolStripMenuItem_Open_Click(object sender, EventArgs e)
        {
            try
            {
                // Show the folder browser.
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                {
                    fbd.Description = "Choose a root direcotry:";
                    fbd.RootFolder = Environment.SpecialFolder.Desktop;
                }

                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Path specified.
                    m_config.RootDirectory = fbd.SelectedPath;

                    PopulateTreeView();
                    PopulateListView(treeView1.SelectedNode);
                }
            }
            catch (Exception)
            { }
        }

        // click the node on left-side tree
        void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // By using TreeView.HideSelection = false, there will be shadow for selected folder.
            // but I want to change folder icon too. so need to set SelectedImageIndex for this folder.
            e.Node.SelectedImageIndex = 1;

            PopulateListView(e.Node);
        }
        #endregion

        #region Lock

        // click ToolStrip button "add password"
        private void ToolStripButton_Lock_Click(object sender, EventArgs e)
        {
            try
            {
                // get selected from listview
                if (listView1.SelectedItems.Count == 0)
                    return;

                // choose a password
                string password = m_config.DefaultPassword;
                if (password == null)
                {
                    password = PromptPassword(null);
                    if (password == null) return;
                }

                // add password for all selected files
                DirectoryInfo nodeDirInfo = (DirectoryInfo)treeView1.SelectedNode.Tag;
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    string filename = listView1.SelectedItems[i].Text;
                    string filepath = Path.Combine(nodeDirInfo.FullName, filename);

                    FileLocker.Lock(filepath, password);
                }

                PopulateListView(treeView1.SelectedNode);
            }
            catch (Exception)
            { }
        }


        // Menu-Edit-LockFile
        private void ToolStripMenuItem_Lock_Click(object sender, EventArgs e)
        {
            ToolStripButton_Lock_Click(sender, e);
        }

        // ListView - Context Menu - Lock
        private void ToolStripMenuItem_Context_Lock_Click(object sender, EventArgs e)
        {
            ToolStripButton_Lock_Click(sender, e);
        }
        #endregion

        #region Unlock

        // click ToolStrip button "remove password"
        private void ToolStripButton_Unlock_Click(object sender, EventArgs e)
        {
            try
            {
                // get selected from listview
                if (listView1.SelectedItems.Count == 0)
                    return;

                // choose a password
                string password = m_config.DefaultPassword;
                if (password == null)
                {
                    password = PromptPassword(null);
                    if (password == null) return;
                }

                // remove password for all selected files
                DirectoryInfo nodeDirInfo = (DirectoryInfo)treeView1.SelectedNode.Tag;
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    string filename = listView1.SelectedItems[i].Text;
                    string filepath = Path.Combine(nodeDirInfo.FullName, filename);

                    FileLocker.Unlock(filepath, password);
                }

                PopulateListView(treeView1.SelectedNode);
            }
            catch (Exception)
            { }
        }


        // Menu-Edit-UnlockFile
        private void ToolStripMenuItem_Unlock_Click(object sender, EventArgs e)
        {
            ToolStripButton_Unlock_Click(sender, e);
        }

        // ListView - Context Menu - Unlock
        private void ToolStripMenuItem_Context_Unlock_Click(object sender, EventArgs e)
        {
            ToolStripButton_Unlock_Click(sender, e);
        }


        // Menu-Edit-UnlockFileTo, user has to choose destination folder
        // refer to: Menu-Edit-UnzipFileTo
        private void ToolStripMenuItem_UnlockTo_Click(object sender, EventArgs e)
        {
            // click disabled (grey)

            try
            {
                // get selected from listview
                if (listView1.SelectedItems.Count != 1)
                {
                    toolStripStatusLabel1.Text = "please choose one file from the list view.";
                    return;
                }

                // get the source folder
                DirectoryInfo nodeDirInfo = (DirectoryInfo)treeView1.SelectedNode.Tag;

                string destination = null;

                // Show the file-save dialog to user
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Title = "Unlock file to specified folder:";
                    dialog.InitialDirectory = nodeDirInfo.FullName; // m_config.RootDirectory;
                    dialog.Filter = "All files (*.*)|*.*|PDF files (*.pdf)|*.pdf|Zip files (*.zip)|*.zip|DOCX files (*.docx)|*.docx"
                        + "|PPTX files (*.pptx)|*.pptx|XLSX files (*.xlsx)|*.xlsx|XLSB files (*.xlsb)|*.xlsb";
                    dialog.FilterIndex = 0;
                    //dialog.RestoreDirectory = true;

                    if (dialog.ShowDialog() != DialogResult.OK)
                    {
                        toolStripStatusLabel1.Text = "no destination folder is chosen.";
                        return;
                    }

                    // Path specified.
                    destination = dialog.FileName;
                }

                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    string filename = listView1.SelectedItems[i].Text;
                    string filepath = Path.Combine(nodeDirInfo.FullName, filename);

                    // unlock file with default password
                    //FileLocker.Unlock(filepath, m_config.DefaultPassword);  // TODO: to destinationDir

                    // if not successfull, try more times??
                }

                // refresh if the destination folder is actually current folder
                string destinationDir = Path.GetDirectoryName(destination);
                if (Path.Equals(destinationDir, nodeDirInfo.FullName))
                {
                    //PopulateTreeView();
                    PopulateListView(treeView1.SelectedNode);
                }

                toolStripStatusLabel1.Text = "Unlock done.";
            }
            catch (Exception)
            { }
        }

        #endregion

        #region Encryption Info

        // click ToolStrip button "encryption information"
        private void ToolStripButton_EncryptionInfo_Click(object sender, EventArgs e)
        {
            try
            {
                // get selected from listview
                if (listView1.SelectedItems.Count != 1)
                {
                    toolStripStatusLabel1.Text = "please choose one file from the list view.";
                    return; // must select only one
                }

                DirectoryInfo nodeDirInfo = (DirectoryInfo)treeView1.SelectedNode.Tag;
                string filename = listView1.SelectedItems[0].Text;
                string filepath = Path.Combine(nodeDirInfo.FullName, filename);

                string text = FileLocker.GetEncryptionInfo(filepath);
                MessageBox.Show(text, filename);
            }
            catch (Exception)
            { }
        }

        private void ToolStripMenuItem_EncryptionInfo_Click(object sender, EventArgs e)
        {
            ToolStripButton_EncryptionInfo_Click(sender, e);
        }

        // ListView - Context Menu - EncryptInfo
        private void ToolStripMenuItem_Context_EncryptInfo_Click(object sender, EventArgs e)
        {
            ToolStripButton_EncryptionInfo_Click(sender, e);
        }

        #endregion

        #region Password generator and verify

        // click ToolStrip button "verify password"
        private void ToolStripButton_Verify_Click(object sender, EventArgs e)
        {
            try
            {
                // get selected from listview
                if (listView1.SelectedItems.Count != 1)
                {
                    toolStripStatusLabel1.Text = "please choose one file from the list view.";
                    return;  // no selection or must select only one
                }

                DirectoryInfo nodeDirInfo = (DirectoryInfo)treeView1.SelectedNode.Tag;
                string filename = listView1.SelectedItems[0].Text;
                string filepath = Path.Combine(nodeDirInfo.FullName, filename);

                if (!FileLocker.IsMyFile(filepath))
                {
                    toolStripStatusLabel1.Text = filename + " cannot be verified.";
                    return;
                }

                if (!FileLocker.IsProtected(filepath))
                {
                    toolStripStatusLabel1.Text = filename + " does not require a password.";
                    return;
                }

                toolStripStatusLabel1.Text = "start to verify " + filename + " ...";

                // firstly find password in cache.
                List<string> passwords = m_config.UsedPasswords;
                if (m_config.DefaultPassword != null && passwords.IndexOf(m_config.DefaultPassword) == -1)
                {
                    passwords.Add(m_config.DefaultPassword);
                }
                if (passwords.Count == 0) // no stored passwords
                {
                    // prompt user to enter a password
                    string promptPwd = PromptPassword(null);
                    if (promptPwd == null)
                    {
                        toolStripStatusLabel1.Text = "no password is given.";
                        return;
                    }

                    passwords.Add(promptPwd);
                }

                foreach (string password in passwords)
                {
                    if (FileLocker.VerifyPassword(filepath, password))
                    {
                        MessageBox.Show("Password \"" + password + "\" is verified.", "Password Verification: " + filename);
                        toolStripStatusLabel1.Text = "successfully verified password for " + filename;

                        m_config.UsedPasswords = passwords; // update password base
                        return;
                    }
                }

                // cannot find password in cache, then prompt user to enter a password
                string promptPwd2 = PromptPassword(null);
                if (promptPwd2 != null)
                {
                    if (FileLocker.VerifyPassword(filepath, promptPwd2))
                    {
                        MessageBox.Show("Password \"" + promptPwd2 + "\" is verified.", "Password Verification: " + filename);
                        toolStripStatusLabel1.Text = "successfully verified password for " + filename;

                        // add this new password to cache
                        if (promptPwd2 != m_config.DefaultPassword && passwords.IndexOf(promptPwd2) == -1)
                        {
                            m_config.UsedPasswords.Add(promptPwd2);
                        }
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Password \"" + promptPwd2 + "\" failed.", "Password Verification: " + filename);
                        toolStripStatusLabel1.Text = "prompt password is wrong for " + filename;
                        return;
                    }
                }
                else
                {
                    // verification failed
                    MessageBox.Show("Unknown password.", filename);
                    toolStripStatusLabel1.Text = "password is unknown for " + filename;
                    return;
                }


                // test more password options.
                // should be put into a thread so program can respond, and statusbar can show sth.
                /*
                if (m_pwdGenerator != null)
                {
                    decimal startIndex = m_pwdGenerator.StartIndex(); // can be 0 simply
                    decimal endIndex = m_pwdGenerator.EndIndex();
                    long t1 = Environment.TickCount;
                    for (decimal ind = startIndex; ind < endIndex; ind++)
                    {
                        string password = m_pwdGenerator.Password(ind);

                        bool verifed = FileLocker.VerifyPassword(filepath, password);

                        long t2 = Environment.TickCount;
                        decimal costtime = (t2 - t1);  // in miliseconds
                        decimal avgspeed = (costtime == 0 ? 0 : (ind + 1 - startIndex) * 1000 / costtime);  // counter/second
                        decimal percent = (ind == endIndex - 1 ? 1 : (ind + 1 - startIndex) / (endIndex - startIndex));
                        decimal remainedtime = (ind == endIndex - 1 ? 0 : (endIndex - ind) * (t2 - t1) / (ind + 1 - startIndex)); // in miliseconds
                        string disp = password + "\t" + percent.ToString("P2") + "\tpps=" + avgspeed.ToString("0.")
                            + "\telapse=" + DisplayElapseTime(costtime) + "\tremain=" + DisplayElapseTime(remainedtime);
                        toolStripStatusLabel1.Text = disp;

                        if (verifed)
                        {
                            MessageBox.Show("Password \"" + password + "\" is verified.", filename);
                            toolStripStatusLabel1.Text = "successfully verified password for " + filename;
                            return;
                        }
                    }
                }
                else
                {
                    // TODO: prompt user to choose password generator
                    // TODO: further provide a password option for user
                    m_pwdGenerator = new PasswordGenerator("9876**");
                    m_pwdGenerator.IncludeChars = true;
                    m_pwdGenerator.IncludeDigits = true;
                }
                */

            }
            catch (Exception)
            { }
        }


        // Menu-Tools-VerifyPassword
        private void ToolStripMenuItem_Verify_Click(object sender, EventArgs e)
        {
            ToolStripButton_Verify_Click(sender, e);
        }

        // ListView - Context Menu - VerifyPassword
        private void ToolStripMenuItem_Context_Verify_Click(object sender, EventArgs e)
        {
            ToolStripButton_Verify_Click(sender, e);
        }



        private void ToolStripMenuItem_PasswordGenerator_Click(object sender, EventArgs e)
        {
            // click disabled (grey)
            // TODO:
            toolStripStatusLabel1.Text = "[Password Generator] is not implemented.";
        }


        private string DisplayElapseTime(decimal miliseconds)
        {
            decimal t = miliseconds;

            if (t < 1000) return t.ToString("0.") + "ms";

            if (t / 1000 < 60) return (t / 1000).ToString("0.") + "s";

            if (t / 1000 / 60 < 60) return (t / 1000 / 60).ToString("0.") + "min";

            if (t / 1000 / 60 / 60 < 24) return (t / 1000 / 60 / 60).ToString("0.") + "h";

            if (t / 1000 / 60 / 60 / 24 < 365) return (t / 1000 / 60 / 60 / 24).ToString("0.") + "d";

            return (t / 1000 / 60 / 60 / 24 / 365).ToString("0.") + "y";
        }


        #endregion

        #region Refresh
        // click ToolStrip button "refresh"
        private void ToolStripButton_Refresh_Click(object sender, EventArgs e)
        {
            PopulateListView(treeView1.SelectedNode);
        }

        // Menu-View-Refresh
        private void ToolStripMenuItem_Refresh_Click(object sender, EventArgs e)
        {
            ToolStripButton_Refresh_Click(sender, e);
        }
        #endregion

        #region Password Setting
        // click ToolStrip button "setting"
        private void ToolStripButton_Setting_Click(object sender, EventArgs e)
        {
            try
            {
                string password = PromptPassword(m_config.DefaultPassword);
                if (password != null && m_config.DefaultPassword != null)
                {
                    toolStripStatusLabel1.Text = "a default password is saved.";
                }

                // TODO: more sophiscated setting window is needed
                //Form2 option = new Form2();
                //option.ShowDialog(this);
            }
            catch (Exception)
            { }
        }

        // Menu-Options for choosing master password for documents
        private void ToolStripMenuItem_Setting_Click(object sender, EventArgs e)
        {
            ToolStripButton_Setting_Click(sender, e);
        }


        // Func: prompt user to enter a password in dialog. set as master password when required
        // usage: string promptValue = Prompt.ShowDialog("password");
        private string PromptPassword(string defaultPwd)
        {
            string password = defaultPwd;
            bool asMasterPwd = false;
            bool confirmed = false;

            Form prompt = new Form();
            prompt.Width = 250;
            prompt.Height = 180;
            prompt.Text = "Enter a password";  // caption
            prompt.StartPosition = FormStartPosition.CenterParent;

            TextBox textBox = new TextBox() { Left = 20, Top = 20, Width = 140, Text = password, UseSystemPasswordChar = true};

            CheckBox checkBox1 = new CheckBox() { Left = 20, Top = 40, Width = 140, Text = "Show Password" };
            checkBox1.Click += (sender, e) => { textBox.UseSystemPasswordChar = !checkBox1.Checked; };

            CheckBox checkBox2 = new CheckBox() { Left = 20, Top = 70, Width = 140, Text = "As Default Password" };
            checkBox2.Click += (sender, e) => { asMasterPwd = checkBox2.Checked; };

            Button cancel = new Button() { Left = 30, Top = 100, Width = 50, Text = "Cancel" };
            cancel.Click += (sender, e) => { password = null; prompt.Close(); };

            Button confirm = new Button() { Left = 120, Top = 100, Width = 50, Text = "OK" };
            confirm.Click += (sender, e) => { password = textBox.Text; confirmed = true; prompt.Close(); };

            prompt.Controls.Add(textBox);
            prompt.Controls.Add(checkBox1);
            prompt.Controls.Add(checkBox2);
            prompt.Controls.Add(cancel);
            prompt.Controls.Add(confirm);

            prompt.ShowDialog(this);
            if ( confirmed )
            {
                if (password != null && asMasterPwd)
                {
                    m_config.DefaultPassword = password;
                }

                return password;
            }

            return null;
        }

        #endregion

        #region Double-click the file to open
        // Double-click document item to call external application,
        // or double-click folder to enter.
        private void ListView1_MouseDoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count == 1)
                {
                    DirectoryInfo nodeDirInfo = (DirectoryInfo)treeView1.SelectedNode.Tag;

                    string name = listView1.SelectedItems[0].Text;
                    string path = Path.Combine(nodeDirInfo.FullName, name);

                    // determine whether the src path is file or folder
                    FileAttributes attr = File.GetAttributes(path);
                    bool srcIsFolder = ((attr & FileAttributes.Directory) == FileAttributes.Directory);
                    if (srcIsFolder && Directory.Exists(path))
                    {
                        foreach (TreeNode tn in treeView1.SelectedNode.Nodes)
                        {
                            if (name == tn.Text)
                            {
                                treeView1.SelectedNode = tn;
                                treeView1.SelectedNode.SelectedImageIndex = 1; // folder is open
                                PopulateListView(treeView1.SelectedNode);
                                return;
                            }
                        }
                    }
                    else if (File.Exists(path)) // assume it is file
                    {
                        System.Diagnostics.Process.Start(path);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Zip

        // Menu-Edit-Zip
        private void ToolStripMenuItem_Zip_Click(object sender, EventArgs e)
        {
            try
            {
                // get selected from listview
                if (listView1.SelectedItems.Count != 1)
                {
                    toolStripStatusLabel1.Text = "please choose one file/folder from the list view.";
                    return;
                }

                DirectoryInfo nodeDirInfo = (DirectoryInfo)treeView1.SelectedNode.Tag;
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    string filename = listView1.SelectedItems[i].Text;
                    string filepath = Path.Combine(nodeDirInfo.FullName, filename);

                    FileLocker.Compress(filepath, null, null); // as zip file without password
                }

                PopulateListView(treeView1.SelectedNode);
            }
            catch (Exception)
            { }
        }

        // ListView - Context Menu - Zip
        private void ToolStripMenuItem_Context_Zip_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_Zip_Click(sender, e);
        }


        // Menu-Edit-ZipWithPassword
        private void ToolStripMenuItem_ZipAndLock_Click(object sender, EventArgs e)
        {
            try
            {
                // get selected from listview
                if (listView1.SelectedItems.Count != 1)
                {
                    toolStripStatusLabel1.Text = "please choose one file/folder from the list view.";
                    return;
                }

                // choose a password
                string password = m_config.DefaultPassword;
                if (password == null)
                {
                    password = PromptPassword(null);
                    if (password == null) return;
                }

                // zip file with default password
                DirectoryInfo nodeDirInfo = (DirectoryInfo)treeView1.SelectedNode.Tag;
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    string filename = listView1.SelectedItems[i].Text;
                    string filepath = Path.Combine(nodeDirInfo.FullName, filename);

                    FileLocker.Compress(filepath, null, password);
                }

                PopulateListView(treeView1.SelectedNode);
            }
            catch (Exception)
            { }
        }


        // ListView - ContextMenu - ZipAndLock
        private void ToolStripMenuItem_Context_ZipAndLock_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_ZipAndLock_Click(sender, e);
        }
        #endregion

        #region Unzip

        // Menu-Edit-Unzip
        private void ToolStripMenuItem_Unzip_Click(object sender, EventArgs e)
        {
            try
            {
                // get selected from listview
                if (listView1.SelectedItems.Count != 1)
                {
                    toolStripStatusLabel1.Text = "please choose one zip file from the list view.";
                    return;
                }

                // choose a password
                string password = m_config.DefaultPassword;
                if (password == null)
                {
                    password = PromptPassword(null);
                    if (password == null) return;
                }

                // unzip file with default password
                DirectoryInfo nodeDirInfo = (DirectoryInfo)treeView1.SelectedNode.Tag;
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    string filename = listView1.SelectedItems[i].Text;
                    string filepath = Path.Combine(nodeDirInfo.FullName, filename);

                    FileLocker.Extract(filepath, null, password);

                    // if not successfull, try more passwords??
                }

                PopulateListView(treeView1.SelectedNode);
            }
            catch (Exception)
            { }
        }


        private void ToolStripMenuItem_Context_Unzip_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_Unzip_Click(sender, e);
        }


        // Menu-Edit-UnzipTo: unzip a zip file to specified directory
        private void ToolStripMenuItem_UnzipTo_Click(object sender, EventArgs e)
        {
            try
            {
                // get selected from listview
                if (listView1.SelectedItems.Count != 1)
                {
                    toolStripStatusLabel1.Text = "please choose one zip file from the list view.";
                    return;
                }

                // get the source folder
                DirectoryInfo nodeDirInfo = (DirectoryInfo)treeView1.SelectedNode.Tag;

                // Show the folder browser dialog to user
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                {
                    fbd.Description = "Unzip archive file to specified folder:";
                    fbd.RootFolder = Environment.SpecialFolder.Desktop;
                    fbd.SelectedPath = nodeDirInfo.FullName; // m_config.RootDirectory;
                    fbd.ShowNewFolderButton = true;
                }

                DialogResult result = fbd.ShowDialog();
                if (result != DialogResult.OK)
                {
                    toolStripStatusLabel1.Text = "no destination folder is chosen.";
                    return;
                }

                // Path specified.
                string destinationDir = fbd.SelectedPath;

                // choose a password
                string password = m_config.DefaultPassword;
                if (password == null)
                {
                    password = PromptPassword(null);
                    if (password == null) return;
                }

                // unzip file with default password
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    string filename = listView1.SelectedItems[i].Text;
                    string filepath = Path.Combine(nodeDirInfo.FullName, filename);

                    FileLocker.Extract(filepath, destinationDir, password);
                    // if not successfull, try more times??
                }

                // refresh if the destination folder is actually current folder
                if (Path.Equals(destinationDir, nodeDirInfo.FullName))
                {
                    //PopulateTreeView();
                    PopulateListView(treeView1.SelectedNode);
                }

                toolStripStatusLabel1.Text = "Unzip done.";
            }
            catch (Exception)
            { }
        }

        #endregion

        #region View statusbar
        // Menu-View-StatusBar
        private void ToolStripMenuItem_StatusBar_Click(object sender, EventArgs e)
        {
            statusStrip1.Visible = !statusStrip1.Visible;
        }
        #endregion

        #region View folder size
        // Menu-View-FolderSize
        private void ToolStripMenuItem_FolderSize_Click(object sender, EventArgs e)
        {
            m_config.DisplayFolderSize = !m_config.DisplayFolderSize;
            if (m_config.DisplayFolderSize)
            {
                PopulateListView(treeView1.SelectedNode); // refresh
            }
        }
        #endregion
        
        #region About CipherBox and Exit
        // Menu-Help-AboutCipherBox
        private void ToolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            try
            {
                string text =
                   "Product:       CipherBox c2012\n"
                 + "Version:       1.0.0\n"
                 + "Author:        Nlog2N\n"
                 + "Contact:       cipherbox@outlook.com\n"
                 + "\n\n"
                 + "CipherBox protects all your documents (Office/PDF/Zip/etc) with password,\n"
                 + "so you won't worry about those unsafe storages like cloud and public computers.";

                //MessageBox.Show(text, "About CipherBox");
                // consider to customize a message box with an http link
                Form msgbox = new Form();
                msgbox.Width = 420;
                msgbox.Height = 220;
                //msgbox.AutoSize = true;
                msgbox.Text = "About CipherBox";  // caption
                msgbox.StartPosition = FormStartPosition.CenterParent;

                Label label = new Label() { Left = 20, Top = 20, AutoSize = true, Text = text };

                LinkLabel link = new LinkLabel() { Left = 20, Top = 130, AutoSize = true, Text = "http://cipherbox.wordpress.com/" };
                link.Links.Add(0, link.Text.Length, link.Text);
                link.LinkClicked += (linksender, linke) =>
                {
                    string http = linke.Link.LinkData as string;
                    if (!string.IsNullOrEmpty(http)) System.Diagnostics.Process.Start(http);
                    msgbox.Close();
                };

                Button button = new Button() { Left = 120, Top = 150, AutoSize = true, Text = "OK" };
                button.Click += (bsender, be) => { msgbox.Close(); };

                msgbox.Controls.Add(label);
                msgbox.Controls.Add(link);
                msgbox.Controls.Add(button);
                msgbox.ShowDialog(this);
            }
            catch (Exception)
            { }
        }

        
        // Menu-File-Exit
        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Sorting Listview Columns
        private int sortColumn = -1;
        private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            try
            {
                // Determine whether the column is the same as the last column clicked.
                if (e.Column != sortColumn)
                {
                    // Set the sort column to the new column.
                    sortColumn = e.Column;
                    // Set the sort order to ascending by default.
                    listView1.Sorting = SortOrder.Ascending;
                }
                else
                {
                    // Determine what the last sort order was and change it.
                    if (listView1.Sorting == SortOrder.Ascending)
                        listView1.Sorting = SortOrder.Descending;
                    else
                        listView1.Sorting = SortOrder.Ascending;
                }

                // Set the ListViewItemSorter property to a new ListViewItemComparer object.
                this.listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, listView1.Sorting);
                // Call the sort method to manually sort.
                listView1.Sort();
            }
            catch (Exception)
            { }
        }

        // Implements the manual sorting of items by columns.
        class ListViewItemComparer : IComparer
        {
            private int col;
            private SortOrder order;
            public ListViewItemComparer()
            {
                col = 0;
                order = SortOrder.Ascending;
            }
            public ListViewItemComparer(int column, SortOrder order)
            {
                col = column;
                this.order = order;
            }
            public int Compare(object x, object y)
            {
                int returnVal;
                try
                {
                    // folder is always put ahead
                    if (((ListViewItem)x).SubItems[2].Text == "Folder" && ((ListViewItem)y).SubItems[2].Text != "Folder")
                        returnVal = -1;
                    else if (((ListViewItem)x).SubItems[2].Text != "Folder" && ((ListViewItem)y).SubItems[2].Text == "Folder")
                        returnVal = 1;
                    else
                    {
                        string s1 = ((ListViewItem)x).SubItems[col].Text;
                        string s2 = ((ListViewItem)y).SubItems[col].Text;

                        /* fanghui: date comparison kinda slow. so i choose date string to be formated
                         * as "2013/08/02 14:00:00"
                         */
                        // Determine whether the type being compared is a date type ??
                        // Parse the two objects passed as a parameter as a DateTime.
                        /*
                        System.DateTime firstDate = DateTime.Parse(s1);
                        System.DateTime secondDate = DateTime.Parse(s2);
                        returnVal = DateTime.Compare(firstDate, secondDate); // Compare the two dates.
                        */

                        if (col == 3 && ((ListViewItem)x).SubItems[2].Text != "Folder") // size
                        {
                            // Compare the two items as integers
                            int firstSize = 0; int.TryParse(s1, out firstSize);
                            int secondSize = 0; int.TryParse(s2, out secondSize);
                            returnVal = (firstSize < secondSize ? (-1) : (firstSize == secondSize ? 0 : 1));
                        }
                        else
                        {
                            // Compare the two items as a string.
                            returnVal = String.Compare(s1, s2);
                        }
                        // Determine whether the sort order is descending.
                        if (order == SortOrder.Descending)
                            returnVal *= -1; // Invert the value returned by String.Compare.
                    }
                    return returnVal;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        #endregion

        #region Display different context menu for file types

        // when user right-clicks a file from list view, program will show different context menu strip
        private ContextMenuStrip GetAdaptiveContextMenuStrip()
        {
            try
            {
                // default setting
                ContextMenuStrip cms = new ContextMenuStrip(this.components);
                cms.Name = "contextMenuStrip1";
                //cms.Size = new System.Drawing.Size(158, 180);

                DirectoryInfo nodeDirInfo = (DirectoryInfo)treeView1.SelectedNode.Tag;
                if (listView1.SelectedItems.Count == 1) // single file: EncryptInfo, Verify, Lock, Unlock, Zip, ZipAndLock, Unzip
                {
                    string filename = listView1.SelectedItems[0].Text;
                    string filepath = Path.Combine(nodeDirInfo.FullName, filename);

                    FileInfo fi = new FileInfo(filepath);
                    string ftype = fi.Extension.ToUpper().TrimStart('.');

                    //if (ftype == "PDF") return "PDF";

                    if (FileLocker.IsProtected(filepath))
                    {
                        if (ftype == "ZIP" || ftype == "7Z" || ftype == "RAR")  // zip file
                        {
                            cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                this.toolStripMenuItem_Context_EncryptInfo,
                                this.toolStripMenuItem_Context_Verify,
                                //this.toolStripMenuItem_Context_Lock,
                                this.toolStripMenuItem_Context_Unlock,
                                //this.toolStripMenuItem_Context_Zip,
                                //this.toolStripMenuItem_Context_ZipAndLock,
                                this.toolStripMenuItem_Context_Unzip
                            });
                            //cms.Items.Add(tsm);
                        }
                        else if (ftype == "DOC" || ftype == "PPT" || ftype == "XLS")
                        {
                            // no decryption support
                            cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                this.toolStripMenuItem_Context_EncryptInfo,
                                this.toolStripMenuItem_Context_Verify,
                                //this.toolStripMenuItem_Context_Lock,
                                //this.toolStripMenuItem_Context_Unlock,
                                this.toolStripMenuItem_Context_Zip,
                                //this.toolStripMenuItem_Context_ZipAndLock,
                                //this.toolStripMenuItem_Context_Unzip
                            });
                            //cms.Items.Add(tsm);
                        }
                        else if (ftype == "PDF")
                        {
                            cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                this.toolStripMenuItem_Context_EncryptInfo,
                                this.toolStripMenuItem_Context_Verify,
                                //this.toolStripMenuItem_Context_Lock,
                                this.toolStripMenuItem_Context_Unlock,
                                this.toolStripMenuItem_Context_Zip,
                                //this.toolStripMenuItem_Context_ZipAndLock,
                                //this.toolStripMenuItem_Context_Unzip,
                                this.toolStripMenuItem_Context_RemoveRestriction,
                                this.toolStripMenuItem_Context_AddWatermark,
                                this.toolStripMenuItem_Context_RemoveWatermark
                            });
                        }
                        else
                        {
                            cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                this.toolStripMenuItem_Context_EncryptInfo,
                                this.toolStripMenuItem_Context_Verify,
                                //this.toolStripMenuItem_Context_Lock,
                                this.toolStripMenuItem_Context_Unlock,
                                this.toolStripMenuItem_Context_Zip,
                                //this.toolStripMenuItem_Context_ZipAndLock,
                                //this.toolStripMenuItem_Context_Unzip
                            });
                            //cms.Items.Add(tsm);
                        }
                    }
                    else // for unprotected file
                    {
                        if (ftype == "ZIP" || ftype == "7Z" || ftype == "RAR")  // zip file
                        {
                            cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                //this.toolStripMenuItem_Context_EncryptInfo,
                                //this.toolStripMenuItem_Context_Verify,
                                this.toolStripMenuItem_Context_Lock,
                                //this.toolStripMenuItem_Context_Unlock,
                                //this.toolStripMenuItem_Context_Zip,
                                //this.toolStripMenuItem_Context_ZipAndLock,
                                this.toolStripMenuItem_Context_Unzip
                            });
                            //cms.Items.Add(tsm);
                        }
                        else if (ftype == "DOC"  || ftype == "PPT"  || ftype == "XLS")
                        {
                            // no encryption support
                            cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                //this.toolStripMenuItem_Context_EncryptInfo,
                                //this.toolStripMenuItem_Context_Verify,
                                //this.toolStripMenuItem_Context_Lock,
                                //this.toolStripMenuItem_Context_Unlock,
                                this.toolStripMenuItem_Context_Zip,
                                this.toolStripMenuItem_Context_ZipAndLock,
                                //this.toolStripMenuItem_Context_Unzip
                            });
                            //cms.Items.Add(tsm);
                        }
                        else if (ftype == "PDF")
                        {
                            cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                //this.toolStripMenuItem_Context_EncryptInfo,
                                //this.toolStripMenuItem_Context_Verify,
                                this.toolStripMenuItem_Context_Lock,
                                //this.toolStripMenuItem_Context_Unlock,
                                this.toolStripMenuItem_Context_Zip,
                                this.toolStripMenuItem_Context_ZipAndLock,
                                //this.toolStripMenuItem_Context_Unzip,
                                this.toolStripMenuItem_Context_RemoveRestriction,
                                this.toolStripMenuItem_Context_AddWatermark,
                                this.toolStripMenuItem_Context_RemoveWatermark
                            });
                        }
                        else
                        {
                            cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                            //this.toolStripMenuItem_Context_EncryptInfo,
                            //this.toolStripMenuItem_Context_Verify,
                            this.toolStripMenuItem_Context_Lock,
                            //this.toolStripMenuItem_Context_Unlock,
                            this.toolStripMenuItem_Context_Zip,
                            this.toolStripMenuItem_Context_ZipAndLock,
                            //this.toolStripMenuItem_Context_Unzip
                            });
                            //cms.Items.Add(tsm);
                        }
                    }
                }
                else  // multiple file selection: Verify, Lock, Unlock, Zip, ZipAndLock, Unzip
                {
                    for (int i = 0; i < listView1.SelectedItems.Count; i++)
                    {
                        string filename = listView1.SelectedItems[i].Text;
                        string filepath = Path.Combine(nodeDirInfo.FullName, filename);

                        //FileLocker.Compress(filepath, null, password);

                        cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                            //this.toolStripMenuItem_Context_EncryptInfo,
                            this.toolStripMenuItem_Context_Verify,
                            this.toolStripMenuItem_Context_Lock,
                            this.toolStripMenuItem_Context_Unlock,
                            this.toolStripMenuItem_Context_Zip,
                            this.toolStripMenuItem_Context_ZipAndLock,
                            //this.toolStripMenuItem_Context_Unzip
                        });
                    }
                }
                return cms;
            }
            catch (Exception)
            {
                return null;
            }
        }

        void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    // update menu
                    ContextMenuStrip cms = GetAdaptiveContextMenuStrip();
                    if (cms == null) return;
                    this.listView1.ContextMenuStrip = cms;
                    this.contextMenuStrip1 = cms;

                    if (listView1.SelectedItems.Count > 0)
                    {
                        this.contextMenuStrip1.Show(this.listView1, new Point(e.X, e.Y));
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Remove PDF restrictions

        private void ToolStripMenuItem_Context_RemoveRestriction_Click(object sender, EventArgs e)
        {
            try
            {
                // get selected from listview
                if (listView1.SelectedItems.Count == 0)
                    return;

                // Note: Unlike Unlock PDF document, this operation does not require a password.
                // it attempts to remove user restrictions only

                DirectoryInfo nodeDirInfo = (DirectoryInfo)treeView1.SelectedNode.Tag;
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    string filename = listView1.SelectedItems[i].Text;
                    string filepath = Path.Combine(nodeDirInfo.FullName, filename);

                    FileLocker.Unlock(filepath, "");
                }

                PopulateListView(treeView1.SelectedNode);
            }
            catch (Exception)
            { }
        }
        #endregion

        #region Add/remove PDF watermark

        private void ToolStripMenuItem_Context_AddWatermark_Click(object sender, EventArgs e)
        {
            try
            {
                // get selected from listview
                if (listView1.SelectedItems.Count == 0)
                {
                    toolStripStatusLabel1.Text = "select PDF file(s) please.";
                    return;
                }

                // choose a watermark text or an image with options
                Form4 wmoption = new Form4();

                // init the window from saved configuration
                {
                    Dictionary<string, string> wmdef = m_config.PdfWatermarkOption;
                    if (wmdef.ContainsKey("UseImage") && wmdef["UseImage"].ToLower() == "true" )
                    {
                        wmoption.radioButton1.Checked = false;
                        wmoption.radioButton2.Checked = true;
                    }
                    else
                    {
                        wmoption.radioButton1.Checked = true;
                        wmoption.radioButton2.Checked = false;
                    }

                    if (wmdef.ContainsKey("Text")) { wmoption.textBox1.Text = wmdef["Text"]; }
                    if (wmdef.ContainsKey("TextFont")) { wmoption.comboBox1.Text = wmdef["TextFont"]; }
                    if (wmdef.ContainsKey("TextFontSize")) { wmoption.comboBox2.Text = wmdef["TextFontSize"]; }
                    if (wmdef.ContainsKey("TextColor")) { wmoption.comboBox3.Text = wmdef["TextColor"]; }

                    if (wmdef.ContainsKey("ImageFileName")) { wmoption.textBox2.Text = wmdef["ImageFileName"]; }
                    if (wmdef.ContainsKey("ImageScalePercentage")) { wmoption.textBox4.Text = wmdef["ImageScalePercentage"]; }

                    if (wmdef.ContainsKey("Rotation")) { wmoption.textBox6.Text = wmdef["Rotation"]; }
                    if (wmdef.ContainsKey("Opacity")) { wmoption.textBox5.Text = wmdef["Opacity"]; }
                    if (wmdef.ContainsKey("Location")) { wmoption.comboBox4.Text = wmdef["Location"]; }

                    if (wmdef.ContainsKey("Pages")) { wmoption.textBox3.Text = wmdef["Pages"]; }
                }

                // upon setting OK, make a change
                //if (setting.ShowDialog(this) == DialogResult.OK)
                wmoption.ShowDialog();
                if (!wmoption.Done)
                {
                    toolStripStatusLabel1.Text = "abort adding PDF watermark.";
                    return;
                }

                // update the watermark configuration
                {
                    Dictionary<string, string> wmdef = m_config.PdfWatermarkOption;

                    wmdef["UseImage"] = wmoption.radioButton2.Checked.ToString();

                    wmdef["Text"] = wmoption.textBox1.Text;
                    wmdef["TextFont"] = (string) wmoption.comboBox1.Text;  // SelectedItem
                    wmdef["TextFontSize"] = (string) wmoption.comboBox2.Text;
                    wmdef["TextColor"] = (string) wmoption.comboBox3.Text;

                    wmdef["ImageFileName"] = wmoption.textBox2.Text;
                    wmdef["ImageScalePercentage"] = wmoption.textBox4.Text;

                    wmdef["Rotation"] = wmoption.textBox6.Text;
                    wmdef["Opacity"] = wmoption.textBox5.Text;
                    wmdef["Location"] = wmoption.comboBox4.Text;

                    wmdef["Pages"] = wmoption.textBox3.Text;
                }


                // remove password for all selected files
                DirectoryInfo nodeDirInfo = (DirectoryInfo)treeView1.SelectedNode.Tag;
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    string filename = listView1.SelectedItems[i].Text;
                    string filepath = Path.Combine(nodeDirInfo.FullName, filename);

                    FileLocker.AddWatermark(filepath, m_config.PdfWatermarkOption);
                }

                PopulateListView(treeView1.SelectedNode);

                toolStripStatusLabel1.Text = "add PDF watermark done.";
            }
            catch (Exception)
            { }
        }

        private void ToolStripMenuItem_Context_RemoveWatermark_Click(object sender, EventArgs e)
        {
            try
            {
                // get selected from listview
                if (listView1.SelectedItems.Count != 1)
                {
                    toolStripStatusLabel1.Text = "select one PDF file.";
                    return;
                }

                // remove password for all selected files
                DirectoryInfo nodeDirInfo = (DirectoryInfo)treeView1.SelectedNode.Tag;
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    string filename = listView1.SelectedItems[i].Text;
                    string filepath = Path.Combine(nodeDirInfo.FullName, filename);

                    Dictionary<string, bool> filter = FileLocker.GetWatermarks(filepath);
                    if (filter == null || filter.Count == 0)
                    {
                        toolStripStatusLabel1.Text = "no watermark found.";
                        continue;
                    }

                    // init a data grid view
                    // Note: in order to autosize datagridview, need to set its AntoSizeColumn = fill, Dock = top
                    Form3 setting = new Form3();
                    foreach (string x in filter.Keys)
                    {
                        string[] row = x.Split(',');
                        setting.dataGridView1.Rows.Add(new string[] { row[0], row[1], row[2], filter[x].ToString()});
                        // Note: in view, set checkboxcell.TrueValue = "true", FalseValue = "false"
                    }

                    // upon setting OK, make a change
                    //if (setting.ShowDialog(this) == DialogResult.OK)
                    setting.ShowDialog();
                    if (setting.Done)
                    {
                        for (int rindex = 0; rindex < setting.dataGridView1.Rows.Count; rindex++)
                        {
                            string robject = (string)setting.dataGridView1.Rows[rindex].Cells[0].Value;
                            string rtype = (string)setting.dataGridView1.Rows[rindex].Cells[1].Value;
                            string rinfo = (string)setting.dataGridView1.Rows[rindex].Cells[2].Value;
                            string k = robject + "," + rtype + "," + rinfo;
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)setting.dataGridView1.Rows[rindex].Cells[3];
                            if (chk.Value == chk.FalseValue || chk.Value == null)
                            {
                                filter[k] = false; // modify
                            }
                            else
                            {
                                filter[k] = true;
                            }
                        }

                        FileLocker.RemoveWatermark(filepath, filter);

                        toolStripStatusLabel1.Text = "remove PDF watermarks done.";
                    }
                }

                PopulateListView(treeView1.SelectedNode);
            }
            catch (Exception)
            { }
        }

        #endregion

    }
}
