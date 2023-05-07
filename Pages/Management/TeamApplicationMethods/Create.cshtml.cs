using ERP.IntegrationUI.Enums;
using ERP.IntegrationUI.Models;
using ERP.IntegrationUI.Models.Integration;
using ERP.IntegrationUI.Repositories.Application;
using ERP.IntegrationUI.Repositories.Owner;
using ERP.IntegrationUI.Repositories.TeamApplication;
using ERP.IntegrationUI.Repositories.TeamApplicationMethod;
using lega.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace ERP.IntegrationUI.Pages.Management.TeamApplicationMethods
{
    public class CreateModel : PageModel
    {
        private readonly IApplicationRepasitory _applicationRepasitory;
        private readonly ITeamApplicationMethodRepasitory _teamApplicationMethodRepasitory;
        private readonly IOwnerRepasitory _ownerRepasitory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateModel(IHttpContextAccessor httpContextAccessor,
                           ITeamApplicationMethodRepasitory teamApplicationMethodRepasitory,
                           IApplicationRepasitory applicationRepasitory,
                           IOwnerRepasitory ownerRepasitory)
        {
            _teamApplicationMethodRepasitory = teamApplicationMethodRepasitory;
            _applicationRepasitory = applicationRepasitory;
            _ownerRepasitory = ownerRepasitory;
            _httpContextAccessor = httpContextAccessor;
            Create = new CreateTeamApplicationModel();
        }
        [BindProperty]
        public Guid OwnerId { get; set; }

        [BindProperty]
        public Guid TeamId { get; set; }

        [BindProperty]
        public string? OwnerName { get; set; }

        [BindProperty]
        public Guid ApplicationId { get; set; }

        [BindProperty]
        public Guid TeamApplicationId { get; set; }

        [BindProperty]
        public string[] SelectedApplicationMethodIds { get; set; }

        public class CreateTeamApplicationModel

        {
            public Guid Id { get; set; }
            public Guid ApplicationMethodId { get; set; }
            public Guid TeamApplicationId { get; set; }

            public string? MethodName { get; set; }

            public bool IsAutomaticJob { get; set; }

            public EMethodType MethodType { get; set; }
        }

        [BindProperty]
        public List<ApplicationMethodReadModel> ApplicationMethods { get; set; }

        [BindProperty]
        public CreateTeamApplicationModel Create { get; set; }


        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }
        public async Task<PageResult> OnGet(Guid ownerId, Guid teamId, Guid applicationId)
        {
            TeamId = teamId;
            OwnerId = ownerId;
            ApplicationId = applicationId;

            ApplicationMethods = await _applicationRepasitory.GetAllApplicationMethodsAsync(applicationId);

            var owner = await _ownerRepasitory.GetOwnerByIdAsync(OwnerId);
            var team = owner.Teams.FirstOrDefault(p => p.TeamIdentificator == teamId.ToString());

            TeamApplicationId = team.TeamApplications.First().Id;

            var teamApplicationMetod = team.TeamApplications.FirstOrDefault(p => p.ApplicationId == applicationId);

            var strings = new List<string>();
            if (teamApplicationMetod.TeamApplicationMethods != null && teamApplicationMetod.TeamApplicationMethods.Count > 0)
            {
                foreach (var item in teamApplicationMetod.TeamApplicationMethods)
                {
                    strings.Add(item.ApplicationMethodId.ToString());
                }
            }
            if (strings.Count > 0)
            {
                SelectedApplicationMethodIds = strings.ToArray();
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
                    var teamApplicationMetod = team.TeamApplications.FirstOrDefault(p => p.ApplicationId == ApplicationId);

                    if (SelectedApplicationMethodIds != null && SelectedApplicationMethodIds.Count() > 0)
                    {
                        teamApplicationMetod.TeamApplicationMethods.Clear();
                        var resultUpdateOwner = await _ownerRepasitory.UpdateOwnerAsync(owner);
                        if (resultUpdateOwner != HttpStatusCode.OK)
                            Errors.Add(new ServiceError { Code = "Error during Update Owner", Description = resultUpdateOwner.ToString() });


                        foreach (var applicationMethodId in SelectedApplicationMethodIds)
                        {
                            var teamApplicationMethod = new TeamApplicationMethodJsonModel
                            {
                                TeamApplicationId = TeamApplicationId.ToString(),
                                ApplicationMethodId = applicationMethodId,
                            };


                            var result = await _teamApplicationMethodRepasitory.AddTeamApplicationMethodAsync(teamApplicationMethod);
                            if (result != HttpStatusCode.OK)
                            {
                                Errors.Add(new ServiceError { Code = "Error during Add TeamApplication", Description = result.ToString() });
                            }
                        }

                        
                    }
                    ///Management/TeamApplicationMethods/@Model.OwnerId/@Model.TeamId/@item.Id/@item.ApplicationId
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
