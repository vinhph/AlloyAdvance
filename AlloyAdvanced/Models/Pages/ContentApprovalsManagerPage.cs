using EPiServer.DataAnnotations;

namespace AlloyAdvanced.Models.Pages
{
    [SiteContentType(
        DisplayName = "Content Approvals Manager", 
        Description = "This page demonstrates how to programmatically manage content approvals.")]
    [SiteImageUrl]
    [AvailableContentTypes(IncludeOn = new[] { typeof(StartPage) })]
    public class ContentApprovalsManagerPage : SitePageData
    {

    }
}