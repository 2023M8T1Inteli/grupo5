using CareApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CareApi.Services
{
    public class CodeQALService
    {
        private readonly IMongoCollection<CodeQAL> _codeQALCollection;

        public CodeQALService(IOptions<CareApiDBSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDataBase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _codeQALCollection = mongoDataBase.GetCollection<CodeQAL>(dbSettings.Value.CodeQALCollectionName);
        }

        public async Task<List<CodeQAL>> GetAllAsync() =>
            await _codeQALCollection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(CodeQAL code) =>
            await _codeQALCollection.InsertOneAsync(code);

    }
}
