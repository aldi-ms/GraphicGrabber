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
            Console.WriteLine("Image Downloader\na.k.a.\ndownload all images from a web page\n-WIP-alpha-");
            Console.WriteLine("\n< N > to download images from url.");
            Console.WriteLine("< R > to change default save folder.");
            Console.WriteLine("< X > to add/remove filter externsions.");
            Console.WriteLine("< M > to clear screen and show this menu.");
            Console.WriteLine("< esc > to quit.");
        }
    }
}
