namespace BtgPactual.Back.Domain.Models.Customers
{
    public class Customer : GeneralEntity
    {
        public string Name { get; set; } = string.Empty;
        public double Balance { get; set; }
        public List<FundTransaction> Transactions { get; set; } = [];
    }
}
