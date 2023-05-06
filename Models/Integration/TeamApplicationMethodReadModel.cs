
using ERP.IntegrationUI.Enums;
using System;

namespace ERP.IntegrationUI.Models.Integration
{
    public class TeamApplicationMethodReadModel
    {
        public Guid Id { get; set; }
        public Guid ApplicationMethodId { get; set; }
        public Guid TeamApplicationId { get; set; }

        public string MethodName { get; set; }

        public bool IsAutomaticJob { get; set; }

        public EMethodType MethodType { get; set; }
    }
}
