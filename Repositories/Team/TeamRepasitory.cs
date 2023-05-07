using ERP.IntegrationUI.Models.Integration;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ERP.IntegrationUI.Repositories.Team
{
    public class TeamRepasitory : ITeamRepasitory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TeamRepasitory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public List<TeamReadModel> GetPaginatedResult(List<TeamReadModel> data, int currentPage, int pageSize)
        {
            return data.OrderBy(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public int GetCount(List<TeamReadModel> data)
        {
            return data.Count;
        }

        public async Task<List<TeamReadModel>> GetTeamsAsync()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("access_token", _httpContextAccessor.HttpContext.Session.GetString("token"));
            var response = await client.GetAsync("https://app-erp-integration-dev.azurewebsites.net/api/v1.0/Management/GetAllTeams");
            var result = await response.Content.ReadAsStringAsync();

            var teamList = JsonConvert.DeserializeObject<List<TeamReadModel>>(result);

            return teamList;
        }
        public async Task<HttpStatusCode> AddTeamAsync(TeamReadModel team)
        {
            string json = JsonConvert.SerializeObject(team, Formatting.Indented);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("access_token", _httpContextAccessor.HttpContext.Session.GetString("token"));
            var response = await client.PostAsync(" https://app-erp-integration-dev.azurewebsites.net/api/v1.0/Management/AddTeam", data);
            var result = await response.Content.ReadAsStringAsync();

            return response.StatusCode;
        }
    }
}
