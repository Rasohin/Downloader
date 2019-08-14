using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAndBooksDownloader.Model
{
    public class MusicSitesParser
    {
        List<Songs> _result = new List<Songs>();
       

        public MusicSitesParser()
        {

        }

        public List<Songs> ParsingSite(string request)
        {

            // тут должна быть логика парсера, пока для теста:
            Songs song1 = new Songs();
            song1.Name = "Picnic - Vertolet.mp3";
            song1.playLink = "play";
            song1.downloadLink = "download";

            Songs song2 = new Songs();
            song2.Name = "Underwood - Epoch.mp3";
            song2.playLink = "play";
            song2.downloadLink = "download";

            Songs song3 = new Songs();
            song3.Name = "Queen - We are the champions.mp3";
            song3.playLink = "play";
            song3.downloadLink = "download";

            _result.Add(song1);
            _result.Add(song2);
            _result.Add(song3);

            return _result;
        }
    }
}
