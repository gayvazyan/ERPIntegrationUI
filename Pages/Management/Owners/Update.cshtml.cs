using ERP.IntegrationUI.Models.Integration;
using ERP.IntegrationUI.Repositories.Owner;
using lega.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ERP.IntegrationUI.Pages.Management.Owners
{
    public class UpdateModel : PageModel
    {
        private readonly IOwnerRepasitory _ownerRepasitory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateModel(IHttpContextAccessor httpContextAccessor, IOwnerRepasitory ownerRepasitory)

        {
            _ownerRepasitory = ownerRepasitory;
            _httpContextAccessor = httpContextAccessor;
            Update = new UpdateContactModel();
        }


        public class UpdateContactModel
        {
            public Guid Id { get; set; }

            [Required]
            public string Name { get; set; }
            [Required]
            public string Description { get; set; }
            public bool IsActive { get; set; }
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            public string? AdditionalData { get; set; }

           // public List<TeamReadModel> Teams { get; set; }
        }

        [BindProperty]
        public UpdateContactModel Update { get; set; }

        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }

        protected async Task PrepareData(Guid id)
        {
            var result = await _ownerRepasitory.GetOwnerByIdAsync(id);

            if (result != null)
            {
                Update.Id = result.Id;
                Update.Name = result.Name;
                Update.Description = result.Description;
                Update.IsActive = result.IsActive;
                Update.AdditionalData = result.AdditionalData;
                Update.FirstName = result.FirstName;
                Update.LastName = result.LastName;
                Update.AdditionalData = result.AdditionalData;
                Update.LastName = result.LastName;
                //Update.Teams = result.Teams;
            }
        }
        public async Task<ActionResult> OnGet(Guid id)
        {
            await PrepareData(id);
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var owner = await _ownerRepasitory.GetOwnerByIdAsync(Update.Id);

                    owner.Name = Update.Name;
                    owner.AdditionalData = Update.AdditionalData;
                    owner.FirstName = Update.FirstName;
                    owner.LastName = Update.LastName;
                    owner.Description = Update.Description;
                    owner.IsActive = Update.IsActive;
                    //owner.Teams = Update.Teams;


                    var result = await _ownerRepasitory.UpdateOwnerAsync(owner);
                    if (result == HttpStatusCode.OK)
                        return Redirect("/Management/Owners/Index");
                    else
                        Errors.Add(new ServiceError { Code = "Error during Update Owner", Description = result.ToString() });
                }
                catch (Exception ex)
                {
                    Errors.Add(new ServiceError { Code = "Error during Update Owner", Description = ex.Message });
                    PrepareData(Update.Id);
                }
            }
            else
            {
                PrepareData(Update.Id);
            }

            return Page();
        }
    }
}
