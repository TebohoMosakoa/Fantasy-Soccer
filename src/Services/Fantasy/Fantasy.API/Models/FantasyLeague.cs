using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Fantasy.API.Models
{
    public class FantasyLeague
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("JoiningKey")]
        public string JoiningKey { get; set; }

        [BsonElement("JoiningFee")]
        public decimal JoiningFee { get; set; }


        public int TeamCount { get; set; }

        public FantasyLeague()
        {
            JoiningKey = Guid.NewGuid().ToString();
            TeamCount = 1;
        }
    }
}
