
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BirthdaysConsole.Data
{
    internal class PersonData
    {
        private string _name = "Имя не задано";

        //

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? BsonId { get; set; }

        [BsonElement("Name")]
        public string Name
        {
            get { return _name; }
            set { _name = value ?? "Имя не задано"; }
        }
        public DateTime Date { get; set; }
    }
}
