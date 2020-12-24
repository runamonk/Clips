using System;
using System.IO;
using System.Windows.Forms;

namespace Utility
{
    class Funcs
    {
        public static string AppPath(string FileName)
        {
            return Path.GetDirectoryName(Application.ExecutablePath) + "\\" + FileName;
        }

        public static string AppPath()
        {
            return Path.GetDirectoryName(Application.ExecutablePath);
        }

        public static string [] GetFiles(string path, string searchPattern)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string[] files;

            if (searchPattern != "")
                files = Directory.GetFiles(path, searchPattern);
            else
                files = Directory.GetFiles(path);

            return files;
        }
    }
}