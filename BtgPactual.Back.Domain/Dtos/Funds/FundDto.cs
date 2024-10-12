using BtgPactual.Back.Domain.Enums;
using BtgPactual.Back.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace BtgPactual.Back.Domain.Dtos.Funds
{
    [ExcludeFromCodeCoverage]
    public class FundDto : GeneralEntityDto
    {
        public string Name { get; set; }
        public double MinimumAmount { get; set; }
        public FundCategoryEnum Category { get; set; }
    }
}
