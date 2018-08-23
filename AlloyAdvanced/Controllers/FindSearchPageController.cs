using AlloyAdvanced.Models.Pages;
using AlloyAdvanced.Models.ViewModels;
using EPiServer.Find;
using EPiServer.Find.Framework;
using EPiServer.Framework.DataAnnotations;
using System.Web.Mvc;
using EPiServer.Find.Framework.Statistics;

namespace AlloyAdvanced.Controllers
{
    [TemplateDescriptor(Default = true)]
    public class FindSearchPageController : PageControllerBase<SearchPage>
    {
        public ActionResult Index(SearchPage currentPage, string q)
        {
            var model = new FindSearchPageViewModel(currentPage, q);

            if (!string.IsNullOrWhiteSpace(q))
            {
                var unifiedSearch = SearchClient.Instance.UnifiedSearchFor(q);
                model.Results = unifiedSearch.Track().ApplyBestBets().GetResult();
            }
            return View(model);
        }
    }
}