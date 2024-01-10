using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CareApi.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Role { get; set; } = null!;
        public string? Password { get; set; } = null!;
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiration { get; set; }
    }
}
