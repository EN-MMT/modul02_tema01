using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace modul02_tema01
{
    public class MusicRepositoryXML : IRepository
    {
        string pathToOpen = @"../../../../";

        public MusicRepositoryXML(string openName)
        {
            cachedData = new List<Media>();

            
                //var albumsDictionary = XDocument.Load(pathToOpen + openName + ".xml").Root.Elements().Select(y => y.Elements().ToDictionary(x => x.Name, x => x.Value)).ToArray();
                var serializer = new XmlSerializer(typeof(List<Media>), new XmlRootAttribute("ArrayOfMedia"));
            //var albumSerializer = new XmlSerializer(typeof(List<Media>));

               // var albums= XDocument.Load(pathToOpen + openName + ".xml").Element("ArrayOfMedia").Elements();
            /*
            foreach(var album in albums)
            {
                Media m = new Media();
                albumSerializer.Serialize(album., m);
            }
            */
            try
            {
                using (var tw = new FileStream(pathToOpen + openName + ".xml",FileMode.Open))
                {
                    cachedData = (List<Media>)serializer.Deserialize(tw );
                }
                /*
                foreach (var albumElem in albumsDictionary)
                {
                    cachedData.Add(new Media(new string[]
                    {
                    albumElem["id"],
                    albumElem["artist"],
                    albumElem["title"],
                    albumElem["year"],
                    albumElem["genre"],
                    albumElem["sales"]
                    }));
                }*/
            }

            catch(Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            
        }

        private int GetNextAvailableID()
        {
            return cachedData.Max<Media>(x => x.Id )+1;
        }

        private List<Media> cachedData;
        public void Insert(Media media)
        {
            media.Id = GetNextAvailableID();
            cachedData.Add(media);
        }

        public Media GetById(int id)
        {
            return cachedData[id];
        }

        public void Delete(int id)
        {
            cachedData.Remove(cachedData.Where(x => x.Id == id).First());
        }

        public void Save(string name)
        {

            XmlSerializer inst = new XmlSerializer(typeof(Media[]));
            TextWriter writer = new StreamWriter(pathToOpen + name + ".xml");
            inst.Serialize(writer, cachedData.ToArray());
            writer.Write(inst);
            writer.Close();
        }

        public IEnumerable<Media> GetAll()
        {
            return cachedData.AsEnumerable<Media>();

        }

        public IEnumerable<Media> GetByArtist(string artist)
        {
            return cachedData.Where(x => x.Artist == artist);
        }

        public IEnumerable<Media> GetByYear(int year)
        {
            return cachedData.Where(x => x.Year == year);
        }
        public IEnumerable<Media> GetBySales(bool isOnSale)
        {
            return cachedData.Where(x => x.Sales == isOnSale);
        }

        public IEnumerable<Media> GetByTitle(string title)
        {
            return cachedData.Where(x => x.Title == title);
        }

        public IEnumerable<Media> GetByGenre(string genre)
        {
            return cachedData.Where(x => x.Genre == genre);
        }

        public void Update(int id, string field, string value)
        {

            switch (field)
            {
                case "year": cachedData[id].Year = Convert.ToInt32(value); break;
                case "artist": cachedData[id].Artist = value; break;
                case "title": cachedData[id].Title = value; break;
                case "genre": cachedData[id].Genre = value; break;
                case "sale": cachedData[id].Sales = Convert.ToBoolean(value); break;
            }
        }
    }
}