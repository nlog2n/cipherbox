using System;
using System.IO;
using System.Collections.Generic;

using CipherBox;


namespace CipherBox
{
    /// <summary>
    /// Configurations for cipherbox, including: root directory, master password etc.
    /// </summary>
    public class CipherBoxConfig
    {
        // general configuration
        public string RootDirectory = @"./";
        public string DefaultPassword = null; 

        public List<string> UsedPasswords = new List<string>();  // cache for used passwords during program session
        public bool EnablePasswordMask = false; // for password recovery
        public int Charset = 0; 

        public bool EnableOfficeEncryption = true;
        public bool EnablePdfEncryption = true;
        public bool EnableZipEncryption = true;

        // office specific
        public bool OfficeAgileEncryption = false; // use standard encryption

        // pdf specific
        public bool PdfRemovePermissionsWhenDecrypted = true;

        // PDF watermark options
        public Dictionary<string, string> PdfWatermarkOption = new Dictionary<string, string>();

        // zip specific
        public bool ZipFolderCompression = false;

        // GUI options
        public bool DisplayFolderSize = false; // which is time consuming


        public CipherBoxConfig(string masterPwd)
        {
            RootDirectory = FindDropboxDirectory();
            if (RootDirectory == null)
            {
                RootDirectory = FindGoogleDriveDirectory();
                if (RootDirectory == null)
                {
                    RootDirectory = FindSkyDriveDirectory();
                    if (RootDirectory == null)
                    {
                        RootDirectory = FindMyDocumentDirectory();
                        if (RootDirectory == null)
                        {
                            RootDirectory = @"./"; // current folder
                        }
                    }
                }
            }

            DefaultPassword = masterPwd;
        }

        public CipherBoxConfig(string rootDir, string masterPwd)
        {
            RootDirectory = rootDir;
            DefaultPassword = masterPwd;
        }


        public void LoadFromRegistry()
        {
        }

        public void SaveToRegistry()
        {
        }

        public void LoadFromConfigFile()
        {
        }

        public void SaveToConfigFile()
        {
        }

        #region find folder location for any cloud storages (dropbox, skydrive, gdrive)
        /*
        How to find the Dropbox directory programmatically in C#
        
        You can read the the dropbox\host.db file. It's a Base64 file located in your AppData\Roaming path,
        like C:\Users\userprofile\AppData\Roaming\Dropbox
 
        Dropbox puts a file in the ApplicationData directory, called "host.db", 
        which contains the Dropbox path in the second line. 
        It's fairly simple to access that data, we just read the whole file (which is small), 
        then we put the second line in a byte array converting it, 
        and in the end we just put the string in a variable, converting in ASCII. 
        It is simple and effective, and you can easily detect if an user has Dropbox installed or not 
        by just checking the file existence in the default path. 
        Here's the code:
        */
        private string FindDropboxDirectory()
        {
            try
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string dropboxPath = System.IO.Path.Combine(appData, "Dropbox\\host.db");
                string[] lines = System.IO.File.ReadAllLines(dropboxPath);
                byte[] dropboxBase64 = Convert.FromBase64String(lines[1]);
                string folderPath = System.Text.ASCIIEncoding.ASCII.GetString(dropboxBase64);
                return folderPath;
            }
            catch (Exception)
            {
                return null;  // no dropbox installed
            }
        }



        private string FindGoogleDriveDirectory()
        {
            try
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string gdrivePath = Path.Combine(appData, "Google\\Drive\\sync_config.db");
                string text = File.ReadAllText(gdrivePath, System.Text.Encoding.ASCII);

                // The "29" refers to the end position of the keyword plus a few extra chars
                string trim = text.Substring(text.IndexOf("local_sync_root_pathvalue") + 29);

                // The "30" is the ASCII code for the record separator
                string drivePath = trim.Substring(0, trim.IndexOf(char.ConvertFromUtf32(30)));

                return drivePath;
            }
            catch (Exception)
            {
                return null; // no google drive installed
            }
        }

        // If you want to get the path to the skydrive folder you need to use the Registery value 
        // at HKEY_CURRENT_USER\Software\Microsoft\SkyDrive, with the name UserFolder. Here is the code :
        private string FindSkyDriveDirectory()
        {
            try
            {
                string SkyDriveFolder = Microsoft.Win32.Registry.GetValue(
                    "HKEY_CURRENT_USER\\Software\\Microsoft\\SkyDrive",
                    "UserFolder", null).ToString();
                return SkyDriveFolder;
            }
            catch (Exception)
            {
                return null; // no microsoft skydrive installed
            }
        }


        private string FindMyDocumentDirectory()
        {
            try
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                return appData;
            }
            catch (Exception)
            {
                return null; // no personal folder detected
            }
        }
        #endregion


    }
}