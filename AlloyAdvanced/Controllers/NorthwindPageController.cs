using System.Web.Mvc;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using AlloyAdvanced.Models.Pages;
using AlloyAdvanced.Models.NorthwindEntities;
using AlloyAdvanced.Models.ViewModels;

namespace AlloyAdvanced.Controllers
{
    public class NorthwindPageController : PageControllerBase<NorthwindPage>
    {
        public ActionResult Index(NorthwindPage currentPage)
        {
            var model = new NorthwindPageViewModel(currentPage);

            // connect to the Northwind database through the domain model
            // to fetch a list of all categories and pass it to the Northwind page
            // instance to show a list of category names and links generated
            // by the partial router. 

            var db = new Northwind();

            // we do not need to track changes or 
            // automatically load related entities
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            foreach (Category category in db.Categories)
            {
                model.CategoryLinks.Add(category.CategoryName, 
                    UrlResolver.Current.GetVirtualPathForNonContent(
                        partialRoutedObject: category, 
                        language: null, 
                        virtualPathArguments: null)
                        .GetUrl());
            }
            return View(model);
        }
    }
}