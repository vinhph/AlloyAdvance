using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using AlloyAdvanced.Models.Blocks;
using EPiServer.Web.Routing;
using AlloyAdvanced.Models.ViewModels;

namespace AlloyAdvanced.Controllers
{
    public class ShareThisBlockController : BlockController<ShareThisBlock>
    {
        private readonly IPageRouteHelper pageRouteHelper;
        private readonly UrlResolver urlResolver;

        public ShareThisBlockController(IPageRouteHelper pageRouteHelper, UrlResolver urlResolver)
        {
            this.pageRouteHelper = pageRouteHelper;
            this.urlResolver = urlResolver;
        }

        public override ActionResult Index(ShareThisBlock currentBlock)
        {
            var model = new ShareThisBlockViewModel();
            PageData page = pageRouteHelper.Page;
            model.FriendlyUrl = UriSupport.AbsoluteUrlBySettings(urlResolver.GetUrl(page.ContentLink));
            model.Settings = currentBlock;
            return PartialView(model);
        }
    }
}
