using Newtonsoft.Json;

namespace ERP.IntegrationUI.Models
{
    public class ApplicationMethod
    {
        [JsonProperty("$id")]
        public string Id { get; set; }
        public string id { get; set; }
        public string applicationId { get; set; }
        public string name { get; set; }
        public string methodType { get; set; }
        public bool isAutomaticJob { get; set; }
    }

    public class ApplicationModel
    {
        [JsonProperty("$id")]
        public string Id { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
        public string url { get; set; }
        public string checkCredentialUrl { get; set; }
        public string integrationUrl { get; set; }
        public DateTime actionDate { get; set; }
        public bool isActive { get; set; }
        public string groupType { get; set; }
        public List<ApplicationMethod> applicationMethods { get; set; }
    }

}
