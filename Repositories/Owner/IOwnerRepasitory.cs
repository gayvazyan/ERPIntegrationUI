using ERP.IntegrationUI.Models.Integration;
using System.Net;

namespace ERP.IntegrationUI.Repositories.Owner
{
    public interface IOwnerRepasitory : IPagingRepositorie<OwnerReadModel> 
    {
        Task<OwnerReadModel> GetOwnerByIdAsync(Guid id);
        Task<List<OwnerReadModel>>  GetOwnersAsync();
        Task<HttpStatusCode> AddOwnerAsync(OwnerReadModel owner);
        Task<HttpStatusCode> UpdateOwnerAsync(OwnerReadModel owner);
    }
}
