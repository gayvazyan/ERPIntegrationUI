using ERP.IntegrationUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ERP.IntegrationUI.Pages.Management.Applications
{
    public class IndexModel : PageModel
    {
        public IndexModel()

        {
            Input = new InputModel();
            InputList = new List<InputModel>();
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public List<InputModel> InputList = new List<InputModel>();
        public class InputModel : ApplicationModel { }

        //START Part Paging
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<ApplicationModel> ApplicationList { get; set; }

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;
        //END Part Paging


        protected async void PrepareData()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://app-erp-integration-dev.azurewebsites.net/api/v1.0/management/getAllApplications");
            var result = await response.Content.ReadAsStringAsync();

            //var myDeserializedClass = JsonConvert.DeserializeObject<List<ApplicationModel>>(myJsonResponse);

            //if (Input.Title != null)
            //{
            //    aboutList = aboutList.Where(p => p.Title.ToUpper().Contains(Input.Title.ToUpper())).ToList();
            //}

            //if (Input.Name != null)
            //{
            //    aboutList = aboutList.Where(p => p.Name.ToUpper().Contains(Input.Name.ToUpper())).ToList();
            //}


            //AboutList = _aboutRepasitory.GetPaginatedResult(aboutList, CurrentPage, PageSize);


            //Count = _aboutRepasitory.GetCount(aboutList);

            //InputList = AboutList.Select(p =>
            //{
            //    return new InputModel()
            //    {
            //        Id = p.Id,
            //        Title = p.Title,
            //        TitleRu = p.TitleRu,
            //        TitleEn = p.TitleEn,
            //        Name = p.Name,
            //        NameRu = p.NameRu,
            //        NameEn = p.NameEn,
            //        Context = p.Context,
            //        ContextRu = p.ContextRu,
            //        ContextEn = p.ContextEn,
            //        ImageUrl = p.ImageUrl,
            //        Visible = p.Visible,
            //    };
            //}).ToList();
        }
        public void OnGet()
        {
            PrepareData();
        }

        public ActionResult OnPost()
        {
            PrepareData();
            return Page();
        }
    }
}
