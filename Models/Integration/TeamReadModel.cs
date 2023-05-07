using System;
using System.Collections.Generic;

namespace ERP.IntegrationUI.Models.Integration
{
    /// <summary>
    /// Team Read Data
    /// </summary>
    public class TeamReadModel
    {
        public Guid Id { get; set; }
        public string TeamIdentificator { get; set; }
        //public OwnerReadModel Owner { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string AdditionalData { get; set; }

        /// <summary>
        /// The integration user prefix will be automatically added to the beginning of the integration user name when it is created.
        /// </summary>
        public string IntegrationUserPrefix { get; set; }

        public string ErpAdminUserName { get; set; }

        public IList<TeamApplicationReadModel> TeamApplications { get; set; }

        public IList<IntegrationUserReadModel> IntegrationUsers { get; set; }
    }
}
