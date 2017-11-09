using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Utilities
{
    public class TagDebug
    {
        private const string DIR = "C:\\Log  ";
        public static void D(Type className, string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string TAG = className.Name;
            Debug.WriteLine(timestamp + " " + TAG + ":   " + message);
        }

        public static void W(Type className, string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd  HH-mm-ss");
            string daystamp = DateTime.Now.ToString("yyyy-MM-dd");
            string path = DIR + daystamp + ".txt";
            string content = timestamp + " " + className.Name + ":   " + message;

            if (File.Exists(path))
            {
                File.AppendAllText(path, content + Environment.NewLine);
            }
            else
            {
                File.Create(path).Dispose();
                TextWriter tw = new StreamWriter(path);
                tw.WriteLine(content);
                tw.Close();
            }
        }
    }
}
