using BtgPactual.Back.Domain.Helpers;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace BtgPactual.Back.Domain.Dtos.Response
{
    [ExcludeFromCodeCoverage]
    public class GenericResponse
    {

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(HttpStatusCodeConverter))]
        public HttpStatusCode Status { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string? Message { get; set; }
    }
}
