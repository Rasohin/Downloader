using MusicAndBooksDownloader.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicAndBooksDownloader.Model
{
    class TimeCounter : ITimeCounter
    {
        int duration;
        int position;
        int? taskID;
        private static TimeCounter counter;

        public static TimeCounter Create()
        {
            return counter ?? new TimeCounter();
        }

        public async void Dispose()
        {
            await Task.Yield();
        }

        public void SetTimings(int dur, int count)
        {
            duration = dur;
            position = count;
        }

        public async void TimeCount(MPlayer plr1)
        {
            //await Task.Run(() =>
            //{
            //    //taskID = Task.CurrentId;
            //    while (duration > position)
            //    {
            //        SecDuration = duration - position;
            //        Position = position;
            //        position ++;
            //        Task.Delay(1000);

            //    }
            //});
        }

        public void CountStop(MPlayer plr)
        {
            Task.WaitAll();
        }
    }
}
