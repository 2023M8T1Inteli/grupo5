using CareApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CareApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IOptions<CareApiDBSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>(dbSettings.Value.UserCollectionName);
        }

        public async Task<List<User>> GetManyAsync() =>
            await _userCollection.Find(_ => true).ToListAsync();

        public async Task<User> GetByNameAsync(string name) =>
            await _userCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task<User> GetByCrefitoAsync(string crefito) =>
            await _userCollection.Find(x => x.Crefito == crefito).FirstOrDefaultAsync();

        public async Task CreateOneAsync(User user) =>
            await _userCollection.InsertOneAsync(user);

        public async Task CreateManyAsync(List<User> users) =>
            await _userCollection.InsertManyAsync(users);

        public async Task UpdateByNameAsync(User user, string name) =>
            await _userCollection.ReplaceOneAsync(x => x.Name == name, user);

        public async Task UpdateByCrefitoAsync(User user, string crefito) =>
            await _userCollection.ReplaceOneAsync(x => x.Crefito == crefito, user);

        public async Task RemoveByNameAsync(string name) =>
            await _userCollection.DeleteOneAsync(x => x.Crefito == name);

        public async Task RemoveByCrefito(string crefito) =>
            await _userCollection.DeleteOneAsync(x => x.Crefito == crefito);
    }
}
