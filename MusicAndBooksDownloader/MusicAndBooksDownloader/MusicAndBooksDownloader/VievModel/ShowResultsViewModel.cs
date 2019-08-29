using MusicAndBooksDownloader.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;


namespace MusicAndBooksDownloader.ViewModel
{
    public class ShowResultsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MusicSitesParser parser = new MusicSitesParser();

        public ShowResultsViewModel(string request)
        {
            parser.ParsingSite(request);
        }

        public bool GetEnd()
        {
            bool end = parser.GetEnd();
            return end;
        }

        public List<Songs> GetResult()
        {
            var result = parser.GetResult();

            return result;
        }
    }
}
