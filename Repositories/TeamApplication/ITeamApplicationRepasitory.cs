using ERP.IntegrationUI.Models.Integration;

namespace ERP.IntegrationUI.Repositories.TeamApplication
{
    public interface ITeamApplicationRepasitory : IPagingRepositorie<TeamApplicationReadModel> 
    {
        Task<List<TeamApplicationReadModel>> GetTeamApplicationsAsync(Guid teamIdentificator);
    }
}
