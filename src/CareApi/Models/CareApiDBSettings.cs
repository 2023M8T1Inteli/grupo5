using MongoDB.Driver.Core.Configuration;

namespace CareApi.Models
{
    public class CareApiDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UserCollectionName { get; set; } = null!;
    }
}
