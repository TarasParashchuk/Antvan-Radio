using System;

namespace Antvan_Radio
{
    [Serializable]
    public class Items_favorites
    {
        public string Icon { get; set; }
        public string Song { get; set; }
        public string Author { get; set; }
        public string Link { get; set; }

        public Items_favorites() { }

        public Items_favorites (string icon, string song, string author, string link)
        {
            Icon = icon;
            Song = song;
            Author = author;
            Link = link;
        }
    }
}
