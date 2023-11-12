using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CareApi.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        public string Name { get; set; } = null!;
        public string Crefito { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
