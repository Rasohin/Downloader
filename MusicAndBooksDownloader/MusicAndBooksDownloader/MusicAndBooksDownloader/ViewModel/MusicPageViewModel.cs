using MusicAndBooksDownloader.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicAndBooksDownloader.ViewModel
{
    public class MusicPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }
        public ICommand searchBtnClick { get; protected set; }
        public ICommand optionBtnClick { get; protected set; }
        private string textRequest { get; set; }

        public string TextRequest
        {
            get { return textRequest; }
            set
            {
                if (textRequest != value)
                {
                    textRequest = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TextRequest"));
                }
            }
        }

        public MusicPageViewModel()
        {
            searchBtnClick = new Command(execute: () =>
            {
                SearchBtnClick(textRequest);
            });
            optionBtnClick = new Command(OptionBtnClick);
        }

        private async void SearchBtnClick(string request)
        {
            if(!String.IsNullOrEmpty(TextRequest))
                await Navigation.PushAsync(new ShowResultsPage(request));
        }

        private async void OptionBtnClick()
        {
            await Navigation.PushAsync(new SearchOptionsPage());
        }
    }
}
