using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CareApi.Dtos
{
    public class CreateUserDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = "therapist";
    }
}
