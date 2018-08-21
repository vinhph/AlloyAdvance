using EPiServer.DataAnnotations;

namespace AlloyAdvanced.Models.Pages
{
    // If a content editor creates an instance of this page type in the page
    // tree then it becomes the entry point for the Northwind partial router. 
    [SiteContentType(DisplayName = "Northwind", 
        Description = "A page that lists categories from the Northwind database.")]
    [SiteImageUrl]
    [AvailableContentTypes(IncludeOn = new[] { typeof(StartPage) })]
    public class NorthwindPage : SitePageData
    {
    }
}