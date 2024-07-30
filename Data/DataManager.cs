using System;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace BirthdaysConsole.Data
{
    internal class DataManager
    {
        /// <summary>
        /// Имя файла, находящегося в директории с программой
        /// </summary>
        private static string _csvFileName = "data.csv";
        
        internal static List<PersonData> GetPersonsByName(string name)
        {
            List<PersonData> list = Program.DB.Where(person => person.Name == name).ToList();
            return list;
        }

        internal static List<PersonData> GetPersonsById(string ids)
        {
            IEnumerable<int> idArray = ids.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            List<PersonData> list = Program.DB.Where(person => idArray.Any(id => id == person.Id)).ToList();
            return list;
        }

        internal static List<PersonData> GetPersonsById(int id)
        {
            List<PersonData> list = Program.DB.Where(person => person.Id == id).ToList();
            return list;
        }

        internal static List<PersonData> GetTodaysPersons()
        {
            DateTime today = DateTime.Today;
            List<PersonData> list = Program.DB.Where(person => person.Date.Month == today.Month && person.Date.Day == today.Day).OrderBy(person => person.Date).ToList();
            return list;
        }

        internal static List<PersonData> GetNearestPersons()
        {
            List<PersonData> list = Program.DB.Where(person => person.Date.DayOfYear > DateTime.Today.DayOfYear
                && person.Date.DayOfYear < DateTime.Today.AddDays(Program.NearestDays).DayOfYear)
                .OrderByDescending(person => person.Date)
                .ToList();
            return list;
        }

        internal static void AddPerson(string name, DateOnly date)
        {
            PersonData newPerson = new PersonData() { Name = name, Date = date };
            Program.DB.Add(newPerson);
            SaveDataToCSV();
        }

        internal static void RemovePersons(List <PersonData> persons)
        {
            foreach (PersonData person in persons)
            {
                Program.DB.Remove(person);
            }
        }

        internal static void EditPersonName(PersonData person, string newName)
        {
            person.Name = newName;
        }

        internal static void EditPersonDate(PersonData person, DateOnly newDate)
        {
            person.Date = newDate;
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
        internal static void ReadDataFromCSV()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), _csvFileName);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("\nФайл с записями не найден.");
                Console.Write("\n--> Нажмите ENTER, чтобы продолжить без загрузки данных <--");
                Console.Read();
                return;
            } 

            using (var reader = new StreamReader(_csvFileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<PersonData>();
                Program.DB = new List<PersonData>(records);
            }
        }
    }
}
