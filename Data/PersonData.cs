
namespace BirthdaysConsole.Data
{
    internal class PersonData
    {
        private static int _idCounter = 0;

        public int Id { get; }

        private string _name = "Имя не задано";
        public string Name
        {
            get { return _name; }
            set { _name = value ?? "Имя не задано"; }
        }
        public DateOnly Date { get; set; }

        public PersonData()
        {
            Id = ++_idCounter;
        }
    }
}
