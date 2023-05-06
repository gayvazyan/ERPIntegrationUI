using ERP.IntegrationUI.Models;
using ERP.IntegrationUI.Models.Integration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ERP.IntegrationUI.Controllers
{
    [Route("[controller]/[action]")]
    public class CommonController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CommonController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<JsonResult> EnableDisable(string idUI)
        {
            //try
            //{
            //    if (!string.IsNullOrEmpty(observerId))
            //    {
            //        var observer = await _observersService.GetObserverByIdAsync(Convert.ToInt32(observerId));
            //        observer.IsValid = observer.IsValid == true ? false : true;

            //        var result = await _observersService.UpdateObserverAsync(observer);

            //        if (result.Succeeded)
            //        {
            //            string message = observer.IsValid == true ? "false" : "true";
            //            return Json(message);
            //        }
            //        else
            //        {
            //            string error = "";
            //            foreach (var err in result.Errors)
            //            {
            //                error += err.Description + ", ";
            //            }
            //            return Json(error);
            //        }
            //    }
            //    else
            //    {
            //        return Json("null");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return Json(ex.Message);
            //}
            return Json("");
        }

        [HttpGet]
        public async Task<JsonResult> GetAdminUser(string teamIdentificator)
        {
            try
            {
                if (!string.IsNullOrEmpty(teamIdentificator))
                {
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("access_token", _httpContextAccessor.HttpContext.Session.GetString("token"));
                    string url = "https://app-erp-integration-dev.azurewebsites.net/api/v1.0/management/GetTeamAdminUserInfo?PartnerTeamId=" + teamIdentificator;
                    var response = await client.GetAsync(url);
                    var result = await response.Content.ReadAsStringAsync();

                    var userInfo = JsonConvert.DeserializeObject<UserInfoJsonModel>(result);

                    if (result != null)
                    {
                        string message =  "User Name - " + userInfo.result.userName + System.Environment.NewLine + "TempPassword - " + userInfo.result.tempPassword;
                        return Json(message);
                    }
                    else
                    {
                        string error = "GetTeamAdminUserInfo json result is null";
                        return Json(error);
                    }
                }
                else
                {
                    return Json("null");
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }
    }
}
