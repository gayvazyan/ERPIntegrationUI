using ERP.IntegrationUI.Models.Integration;
using ERP.IntegrationUI.Repositories.Owner;
using lega.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ERP.IntegrationUI.Pages.Management.Owners
{
    public class CreateModel : PageModel
    {
        private readonly IOwnerRepasitory _ownerRepasitory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateModel(IHttpContextAccessor httpContextAccessor, IOwnerRepasitory ownerRepasitory)
        {
            _ownerRepasitory = ownerRepasitory;
            _httpContextAccessor = httpContextAccessor;
            Create = new CreateOwnerModel();
        }

        public class CreateOwnerModel

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

            //public List<TeamReadModel> Teams { get; set; }

        }

        [BindProperty]
        public CreateOwnerModel Create { get; set; }


        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }
        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var owner = new OwnerReadModel
                    {
                        //Id = Create.Id,
                        Name = Create.Name,
                        AdditionalData = Create.AdditionalData,
                        Description = Create.Description,
                        IsActive = Create.IsActive,
                        FirstName = Create.FirstName,
                        LastName = Create.LastName,
                    };

                    var result = await _ownerRepasitory.AddOwnerAsync(owner);

                    if (result == HttpStatusCode.OK)
                        return RedirectToPage("/Management/Owners/Index");
                    else
                        Errors.Add(new ServiceError { Code = "Error during Add Owner", Description = result.ToString() });


                }
                catch (Exception ex)
                {
                    Errors.Add(new ServiceError { Code = "Error during Add Owner", Description = ex.Message });
                }
            }
            return Page();
        }
    }
}
