using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json;
using ThirdParty.Json.LitJson;
using Online_Survey.Services.Base;
using Online_Survey.Models.Base;

namespace Online_Survey.Models
{
    public class TestCollection : TEntity
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public List<BsonDocument> UnknownObjects { get; set; }
    }
}
