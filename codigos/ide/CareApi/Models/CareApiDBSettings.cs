using MongoDB.Driver.Core.Configuration;

namespace CareApi.Models
{
    public class CareApiDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UserCollectionName { get; set; } = null!;
        public string TherapyCollectionName { get; set; } = null!;
        public string PacientCollectionName { get; set; } = null!;
    }
}
