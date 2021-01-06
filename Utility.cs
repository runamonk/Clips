using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

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

        public static string[] GetFiles(string path, string searchPattern)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string[] files;

            if (searchPattern != "")
                files = Directory.GetFiles(path, searchPattern).OrderBy(f => new FileInfo(f).CreationTime).ToArray();
            else
                files = Directory.GetFiles(path).OrderBy(f => new FileInfo(f).CreationTime).ToArray();

            return files;
        }

        public static string GetName()
        {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }

        public static string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public static Boolean IsSame(Image img1, Image img2)
        {
            if ((img1 == null) || (img2 == null))
                return false;

            string s1, s2;

            MemoryStream ms = new MemoryStream();
            img1.Save(ms, ImageFormat.Png);
            s1 = Convert.ToBase64String(ms.ToArray());
            ms = null;
            MemoryStream ms2 = new MemoryStream();
            img2.Save(ms2, ImageFormat.Png);
            s2 = Convert.ToBase64String(ms2.ToArray());
            ms2 = null;

            return (s1 == s2);
        }

        public static Boolean IsUrl(string s)
        {
            return (s.Length <= 2048) && s.ToLower().StartsWith("www") || s.ToLower().StartsWith("http") && Uri.IsWellFormedUriString(s, UriKind.RelativeOrAbsolute);
        }

        public static void MoveFormToCursor(Form form, bool IgnoreBounds = false)
        {
            Point p = new Point(Cursor.Position.X + 10, Cursor.Position.Y - 10);
            
            if (!IgnoreBounds)
            {
                //Height
                if ((p.Y + form.Size.Width) > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    p.Y = (p.Y - form.Size.Height);
                }

                //Width
                if ((p.X + form.Size.Width) > Screen.PrimaryScreen.WorkingArea.Width)
                {
                    p.X = (p.X - form.Size.Width);
                }
            }

            form.Location = p;
        }

        public static string RandomString(int size, bool lowerCase)
        {
            const string src = "abcdefghijklmnopqrstuvwxyz0123456789";
            var sb = new StringBuilder();
            Random RNG = new Random();
            for (var i = 0; i < size; i++)
            {
                var c = src[RNG.Next(0, src.Length)];
                sb.Append(c);
            }
            if (lowerCase)
                return sb.ToString().ToLower();
            else
                return sb.ToString();
        }

        public static string SaveToCache(string fileContents)
        {
            string randFileName = AppPath() + "\\Cache\\" + DateTime.Now.ToString("yyyymmddhhmmssfff")  + RandomString(10, true) + ".xml";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(fileContents);            
            doc.Save(randFileName);
            doc = null;
            return randFileName;
        }
    }
}