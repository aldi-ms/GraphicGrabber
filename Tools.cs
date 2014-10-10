using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;


namespace GraphicGrabber
{
    public static class Tools
    {
        private static string SavePath
        {
            get { return GraphicGrabberMain.SavePath; }
        }

        public static void DownloadURL(params string[] URLs)
        {
            Console.WriteLine("downloading files to {0}. . .", SavePath);
            using (var client = new WebClient())
            {
                for (int i = 0; i < URLs.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        string fileName = GetFileName(URLs[i]);
                        client.DownloadFile(URLs[i], SavePath + fileName);
                        Console.WriteLine("dl: {0}", URLs[i]);
                    }
                }
            }
        }
        
        /// <summary>
        /// Return links from a page, filtered by extension
        /// (only links with extensions met in string[] ext are returned).
        /// </summary>
        /// <param name="url">The URL from the w3 from which we are going to extract links.</param>
        /// <param name="ext">Extensions to filter the links by - eg: ".jpg" return all links ending in ".jpg"</param>
        /// <returns>An array of strings, containg all links which are returned by the filter.</returns>
        public static string[] GetSiteURLs(string url, params string[] ext)
        {
            Console.WriteLine("getting urls from web page. . .");

            string source = GetSiteString(url);
            bool insideBody = false;
            List<string> resultURLs = new List<string>();

            for (int i = 0; i < source.Length; i++)
            {
                if (i > 1 && i < source.Length - 4)
                {
                    if (insideBody)
                    {
                        if (source[i] == 'h' && source[i + 1] == 'r' && source[i + 2] == 'e' && source[i + 3] == 'f')
                        {
                            for (int j = i + 4; j < i + 10; j++)         //<-- in 10 symbols the " or ' char meaning an opening of a ling (href="..") has to be met
                            {
                                if (source[j] == '\"' || source[j] == '\'')
                                {
                                    var getURL = new StringBuilder();
                                    j++;
                                    char ch = source[j];
                                    do
                                    {
                                        getURL.Append(ch);
                                        j++;
                                        ch = source[j];
                                    } while (ch != '\"' && ch != '\'');

                                    resultURLs.Add(getURL.ToString());
                                    break;
                                }
                            }
                            i += 3;
                        }
                    }
                    else if (source[i] == 'b' && source[i - 1] == '<' &&
                            source[i + 1] == 'o' && source[i + 2] == 'd')
                    {
                        insideBody = !insideBody;
                        i += 2;
                    }
                }
            }

            if (ext != null)
                return CreateWorkingURLs(FilterResults(resultURLs, ext).ToArray());
            else
                return CreateWorkingURLs(resultURLs.ToArray());
        }

        private static List<string> FilterResults(List<string> resultURLs, params string[] ext)
        {
            Console.WriteLine("filter results by file extension. . .");
            List<string> filteredResults = new List<string>();
            foreach (string url in resultURLs)
            {
                char ch = url[url.Length - 1];
                if (ch == '\\') continue;

                if (url.Length > 5)
                    for (int iExt = 0; iExt < ext.Length; iExt++)
                    {
                        string urlExt = new string(url.ToCharArray(), url.Length - ext[iExt].Length, ext[iExt].Length).ToLowerInvariant();
                        if (urlExt.CompareTo(ext[iExt]) == 0)
                            filteredResults.Add(url);
                    }
            }

            return filteredResults;
        }

        private static string[] CreateWorkingURLs(params string[] URLs)
        {
            Console.WriteLine("touching the download urls. . .");
            for (int i = 0; i < URLs.Length; i++)
            {
                if (URLs[i][0] == '/')
                    URLs[i] = "http:" + URLs[i];
            }

            /*CHECK FOR REPEATING URLs*/

            return URLs;
        }

        private static string GetSiteString(string url)
        {
            Console.WriteLine("getting the web page html. . .");
            using (WebClient client = new WebClient())
                return client.DownloadString(url);
        }

        private static string GetFileName(string url)
        {
            int start = 0;
            for (int i = url.Length - 1; i >= 0; i--)
            {
                if (url[i] == '/')
                {
                    start = i + 1;
                    break;
                }
            }

            return
                new string(url.ToCharArray(), start, url.Length - start);
        }
    }
}
