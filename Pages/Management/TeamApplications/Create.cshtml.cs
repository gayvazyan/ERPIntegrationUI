using ERP.IntegrationUI.Enums;
using ERP.IntegrationUI.Models.Integration;
using ERP.IntegrationUI.Repositories.Application;
using ERP.IntegrationUI.Repositories.Owner;
using ERP.IntegrationUI.Repositories.Team;
using ERP.IntegrationUI.Repositories.TeamApplication;
using lega.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ERP.IntegrationUI.Pages.Management.TeamApplications
{
    public class CreateModel : PageModel
    {
        private readonly IApplicationRepasitory _applicationRepasitory;
        private readonly ITeamApplicationRepasitory _teamApplicationRepasitory;
        private readonly IOwnerRepasitory _ownerRepasitory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateModel(IHttpContextAccessor httpContextAccessor,
                           ITeamApplicationRepasitory teamApplicationRepasitory,
                           IApplicationRepasitory applicationRepasitory,
                           IOwnerRepasitory ownerRepasitory)
        {
            _teamApplicationRepasitory = teamApplicationRepasitory;
            _applicationRepasitory = applicationRepasitory;
            _ownerRepasitory = ownerRepasitory;
            _httpContextAccessor = httpContextAccessor;
            Create = new CreateTeamApplicationModel();
        }
        [BindProperty]
        public Guid OwnerId { get; set; }

        [BindProperty]
        public string? OwnerName { get; set; }

        [BindProperty]
        public Guid TeamId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Անհրաժեշտ է ընտրել գոնե մեկ Application")]
        public string[] SelectedApplicationIds { get; set; }

        public class CreateTeamApplicationModel

        {
            //public Guid Id { get; set; }

            public Guid TeamId { get; set; }

            public Guid ApplicationId { get; set; }

            //public string ApplicationName { get; set; }

            //public string CredentialSchemes { get; set; }

            //public string ExternalPortalSubscriptionUser { get; set; }

            //public bool IsActive { get; set; }

            //public EApplicationAuthenticationType AuthenticationType { get; set; }

            //public IList<TeamApplicationMethodReadModel> TeamApplicationMethods { get; set; }
        }

        [BindProperty]
        public List<ApplicationReadModel> Applications { get; set; }

        [BindProperty]
        public CreateTeamApplicationModel Create { get; set; }


        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }
        public async Task<PageResult> OnGet(Guid ownerId, Guid teamId)
        {
            OwnerId = ownerId;
            TeamId = teamId;

            Applications = await _applicationRepasitory.GetApplicationsAsync();

            var owner = await _ownerRepasitory.GetOwnerByIdAsync(OwnerId);
            var team = owner.Teams.FirstOrDefault(p=>p.TeamIdentificator == teamId.ToString());

            var strings = new List<string>();
            if (team.TeamApplications != null && team.TeamApplications.Count> 0)
            {
                foreach (var item in team.TeamApplications) 
                {
                   strings.Add(item.ApplicationId.ToString());
                }
            }
            if (strings.Count > 0)
            {
                SelectedApplicationIds = strings.ToArray();
            }

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var owner = await _ownerRepasitory.GetOwnerByIdAsync(OwnerId);
                    OwnerName = owner.Name;

                    var team = owner.Teams.FirstOrDefault(p => p.TeamIdentificator == TeamId.ToString());

                    if (SelectedApplicationIds != null && SelectedApplicationIds.Count() > 0)
                    {
                        team.TeamApplications.Clear();
                        var resultUpdateOwner = await _ownerRepasitory.UpdateOwnerAsync(owner);
                        if (resultUpdateOwner != HttpStatusCode.OK)
                            Errors.Add(new ServiceError { Code = "Error during Update Owner", Description = resultUpdateOwner.ToString() });

                        foreach (var applicationId in SelectedApplicationIds)
                        {
                            var teamApplication = new TeamApplicationReadModel
                            {
                                TeamId = TeamId,
                                ApplicationId = new Guid(applicationId),
                            };
                            var result = await _teamApplicationRepasitory.AddTeamApplicationAsync(teamApplication);
                            if (result != HttpStatusCode.OK)
                            {
                                Errors.Add(new ServiceError { Code = "Error during Add TeamApplication", Description = result.ToString() });
                            }
                        }
                    }

                    return Redirect("/Management/TeamApplications/Index/" + OwnerName + "/" + OwnerId + "/" + TeamId);

                }
                catch (Exception ex)
                {
                    Errors.Add(new ServiceError { Code = "Error during Add TeamApplication", Description = ex.Message });
                }
            }
            return Page();
        }
    }
}
