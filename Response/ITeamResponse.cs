using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.IntegrationUI.Response
{
    public interface ITeamResponse
    {
        Guid Id { get; set; }
        string TeamIdentificator { get; set; }
        Guid OwnerId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsActive { get; set; }
        string AdditionalData { get; set; }
    }
}
