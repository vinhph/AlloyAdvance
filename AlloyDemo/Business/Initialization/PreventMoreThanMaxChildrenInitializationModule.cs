using AlloyDemo.Models.Pages;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using System.Linq;

namespace AlloyDemo.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class PreventMoreThanMaxChildrenInitializationModule : IInitializableModule
    {
        private bool initialized = false;
        private IContentEvents events;
        private IContentLoader loader;
        private const int maxChildren = 8;

        public void Initialize(InitializationEngine context)
        {
            if (!initialized)
            {
                loader = context.Locate.ContentLoader();
                events = context.Locate.ContentEvents();
                events.CreatingContent += Events_CreatingContent;
                initialized = true;
            }
        }

        private void Events_CreatingContent(object sender, ContentEventArgs e)
        {
            var sitepage = e.Content as SitePageData;
            if (sitepage != null)
            {
                var children = loader.GetChildren<IContent>(sitepage.ParentLink);
                if (children.Count() >= maxChildren)
                {
                    e.CancelAction = true;
                    e.CancelReason = $"Cannot create a new page if the parent has {maxChildren} or more children.";
                }
            }
        }

        public void Uninitialize(InitializationEngine context)
        {
            if (initialized)
            {
                events.CreatingContent -= Events_CreatingContent;
            }
        }
    }
}