using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


using CipherBox.Office;
using CipherBox.Zip;
using CipherBox.Pdf;

namespace CipherBox
{
    // generic file helper for office, pdf, and zip files
    public static class FileLocker
    {
        public static bool IsMyFile(string filepath)
        {
            return (OfficeHelper.IsMyFile(filepath) || PDFHelper.IsMyFile(filepath) || ZipHelper.IsMyFile(filepath));
        }

        public static string GetFileType(string filepath)
        {
            FileInfo fi = new FileInfo(filepath);

            string ftype = fi.Extension.ToUpper().TrimStart('.');
            if (ftype == "DOC" || ftype == "DOCX" ) return "Word";
            if (ftype == "PPT" || ftype == "PPTX" ) return "Slides";
            if (ftype == "XLS" || ftype == "XLSX" || ftype == "XLSB" ) return "Excel";
            if (ftype == "PDF" ) return "PDF";
            if (ftype == "ZIP") return "ZIP";

            return "File";
        }
        
        #region encryption part

        public static bool IsProtected(string filepath)
        {
            try
            {
                FileInfo fi = new FileInfo(filepath);
                if ((fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                    return false;

                if (OfficeHelper.IsMyFile(filepath) && OfficeHelper.IsProtected(filepath))
                {
                    return true;
                }
                else if (PDFHelper.IsMyFile(filepath) && PDFHelper.IsEncrypted(filepath))
                {
                    return true;
                }
                else if (ZipHelper.IsMyFile(filepath) && ZipHelper.IsEncrypted(filepath))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool Lock(string filepath, string password)
        {
            try
            {
                if (OfficeHelper.IsMyFile(filepath))
                {
                    OfficeHelper.AddPassword(filepath, password);
                }
                else if (PDFHelper.IsMyFile(filepath))
                {
                    PDFHelper.AddPassword(filepath, password);
                }
                else if (ZipHelper.IsMyFile(filepath))
                {
                    ZipHelper.AddPassword(filepath, password);
                }
                else
                {
                    return false;
                }
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        public static bool Unlock(string filepath, string password)
        {
            try
            {
                if (OfficeHelper.IsMyFile(filepath))
                {
                    OfficeHelper.RemovePassword(filepath, password);
                }
                else if (PDFHelper.IsMyFile(filepath))
                {
                    PDFHelper.RemovePassword(filepath, password);
                }
                else if (ZipHelper.IsMyFile(filepath))
                {
                    ZipHelper.RemovePassword(filepath, password);
                }
                else
                {
                    return false;
                }
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        public static string GetEncryptionInfo(string filepath)
        {
            string text = ""; // filepath;
            try
            {
                if (OfficeHelper.IsMyFile(filepath))
                {
                    text += OfficeHelper.GetEncryptionInfo(filepath);
                }
                else if (PDFHelper.IsMyFile(filepath))
                {
                    text += PDFHelper.GetEncryptionInfo(filepath);
                }
                else if (ZipHelper.IsMyFile(filepath))
                {
                    text += ZipHelper.GetEncryptionInfo(filepath);
                }
                else
                {
                    text += "\nN/A";
                }
            }
            catch(Exception)
            {
                text += "\nN/A";
            }

            return text;
        }

        public static bool VerifyPassword(string filepath, string password)
        {
            try
            {
                if (OfficeHelper.IsMyFile(filepath))
                {
                    return OfficeHelper.VerifyPassword(filepath, password);
                }
                else if (PDFHelper.IsMyFile(filepath))
                {
                    return PDFHelper.VerifyPassword(filepath, password);
                }
                else if (ZipHelper.IsMyFile(filepath))
                {
                    return ZipHelper.VerifyPassword(filepath, password);
                }
                else
                {
                    return false;
                }
            }
            catch(Exception)
            {
                return false;
            }
        }
        
        #endregion

        #region zip and unzip

        public static bool Compress(string srcFileOrFolderName, string destFileOrFolderName, string password)
        {
            return ZipHelper.Compress(srcFileOrFolderName, destFileOrFolderName, password);
        }

        public static bool Extract(string srcFileOrFolderName, string destFileOrFolderName, string password)
        {
            return ZipHelper.Extract(srcFileOrFolderName, destFileOrFolderName, password);
        }
        #endregion

        #region Pdf specific operations

        public static bool AddWatermark(string filename, string wmtext)
        {
            if (! PDFHelper.IsMyFile(filename)) return false;

            PDFHelper.AddWaterMark(filename, wmtext, null);
            return true;
        }


        public static bool AddWatermark(string filename, string wmtext, string imgfilename)
        {
            if (!PDFHelper.IsMyFile(filename)) return false;

            PDFHelper.AddWaterMark(filename, wmtext, imgfilename);
            return true;
        }


        public static bool AddWatermark(string filename, Dictionary<string, string> wmdef)
        {
            if (!PDFHelper.IsMyFile(filename)) return false;

            PdfWaterMarkOption wm = new PdfWaterMarkOption();
            {
                if (wmdef.ContainsKey("Text")) { wm.Text = (string)wmdef["Text"]; }
                if (wmdef.ContainsKey("TextFont")) { wm.TextFont = (string)wmdef["TextFont"]; }
                if (wmdef.ContainsKey("TextFontSize"))
                {
                    float fs; if (float.TryParse(wmdef["TextFontSize"], out fs)) { wm.TextFontSize = Math.Max(0, fs); }
                }
                if (wmdef.ContainsKey("TextColor")) { wm.TextColor = (string)wmdef["TextColor"]; }

                if (wmdef.ContainsKey("ImageFileName")) { wm.ImageFileName = (string)wmdef["ImageFileName"]; }
                if (wmdef.ContainsKey("ImageScalePercentage"))
                {
                    float sp; if (float.TryParse(wmdef["ImageScalePercentage"].TrimEnd('%'), out sp)) { wm.ImageScalePercentage =Math.Min(Math.Max(0,sp), 100); }
                }

                // UseImage
                if (wmdef.ContainsKey("UseImage"))
                {
                    bool UseImage = (wmdef["UseImage"].ToLower() == "true");
                    if (UseImage)
                    {
                        wm.Text = null;
                    }
                    else
                    {
                        wm.ImageFileName = null;
                    }
                }

                if (wmdef.ContainsKey("Rotation")) 
                {
                    float rt; if (float.TryParse(wmdef["Rotation"], out rt)) { wm.Rotation = Math.Min(Math.Max(0, rt), 360); }
                }
                if (wmdef.ContainsKey("Opacity"))
                {
                    float op; if (float.TryParse(wmdef["Opacity"].TrimEnd('%'), out op)) { wm.Opacity = Math.Min(Math.Max(0,op), 100) / 100; }
                }
                if (wmdef.ContainsKey("Location")) { wm.Location = wmdef["Location"]; }

                if (wmdef.ContainsKey("Pages"))
                {
                    string p = wmdef["Pages"];
                    if (p.ToUpper() != "ALL")
                    {
                        string[] pp = p.Split('-');
                        if (pp.Length == 2)
                        {
                            int p1, p2;
                            if (int.TryParse(pp[0], out p1) && int.TryParse(pp[1], out p2))
                            {
                                wm.PageStart = Math.Max(1,p1);
                                wm.PageEnd = Math.Max(wm.PageStart,p2);
                            }
                        }
                        else if (pp.Length == 1)
                        {
                            int p1;
                            if (int.TryParse(pp[0], out p1))
                            {
                                wm.PageStart = Math.Max(1,p1);
                                wm.PageEnd = wm.PageStart;
                            }
                        }
                    }
                }
            }

            PDFHelper.AddWaterMark(filename, wm);
            return true;
        }


        public static Dictionary<string, bool> GetWatermarks(string filename)
        {
            if (!PDFHelper.IsMyFile(filename)) return new Dictionary<string,bool>();

            return PDFHelper.GetWaterMarks(filename);
        }

        /// <summary>
        /// remove all PDF watermarks by default
        /// </summary>
        /// <param name="filename">PDF file name</param>
        /// <returns></returns>
        public static bool RemoveWatermark(string filename)
        {
            if (!PDFHelper.IsMyFile(filename)) return false;

            PDFHelper.RemoveWaterMarks(filename);
            return true;
        }

        public static bool RemoveWatermark(string filename, Dictionary<string, bool> filter)
        {
            if (!PDFHelper.IsMyFile(filename)) return false;

            PDFHelper.RemoveWaterMarks(filename,filter);
            return true;
        }

        #endregion

    }
}
