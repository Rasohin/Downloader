using MusicAndBooksDownloader.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace MusicAndBooksDownloader.ViewModel
{
    public class SearchOptionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Site> sites { get; set; }
        public List<Site> activeSites { get; set; }
        public INavigation Navigation { get; set; }

        public SearchOptionsViewModel()
        {
            SeriolizedSiteList sl = SeriolizedSiteList.Create();
            sites = sl.GetSitesList();
            activeSites = sites;
        }

        //public void SeriolizeSitesList()
        //{
        //    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "current.xml");

        //    XmlSerializer serializer = new XmlSerializer(typeof(SitesList));
        //    using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        //    {
        //        serializer.Serialize(fs, activeSites);
        //    }
        //}
    }
}
