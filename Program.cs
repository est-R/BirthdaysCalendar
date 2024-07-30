using BirthdaysConsole.Data;
using BirthdaysConsole.Menu;
using System.Globalization;

namespace BirthdaysConsole;

class Program {
    /// <summary>
    /// Количество дней, которые считаются за ближайшие. 
    /// </summary>
    internal static int NearestDays { get; set; } = 15;

    internal static MenuID CurrentMenu { get; set; } = MenuID.Main;
    internal static List<PersonData> DB { get; set; }

    //internal static bool Entry { get; set; } = true;

    static void Main(string[] args)
    {
        //CultureInfo.CurrentCulture = new CultureInfo("ru-RU");

        DB = new List<PersonData>();
        DataManager.ReadDataFromCSV();

        while (true)
        {
            MenuManager.ShowMenu(CurrentMenu);
        }
    }
}

