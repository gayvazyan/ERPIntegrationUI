using ERP.IntegrationUI.Models.Integration;
using ERP.IntegrationUI.Repositories.Owner;
using ERP.IntegrationUI.Repositories.Team;
using ERP.IntegrationUI.Repositories.TeamApplicationMethod;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ERP.IntegrationUI.Pages.Management.TeamApplicationMethods
{
    public class IndexModel : PageModel
    {
        private readonly ITeamRepasitory _teamRepasitory;
        private readonly IOwnerRepasitory _ownerRepasitory;
        private readonly ITeamApplicationMethodRepasitory _teamApplicationMethodRepasitory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexModel(IHttpContextAccessor httpContextAccessor,
                          ITeamRepasitory teamRepasitory,
                          IOwnerRepasitory ownerRepasitory,
                          ITeamApplicationMethodRepasitory teamApplicationMethodRepasitory)

        {
            _teamRepasitory = teamRepasitory;
            _ownerRepasitory = ownerRepasitory;
            _httpContextAccessor = httpContextAccessor;
            _teamApplicationMethodRepasitory = teamApplicationMethodRepasitory;
            Input = new InputModel();
            InputList = new List<InputModel>();
            _ownerRepasitory = ownerRepasitory;
        }
        [BindProperty]
        public Guid OwnerId { get; set; }

        [BindProperty]
        public Guid TeamId { get; set; }

        [BindProperty]
        public Guid TeamApplicationId { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public string OwnerName { get; set; }

        public List<InputModel> InputList;
        public class InputModel : TeamApplicationMethodReadModel { }

        //START Part Paging
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<TeamApplicationMethodReadModel> TeamApplicationMethodList { get; set; }

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;
        //END Part Paging


        protected async Task PrepareDataAsync(Guid ownerId, Guid teamId, Guid teamApplicationId,Guid applicationId)
        {
            TeamId = teamId;
            OwnerId = ownerId;
            TeamApplicationId = applicationId;

            var allApplicationMethodList = await _teamApplicationMethodRepasitory.GetTeamApplicationMethodsAsync();
            var teamApplicationMethodList = allApplicationMethodList.Where(p => p.TeamApplicationId == teamApplicationId).ToList();

            if (Input.MethodName != null)
            {
                teamApplicationMethodList = teamApplicationMethodList.Where(p => p.MethodName.ToUpper().Contains(Input.MethodName.ToUpper())).ToList();
            }


            TeamApplicationMethodList = _teamApplicationMethodRepasitory.GetPaginatedResult(teamApplicationMethodList, CurrentPage, PageSize);

            Count = TeamApplicationMethodList.Count();

            InputList = TeamApplicationMethodList.Select(p =>
            {
                return new InputModel()
                {
                    Id = p.Id,
                    MethodType = p.MethodType,
                    MethodName = p.MethodName,
                    TeamApplicationId = p.TeamApplicationId,
                    ApplicationMethodId = p.ApplicationMethodId,
                    IsAutomaticJob = p.IsAutomaticJob,
                };
            }).ToList();
        }


        public async Task<ActionResult> OnGet(Guid ownerId, Guid teamId, Guid teamApplicationId, Guid applicationId)
        {
            await PrepareDataAsync(ownerId,teamId, teamApplicationId, applicationId);
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            //await PrepareDataAsync();
            return Page();
        }
    }
}
