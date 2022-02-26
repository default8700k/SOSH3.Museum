using Microsoft.AspNetCore.Mvc;
using SOSH3.Museum.WebApplication.ViewModels;

namespace SOSH3.Museum.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index(string page)
        {
            return View(
                new IndexViewModel
                {
                    Robots = "noindex, nofollow",
                    Title = "title",
                    Description = "no description",
                    Content = $"page: {page}"
                }
            );
        }
    }
}
