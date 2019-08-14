using System.Collections.Generic;

namespace MusicAndBooksDownloader.Model
{
    public class SortLists
    {
        public List<Songs> SortList(List<Songs> list, bool atoz)
        {
            if (atoz)
                list.Sort(Songs.CompareSongsNameAtoZ);
            else
                list.Sort(Songs.CompareSongsNameZtoA);

            return list;
        }
    }
}
