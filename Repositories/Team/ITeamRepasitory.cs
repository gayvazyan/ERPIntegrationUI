using ERP.IntegrationUI.Models.Integration;

namespace ERP.IntegrationUI.Repositories.Team
{
    public interface ITeamRepasitory : IPagingRepositorie<TeamReadModel> 
    {
        Task<List<TeamReadModel>> GetTeamsAsync();
    }
}
