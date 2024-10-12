using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BtgPactual.Back.Domain.Models
{
    public class GeneralEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateAt { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateAt { get; set; }
    }
}
