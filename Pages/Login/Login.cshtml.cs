using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using ERP.IntegrationUI.Models;
using lega.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ERP.IntegrationUI.Pages.Login
{
 
    public class LoginModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginModel(IHttpContextAccessor httpContextAccessor)

        {
            _httpContextAccessor = httpContextAccessor;
            Input = new InputModel();
        }

        [BindProperty]
        public InputModel Input { get; set; }


        public class InputModel : User { }


        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }
        
        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User
                    {
                        Email = Input.Email,
                        Password = Input.Password,
                    };

                    string json = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    var client = new HttpClient();
                    var response = await client.PostAsync("https://app-erp-integration-dev.azurewebsites.net/api/v1.0/account/login", data);
                    var result = await response.Content.ReadAsStringAsync();


                    LoginJsonResultModel res = new LoginJsonResultModel();
                    if (result != null)
                    {
                        try
                        {
                            res = JsonConvert.DeserializeObject<LoginJsonResultModel>(result);
                            _httpContextAccessor.HttpContext.Session.SetString("token", res.token);

                        }
                        catch (Exception ex)
                        {

                            Errors.Add(new ServiceError { Code = "Մուտքի սխալ", Description = "Մուտքագրված տվյալներով օգտատեր չի գտնվել։" });
                            return Page();
                        }

                    }
                    
                    if (!string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.Session.GetString("token")))
                    {
                        var test = _httpContextAccessor.HttpContext.Session.GetString("token");
                        return RedirectToPage("/Management/Index");
                    }
                    else
                    {
                        Errors.Add(new ServiceError { Code = "Մուտքի սխալ", Description = "Մուտքագրված տվյալներով օգտատեր չի գտնվել։" });
                        return Page();
                    }

                }
                catch (Exception ex)
                {
                    Errors.Add(new ServiceError { Code = "005", Description = ex.Message });
                }
            }
            return Page();
        }
       
    }
}
