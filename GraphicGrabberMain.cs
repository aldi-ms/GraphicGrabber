using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;

namespace GraphicGrabber
{
    public static class GraphicGrabberMain
    {
        private static string savePath = @"..\..\grabbed\";
        private static List<string> extensions = new List<string> { ".jpg", ".png", ".gif", ".webm" };

        public static string SavePath
        {
            get { return savePath; }
        }

        public static List<string> Extensions
        {
            get { return extensions; }
            set { extensions = value; }
        }

        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-GB");
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Image Downloader -WIP-";
            VisualTools.ResizeConsole(100, 30);
            VisualTools.MainMenu();
            Loop();
        }

        public static void Loop()
        {
            bool loop = true;
            while (loop)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.N:;
                            Console.Write("\ngrab new url: ");
                            Tools.DownloadURL(Tools.GetSiteURLs(Console.ReadLine(), extensions.ToArray()));;
                            Console.WriteLine("finished");
                            break;
                        case ConsoleKey.R:
                            Console.WriteLine("\nchange save folder.\ncurrent save folder: \"{0}\"", savePath);
                            Console.Write("input new folder path (should exist), or ~ for default: ");
                            savePath = Console.ReadLine();
                            if (savePath.CompareTo("~") == 0)
                            {
                                savePath = @"..\..\grabbed\";
                                Console.WriteLine("path set to default.");
                            }
                            else Console.WriteLine("path edited.");
                            break;
                        case ConsoleKey.X:
                            VisualTools.ExtensionMenu();
                            //bool innerLoop = true;
                            //while (innerLoop)
                            //{

                            //}
                            break;
                        case ConsoleKey.M:
                            VisualTools.MainMenu();
                            break;
                        case ConsoleKey.Escape:
                            loop = false;
                            break;
                    }
                }
            }
        }
    }
}
