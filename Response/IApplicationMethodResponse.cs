using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.IntegrationUI.Response
{
    public interface IApplicationMethodResponse
    {
       Guid Id { get; set; }
       Guid ApplicationId { get; set; }
       string Name { get; set; }
    }
}
