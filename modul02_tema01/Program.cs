using System;
using System.Collections.Generic;
using System.Linq;

namespace modul02_tema01
{
    class UserInterface
    {
        public bool endProgram;
        public MusicRepository currentlyOpenedRepo;
        public List<Media> returnedQuery;

        public UserInterface()
        {
            returnedQuery = new List<Media>();

            while (!endProgram)
            {
                ReadCommand();
            }
        }
        public void ReadCommand()
        {
            Console.WriteLine("Type your command:");
            Console.WriteLine(
              "Commands:" +
              "\n1]open" +
              "\n2]filter" +
              "\n3]peek" +
              "\n4]update" +
              "\n5]save" +
              "\n6]delete" +
              "\n7]insert" +
              "\n8]quit"
            );
            string givenCommand = Console.ReadLine();

            switch (givenCommand)
            {
                case "open":
                    OpenRepo();
                    break;
                case "filter":
                    InputFilters();
                    break;
                case "peek":
                    PrintAll(GetAll());
                    break;
                case "update":
                    UpdateDataRequest();
                    break;
                case "save":
                    RequestSave();
                    break;
                case "delete":
                    RequestDeletion();
                    break;
                case "insert":
                    AddNewAlbum();
                    break;
                case "quit":
                    endProgram = true;
                    break;
                default:
                    Console.WriteLine("Unrecognized command");
                    break;
            }
        }

        public void AddNewAlbum()
        {
            string[] newAlbumData = new string[6];
            Console.WriteLine("Artist: ");
            newAlbumData[1] = Console.ReadLine();
            Console.WriteLine("Title: ");
            newAlbumData[2] = Console.ReadLine();
            Console.WriteLine("Year: ");
            newAlbumData[3] = Console.ReadLine();
            Console.WriteLine("Genre: ");
            newAlbumData[4] = Console.ReadLine();
            Console.WriteLine("On sale?: ");
            newAlbumData[5] = Console.ReadLine();

            int id = 0;
            newAlbumData[0] = id.ToString();

            Media newAlbum = new Media(newAlbumData);
            currentlyOpenedRepo.Insert(newAlbum);
        }
        public void RequestDeletion()
        {
            foreach (var media in returnedQuery)
            {
                currentlyOpenedRepo.Delete(media.Id);
            }

            returnedQuery = GetAll();
        }

        public void RequestSave()
        {
            Console.WriteLine("Type out the repo name:");
            string givenName = Console.ReadLine();

            currentlyOpenedRepo.Save(givenName);

        }

        public List<Media> GetAll()
        {
            return currentlyOpenedRepo.GetAll().ToList();
        }
        public void PrintAll(List<Media> results)
        {
            foreach (var line in results)
            {
                Console.WriteLine(line.ToString());
            }
        }

        public void UpdateDataRequest()
        {
            Console.WriteLine("By what do you wish to update?\nAvailable: year, artist, title, genre, sale\n>");
            string givenField = Console.ReadLine();
            Console.WriteLine("Enter the value: ");
            string givenValue = Console.ReadLine();

            foreach (var media in returnedQuery)
            {
                currentlyOpenedRepo.Update(media.Id, givenField, givenValue);
            }
        }

        public void CallProperFilter(string filterType, string value)
        {
            returnedQuery = new List<Media>();

            switch (filterType)
            {
                case "id":
                    try
                    {
                        returnedQuery.Add(currentlyOpenedRepo.GetById(Convert.ToInt32(value)));
                    }
                    catch
                    {
                        Console.WriteLine("[!]Bad ID.");
                    }
                    break;
                case "year":
                    try
                    {
                        returnedQuery.AddRange(currentlyOpenedRepo.GetByYear(Convert.ToInt32(value)));
                    }
                    catch
                    {
                        Console.WriteLine("[!]Bad year.");
                    }
                    break;
                case "artist":
                    returnedQuery.AddRange(currentlyOpenedRepo.GetByArtist(value));
                    break;
                case "title":
                    returnedQuery.AddRange(currentlyOpenedRepo.GetByTitle(value));
                    break;
                case "genre":
                    returnedQuery.AddRange(currentlyOpenedRepo.GetByGenre(value));
                    break;
                case "sale":
                    try
                    {
                        returnedQuery.AddRange(currentlyOpenedRepo.GetBySales(Convert.ToBoolean(value)));
                    }
                    catch
                    {
                        Console.WriteLine("[!]Must be true or false.");
                    }
                    break;
            }
            PrintAll(returnedQuery);
        }

        public void InputFilters()
        {
            Console.WriteLine("By what do you wish to filter?\nAvailable: year, artist, title, genre, sale\n>");
            string givenFilterType = Console.ReadLine();
            Console.WriteLine("Enter the value: ");
            string givenFilterValue = Console.ReadLine();

            CallProperFilter(givenFilterType, givenFilterValue);
        }
        public void OpenRepo()
        {
            Console.WriteLine("Write the name of the repo to open");
            currentlyOpenedRepo = new MusicRepository(Console.ReadLine());
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            UserInterface UI = new UserInterface();
        }
    }
}