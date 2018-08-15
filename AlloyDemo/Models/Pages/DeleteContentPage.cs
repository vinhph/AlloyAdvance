using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace AlloyDemo.Models.Pages
{
    [ContentType(DisplayName = "Delete Content", 
        GUID = "0f01522d-fa66-4dff-92f3-e395f2ed4f36", 
        GroupName = Global.GroupNames.Specialized,
        Description = "Use this to soft or hard delete content.")]
    [SiteImageUrl]
    [AvailableContentTypes(IncludeOn = new[] { typeof(StartPage) })]
    public class DeleteContentPage : SitePageData
    {
    }
}