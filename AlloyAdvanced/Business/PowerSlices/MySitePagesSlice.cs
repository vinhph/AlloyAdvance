using AlloyAdvanced.Models.Pages;
using EPiServer.Cms.Shell.UI.Rest.ContentQuery;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ContentQuery;
using PowerSlice;
using System.Web;

namespace AlloyAdvanced.Business.PowerSlices
{
    [ServiceConfiguration(typeof(IContentQuery)),
    ServiceConfiguration(typeof(IContentSlice))]
    public class MySitePagesSlice : ContentSliceBase<SitePageData>
    {
        public override string Name
        {
            get { return "Site Pages I Created"; }
        }

        protected override ITypeSearch<SitePageData> Filter(
            ITypeSearch<SitePageData> searchRequest,
            ContentQueryParameters parameters)
        {
            var userName = HttpContext.Current.User.Identity.Name;

            return searchRequest.Filter(x => x.MatchTypeHierarchy(
                typeof(IChangeTrackable)) &
                ((IChangeTrackable)x).CreatedBy.Match(userName));
        }
    }
}