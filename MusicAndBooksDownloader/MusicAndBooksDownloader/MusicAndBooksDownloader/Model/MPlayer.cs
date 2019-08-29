using Android.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAndBooksDownloader.Model
{
    internal sealed class MPlayer : MediaPlayer
    {
        private static MPlayer player;
       

        public static MPlayer Create()
        {
            return player ?? new MPlayer();
        }


    }
}
