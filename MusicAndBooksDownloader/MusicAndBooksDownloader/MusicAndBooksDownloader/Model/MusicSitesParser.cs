using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MusicAndBooksDownloader.Model
{
    public class MusicSitesParser
    {
        List<Songs> _result = new List<Songs>();
        private bool endOfSearch = false;
       
        public MusicSitesParser()
        {
            
        }

        public async void ParsingSite(string req)
        {
            if (req.Trim() != "")
            {
                _result.Clear();
                string finderMusic = req;
                //WebRequest request = WebRequest.Create("https://wwu.mp3-tut.online/search?query=" + finderMusic);
                //WebResponse response = request.GetResponse();
                //Stream stream = response.GetResponseStream();
                //StreamReader reader = new StreamReader(stream);
                //string search = reader.ReadToEnd();

                //if (finderMusic.Trim() == "")
                //{
                //    throw new Exception("Песни не найдено"); //добавить обработку исключения позже
                //}
                //else

                //        while (search.IndexOf("<div class=\"audio-list-entry\" data-key=\"") != -1 && _result.Count<3)
                //        {
                //            search = search.Remove(0, search.IndexOf("<div class=\"audio-list-entry\" data-key=\">") + 42);
                //            string search2 = search;
                //            search2 = search2.Remove(0, search2.IndexOf("<div class=\"track\">") + 19);
                //            search2 = search2.Remove(0, search2.IndexOf("<div class=\"title\">") + 19);
                //            string authorName;
                //            authorName = search2.Remove(0, search2.IndexOf("\">") + 2);
                //            authorName = authorName.Remove(authorName.IndexOf("</a></div>"));
                //            string musicName;
                //            musicName = search2.Remove(0, search2.IndexOf("<div class=\"title\">") + 19);
                //            musicName = musicName.Remove(musicName.IndexOf("</div>"));
                //            search2 = search2.Remove(0, search2.IndexOf("<div class=\"download-container\">") + 54);
                //            search2 = search2.Remove(search2.IndexOf("\" title"));
                //            if (!indexes.Contains(search2) && !isHave(musicName, authorName))
                //            {
                //                indexes.Add(search2);
                //                Songs song = new Songs();
                //                song.Name = $"{authorName} - {musicName}.mp3";
                //                song.playLink = "play";
                //                song.downloadLink = "download";
                //                _result.Add(song);
                //            }
                //        }
                WebRequest request = WebRequest.Create("https://zaycev.net/search.html/?query_search=" + finderMusic);
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string search = reader.ReadToEnd();
                int index = search.ToLower().IndexOf("<div class=\"musicset-track-list__items\">");
                search = search.Remove(0, index);

                await Task.Run(() => {
                    endOfSearch = false;
                    while (search.Remove(0, search.IndexOf("<a href=\"") + 9).IndexOf("\" class=") != -1)
                    {
                        try
                        {
                            string musicPage = search.Remove(0, search.IndexOf("<a href=\"") + 9);
                            musicPage = musicPage.Remove(musicPage.IndexOf("\" class="));
                            search = search.Remove(search.IndexOf("<a href=\""), 50);
                            request = WebRequest.Create("https://zaycev.net" + musicPage);
                            response = request.GetResponse();
                            stream = response.GetResponseStream();
                            reader = new StreamReader(stream);
                            string site;
                            site = reader.ReadToEnd();
                            string musicName;
                            musicName = site.Remove(0, site.IndexOf("На музыкальном портале Зайцев.нет Вы можете бесплатно скачать и слушать онлайн песню «") + 86);
                            musicName = musicName.Remove(musicName.IndexOf("»"));
                            string authorName;
                            authorName = site.Remove(0, site.IndexOf("На музыкальном портале Зайцев.нет Вы можете бесплатно скачать и слушать онлайн песню «" + musicName + "» (") + 89 + musicName.Length);
                            authorName = authorName.Remove(authorName.IndexOf(")"));
                            if (site.IndexOf("<a target=\"__blank\" data-cacheable=\"true\" class=\"button-download__link\" id=\"audiotrack-download-link--dwnl\" href=\"") != -1)
                            {
                                site = site.Remove(0, site.IndexOf("<a target=\"__blank\" data-cacheable=\"true\" class=\"button-download__link\" id=\"audiotrack-download-link--dwnl\" href=\"") + 128);
                                site = site.Remove(site.IndexOf("\"><img src=\""));
                                if (!isHave(musicName, authorName))
                                {
                                    Songs song = new Songs();
                                    song.Name = $"{authorName} - {musicName}.mp3";
                                    song.playLink = "play";
                                    song.downloadLink = "https://cdndl." + site;
                                    _result.Add(song);
                                }
                            }
                        }
                        catch { }
                    }
                    endOfSearch = true;
                    response.Close();
                    stream.Close();
                    reader.Close();
                });
            }
        }

        private bool isHave(string musicName, string authorName)
        {
            for (int i = 0; i < _result.Count; i++)
            {
                if (_result[i].Name == $"{authorName} - {musicName}.mp3")
                    return true;
            }
            return false;
        }

        //public List<Songs> GetParsingResult(string request)
        //{
        //    ParsingSite(request);
        //    return _result;
        //}

        public bool GetEnd()
        {
            return endOfSearch;
        }

        public List<Songs> GetResult()
        {
            return _result;
        }
    }
}
