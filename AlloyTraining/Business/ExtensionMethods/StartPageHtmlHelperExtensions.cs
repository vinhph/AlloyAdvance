using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.ServiceLocation;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AlloyStartPage = AlloyTraining.Models.Pages.StartPage;

namespace AlloyTraining.Business.ExtensionMethods
{
    public static class StartPageHtmlHelperExtensions
    {
        public static AlloyStartPage GetStartPage(
            this HtmlHelper html)
        {
            var loader = ServiceLocator.Current.GetInstance<IContentLoader>();
            return loader.Get<AlloyStartPage>(ContentReference.StartPage);
        }

        public static IEnumerable<PageData> GetStartPageChildren(
            this HtmlHelper html)
        {
            var loader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var children = loader.GetChildren<PageData>(
                ContentReference.StartPage);
            return FilterForVisitor.Filter(children).Cast<PageData>()
                .Where(page => page.VisibleInMenu);
        }
    }
}