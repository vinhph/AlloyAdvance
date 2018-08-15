using AlloyTraining.Models.Pages;
using AlloyTraining.Models.ViewModels;
using EPiServer;
using System.Web.Mvc;

namespace AlloyTraining.Controllers
{
    public class NewsLandingPageController : PageControllerBase<NewsLandingPage>
    {
        public NewsLandingPageController(IContentLoader loader) : base(loader)
        {
        }

        public ActionResult Index(NewsLandingPage currentPage)
        {
            var viewmodel = new DefaultPageViewModel<NewsLandingPage>(currentPage);
            return View(viewmodel);
        }
    }
}