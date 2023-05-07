using ERP.IntegrationUI.Models.Integration;
using System.Net;

namespace ERP.IntegrationUI.Repositories.TeamApplication
{
    public interface ITeamApplicationRepasitory : IPagingRepositorie<TeamApplicationReadModel> 
    {
        Task<List<TeamApplicationReadModel>> GetTeamApplicationsAsync(Guid teamIdentificator);
        Task<HttpStatusCode> AddTeamApplicationAsync(TeamApplicationReadModel teamApplication);
    }
}
