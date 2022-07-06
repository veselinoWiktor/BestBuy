using BestCameraApp.Services;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BestCameraApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationService dataReaderService;

        [BindProperty]
        public Dictionary<string, List<Picture>> Pictures { get; set; }

        public IndexModel(ApplicationService dataReaderService)
        {
            this.dataReaderService = dataReaderService;
        }

        public async Task<IActionResult> OnGet()
        {
            Pictures = await dataReaderService.GetData();
            return Page();
        }
        //public async Task<IActionResult> OnGetAjax()
        //{
        //    var data = await dataReaderService.GetData();
        //    var json = JsonConvert.SerializeObject(data);
        //    return Content(json);
        //}
    }
}