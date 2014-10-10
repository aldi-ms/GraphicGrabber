using System;


namespace GraphicGrabber
{
    public static class VisualTools
    {
        public static void ResizeConsole(int width, int height)
        {
            Console.WindowWidth = width;
            Console.WindowHeight = height;
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;
        }

        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Image Downloader\na.k.a.\ndownload all images from a web page\n-WIP-");
            Console.WriteLine("\n< N > to download images from url.");
            Console.WriteLine("< R > to change default save folder.");
            Console.WriteLine("< X > to add/remove filter extensions (*.*).");
            Console.WriteLine("< M > to clear screen and show this menu.");
            Console.WriteLine("< esc > to quit.");
        }
        public static void ExtensionMenu()
        {
            Console.WriteLine("extension menu");
            Console.WriteLine("\n< A > to add extension.");
            Console.WriteLine("< D > to remove extension.");
            Console.WriteLine("< Z > to clear all extesions.");
            Console.WriteLine("< esc > to go back.");
        }
    }
}
