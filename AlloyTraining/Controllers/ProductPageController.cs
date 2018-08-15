using AlloyTraining.Models.Pages;
using AlloyTraining.Models.ViewModels;
using EPiServer;
using System.Web.Mvc;

namespace AlloyTraining.Controllers
{
    public class ProductPageController 
        : PageControllerBase<ProductPage>
    {
        public ProductPageController(IContentLoader loader) : base(loader)
        {
        }

        public ActionResult Index(ProductPage currentPage)
        {
            var viewmodel = new DefaultPageViewModel<ProductPage>(currentPage);
            return View(viewmodel);
        }
    }
}