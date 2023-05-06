
using ERP.IntegrationUI.Response;
using System;
using System.Collections.Generic;

namespace ERP.IntegrationUI.Models.Integration
{
    public class IntegrationUserReadModel : IIntegrationUserResponse<TeamApplicationMethodUserReadModel>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// ERP system UserId
        /// </summary>
        public int ErpUserId { get; set; }

        /// <summary>
        /// ERP System User Name
        /// </summary>
        public string ErpUserName { get; set; }

        //public string ErpPassword { get; set; }

        public string Key { get; set; }

        public Guid TeamId { get; set; }

        public Guid ApplicationUserId { get; set; }

        /// <summary>
        /// Temporarry Password
        /// </summary>
        public string AppUserTempPassword { get; set; }

        public IEnumerable<TeamApplicationMethodUserReadModel> TeamApplicationMethodUsers { get; set; }
    }
}
