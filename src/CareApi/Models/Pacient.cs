using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace CareApi.Models
{
    [BsonIgnoreExtraElements]
    public class Pacient
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = null!;
        public string BirthDate { get; set; } = null!;
        public List<Sessions>? Sessions { get; set; } = new List<Sessions>();
    }

    public class Sessions
    {
        public string StartedAt { get; set; } = null!;
        public string EndedAt { get; set; } = null!;
        public string TherapyName { get; set; } = null!;
        public string Results { get; set; } = null!;
    }
}