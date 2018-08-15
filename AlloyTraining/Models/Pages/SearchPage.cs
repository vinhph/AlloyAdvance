using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace AlloyTraining.Models.Pages
{
    [ContentType(DisplayName = "Search", 
        GroupName = SiteGroupNames.Specialized,
        Order = 30,
        Description = "Use this to enable visitors to search for pages and media on the site.")]
    [SiteSearchIcon]
    public class SearchPage : SitePageData
    {
    }
}