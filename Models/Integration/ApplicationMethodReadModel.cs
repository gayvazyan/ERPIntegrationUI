using ERP.IntegrationUI.Enums;
using ERP.IntegrationUI.Response;
using System;

namespace ERP.IntegrationUI.Models.Integration
{
    public class ApplicationMethodReadModel : IApplicationMethodResponse
    {
        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public string Name { get; set; }
        public EMethodType MethodType { get; set; }
        public bool IsAutomaticJob { get; set; }
        //public List<TeamApplicationMethodEModel> TeamApplicationMethods { get; set; } = new List<TeamApplicationMethodEModel>();
    }
}
