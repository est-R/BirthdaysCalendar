
using BirthdaysConsole.Data;

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

            List<PersonData> list = DataManager.GetPersonsById(id);
            if (list.Count == 0)
            {
                Console.WriteLine("\nНет записи с такким ID");
                return;
            }

            Console.WriteLine("\nСледующая запись будет отредактирована. Продолжить? Y/N");
            Templates.PersonsTable(list);
            string? input = Console.ReadLine();

            if (input.Equals("n", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            PersonData editedPerson = new PersonData();

            Console.Write("Введите новую дату рождения в формате ДД/ММ/ГГГГ. Оставте поле пустым, если редактирование не требуется. ");
            string? dateInput = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(dateInput))
            {
                editedPerson.Date = list.First().Date;
            }
            else
            {
                int[] dateArray = dateInput.Split('/', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                if (dateArray[0] <= 0) dateArray[0] = 1;
                if (dateArray[0] > DateOnly.MaxValue.Day) dateArray[0] = DateOnly.MaxValue.Day;
                if (dateArray[1] <= 0) dateArray[1] = 1;
                if (dateArray[1] > DateOnly.MaxValue.Month) dateArray[1] = DateOnly.MaxValue.Month;
                if (dateArray[2] <= 0) dateArray[2] = 2000;
                if (dateArray[2] > DateOnly.MaxValue.Year) dateArray[2] = DateOnly.MaxValue.Year;

                DateOnly date = new DateOnly(dateArray[2], dateArray[1], dateArray[0]);
                editedPerson.Date = date;
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
                        DataManager.EditPersonName(list.First(), editedPerson.Name);
                        DataManager.EditPersonDate(list.First(), editedPerson.Date);
                        Program.CurrentMenu = MenuID.Main;
                        return;
                    }
            }
        }
    }
}
