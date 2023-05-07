using ERP.IntegrationUI.Models.Integration;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ERP.IntegrationUI.Repositories.Owner
{
    public class OwnerRepasitory : IOwnerRepasitory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OwnerRepasitory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public List<OwnerReadModel> GetPaginatedResult(List<OwnerReadModel> data, int currentPage, int pageSize)
        {
            return data.OrderBy(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public int GetCount(List<OwnerReadModel> data)
        {
            return data.Count;
        }

        public async Task<OwnerReadModel> GetOwnerByIdAsync(Guid id)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("access_token", _httpContextAccessor.HttpContext.Session.GetString("token"));
            var response = await client.GetAsync("https://app-erp-integration-dev.azurewebsites.net/api/v1.0/management/GetAllOwners");
            var result = await response.Content.ReadAsStringAsync();
            var ownerList = JsonConvert.DeserializeObject<List<OwnerReadModel>>(result);

            return ownerList.FirstOrDefault(p =>p.Id == id);
        }

        public async Task<List<OwnerReadModel>> GetOwnersAsync()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("access_token", _httpContextAccessor.HttpContext.Session.GetString("token"));
            var response = await client.GetAsync("https://app-erp-integration-dev.azurewebsites.net/api/v1.0/management/GetAllOwners");
            var result = await response.Content.ReadAsStringAsync();

            var ownerList = JsonConvert.DeserializeObject<List<OwnerReadModel>>(result);

            return ownerList;
        }

        public async Task<HttpStatusCode> AddOwnerAsync(OwnerReadModel owner)
        {
            string json = JsonConvert.SerializeObject(owner, Formatting.Indented);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("access_token", _httpContextAccessor.HttpContext.Session.GetString("token"));
            var response = await client.PostAsync(" https://app-erp-integration-dev.azurewebsites.net/api/v1.0/management/AddOwner", data);
            var result = await response.Content.ReadAsStringAsync();

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> UpdateOwnerAsync(OwnerReadModel owner)
        {
            string json = JsonConvert.SerializeObject(owner, Formatting.Indented);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("access_token", _httpContextAccessor.HttpContext.Session.GetString("token"));
            var response = await client.PostAsync("https://app-erp-integration-dev.azurewebsites.net/api/v1.0/management/UpdateOwner", data);
            var result = await response.Content.ReadAsStringAsync();

            return response.StatusCode;
        }

    }
}
