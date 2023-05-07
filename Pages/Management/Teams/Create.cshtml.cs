using ERP.IntegrationUI.Models.Integration;
using ERP.IntegrationUI.Repositories.Owner;
using ERP.IntegrationUI.Repositories.Team;
using lega.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ERP.IntegrationUI.Pages.Management.Teams
{
    public class CreateModel : PageModel
    {
        private readonly ITeamRepasitory _teamRepasitory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateModel(IHttpContextAccessor httpContextAccessor, ITeamRepasitory teamRepasitory)
        {
            _teamRepasitory = teamRepasitory;
            _httpContextAccessor = httpContextAccessor;
            Create = new CreateTeamModel();
        }

        [BindProperty]
        public Guid OwnerId { get; set; }

        [BindProperty]
        public string OwnerName { get; set; }
        public class CreateTeamModel

        {
            public Guid Id { get; set; }
            public string? TeamIdentificator { get; set; }
            public Guid OwnerId { get; set; }

            [Required]
            public string Name { get; set; }
            public string? Description { get; set; }
            public bool IsActive { get; set; }
            public string? AdditionalData { get; set; }

            [Required]
            public string IntegrationUserPrefix { get; set; }

            [Required]
            public string ErpAdminUserName { get; set; }

            // public IList<TeamApplicationReadModel> TeamApplications { get; set; }
            //public IList<IntegrationUserReadModel> IntegrationUsers { get; set; }
        }

        [BindProperty]
        public CreateTeamModel Create { get; set; }


        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }
        public void OnGet(Guid ownerId, string name)
        {
            OwnerId = ownerId;
            OwnerName = name;
        }

        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var team = new TeamReadModel
                    {
                        //Id = Create.Id,
                        Name = Create.Name,
                        AdditionalData = Create.AdditionalData,
                        Description = Create.Description,
                        IsActive = Create.IsActive,
                        IntegrationUserPrefix = Create.IntegrationUserPrefix,
                        OwnerId = OwnerId,
                        TeamIdentificator = string.Empty,
                        ErpAdminUserName = Create.ErpAdminUserName,
                    };

                    var result = await _teamRepasitory.AddTeamAsync(team);

                    if (result == HttpStatusCode.OK)
                        return Redirect("/Management/Teams/Index/" + OwnerId + "/" + OwnerName);
                    else
                        Errors.Add(new ServiceError { Code = "Error during Add Team", Description = result.ToString() });


                }
                catch (Exception ex)
                {
                    Errors.Add(new ServiceError { Code = "Error during Add Team", Description = ex.Message });
                }
            }
            return Page();
        }
    }
}
