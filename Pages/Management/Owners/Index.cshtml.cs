using ERP.IntegrationUI.Models;
using ERP.IntegrationUI.Models.Integration;
using ERP.IntegrationUI.Repositories;
using ERP.IntegrationUI.Repositories.Owner;
using lega.Core;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace ERP.IntegrationUI.Pages.Management.Owners
{
    public class IndexModel : PageModel
    {
        private readonly IOwnerRepasitory _ownerRepasitory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexModel(IHttpContextAccessor httpContextAccessor, IOwnerRepasitory ownerRepasitory)

        {
            _ownerRepasitory = ownerRepasitory;
            _httpContextAccessor = httpContextAccessor; 
            Input = new InputModel();
            InputList = new List<InputModel>();
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public List<InputModel> InputList;
        public class InputModel : OwnerReadModel { }

        //START Part Paging
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<OwnerReadModel> OwnerList { get; set; }

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;
        //END Part Paging


        protected async Task PrepareDataAsync()
        {

            var ownerList = await _ownerRepasitory.GetOwnersAsync();

            if (Input.Description != null)
            {
                ownerList = ownerList.Where(p => p.Description.ToUpper().Contains(Input.Description.ToUpper())).ToList();
            }

            if (Input.FirstName != null)
            {
                ownerList = ownerList.Where(p => p.FirstName.ToUpper().Contains(Input.FirstName.ToUpper())).ToList();
            }

            OwnerList = _ownerRepasitory.GetPaginatedResult(ownerList, CurrentPage, PageSize);

            Count = OwnerList.Count();

            InputList = OwnerList.Select(p =>
            {
                return new InputModel()
                {
                    Id = p.Id,
                    Description = p.Description,
                    AdditionalData = p.AdditionalData,
                    IsActive = p.IsActive,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Name = p.Name,
                    Teams = p.Teams,
                };
            }).ToList();
        }


        public async Task<ActionResult> OnGet()
        {
           await PrepareDataAsync();
           return Page();
        }

        public async Task<ActionResult> OnPost()
        {
           await PrepareDataAsync();
            return Page();
        }
    }
}
