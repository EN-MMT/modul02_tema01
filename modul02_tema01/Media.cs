using System;
using System.Xml.Serialization;

namespace modul02_tema01
{
    [Serializable]
    [XmlType("Media")]
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
        [XmlElement("Id")]
        public int Id { get; set; }
        [XmlElement("Artist")]
        public string Artist { get; set; }
        [XmlElement("Title")]
        public string Title { get; set; }
        [XmlElement("Year")]
        public int Year { get; set; }
        [XmlElement("Genre")]
        public string Genre { get; set; }
        [XmlElement("Sales")]
        public bool Sales { get; set; }

        public override string ToString() { return $"{Id},{Artist},{Title},{Year},{Genre},{Sales}"; }
    }
}
