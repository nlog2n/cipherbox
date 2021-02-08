using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;


namespace CipherBox
{
    /// <summary>
    /// Lock/unlock various password-protected files
    /// </summary>
    public interface ILocker
    {
        bool   IsMyFile(string filepath);
        bool   IsProtected(string filepath);
        bool   AddPassword(string filepath, string password);
        bool   RemovePassword(string filepath, string password);
        void   ChangePassword(string filepath, string oldPassword, string newPassword);
        bool   VerifyPassword(string filepath, string password);
        string GetEncryptionInfo(string filepath);
    }

    /// <summary>
    /// Zip/unzip file or folder
    /// </summary>
    public interface IZipper
    {
        bool Compress(string srcFileOrFolderName, string destFileOrFolderName, string password);
        bool Extract(string srcFileOrFolderName, string destFileOrFolderName, string password);
    }

    /// <summary>
    /// Stamp/unstamp PDF watermarks
    /// </summary>
    public interface IWatermarker
    {
        Dictionary<string, bool> GetWatermarks(string filename);
        bool AddWatermark(string filename, string wmtext);
        bool AddWatermark(string filename, string wmtext, string imgfilename);
        bool AddWatermark(string filename, Dictionary<string, string> wmdef);
        bool RemoveWatermark(string filename);
        bool RemoveWatermark(string filename, Dictionary<string, bool> filter);
    }
}