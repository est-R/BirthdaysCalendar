using BirthdaysConsole.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdaysConsole.Menu
{
    internal class Templates
    {
        internal static void PersonsTable (PersonData person)
        {
            Console.WriteLine($"ID:{Program.DB.IndexOf(person)} | {person.Name} | {person.Date} |");
        }

        internal static void PersonsTable(List<PersonData> persons)
        {
            int idWidth = persons.Max(p => Program.DB.IndexOf(p).ToString().Length) + 3;
            int nameWidth = persons.Max(p => p.Name.Length) + 2;
            int dateWidth = persons.Max(p => DateOnly.FromDateTime(p.Date).ToString().Length) + 1;

            Console.WriteLine($"{"ID".PadRight(idWidth)}| {"Имя".PadRight(nameWidth)}| {"Дата".PadRight(dateWidth)}|");

            foreach (var person in persons)
            {
                Console.WriteLine($"{Program.DB.IndexOf(person).ToString().PadRight(idWidth)}| {person.Name.PadRight(nameWidth)}| {DateOnly.FromDateTime(person.Date).ToString().PadRight(dateWidth)}|");
            }
        }

        internal static void ResetScreen()
        {
            Console.Clear();

            Console.WriteLine("\nПоздравлятор\nСегодняшняя дата: " + DateOnly.FromDateTime(DateTime.Today) + "\n");

            //if (Program.Entry)
            //{
            //    MenuMain.PrintToday(false);
            //    MenuMain.PrintNearest(false);

            //    Program.Entry = false;
            //}
        }
    }
}
