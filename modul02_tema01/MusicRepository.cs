using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace modul02_tema01
{
    public class MusicRepository : IRepository
    {
        string pathToOpen = @"../../../../";

        public MusicRepository(string openName)
        {
            try
            {
                var lines = File.ReadLines(pathToOpen + openName + ".csv");

                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                    string[] data = line.Split(',');
                    Media m = new Media(data);
                    cachedData.Add(m.Id, m);

                }
            }
            catch
            {
                Console.WriteLine("[!]Repo doesn't exist.");
            }
            
        }

        public Dictionary<int, Media> cachedData = new Dictionary<int, Media>();
        public void Insert(Media media)
        {
           if(!cachedData.ContainsKey(media.Id))
                cachedData.Add(media.Id,media);
        }

        public Media GetById(int id)
        {
            return cachedData[id];
        }

        public void Delete(int id)
        {
            if (cachedData.ContainsKey(id))
                cachedData.Remove(id);
        }

        public void Save(string name)
        {
            File.WriteAllText(pathToOpen+name+".csv" , "");
            foreach (var item in cachedData)
            {
                File.AppendAllText(pathToOpen + name + ".csv", item.Value.ToString()+'\n');
            }
           
        }

        public IEnumerable<Media> GetAll()
        {
            return cachedData.Values.AsEnumerable<Media>();

        }

        public IEnumerable<Media> GetByArtist(string artist)
        {
            return cachedData.Values.Where(x => x.Artist.Equals(artist));
        }

        public IEnumerable<Media> GetByYear(int year)
        {
            return cachedData.Values.Where(x => x.Year == year).Select(x => x);
        }
        public IEnumerable<Media> GetBySales(bool isOnSale)
        {
            return cachedData.Values.Where(x => x.Sales == isOnSale);
        }

        public IEnumerable<Media> GetByTitle(string title)
        {
            return cachedData.Values.Where(x => x.Title.Equals(title));
        }

        public IEnumerable<Media> GetByGenre(string genre)
        {
            return cachedData.Values.Where(x => string.Compare(genre, x.Genre)==0);
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
