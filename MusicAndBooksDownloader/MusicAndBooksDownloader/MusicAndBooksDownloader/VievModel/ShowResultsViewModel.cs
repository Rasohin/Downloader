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
        private List<Songs> _result = new List<Songs>();

        public ShowResultsViewModel(string request)
        {
            var result = new MusicSitesParser().GetParsingResult(request);
            _result = result;
        }

        public List<Songs> GetResult()
        {
            return _result;
        }
    }
}
