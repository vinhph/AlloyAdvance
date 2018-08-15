using AlloyTraining.Models.Pages;
using EPiServer;
using EPiServer.Web.Mvc;
using System.Web.Mvc;
using System.Web.Security;

namespace AlloyTraining.Controllers
{
    public abstract class PageControllerBase<T>
        : PageController<T> where T : SitePageData
    {
        protected readonly IContentLoader loader;

        public PageControllerBase(IContentLoader loader)
        {
            this.loader = loader;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}