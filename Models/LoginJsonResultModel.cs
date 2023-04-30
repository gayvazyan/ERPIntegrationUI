using Newtonsoft.Json;

namespace ERP.IntegrationUI.Models
{
    public class LoginJsonResultModel
    {
        [JsonProperty("$id")]
        public string id { get; set; }
        public string token { get; set; }
    }
}
