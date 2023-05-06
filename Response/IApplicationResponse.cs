
using ERP.IntegrationUI.Enums;
using System;
using System.Collections.Generic;

namespace ERP.IntegrationUI.Response
{
    public interface IApplicationResponse<TMethod>
        where TMethod : IApplicationMethodResponse
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Logo { get; set; }
        string Url { get; set; }
        string CheckCredentialUrl { get; set; }
        string IntegrationUrl { get; set; }

        bool IsActive { get; set; }
        //List<CredentialSchemeEModel> CredentialScheme { get; set; }
        //string CredentialRequestScheme { get; set; }
        //string SecretKey { get; set; }
        EIntegrationGroup GroupType { get; set; }
        IEnumerable<TMethod> ApplicationMethods { get; set; }
    }
}
