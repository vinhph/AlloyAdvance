using AlloyTraining.Models.Blocks;
using AlloyTraining.Models.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AlloyTraining.Controllers
{
    public class ListingBlockController : BlockController<ListingBlock>
    {
        private readonly IContentLoader loader;

        public ListingBlockController(IContentLoader loader)
        {
            this.loader = loader;
        }

        public override ActionResult Index(ListingBlock currentBlock)
        {
            var viewmodel = new ListingBlockViewModel
            {
                Heading = currentBlock.Heading
            };

            if (currentBlock.ShowChildrenOfThisPage != null)
            {
                IEnumerable<PageData> children = loader.GetChildren<PageData>(currentBlock.ShowChildrenOfThisPage);

                // Remove pages:
                // 1. that are not published
                // 2. that the visitor does not have Read access to
                // 3. that do not have a page template
                IEnumerable<IContent> filteredChildren = FilterForVisitor.Filter(children);

                // 4. that do not have "Display in navigation" selected
                viewmodel.Pages = filteredChildren.Cast<PageData>()
                    .Where(page => page.VisibleInMenu);
            }

            return PartialView(viewmodel);
        }
    }
}