using System;

namespace MusicAndBooksDownloader.Model
{
    public class Songs : IComparable
    {
        public string Name { get; set; }
        public string playLink { get; set; }
        public string downloadLink { get; set; }

        public int CompareTo(object obj)
        {
            return 1;
        }

        static public int CompareSongsNameAtoZ(Songs s1, Songs s2)
        {
            return string.Compare(s1.Name, s2.Name, true);
        }

        static public int CompareSongsNameZtoA(Songs s1, Songs s2)
        {
            return -string.Compare(s1.Name, s2.Name, true);
        }
    }


}
