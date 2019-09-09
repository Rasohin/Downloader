using System.Collections.Generic;
using System.Xml.Serialization;

namespace MusicAndBooksDownloader.Model
{
    [XmlRoot("SitesList")]
    public class SitesList
    {
        
        public SitesList() { this.sites = new List<Site>(); }

        [XmlElement("Site")]
        public List<Site> sites { get; set; }
    }
}
