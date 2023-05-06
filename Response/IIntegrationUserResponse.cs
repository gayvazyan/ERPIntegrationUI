using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.IntegrationUI.Response
{
    public interface IIntegrationUserResponse<TTeamApplicationMethodUser>
        where TTeamApplicationMethodUser : ITeamApplicationMethodUserResponse
    {
        Guid Id { get; set; }

        int ErpUserId { get; set; }

        /// <summary>
        /// ERP System User Name
        /// </summary>
        public string ErpUserName { get; set; }

        IEnumerable<TTeamApplicationMethodUser> TeamApplicationMethodUsers { get; set; }
    }
}
