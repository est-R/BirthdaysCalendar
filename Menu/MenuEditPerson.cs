
using BirthdaysConsole.Data;
using MongoDB.Bson.IO;

namespace BirthdaysConsole.Menu
{
    internal class MenuEditPerson
    {
        internal static void ShowMenu()
        {
            Console.WriteLine("\nВведите ID записи: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Program.CurrentMenu = MenuID.Main;
                Console.Write("\nID не задан или не является числом");
                Console.Write("\n--> Нажмите ENTER, чтобы вернуться в меню <--");
                Console.Read();
                return;
            }

            List<PersonData> list = DataManager.GetPersonById(id);
            if (list.Count == 0)
            {
                Console.WriteLine("\nНет записи с таким ID");
                return;
            }

            Console.WriteLine("\nСледующая запись будет отредактирована. Продолжить? Y/N");
            Templates.PersonsTable(list);
            string? input = Console.ReadLine();

            if (input.Equals("n", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            PersonData editedPerson = new PersonData
            {
                BsonId = list.First().BsonId
            };

            Console.Write("Введите новую дату рождения в формате ДД/ММ/ГГГГ. Оставте поле пустым, если редактирование не требуется. ");
            string? dateInput = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(dateInput))
            {
                editedPerson.Date = list.First().Date;
            }
            else
            {
                try
                {
                    int[] dateArray = dateInput.Split('/', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                    DateTime date = new DateTime(dateArray[2], dateArray[1], dateArray[0]);
                    editedPerson.Date = date;
                }
                catch { 
                    Console.WriteLine("\nНеправильный формат даты."); 
                    return; 
                }
            }

            string newName = "";
            if (input.Equals("y", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("\nВведите новое имя. Оставте поле пустым, если редактирование не требуется. ");
                newName = Console.ReadLine();
            }


            if (String.IsNullOrWhiteSpace(newName))
            {
                editedPerson.Name = list.First().Name;
            }
            else
            {
                editedPerson.Name = newName;
            }

            Console.Write($"\nВсё верно?\n | {editedPerson.Name} | {editedPerson.Date} | Y/N/Меню: ");
            input = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(input)) input = "Y";
            switch (input.ToLower())
            {
                case "меню":
                    {
                        Program.CurrentMenu = MenuID.Main;
                        return;
                    }
                case "n":
                    return;
                case "y":
                    {
                        DataManager.EditPersonAsync(list.First(), editedPerson);
                        Program.CurrentMenu = MenuID.Main;
                        return;
                    }
            }
        }
    }
}
