using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Antvan_Radio
{
    public class SaveOpenFavorites
    {
        Model_Song Items;

        string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "f1avori1tes.xml");

        public SaveOpenFavorites() { }

        public SaveOpenFavorites( Model_Song items)
        {
            Items = items;
        }

        public void Save_Favorites()
        {
            XDocument xmlDoc;

            if (!File.Exists(filename))
                xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Items"));
            else xmlDoc = XDocument.Load(filename);

            xmlDoc.Element("Items").Add(new XElement("Items_favorite",
                new XElement("Icon", Items.Icon),
                new XElement("Song", Items.Song),
                new XElement("Author", Items.Author)
                ));

            xmlDoc.Save(filename);
        }

        public void Remove_Favorites(string name_song)
        {
            var xmlDoc = XDocument.Load(filename);

            xmlDoc.Element("Items").Elements("Items_favorite").Where(x => x.Element("Song").Value == name_song).Remove();
            xmlDoc.Save(filename);
        }

        public bool Find_Favorites(string name_song) 
        {
            bool flag = false;
            if (File.Exists(filename))
            {
                var xmlDoc = XDocument.Load(filename);
                var count = xmlDoc.Element("Items").Elements("Items_favorite").Where(x => x.Element("Song").Value == name_song).Count();
                if (count != 0)
                {
                    flag = true;
                }
                else flag = false;
            }

            return flag;
        }

        public IEnumerable<Model_Song> Open_Favorites()
        {
            if (File.Exists(filename))
            {
                var all_items = from Item in XDocument.Load(filename).Descendants("Items_favorite")
                                select new Model_Song { Icon = Item.Element("Icon").Value, Song = Item.Element("Song").Value, Author = Item.Element("Author").Value };
                return all_items;
            }
            else return null;

        }
    }
}
