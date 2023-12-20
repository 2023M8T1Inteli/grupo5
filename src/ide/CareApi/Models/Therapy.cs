using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CareApi.Models
{
    [BsonIgnoreExtraElements]
    public class Therapy
    {
        public string Name { get; set; } = null!;
        public string CreatedByUser { get; set; } = null!;
        public List<Command> Command { get; set; } = null!;
    }

    public class Command
    {
        public string Name { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string SoundUrl { get; set; } = null!;
        public int Order { get; set; } = 0;
    }
}
