using MusicAndBooksDownloader.Interfaces;
using MusicAndBooksDownloader.Model;
using MusicAndBooksDownloader.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicAndBooksDownloader.ViewModel
{
    public class ShowResultsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }
        public ICommand optionBtnClick { get; protected set; }
        public ICommand dloadBtnClick { get; protected set; }
        public ICommand playBtnClick { get; protected set; }
        public ICommand pauseBtnClick { get; protected set; }
        private bool playerVisible { get; set; }
        private double sliderMax { get; set; }
        private double sliderValue { get; set; }
        private string trackName { get; set; }
        private int secduration { get; set; }
        private int position { get; set; }
        bool pauseBtnStatus = true;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private CancellationToken token;
        MPlayer player = MPlayer.Create();
        private MusicSitesParser parser = new MusicSitesParser();
        private bool timeCountStart = false;

        public ShowResultsViewModel(string request)
        {
            parser.ParsingSite(request);
            optionBtnClick = new Command(OptionBtnClick);
            dloadBtnClick = new Command(execute:(parametr) =>
            {
                DloadBtnClick(parametr.ToString());
            });
            playBtnClick = new Command(execute:(parametr) =>
            {
                PlayBtnClick(parametr.ToString());
            });
            pauseBtnClick = new Command(PauseBtnClick);
            playerVisible = false;
            SliderMax = 100;
        }

        #region fields
        public int SecDuration
        {
            get { return secduration; }
            set
            {
                if (secduration != value)
                {
                    secduration = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SecDuration"));
                }
            }
        }

        public int Position
        {
            get { return position; }
            set
            {
                if (position != value)
                {
                    position = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Position"));
                }
            }
        }

        public string TrackName
        {
            get { return trackName; }
            set
            {
                if (trackName != value)
                {
                    trackName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TrackName"));
                }
            }

        }

        public double SliderMax
        {
            get { return sliderMax; }
            set
            {
                if (sliderMax != value)
                {
                    sliderMax = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SliderMax"));
                }
            }
        }

        public double SliderValue
        {
            get { return sliderValue; }
            set
            {
                if (sliderValue != value)
                {
                    sliderValue = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SliderValue"));
                }
            }
        }
        
        public bool PlayerVisible
        {
            get { return playerVisible; }
            set
            {
                if (playerVisible != value)
                {
                    playerVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PlayerVisible"));
                }
            }
        }
        #endregion

        #region commands
        private async void OptionBtnClick()
        {
            await Navigation.PushAsync(new DownloadSettingsPage());
        }



        private async void DloadBtnClick(string arg)
        {
            int index = Convert.ToInt32(arg);
            
            string filename = GetResult()[index].Name;
            if (String.IsNullOrEmpty(filename)) return;
            // если файл существует
            if (await DependencyService.Get<IFileWorker>().ExistsAsync(filename))
            {
                // запрашиваем разрешение на перезапись
                //bool isRewrited = await DisplayAlert("Предупреждение", "Файл уже существует, перезаписать его?", "Да", "Нет");
                //if (isRewrited == false) return;
                return;
            }
            // перезаписываем файл
            await DependencyService.Get<IFileWorker>().SaveFileAsync(GetResult()[index]);
            //await DisplayAlert("Подтверждение", "Файл успешно сохранен!", "ОК");
        }

        private void PauseBtnClick()
        {
            if (pauseBtnStatus)
            {
                player.Pause();
                pauseBtnStatus = false;
            }
            else
            {
                player.Start();
                pauseBtnStatus = true;
            }
        }

        private void PlayBtnClick(string arg)
        {
            int index = Convert.ToInt32(arg);
            PlayerVisible = true;
            if (timeCountStart)
            {
                cts.Cancel();
                cts = new CancellationTokenSource();
                token = cts.Token;
            }
            else
            {
                token = cts.Token;
            }

            string filename = GetResult()[index].downloadLink;

            if (String.IsNullOrEmpty(filename)) return;

            StartPlayer(filename, player);

            TrackName = GetResult()[index].Name;
            SliderMax = player.Duration;
            TimeCount(token);
            timeCountStart = true;
            pauseBtnStatus = true;
        }

        #endregion

        #region player work

        private void StartPlayer(string filePath, MPlayer player1)
        {
            player1.Reset();
            player1.SetDataSource(filePath);
            player1.Prepare();
            player1.Start();
        }

        public void PlayerSeek(int arg)
        {
            if(Math.Abs(Position-arg)>200)
                player.SeekTo(arg);
        }

        public void PlayerStandBy(bool flag)
        {
            if(flag)
            {
                cts.Cancel();
                timeCountStart = false;
                player.Stop();
                player.Release();
            }
            else
            {
                player = MPlayer.Create();
            }
        }

        private async void TimeCount(CancellationToken tok)
        {
            try
            {
                await Task.Run(() =>
                {
                    while (SliderValue < player.Duration)
                    {
                        SecDuration = player.Duration - player.CurrentPosition;
                        Position = player.CurrentPosition;
                        tok.ThrowIfCancellationRequested();
                        Task.Delay(1000);
                    }
                }, tok);
            }
            catch (OperationCanceledException)
            {

            }
        }

        #endregion

        public bool GetEnd()
        {
            bool end = parser.GetEnd();
            return end;
        }

        public List<Songs> GetResult()
        {
            var result = parser.GetResult();

            return result;
        }

    }
}
