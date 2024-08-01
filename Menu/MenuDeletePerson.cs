
using BirthdaysConsole.Data;

namespace BirthdaysConsole.Menu
{
    internal class MenuDeletePerson
    {
        internal static void ShowMenu()
        {
            Console.WriteLine("Введите ID записей через запятую: ");
            Console.Write("->");
            string? ids = Console.ReadLine();
            if (String.IsNullOrEmpty(ids))
            {
                Program.CurrentMenu = MenuID.Main;
                Console.Write("\nID не заданы");
                Console.Write("\n--> Нажмите ENTER, чтобы вернуться в меню <--");
                Console.Read();
                return;
            }

            List<PersonData> list = DataManager.GetPersonsById(ids);
            if (list.Count() == 0)
            {
                Console.WriteLine("Нет людей с такими ID. Нажмите ENTER, чтобы повторить.");
                Console.Read();
                return;
            }

            Console.WriteLine("\nСледующие записи будут удалены. Продолжить? Y/N");
            Templates.PersonsTable(list);
            Console.Write("\n->");
            string? input = Console.ReadLine();

            if (input.Equals("y", StringComparison.CurrentCultureIgnoreCase))
            {
                DataManager.RemovePersons(list);
                Program.CurrentMenu = MenuID.Main;
                return;
            }

            if (input.Equals("n", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }
        }
    }
}
