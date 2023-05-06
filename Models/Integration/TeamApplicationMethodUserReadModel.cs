
using ERP.IntegrationUI.Response;
using System;

namespace ERP.IntegrationUI.Models.Integration
{
    public class TeamApplicationMethodUserReadModel : ITeamApplicationMethodUserResponse
    {
        public Guid IntegrationUserId { get; set; }

        public Guid TeamApplicationMethodId { get; set; }
    }
}
