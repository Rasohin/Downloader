using MusicAndBooksDownloader.Model;
using MusicAndBooksDownloader.ViewModel;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicAndBooksDownloader.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchOptionsPage : ContentPage
	{
        SearchOptionsViewModel myVM;
        public SearchOptionsPage ()
		{
			InitializeComponent ();

            myVM = new SearchOptionsViewModel() { Navigation = this.Navigation };
            BindingContext = myVM;
            Fill(myVM.activeSites);

            //SeriolizedSiteList sl = SeriolizedSiteList.Create();
        }

        private void Fill(List<Site> list)
        {
            foreach(Site site in list)
            {
                SwitchCell cell = new SwitchCell();
                cell.Text = site.Name;
                if(site.State == "on")
                    cell.On = true;
                else
                    cell.On = false;
                Device.BeginInvokeOnMainThread(() =>
                {
                    parsingSites.Add(cell);
                });
            }
            
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            int count = 0;
            foreach(SwitchCell cell in parsingSites)
            {
                if(cell.On == true)
                {
                    myVM.activeSites[count].State = "on";
                }
                else
                {
                    myVM.activeSites[count].State = "off";
                }
                count++;
            }
            //myVM.SeriolizeSitesList();
            await Navigation.PopAsync();
        }
    }
}