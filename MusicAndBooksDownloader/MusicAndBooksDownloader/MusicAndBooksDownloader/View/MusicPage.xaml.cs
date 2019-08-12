using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicAndBooksDownloader.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MusicPage : ContentPage
	{
		public MusicPage ()
		{
			InitializeComponent ();
		}

       
        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (inputOption.IsToggled == true)
                atozLable.Text = "A - z";
            else
                atozLable.Text = "Z - a";
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

        private async void SearchOptionImg_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchOptionsPage());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowResultsPage());
        }
    }
}