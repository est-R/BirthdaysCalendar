using BirthdaysConsole.Data;
using System;
using System.Globalization;

namespace BirthdaysConsole.Menu
{
    internal class MenuMain
    {
        internal static void ShowMenu()
        {
            Templates.ResetScreen();

            PrintToday(false);
            PrintNearest(false);

            PrintMenu();

            Console.Write("->");
            if (!int.TryParse(Console.ReadLine(), out int input) || input < 1 || input > 7)
            {
                return;
            }

            switch (input)
            {
                case 1:
                    Templates.ResetScreen();
                    PrintAll();
                    break;
                case 2:
                    Templates.ResetScreen();
                    Program.CurrentMenu = MenuID.AddPerson;
                    return;
                case 3:
                    Templates.ResetScreen();
                    Program.CurrentMenu = MenuID.DeletePerson;
                    return;
                case 4:
                    Templates.ResetScreen();
                    Program.CurrentMenu = MenuID.EditPerson;
                    return;
                case 5:
                    DataManager.SaveDataToCSV();
                    break;
                case 6:
                    //DataManager.ReadDataFromCSV();
                    break;
                default:
                    Templates.ResetScreen();
                    Console.WriteLine("Ошибка. Повторите ввод");
                    return;
            }
            Console.Write("\n--> Нажмите ENTER, чтобы вернуться в меню <--");
            Console.Read();
        }

        private static void PrintMenu()
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Все дни рождения");
            Console.WriteLine("2. Добавить запись");
            Console.WriteLine("3. Удалить запись");
            Console.WriteLine("4. Редактировать запись");
            Console.WriteLine("5. Сохранить данные в CSV");
            //Console.WriteLine("6. Загрузить данные из CSV");
        }

        private static void PrintAll()
        {
            DataManager.UpdateDatabaseAsync();

            if (Program.DB.Count == 0)
            {
                Console.WriteLine("\nБаза данных пуста");
                return;
            }

            Console.WriteLine("\nВсе дни рождения: ");

            Templates.PersonsTable(Program.DB);
        }

        /// <summary>
        /// Если notice = true, то пользователь получит текст, что именинников нет. Если false, текста не будет.
        /// </summary>
        /// <param name="notice"></param>
        internal static void PrintToday(bool notice)
        {
            List<PersonData> list = DataManager.GetTodaysPersons(true);

            if ((list.Count == 0) && notice)
            {
                Console.WriteLine("\nСегодня именинников нет.");
                return;
            }

            if (list.Count > 0)
            {
                Console.WriteLine("\nСегодняшние именинники: ");

                Templates.PersonsTable(list);
            }
        }
    

        /// <summary>
    /// Если notice = true, то пользователь получит текст, что ближайших дней рождений нет. Если false, текста не будет.
    /// </summary>
    /// <param name="notice"></param>
        internal static void PrintNearest(bool notice)
    {
        List<PersonData> list = DataManager.GetNearestPersons();

        if ((list.Count == 0) && notice)
        {
            Console.WriteLine("\nБлижайших дней рождения нет.");
            Console.Write("\n--> Нажмите любую клавишу, чтобы вернуться в меню <--");
            Console.Read();
            return;
        }

        if (list.Count > 0)
        {
            Console.WriteLine("\nБлижайшие дни рождения: ");
            Templates.PersonsTable(list);
        }
    }
   
    
    }
}

