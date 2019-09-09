using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace MusicAndBooksDownloader.Model
{
    internal sealed class SeriolizedSiteList
    {
        private static SeriolizedSiteList siteslist;
        List<Site> sites;

        public SeriolizedSiteList()
        {
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "current.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(SitesList));
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    var root = (SitesList)serializer.Deserialize(fs);
                    sites = root == null ? new List<Site>() : root.sites;
                }

            }
            catch
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(SitesList)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("MusicAndBooksDownloader.Model.Sites.xml");

                using (var reader = new StreamReader(stream))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SitesList));
                    var root = (SitesList)serializer.Deserialize(reader);
                    sites = root == null ? new List<Site>() : root.sites;
                }
            }
            
            
        }


        public List<Site> GetSitesList()
        {
            return sites;
        }

        public static SeriolizedSiteList Create()
        {
            return siteslist ?? new SeriolizedSiteList();
        }
    }
}
