using AlloyTraining.Models.Pages;
using AlloyTraining.Models.ViewModels;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using System.Web.Mvc;

namespace AlloyTraining.Controllers
{
    [TemplateDescriptor(Inherited = true, 
        Tags = new[] { SiteTags.Full }, AvailableWithoutTag = true)]
    public class AllPagesFullPartialController 
        : PartialContentController<SitePageData>
    {
        public override ActionResult Index(SitePageData currentPage)
        {
            var viewmodel = new DefaultPageViewModel<SitePageData>(currentPage);
            return PartialView(viewName: SiteTags.Full, model: viewmodel);
        }
    }

    [TemplateDescriptor(Inherited = true, 
        Tags = new[] { SiteTags.Wide }, AvailableWithoutTag = false)]
    public class AllPagesWidePartialController
        : PartialContentController<SitePageData>
    {
        public override ActionResult Index(SitePageData currentPage)
        {
            var viewmodel = new DefaultPageViewModel<SitePageData>(currentPage);
            return PartialView(viewName: SiteTags.Wide, model: viewmodel);
        }
    }

    [TemplateDescriptor(Inherited = true, 
        Tags = new[] { SiteTags.Narrow }, AvailableWithoutTag = false)]
    public class AllPagesNarrowPartialController
        : PartialContentController<SitePageData>
    {
        public override ActionResult Index(SitePageData currentPage)
        {
            var viewmodel = new DefaultPageViewModel<SitePageData>(currentPage);
            return PartialView(viewName: SiteTags.Narrow, model: viewmodel);
        }
    }
}