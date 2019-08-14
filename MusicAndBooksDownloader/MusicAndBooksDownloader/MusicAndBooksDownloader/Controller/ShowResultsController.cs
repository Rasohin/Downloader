using MusicAndBooksDownloader.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAndBooksDownloader.Controller
{
    public class ShowResultsController
    {
        private List<Songs> _result = new List<Songs>();

        public ShowResultsController(string request)
        {
            var result = new MusicSitesParser().ParsingSite(request);
            _result = result;
        }

        public List<Songs> GetResult()
        {
            return _result;
        }
    }
}
