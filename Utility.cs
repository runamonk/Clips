﻿using System;
using System.IO;
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
            string randFileName = AppPath() + "\\Cache\\" + DateTime.Now.ToString("ddMMyyHHmmss")  + RandomString(10, true) + ".xml";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(fileContents);            
            doc.Save(randFileName);
            doc = null;
            return randFileName;
        }
    }
}