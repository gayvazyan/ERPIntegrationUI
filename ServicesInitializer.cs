using ERP.IntegrationUI.Repositories.Application;
using ERP.IntegrationUI.Repositories.Owner;
using ERP.IntegrationUI.Repositories.Team;
using ERP.IntegrationUI.Repositories.TeamApplication;
using ERP.IntegrationUI.Repositories.TeamApplicationMethod;

namespace ERP.IntegrationUI
{
    public static partial class ServicesInitializer
    {
        public static void ConfigureServices(IServiceCollection services)
        {
             //services.AddTransient(typeof(IPagingRepositorie<>), typeof(PagingRepositorie<>));
             services.AddTransient<IApplicationRepasitory, ApplicationRepasitory>();
             services.AddTransient<IOwnerRepasitory, OwnerRepasitory>();
             services.AddTransient<ITeamRepasitory, TeamRepasitory>();
             services.AddTransient<ITeamApplicationRepasitory, TeamApplicationRepasitory>();
             services.AddTransient<ITeamApplicationMethodRepasitory, TeamApplicationMethodRepasitory>();
        }
    }
}
