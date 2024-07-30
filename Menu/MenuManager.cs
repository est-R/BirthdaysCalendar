namespace BirthdaysConsole.Menu
{
    internal class MenuManager
    {
        internal static void ShowMenu(MenuID id)
        {
            switch (id)
            {
                case MenuID.Main:
                    {
                        MenuMain.ShowMenu();
                        break;
                    }
                case MenuID.AddPerson:
                    {
                        MenuAddPerson.ShowMenu();
                        break;
                    }
                case MenuID.DeletePerson:
                    {
                        MenuDeletePerson.ShowMenu();
                        break;
                    }
                case MenuID.EditPerson:
                    {
                        MenuEditPerson.ShowMenu();
                        break;
                    }
            }
        }
    }
}