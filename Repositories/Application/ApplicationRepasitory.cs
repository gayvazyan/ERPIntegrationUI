﻿using ERP.IntegrationUI.Models.Integration;
using Newtonsoft.Json;

namespace ERP.IntegrationUI.Repositories.Application
{
    public class ApplicationRepasitory : IApplicationRepasitory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationRepasitory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public List<ApplicationReadModel> GetPaginatedResult(List<ApplicationReadModel> data, int currentPage, int pageSize)
        {
            return data.OrderBy(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public int GetCount(List<ApplicationReadModel> data)
        {
            return data.Count;
        }

        public async Task<List<ApplicationReadModel>> GetApplicationsAsync()
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("access_token", _httpContextAccessor.HttpContext.Session.GetString("token"));
            string url = "https://app-erp-integration-dev.azurewebsites.net/api/v1.0/management/getAllApplications";
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            var applicationsList = JsonConvert.DeserializeObject<List<ApplicationReadModel>>(result);

            return applicationsList;
        }

        public async Task<List<ApplicationMethodReadModel>> GetAllApplicationMethodsAsync(Guid applicationId)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("access_token", _httpContextAccessor.HttpContext.Session.GetString("token"));
            string url = "https://app-erp-integration-dev.azurewebsites.net/api/v1.0/management/GetAllApplicationMethods?ApplicationId="+ applicationId.ToString();
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            var applicationMethods = JsonConvert.DeserializeObject<List<ApplicationMethodReadModel>>(result);

            return applicationMethods;
        }
    }
}
