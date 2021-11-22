using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace modul02_tema01
{
    public class MusicRepository : IRepository
    {
        string pathToOpen = @"../../../../";
        public MusicRepository(string openName)
        {
            cachedData = new List<Media>();
            try
            {
                var lines = File.ReadLines(pathToOpen + openName + ".csv");

                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                    string[] data = line.Split(',');
                    Media m = new Media(data);
                    cachedData.Add(m);

                }
            }
            catch
            {
                Console.WriteLine("[!]Repo doesn't exist.");
            }

        }

        private int GetNextAvailableID()
        {
            return cachedData.Max<Media>(x => x.Id) + 1;
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
            File.WriteAllText(pathToOpen + name + ".csv", "");
            foreach (var item in cachedData)
            {
                File.AppendAllText(pathToOpen + name + ".csv", item.ToString() + '\n');
            }

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
