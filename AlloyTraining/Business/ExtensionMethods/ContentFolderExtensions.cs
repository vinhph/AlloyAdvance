using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using System;
using System.Linq;

namespace AlloyTraining.Business.ExtensionMethods
{
    public static class ContentFolderExtensions
    {
        public static int GetItemCount(this ContentFolder folder)
        {
            using (var cache = new ContentCacheScope { SlidingExpiration = TimeSpan.Zero })
            {
                var loader = ServiceLocator.Current.GetInstance<IContentLoader>();
                var items = loader.GetChildren<IContent>(folder.ContentLink);
                return items.Count();
            }
        }
    }
}