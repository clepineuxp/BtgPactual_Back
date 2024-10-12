using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using BtgPactual.Back.Domain.Enums;

namespace BtgPactual.Back.Domain.Models.Customers
{
    public class FundTransaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public ObjectId FundId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public TransactionTypeEnum Type { get; set; }
        [BsonIgnoreIfNull]
        public bool? Active { get; set; }
        public double Amount { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }
    }
}
