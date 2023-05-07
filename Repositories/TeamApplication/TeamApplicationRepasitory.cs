using ERP.IntegrationUI.Models.Integration;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ERP.IntegrationUI.Repositories.TeamApplication
{
    public class TeamApplicationRepasitory : ITeamApplicationRepasitory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TeamApplicationRepasitory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public List<TeamApplicationReadModel> GetPaginatedResult(List<TeamApplicationReadModel> data, int currentPage, int pageSize)
        {
            return data.OrderBy(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public int GetCount(List<TeamApplicationReadModel> data)
        {
            return data.Count;
        }

        public async Task<List<TeamApplicationReadModel>> GetTeamApplicationsAsync(Guid teamIdentificator)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("access_token", _httpContextAccessor.HttpContext.Session.GetString("token"));
            string url = "https://app-erp-integration-dev.azurewebsites.net/api/v1.0/management/GetAllTeamApplications?api-version=1.0&TeamIdentificator=" + teamIdentificator;
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            var teamApplicationsList = JsonConvert.DeserializeObject<List<TeamApplicationReadModel>>(result);

            return teamApplicationsList;
        }
        public async Task<HttpStatusCode> AddTeamApplicationAsync(TeamApplicationReadModel teamApplication)
        {
            string json = JsonConvert.SerializeObject(teamApplication, Formatting.Indented);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("access_token", _httpContextAccessor.HttpContext.Session.GetString("token"));
            var response = await client.PostAsync(" https://app-erp-integration-dev.azurewebsites.net/api/v1.0/Management/AddTeamApplicationd", data);
            var result = await response.Content.ReadAsStringAsync();

            return response.StatusCode;
        }


    }
}
