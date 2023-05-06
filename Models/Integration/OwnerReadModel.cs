using System;
using System.Collections.Generic;

namespace ERP.IntegrationUI.Models.Integration
{
    /// <summary>
    /// 
    /// </summary>
    public class OwnerReadModel
    {
        /// <summary>
        /// Owner Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// External portal name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }
        //public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        /// <summary>
        /// Additional Data
        /// </summary>
        public string AdditionalData { get; set; }

        public List<TeamReadModel> Teams { get; set; }
    }
}
