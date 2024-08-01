using System;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using BirthdaysConsoleDB.Data;
using CsvHelper;

namespace BirthdaysConsole.Data
{
    internal class DataManager
    {
        /// <summary>
        /// Имя файла, находящегося в директории с программой
        /// </summary>
        private static string _csvFileName = "data.csv";

        internal static void UpdateDatabaseAsync()
        {
            Program.DB = Program.mongoHelper.GetAsync().Result;
        }

        internal static List<PersonData> GetPersonsByName(string name)
        {
            List<PersonData> list = Program.DB.Where(person => person.Name == name).ToList();
            return list;
        }

        internal static List<PersonData> GetPersonsById(string ids)
        {
            UpdateDatabaseAsync();
            List<PersonData> list = new();
            try
            {
                IEnumerable<int> idArray = ids.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
                list = Program.DB.Where(person => idArray.Any(id => id == Program.DB.IndexOf(person))).ToList();
            }
            catch { }
            return list;
        }

        internal static List<PersonData> GetPersonById(int id)
        {
            UpdateDatabaseAsync();
            List<PersonData> list = Program.DB.Where(person => Program.DB.IndexOf(person) == id).ToList();
            return list;
        }

        internal static List<PersonData> GetTodaysPersons(bool updateDB = false)
        {
            if (updateDB) { UpdateDatabaseAsync(); }

            DateTime today = DateTime.Today;
            List<PersonData> list = Program.DB.Where(person => person.Date.Month == today.Month && person.Date.Day == today.Day).OrderBy(person => person.Date).ToList();
            return list;
        }

        internal static List<PersonData> GetNearestPersons(bool updateDB = false)
        {
            if (updateDB) { UpdateDatabaseAsync(); }

            List<PersonData> list = Program.DB.Where(person => person.Date.DayOfYear > DateTime.Today.DayOfYear 
                && person.Date.DayOfYear < DateTime.Today.AddDays(Program.NearestDays).DayOfYear)
                .OrderBy(person => person.Date.DayOfYear)
                .ToList();
            return list;
        }

        internal static void AddPerson(string name, DateTime date)
        {
            PersonData newPerson = new PersonData() { Name = name, Date = date };
            Program.mongoHelper.CreateAsync(newPerson);
            //Program.DB.Add(newPerson);
            //SaveDataToCSV();
        }

        internal async static void RemovePersons(List <PersonData> persons)
        {
            foreach (PersonData person in persons)
            {
                await Program.mongoHelper.RemoveAsync(person.BsonId);
                //Program.DB.Remove(person);
            }
        }

        internal static async Task EditPersonAsync (PersonData person, PersonData newPerson)
        {
            await Program.mongoHelper.UpdateAsync(person.BsonId, newPerson);
        }

        internal static void SaveDataToCSV()
        {
            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), _csvFileName);
            using (var writer = new StreamWriter(_csvFileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(Program.DB);
            }
        }
        //internal static void ReadDataFromCSV()
        //{
        //    string filePath = Path.Combine(Directory.GetCurrentDirectory(), _csvFileName);

        //    if (!File.Exists(filePath))
        //    {
        //        Console.WriteLine("\nФайл с записями не найден.");
        //        Console.Write("\n--> Нажмите ENTER, чтобы продолжить без загрузки данных <--");
        //        Console.Read();
        //        return;
        //    } 

        //    using (var reader = new StreamReader(_csvFileName))
        //    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        //    {
        //        var records = csv.GetRecords<PersonData>();
        //        Program.DB = new List<PersonData>(records);
        //    }
        //}
    }
}
