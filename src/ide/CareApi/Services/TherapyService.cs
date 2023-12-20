using CareApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CareApi.Services
{
    public class TherapyService
    {
        private readonly IMongoCollection<Therapy> _therapyCollection;

        public TherapyService(IOptions<CareApiDBSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            
            _therapyCollection = mongoDatabase.GetCollection<Therapy>(dbSettings.Value.TherapyCollectionName);
        }

        public async Task<List<Therapy>> GetManyAsync() =>
            await _therapyCollection.Find(_  => true).ToListAsync();

        public async Task<Therapy?> GetByNameAsync(string name) =>
            await _therapyCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        //public async Task<Therapy?> GetByCodigoAsync(string codigo) =>
        //    await _therapyCollection.Find(x => x.Codigo == codigo).FirstOrDefaultAsync();

        //public async Task<Therapy?> GetByImageUrlAsync(string imageUrl) =>
        //    await _therapyCollection.Find(x => x.ImageUrl == imageUrl).FirstOrDefaultAsync();

        //public async Task<Therapy?> GetBySoundUrlAsync(string soundUrl) =>
        //    await _therapyCollection.Find(x => x.SoundUrl == soundUrl).FirstOrDefaultAsync();

        public async Task CreateOneAsync(Therapy therapy) =>
            await _therapyCollection.InsertOneAsync(therapy);

        public async Task CreateManyAsync(List<Therapy> therapies) =>
            await _therapyCollection.InsertManyAsync(therapies);

        public async Task UpdateByNameAsync(Therapy therapy, string name) =>
            await _therapyCollection.ReplaceOneAsync(x => x.Name == name, therapy);

        public async Task RemoveByNameAsync(string name) =>
            await _therapyCollection.DeleteOneAsync(x => x.Name == name);
    }
}
