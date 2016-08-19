using System;
using System.IO;

namespace StarboundSaveBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = @"C:\Program Files (x86)\Steam\steamapps\common\Starbound\Storage";
            DateTime dt = DateTime.Now;
            string pattern = @"yyyy-MM-dd-HHmm";
            string destination = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Desktop\Starbound" + dt.ToString(pattern));
            Console.WriteLine("Now copying:");
            Console.Title = @"Creating Starbound save backup in Desktop\Starbound" + dt.ToString(pattern);
            CopyDirectory(source, destination);
            Console.WriteLine("\nBackup done. Press any key to exit.");
            Console.ReadKey();
        }
        private static void CopyDirectory(string source, string destination)
        {
            Directory.CreateDirectory(destination);
            DirectoryInfo dir = new DirectoryInfo(source);
            if (!dir.Exists)
            {
                Console.WriteLine("No Starbound storage folder found. This program support only default Steam/game installation path.\nPress any key to close.");
                Console.ReadKey();
            }
            else
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temp = Path.Combine(destination, file.Name);
                    file.CopyTo(temp, true);
                    Console.WriteLine(file.Name);
                }
                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destination, subdir.Name);
                    CopyDirectory(subdir.FullName, temppath);
                }
            }
        }
    }
}