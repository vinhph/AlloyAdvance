using EPiServer.Core;
using EPiServer.Web.Mvc;
using System.Web.Mvc;

namespace AlloyTraining.Controllers
{
    public class ContentFolderController : PartialContentController<ContentFolder>
    {
        public override ActionResult Index(ContentFolder currentContent)
        {
            return PartialView(currentContent);
        }
    }
}
