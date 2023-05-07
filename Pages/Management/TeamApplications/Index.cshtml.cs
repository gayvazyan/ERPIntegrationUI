using ERP.IntegrationUI.Models.Integration;
using ERP.IntegrationUI.Repositories.Owner;
using ERP.IntegrationUI.Repositories.TeamApplication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace ERP.IntegrationUI.Pages.Management.TeamApplications
{
    public class IndexModel : PageModel
    {
        private readonly ITeamApplicationRepasitory _teamApplicationRepasitory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexModel(IHttpContextAccessor httpContextAccessor, ITeamApplicationRepasitory teamApplicationRepasitory)

        {
            _teamApplicationRepasitory = teamApplicationRepasitory;
            _httpContextAccessor = httpContextAccessor;
            Input = new InputModel();
            InputList = new List<InputModel>();
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public List<InputModel> InputList;
        public class InputModel : TeamApplicationReadModel { }

        //START Part Paging
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<TeamApplicationReadModel> TeamApplicationList { get; set; }

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;
        //END Part Paging


        protected async Task PrepareDataAsync(Guid teamIdentificator)
        {

            var teamApplicationList = await _teamApplicationRepasitory.GetTeamApplicationsAsync(teamIdentificator);

            if (Input.ApplicationName != null)
            {
                teamApplicationList = teamApplicationList.Where(p => p.ApplicationName.ToUpper().Contains(Input.ApplicationName.ToUpper())).ToList();
            }
            if (Input.CredentialSchemes != null)
            {
                teamApplicationList = teamApplicationList.Where(p => p.CredentialSchemes.ToUpper().Contains(Input.CredentialSchemes.ToUpper())).ToList();
            }


            TeamApplicationList = _teamApplicationRepasitory.GetPaginatedResult(teamApplicationList, CurrentPage, PageSize);

            Count = TeamApplicationList.Count();

            InputList = TeamApplicationList.Select(p =>
            {
                return new InputModel()
                {
                    Id = p.Id,
                    IsActive = p.IsActive,
                    ApplicationId = p.ApplicationId,
                    ApplicationName = p.ApplicationName,
                    AuthenticationType = p.AuthenticationType,
                    CredentialSchemes = p.CredentialSchemes,
                    ExternalPortalSubscriptionUser = p.ExternalPortalSubscriptionUser,
                    TeamApplicationMethods = p.TeamApplicationMethods,
                };
            }).ToList();
        }


        public async Task<ActionResult> OnGet(Guid teamIdentificator)
        {
            await PrepareDataAsync(teamIdentificator);
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            //await PrepareDataAsync();
            return Page();
        }
    }
}
