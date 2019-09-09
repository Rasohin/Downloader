
using MusicAndBooksDownloader.ViewModel;
using MusicAndBooksDownloader.Model;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Threading.Tasks;

namespace MusicAndBooksDownloader.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowResultsPage : ContentPage
	{
        ShowResultsViewModel myCtrl;
        bool sortButton = true;
        
        public ShowResultsPage (string request)
		{
			InitializeComponent ();
            myCtrl = new ShowResultsViewModel(request) { Navigation = this.Navigation };
            BindingContext = myCtrl;
            Fill();
        }

        protected override void OnDisappearing()
        {
            myCtrl.PlayerStandBy(true);
        }

        protected override void OnAppearing()
        {
            myCtrl.PlayerStandBy(false);
        }

        // начальное заполнение - асинхронно
        private async void Fill()
        {
            int count = 0;
            await Task.Run(() =>
            {
                bool end = myCtrl.GetEnd();
                while (!end)
                {
                    if (myCtrl.GetResult().Count > count)
                    {
                        ImageButton btn1 = new ImageButton { Source = "dl1.png", WidthRequest = 30, HeightRequest = 30, BackgroundColor = Color.Orange, Margin = 5, Command = myCtrl.dloadBtnClick, CommandParameter = count};
                        ImageButton btn2 = new ImageButton { Source = "pl1.png", WidthRequest = 30, HeightRequest = 30, BackgroundColor = Color.Orange, Margin = new Thickness(-5, 5, 0, 5), Command = myCtrl.playBtnClick, CommandParameter = count};
                        StackLayout stck = new StackLayout { Orientation = StackOrientation.Horizontal };
                        Label lbl = new Label { Text = myCtrl.GetResult()[count].Name, TextColor = Color.Black, Margin = 12 };
                        stck.Children.Add(btn1);
                        stck.Children.Add(btn2);
                        stck.Children.Add(lbl);
                        ViewCell cell = new ViewCell();
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            cell.View = stck;
                            resultTableSection.Add(cell);
                        });
                        count++;
                    }
                    end = myCtrl.GetEnd();
                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    searchLbl.Text = "Search results:";
                });
            });  
        }


        //заполняем таблицу отсортированным списком песен
        private void FillTable(List<Songs> songs, bool atoz)
        {
            SortLists sortLists = new SortLists();
            songs = sortLists.SortList(songs, atoz);
            int count = 0;
            int n = resultTableSection.Count;
            if (n > 1)
            {
                for (int i = n-1; i >= 1; i--)
                    resultTableSection.RemoveAt(i);
            }
            foreach (Songs sng in songs)
            {
                ImageButton btn1 = new ImageButton { Source = "dl1.png", WidthRequest = 30, HeightRequest = 30, BackgroundColor = Color.Orange, Margin = 5, Command = myCtrl.dloadBtnClick, CommandParameter = count };
                ImageButton btn2 = new ImageButton { Source = "pl1.png", WidthRequest = 30, HeightRequest = 30, BackgroundColor = Color.Orange, Margin = new Thickness(-5, 5, 0, 5), Command = myCtrl.playBtnClick, CommandParameter = count };
                StackLayout stck = new StackLayout { Orientation = StackOrientation.Horizontal };
                Label lbl = new Label { Text = sng.Name, TextColor = Color.Black, Margin = 12 };
                stck.Children.Add(btn1);
                stck.Children.Add(btn2);
                stck.Children.Add(lbl);
                ViewCell cell = new ViewCell();
                cell.View = stck;
                resultTableSection.Add(cell);
                count++;
            }   
        }

      
        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            myCtrl.PlayerSeek(Convert.ToInt32(mediaSlider.Value));
        }

        private void MediaPlayerButton_Clicked(object sender, System.EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            if (btn.Source.ToString() == "File: pause.png")
                btn.Source = "play.png";
            else
                btn.Source = "pause.png";
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
    }
}