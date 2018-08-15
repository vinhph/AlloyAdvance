﻿using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace AlloyTraining.Models.Pages
{
    public abstract class SitePageData : PageData
    {
        [CultureSpecific]
        [Display(Name = "Meta title",
            GroupName = SiteTabNames.SEO, Order = 100)]
        [StringLength(65, MinimumLength = 5)]
        public virtual string MetaTitle { get; set; }

        [CultureSpecific]
        [Display(Name = "Meta keywords",
            GroupName = SiteTabNames.SEO, Order = 200)]
        public virtual string MetaKeywords { get; set; }

        [CultureSpecific]
        [Display(Name = "Meta description",
            GroupName = SiteTabNames.SEO, Order = 300)]
        [UIHint(UIHint.Textarea)] // multi-row text editor
        public virtual string MetaDescription { get; set; }

        [Display(Name = "Page image",
            GroupName = SystemTabNames.Content, Order = 100)]
        [UIHint(UIHint.Image)] // filters to only show images
        public virtual ContentReference PageImage { get; set; }

        [CultureSpecific]
        [Display(Name = "Teaser text",
            GroupName = SystemTabNames.Content, Order = 200)]
        [UIHint(UIHint.Textarea)]
        public virtual string TeaserText { get; set; }
    }
}