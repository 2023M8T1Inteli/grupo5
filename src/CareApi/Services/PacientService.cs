using CareApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CareApi.Services
{
    public class PacientService
    {
        private readonly IMongoCollection<Pacient> _pacientCollection;

        public PacientService(IOptions<CareApiDBSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

            _pacientCollection = mongoDatabase.GetCollection<Pacient>(dbSettings.Value.PacientCollectionName);
        }

        public async Task<List<Pacient>> GetManyAsync() =>
            await _pacientCollection.Find(_ => true).toListAsync();

        public async Task<Pacient> GetByNameAsync(string name) =>
            await _pacientCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task<Pacient> GetByCifAsync(string cif) =>
            await _pacientCollection.Find(x => x.Cif == cif).FirstOrDefaultAsync();

        public async Task CreateOneAsync(Pacient pacient) =>
            await _pacientCollection.InsertOneAsync(pacient);

        public async Task CreateManyAsync(List<Pacient> pacient) =>
             await _pacientCollection.InsertManyAsync(pacient);

        public async Task UpdateByNameAsync(Pacient pacient, string name) =>
            await _pacientCollection.ReplaceOneAsync(x => x.Name == name, pacient);

        public async Task RemoveByNameAsync(string name) =>
            await _pacientCollection.DeleteOneAsync(x => x.Name == name);
    }
}