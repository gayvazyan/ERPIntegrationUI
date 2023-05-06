
using ERP.IntegrationUI.Enums;
using ERP.IntegrationUI.Response;
using System;
using System.Collections.Generic;

namespace ERP.IntegrationUI.Models.Integration
{
    public class ApplicationReadModel : IApplicationResponse<ApplicationMethodReadModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }
        public string CheckCredentialUrl { get; set; }
        public string IntegrationUrl { get; set; }
        public DateTime ActionDate { get; set; }
        public bool IsActive { get; set; }
        //public List<CredentialSchemeEModel> CredentialScheme { get; set; }
        //public string CredentialRequestScheme { get; set; }
        //public string SecretKey { get; set; }
        public EIntegrationGroup GroupType { get; set; }
        public IEnumerable<ApplicationMethodReadModel> ApplicationMethods { get; set; }
    }
}
