using ERP.IntegrationUI.Models.Integration;
using System.Net;

namespace ERP.IntegrationUI.Repositories.Team
{
    public interface ITeamRepasitory : IPagingRepositorie<TeamReadModel> 
    {
        Task<List<TeamReadModel>> GetTeamsAsync();
        Task<HttpStatusCode> AddTeamAsync(TeamReadModel team);
    }
}
