using ERP.IntegrationUI.Models.Integration;

namespace ERP.IntegrationUI.Repositories.Owner
{
    public interface IOwnerRepasitory : IPagingRepositorie<OwnerReadModel> 
    {
        Task<List<OwnerReadModel>>  GetOwnersAsync();
    }
}
