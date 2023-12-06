using CareApi.Dtos;
using CareApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
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

        public async Task<List<dynamic>> GetManyAsync()
        {
            var projection = Builders<User>.Projection.Exclude(u => u.Password);
            var bsonUsers = await _userCollection.Find(_ => true)
                                                 .Project(projection)
                                                 .ToListAsync();

            var users = bsonUsers.Select(bson => BsonSerializer.Deserialize<dynamic>(bson)).ToList();

            return users;
        }



        public async Task<User> GetByNameAsync(string name) =>
            await _userCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task<User> CreateOneAsync(CreateUserDto createUserDto)
        {
            var user = new User
            {
                Id = createUserDto.Id,
                Name = createUserDto.Name,
                Email = createUserDto.Email,
                Role = createUserDto.Role
            };

            await _userCollection.InsertOneAsync(user);

            return user;
        }
             

        public async Task CreateManyAsync(List<User> users) =>
            await _userCollection.InsertManyAsync(users);

        public async Task UpdateByNameAsync(User user, string name) =>
            await _userCollection.ReplaceOneAsync(x => x.Name == name, user);

        public async Task RemoveByNameAsync(string name) =>
            await _userCollection.DeleteOneAsync(x => x.Name == name);
    }
}
