using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace BtgPactual.Back.Domain.Dtos
{
    [ExcludeFromCodeCoverage]
    public class GeneralEntityDto
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("createAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreateAt { get; set; }

        [JsonProperty("updateAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdateAt { get; set; }
    }
}
