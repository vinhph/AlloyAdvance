using AlloyTraining.Models.Blocks;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace AlloyTraining.Models.Pages
{
    [ContentType(DisplayName = "News Landing",
        GroupName = SiteGroupNames.News,
        Description = "Use this as a landing page for a list of news articles.")]
    [SitePageIcon]
    public class NewsLandingPage : StandardPage
    {
        [Display(Name = "News listing", Order = 315)]
        public virtual ListingBlock NewsListing { get; set; }
    }
}