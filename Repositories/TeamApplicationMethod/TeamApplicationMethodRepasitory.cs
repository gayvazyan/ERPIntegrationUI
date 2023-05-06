using ERP.IntegrationUI.Models.Integration;
using Newtonsoft.Json;

namespace ERP.IntegrationUI.Repositories.TeamApplicationMethod
{
    public class TeamApplicationMethodRepasitory : ITeamApplicationMethodRepasitory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TeamApplicationMethodRepasitory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public List<TeamApplicationMethodReadModel> GetPaginatedResult(List<TeamApplicationMethodReadModel> data, int currentPage, int pageSize)
        {
            return data.OrderBy(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public int GetCount(List<TeamApplicationMethodReadModel> data)
        {
            return data.Count;
        }

        public async Task<List<TeamApplicationMethodReadModel>> GetTeamApplicationMethodsAsync()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("access_token", _httpContextAccessor.HttpContext.Session.GetString("token"));
            string url = "https://app-erp-integration-dev.azurewebsites.net/api/v1.0/Management/GetAllTeamApplicationMethods?api-version=1.0";
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            var teamApplicationMethodsList = JsonConvert.DeserializeObject<List<TeamApplicationMethodReadModel>>(result);

            return teamApplicationMethodsList;
        }
    }
}
