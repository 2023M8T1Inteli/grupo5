using MongoDB.Bson.Serialization.Attributes;

namespace CareApi.Models
{
    [BsonIgnoreExtraElements]
    public class CodeQAL
    {
        public string Code { get; set; } = null!;
    }
}
