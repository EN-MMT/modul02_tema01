using System.Collections.Generic;

namespace modul02_tema01
{
    public interface IRepository
    {

        IEnumerable<Media> GetAll();
        Media GetById(int id);
        public void Insert(Media media);
        void Delete(int id);
        void Save(string name);
        public void Update(int id, string field, string value);
        public IEnumerable<Media> GetByArtist(string artist);
        public IEnumerable<Media> GetByYear(int year);
        public IEnumerable<Media> GetBySales(bool isOnSale);
        public IEnumerable<Media> GetByTitle(string title);
        public IEnumerable<Media> GetByGenre(string genre);

    }
}
