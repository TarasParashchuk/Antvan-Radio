using System;

namespace Antvan_Radio
{
    public class Model_Song
    {
        public string Icon { get; set; }
        public string Song { get; set; }
        public string Author { get; set; }

        public Model_Song() { }

        public Model_Song(string icon, string song, string author)
        {
            Icon = icon;
            Song = song;
            Author = author;
        }
    }
}
