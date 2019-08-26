
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Xml.Linq;


namespace MusicAndBooksDownloader.Model
{
    internal sealed class SitesList
    {
        private static SitesList sites;

       

        private SitesList()
        {
            
            XDocument document = XDocument.Load("");
            var root = document.Root;
        }

        public static SitesList Create()
        {
            return sites ?? new SitesList();
        }
    }
}
