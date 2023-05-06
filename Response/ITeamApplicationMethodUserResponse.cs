using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.IntegrationUI.Response
{
    public interface ITeamApplicationMethodUserResponse
    {
        Guid IntegrationUserId { get; set; }

        Guid TeamApplicationMethodId { get; set; }
    }
}
