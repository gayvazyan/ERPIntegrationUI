using ERP.IntegrationUI.Enums;
using ERP.IntegrationUI.Response;
using System;
using System.Collections.Generic;

namespace ERP.IntegrationUI.Models.Integration
{
    public class TeamApplicationReadModel : IIntegrationTeamApplicationResponse
    {
        public Guid Id { get; set; }

        public Guid TeamId { get; set; }

        public Guid ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public string CredentialSchemes { get; set; }

        /// <summary>
        /// For example, Microsoft Account, Bonee Account
        /// </summary>
        public string ExternalPortalSubscriptionUser { get; set; }

        public bool IsActive { get; set; }

        public EApplicationAuthenticationType AuthenticationType { get; set; }

        public IList<TeamApplicationMethodReadModel> TeamApplicationMethods { get; set; }
    }
}
