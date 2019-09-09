using Xamarin.Forms;
using MusicAndBooksDownloader.ViewModel;

namespace MusicAndBooksDownloader
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel() { Navigation = this.Navigation };
        }

    }
}
