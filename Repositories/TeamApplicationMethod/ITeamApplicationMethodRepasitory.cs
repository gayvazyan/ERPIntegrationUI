using ERP.IntegrationUI.Models;
using ERP.IntegrationUI.Models.Integration;
using System.Net;

namespace ERP.IntegrationUI.Repositories.TeamApplicationMethod
{
    public interface ITeamApplicationMethodRepasitory : IPagingRepositorie<TeamApplicationMethodReadModel> 
    {
        Task<List<TeamApplicationMethodReadModel>> GetTeamApplicationMethodsAsync();
        Task<HttpStatusCode> AddTeamApplicationMethodAsync(TeamApplicationMethodJsonModel teamApplicationMethod);

    }
}
