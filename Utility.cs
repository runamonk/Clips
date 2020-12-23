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

    }
}