using Newtonsoft.Json;

namespace ERP.IntegrationUI.Models
{
    public class Result
    {
        public string userName { get; set; }
        public string tempPassword { get; set; }
    }

    public class UserInfoJsonModel
    {
        [JsonProperty("$id")]
        public string id { get; set; }
        public Result result { get; set; }
        public bool success { get; set; }
        public object message { get; set; }
        public object questionMessage { get; set; }
    }
}
