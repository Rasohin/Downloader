using MusicAndBooksDownloader.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicAndBooksDownloader.Interfaces
{
    interface ITimeCounter: IDisposable
    {
        void TimeCount(MPlayer plr1);
    }
}
