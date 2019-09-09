using MusicAndBooksDownloader.View;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicAndBooksDownloader.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }
        public ICommand startMusicPage { get; protected set; }
        public ICommand startBooksPage { get; protected set; }
        public ICommand startAudioBooksPage { get; protected set; }

        public MainPageViewModel()
        {
            startMusicPage = new Command(StartMusicPage);
            startBooksPage = new Command(StartBooksPage);
            startAudioBooksPage = new Command(StartAudioBooksPage);
        }

        private async void StartMusicPage()
        {
            await Navigation.PushAsync(new MusicPage());
        }

        private async void StartBooksPage()
        {
            await Navigation.PushAsync(new BooksPage());
        }

        private async void StartAudioBooksPage()
        {
            await Navigation.PushAsync(new AudioBooksPage());
        }
    }
}
