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

    }
}
