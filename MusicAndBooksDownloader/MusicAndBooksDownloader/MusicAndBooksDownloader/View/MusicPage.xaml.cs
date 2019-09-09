using MusicAndBooksDownloader.Model;
using MusicAndBooksDownloader.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicAndBooksDownloader.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MusicPage : ContentPage
	{
        bool sortSwitcher = true;

		public MusicPage ()
		{
			InitializeComponent ();
            BindingContext = new MusicPageViewModel() { Navigation = this.Navigation};
        }

       
        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (inputOption.IsToggled == true)
            {
                atozLable.Text = "A - z";
                sortSwitcher = true;
            }
            else
            {
                atozLable.Text = "Z - a";
                sortSwitcher = false;
            }
        }

        private void SaerchOption_Toggled(object sender, ToggledEventArgs e)
        {
            if (searchOption.IsToggled == true)
            {
                searchOptionLabel.Text = "All sites";
                searchOptionImg.IsVisible = false;
            }
            else
            {
                searchOptionLabel.Text = "Manual";
                searchOptionImg.IsVisible = true;
            }
        }
    }
}