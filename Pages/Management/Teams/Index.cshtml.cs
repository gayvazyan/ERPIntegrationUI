using ERP.IntegrationUI.Models.Integration;
using ERP.IntegrationUI.Repositories.Owner;
using ERP.IntegrationUI.Repositories.Team;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ERP.IntegrationUI.Pages.Management.Teams
{
    public class IndexModel : PageModel
    {
        private readonly ITeamRepasitory _teamRepasitory;
        private readonly IOwnerRepasitory _ownerRepasitory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexModel(IHttpContextAccessor httpContextAccessor, ITeamRepasitory teamRepasitory, IOwnerRepasitory ownerRepasitory)

        {
            _teamRepasitory = teamRepasitory;
            _ownerRepasitory=ownerRepasitory;
            _httpContextAccessor = httpContextAccessor;
            Input = new InputModel();
            InputList = new List<InputModel>();
            _ownerRepasitory = ownerRepasitory;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public string OwnerName { get; set; }

        public List<InputModel> InputList;
        public class InputModel : TeamReadModel { }

        //START Part Paging
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<TeamReadModel> TeamList { get; set; }

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;
        //END Part Paging

        private async void SetOwnerName(Guid id)
        {
            var owners = await _ownerRepasitory.GetOwnersAsync();

            var ownerName = owners?.FirstOrDefault(p => p.Id == id).Name;
            if (!string.IsNullOrWhiteSpace(ownerName))
            {
                OwnerName = ownerName;
            }
        }

        protected async Task PrepareDataAsync(Guid id)
        {
            SetOwnerName(id);

             var allTeamList = await _teamRepasitory.GetTeamsAsync();
            var teamList = allTeamList.Where(p => p.OwnerId == id).ToList();

            if (Input.Description != null)
            {
                teamList = teamList.Where(p => p.Description.ToUpper().Contains(Input.Description.ToUpper())).ToList();
            }
            if (Input.Name != null)
            {
                teamList = teamList.Where(p => p.Description.ToUpper().Contains(Input.Name.ToUpper())).ToList();
            }


            TeamList = _teamRepasitory.GetPaginatedResult(teamList, CurrentPage, PageSize);

            Count = TeamList.Count();

            InputList = TeamList.Select(p =>
            {
                return new InputModel()
                {
                    Id = p.Id,
                    TeamIdentificator = p.TeamIdentificator,
                    OwnerId = p.OwnerId,
                    Name = p.Name,
                    Description = p.Description,
                    IsActive = p.IsActive,
                    AdditionalData = p.AdditionalData,
                    IntegrationUserPrefix = p.IntegrationUserPrefix,
                    TeamApplications = p.TeamApplications,
                    IntegrationUsers = p.IntegrationUsers,
               
                };
            }).ToList();
        }


        public async Task<ActionResult> OnGet(Guid id)
        {
            await PrepareDataAsync(id);
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            //await PrepareDataAsync();
            return Page();
        }
    }
}
