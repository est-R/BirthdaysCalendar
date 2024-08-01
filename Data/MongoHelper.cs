using MongoDB.Driver;
using MongoDB.Bson;
using BirthdaysConsole.Data;
using Microsoft.Extensions.Configuration;
using BirthdaysConsole;
using static MongoDB.Driver.WriteConcern;

namespace BirthdaysConsoleDB.Data
{
    internal class MongoHelper
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<PersonData> _persons;

        internal MongoHelper(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
            _persons = _database.GetCollection<PersonData>(collectionName);
        }

        internal async Task<List<PersonData>> GetAsync() =>
            await _persons.Find(_ => true).ToListAsync();

        internal async Task<PersonData?> GetAsync(string BsonId) =>
            await _persons.Find(x => x.BsonId == BsonId).FirstOrDefaultAsync();

        internal async Task CreateAsync(PersonData newPerson) =>
            await _persons.InsertOneAsync(newPerson);

        internal async Task UpdateAsync(string BsonId, PersonData updatedPerson)
        {
            var filter = Builders<PersonData>.Filter
                .Eq(p => p.BsonId, BsonId);
            //await _persons.ReplaceOneAsync(filter, updatedPerson);
            _persons.ReplaceOne(filter, updatedPerson);

        }

        internal async Task RemoveAsync(string BsonId) =>
            await _persons.DeleteOneAsync(x => x.BsonId == BsonId);
    }
}

