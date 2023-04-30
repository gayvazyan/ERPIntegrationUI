using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ERP.IntegrationUI.Pages.Login
{    public class LogoutModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LogoutModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            _httpContextAccessor.HttpContext.Session.Remove("token");
            return RedirectToPage("/Index");
        }
    }
}
