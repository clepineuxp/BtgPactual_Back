using BtgPactual.Back.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BtgPactual.Back.Domain.Models.Funds
{
    public class Fund : GeneralEntity
    {
        public string Name { get; set; }
        public double MinimumAmount { get; set; }
        [BsonRepresentation(BsonType.String)]
        public FundCategoryEnum Category{ get; set; }
    }
}
