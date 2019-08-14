
using MusicAndBooksDownloader.Controller;
using MusicAndBooksDownloader.Model;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicAndBooksDownloader.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowResultsPage : ContentPage
	{
        ShowResultsController myCtrl;
        bool sortButton = true;

		public ShowResultsPage (string request, bool sort)
		{
			InitializeComponent ();
            myCtrl = new ShowResultsController(request);
            FillTable(myCtrl.GetResult(), sort);
            if(sort)
            {
                sortButton = true;
                sortBtn.Source = "sort1.png";
            }
            else
            {
                sortBtn.Source = "sort2.png";
                sortButton = false;
            }
        }

        private void FillTable(List<Songs> songs, bool atoz)
        {
            SortLists sortLists = new SortLists();
            songs = sortLists.SortList(songs, atoz);

            int n = resultTableSection.Count;
            if (n > 1)
            {
                for (int i = n-1; i >= 1; i--)
                    resultTableSection.RemoveAt(i);
            }

            foreach (Songs sng in songs)
            {
                resultTableSection.Add(new ViewCell
                {
                    View = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new ImageButton{ Source = "dl1.png", WidthRequest=30, HeightRequest=30, BackgroundColor = Color.Orange, Margin=5},
                            new ImageButton{ Source = "pl1.png", WidthRequest=30, HeightRequest=30, BackgroundColor = Color.Orange, Margin= new Thickness(-5,5,0,5)},
                            new Label {Text=sng.Name, TextColor=Color.Black, Margin=12}
                        }
                    }
                });
            }
            
        }

        private void SortImageButton_Clicked(object sender, System.EventArgs e)
        {
            if (sortButton == true)
            {
                sortBtn.Source = "sort2.png";
                sortButton = false;
            }
            else
            {
                sortBtn.Source = "sort1.png";
                sortButton = true;
            }

            FillTable(myCtrl.GetResult(), sortButton);
        }

        private async void SettingsImageButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new DownloadSettingsPage());
        }
    }
}