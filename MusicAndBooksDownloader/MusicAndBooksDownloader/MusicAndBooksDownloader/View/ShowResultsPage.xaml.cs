
using MusicAndBooksDownloader.ViewModel;
using MusicAndBooksDownloader.Model;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using MusicAndBooksDownloader.Interfaces;

namespace MusicAndBooksDownloader.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowResultsPage : ContentPage
	{
        ShowResultsViewModel myCtrl;
        bool sortButton = true;

		public ShowResultsPage (string request, bool sort)
		{
			InitializeComponent ();
            myCtrl = new ShowResultsViewModel(request);
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

        //заполняем таблицу песен
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
                ImageButton btn1 = new ImageButton { Source = "dl1.png", WidthRequest = 30, HeightRequest = 30, BackgroundColor = Color.Orange, Margin = 5};
                ImageButton btn2 = new ImageButton { Source = "pl1.png", WidthRequest = 30, HeightRequest = 30, BackgroundColor = Color.Orange, Margin = new Thickness(-5, 5, 0, 5) };
                StackLayout stck = new StackLayout { Orientation = StackOrientation.Horizontal };
                Label lbl = new Label { Text = sng.Name, TextColor = Color.Black, Margin = 12 };
                btn1.Clicked += DownloadButton_Clicked;
                btn2.Clicked += PlayButton_Clicked;
                stck.Children.Add(btn1);
                stck.Children.Add(btn2);
                stck.Children.Add(lbl);
                ViewCell cell = new ViewCell();
                cell.View = stck;
                resultTableSection.Add(cell);
            }
            
        }

        private async void DownloadButton_Clicked(object sender, System.EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            ViewCell cell = (ViewCell)btn.Parent.Parent;
            int index = resultTableSection.IndexOf(cell);

            string filename = myCtrl.GetResult()[index-1].Name;
            if (String.IsNullOrEmpty(filename)) return;
            // если файл существует
            if (await DependencyService.Get<IFileWorker>().ExistsAsync(filename))
            {
                // запрашиваем разрешение на перезапись
                bool isRewrited = await DisplayAlert("Подверждение", "Файл уже существует, перезаписать его?", "Да", "Нет");
                if (isRewrited == false) return;
            }
            // перезаписываем файл
            await DependencyService.Get<IFileWorker>().SaveFileAsync(myCtrl.GetResult()[index-1]);
        }

        private void PlayButton_Clicked(object sender, System.EventArgs e)
        {
            //тут будет проигрываться файл
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