using EPiServer.DataAnnotations;

namespace AlloyAdvanced.Models.Pages
{
    [SiteContentType(DisplayName = "Send Notification", 
        GroupName = Global.GroupNames.Specialized,
        GUID = "930d3fb0-946c-45c3-8453-6d8ab03760b2", 
        Description = "Use to send a notification to another Episerver user.")]
    [SiteImageUrl]
    [AvailableContentTypes(IncludeOn = new[] { typeof(StartPage) })]
    public class SendNotificationPage : SitePageData
    {
    }
}