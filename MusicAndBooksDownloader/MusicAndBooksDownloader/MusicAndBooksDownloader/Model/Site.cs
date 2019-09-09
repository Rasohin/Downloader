using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MusicAndBooksDownloader.Model
{
    [Serializable]
    public class Site
    {
        public string Name { get; set; }
        public string State { get; set; }
    }
}
