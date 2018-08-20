using EPiServer.DataAnnotations;
using EPiServer.Notification;
using System.Collections.Generic;

namespace AlloyAdvanced.Models.Pages
{
    [SiteContentType(DisplayName = "Notifications",
        GroupName = Global.GroupNames.Specialized,
        GUID = "b08ab39c-8113-4c86-aaf7-c92284507542",
        Description = "Use to manage user notifications.")]
    [SiteImageUrl]
    [AvailableContentTypes(IncludeOn = new[] { typeof(StartPage) })]
    public class NotificationsPage : SitePageData
    {
        [Ignore]
        public virtual Dictionary<string, PagedUserNotificationMessageResult> Notifications { get; set; }
    }
}