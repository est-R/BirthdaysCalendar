using BirthdaysConsole.Data;

namespace BirthdaysConsole.Menu
{
    internal class MenuAddPerson
    {
        internal static void ShowMenu()
        {
            Console.WriteLine("Имя: ");
            Console.Write("->");
            string? name = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(name)) 
            {
                Console.WriteLine("Имя не задано. Повторить ввод? Y/N: ");

                string? input = Console.ReadLine();
                if (input.Equals("n", StringComparison.CurrentCultureIgnoreCase))
                {
                    Program.CurrentMenu = MenuID.Main;
                    return;
                }

                if (input.Equals("y", StringComparison.CurrentCultureIgnoreCase))
                {
                    return;
                }

                return;
            }


            while (true)
            {
                try
                {
                    Console.Write("Дата рождения в формате ДД/ММ/ГГГГ: ");
                    string? dateInput = Console.ReadLine();
                    int[] dateArray = dateInput.Split('/', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                    DateTime date = new DateTime(dateArray[2], dateArray[1], dateArray[0]);

                    Console.WriteLine($"\nВсё верно?\n | {name} | {date} | Y/N/Меню: ");

                    Console.Write("->");
                    string? input = Console.ReadLine();
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
                            DataManager.AddPerson(name, date);
                            Program.CurrentMenu = MenuID.Main;
                            return;
                            }
                    }
                }
                catch
                {
                    Console.WriteLine("Неправильная дата. Попробовать снова? y/n");
                    Console.Write("->");
                    string? input = Console.ReadLine();

                    if (input.Equals("y", StringComparison.CurrentCultureIgnoreCase) || String.IsNullOrWhiteSpace(input))
                    {
                        continue;
                    }
                    else
                    {
                        Program.CurrentMenu = MenuID.Main;
                        return;
                    }

                }
            }

        }
    }
}
