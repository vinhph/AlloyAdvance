using AlloyAdvanced.Business.Validation;
using AlloyAdvanced.Models.Blocks;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlloyAdvanced.Models.Pages
{
    /// <summary>
    /// Used for the site's start page and also acts as a container for site settings
    /// </summary>
    [SiteContentType(
        GUID = "19671657-B684-4D95-A61F-8DD4FE60D559",
        GroupName = Global.GroupNames.Specialized)]
    [SiteImageUrl]
    [AvailableContentTypes(
        Availability.Specific,
        Include = new[] { typeof(ContainerPage), typeof(ProductPage), typeof(StandardPage), typeof(ISearchPage), typeof(LandingPage), typeof(ContentFolder) }, // Pages we can create under the start page...
        ExcludeOn = new[] { typeof(ContainerPage), typeof(ProductPage), typeof(StandardPage), typeof(ISearchPage), typeof(LandingPage) })] // ...and underneath those we can't create additional start pages
    public class StartPage : SitePageData
    {
        [AllowedTypes(typeof(ShippersPage))]
        public virtual ContentReference Shippers { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 320)]
        [CultureSpecific]
        public virtual ContentArea MainContentArea { get; set; }

        [Display(GroupName = Global.GroupNames.SiteSettings, Order = 300)]
        public virtual LinkItemCollection ProductPageLinks { get; set; }

        [Display(GroupName = Global.GroupNames.SiteSettings, Order = 350)]
        public virtual LinkItemCollection CompanyInformationPageLinks { get; set; }

        [Display(GroupName = Global.GroupNames.SiteSettings, Order = 400)]
        public virtual LinkItemCollection NewsPageLinks { get; set; }

        [Display(GroupName = Global.GroupNames.SiteSettings, Order = 450)]
        public virtual LinkItemCollection CustomerZonePageLinks { get; set; }

        [Display(GroupName = Global.GroupNames.SiteSettings)]
        public virtual PageReference GlobalNewsPageLink { get; set; }

        [Display(GroupName = Global.GroupNames.SiteSettings)]
        public virtual PageReference ContactsPageLink { get; set; }

        [Display(GroupName = Global.GroupNames.SiteSettings)]
        public virtual PageReference SearchPageLink { get; set; }

        [Display(GroupName = Global.GroupNames.SiteSettings)]
        public virtual SiteLogotypeBlock SiteLogotype { get; set; }

        //[StrongPassword(
        //    MinimumTotalCharacters = 12,
        //    MinimumDigitCharacters = 2,
        //    MinimumSpecialCharacters = 3)]
        //public virtual string Password { get; set; }
        //public virtual DateTime StartDate { get; set; }
        //public virtual DateTime EndDate { get; set; }

        // The start page should show lists of events (instances of an EventBlock).
        // There are three common ways to store this:
        // 1. A content area restricted to contain only EventBlocks.
        // 2. An IList of ContentReferences restricted to contain only EventBlocks.
        // 3. A content reference to a block folder.

        [Display(
            GroupName = "Events",
            Name = "Event list as content area",
            Order = 20)]
        [AllowedTypes(typeof(EventBlock))]
        // implicitly disallows everything else
        public virtual ContentArea EventListAsContentArea { get; set; }

        [Display(
            GroupName = "Events",
            Name = "Event list as a list of content references",
            Order = 30)]
        [AllowedTypes(typeof(EventBlock))]
        public virtual IList<ContentReference> EventListAsListOfContentReferences { get; set; }

        [Display(
            GroupName = "Events",
            Name = "Event list as reference to assets folder",
            Order = 40)]
        [UIHint(UIHint.AssetsFolder)]
        public virtual ContentReference EventListAsReferenceToAssetsFolder { get; set; }

    }
}
