using System;

namespace modul02_tema01
{
    public class Media
    {
        public Media() 
        { 
        }
        public Media(string[] data)
        {
            try
            {
                Id = Convert.ToInt32(data[0]);
            }
            catch
            {
                Console.WriteLine("[!]Bad ID.");
                return;
            }
            Artist = data[1];
            Title = data[2];
            try
            {
                Year = Convert.ToInt32(data[3]);
            }
            catch
            {
                Year = 0;
            }
            Genre = data[4];

            try
            {
                Sales = Convert.ToBoolean(data[5]);
            }
            catch
            {
                Sales = false;
            }
        }
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public bool Sales { get; set; }

        public override string ToString() { return $"{Id},{Artist},{Title},{Year},{Genre},{Sales}"; }
    }
}
