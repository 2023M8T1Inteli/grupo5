using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CareApi.Models
{
    [BsonIgnoreExtraElements]
    public class Pacient
    {
        public string Name { get; set; } = null!;
        public string BirthDate { get; set; } = null!;
        public string Disease { get; set; } = null!;
        public string Cif { get; set; } = null!;
        public List<Sessions> Sessions { get; set; } = null!;
    }

    public class Sessions
    {
        public string StartedAt { get; set; } = null!;
        public string EndedAt { get; set; } = null!;
        public string TherapyName { get; set; } = null!;
        public string Results { get; set; } = null!;
    }
}