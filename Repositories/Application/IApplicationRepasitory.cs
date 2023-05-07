using ERP.IntegrationUI.Models.Integration;

namespace ERP.IntegrationUI.Repositories.Application
{
    public interface IApplicationRepasitory : IPagingRepositorie<ApplicationReadModel> 
    {
        Task<List<ApplicationReadModel>> GetApplicationsAsync();
        Task<List<ApplicationMethodReadModel>> GetAllApplicationMethodsAsync(Guid applicationId);
    }
}
