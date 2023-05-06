using ERP.IntegrationUI.Models.Integration;

namespace ERP.IntegrationUI.Repositories.TeamApplicationMethod
{
    public interface ITeamApplicationMethodRepasitory : IPagingRepositorie<TeamApplicationMethodReadModel> 
    {
        Task<List<TeamApplicationMethodReadModel>> GetTeamApplicationMethodsAsync();
    }
}
