using BirthdaysConsole.Data;
using BirthdaysConsole.Menu;
using BirthdaysConsoleDB.Data;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace BirthdaysConsole;

class Program {
    /// <summary>
    /// Количество дней, которые считаются за ближайшие. 
    /// </summary>
    internal static int NearestDays { get; set; } = 15;

    internal static MenuID CurrentMenu { get; set; } = MenuID.Main;

    internal static MongoHelper mongoHelper;
    internal static List<PersonData> DB { get; set; }

    public static IConfigurationRoot Configuration { get; private set; }


    //internal static bool Entry { get; set; } = true;

    static void Main(string[] args)
    {
        //CultureInfo.CurrentCulture = new CultureInfo("ru-RU");

        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        mongoHelper = new MongoHelper(
        Configuration.GetSection("BirthdaysDatabase")["ConnectionString"],
        Configuration.GetSection("BirthdaysDatabase")["DatabaseName"],
        Configuration.GetSection("BirthdaysDatabase")["Persons"]);

        DataManager.UpdateDatabaseAsync();

        // Main loop
        while (true)
        {
            MenuManager.ShowMenu(CurrentMenu);
        }
    }
}

