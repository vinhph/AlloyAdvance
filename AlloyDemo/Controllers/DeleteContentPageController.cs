using AlloyDemo.Models.Pages;
using AlloyDemo.Models.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.Security;
using System.Web.Mvc;

namespace AlloyDemo.Controllers
{
    public class DeleteContentPageController : PageControllerBase<DeleteContentPage>
    {
        private readonly IContentRepository repo = null;

        public DeleteContentPageController(IContentRepository repo)
        {
            this.repo = repo;
        }

        public ActionResult Index(DeleteContentPage currentPage)
        {
            var viewmodel = PageViewModel.Create(currentPage);
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Delete(DeleteContentPage currentPage, 
            ContentReference contentReference, string hardDelete)
        {
            string name = repo.Get<IContent>(contentReference).Name;

            if (hardDelete == "on")
            {
                repo.Delete(contentReference, forceDelete: true, 
                    access: AccessLevel.NoAccess);

                ViewData["message"] = $"'{name}' was deleted permanently.";
            }
            else
            {
                repo.Move(contentReference, destination: ContentReference.WasteBasket,
                    requiredSourceAccess: AccessLevel.NoAccess,
                    requiredDestinationAccess: AccessLevel.NoAccess);

                ViewData["message"] = $"'{name}' was moved to trash.";
            }
            var viewmodel = PageViewModel.Create(currentPage);
            return View("Index", viewmodel);
        }
    }
}