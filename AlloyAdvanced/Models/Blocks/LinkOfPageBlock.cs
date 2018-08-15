using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace AlloyAdvanced.Models.Blocks
{
    [SiteContentType(DisplayName = "LinkOfPageBlock", GUID = "6d83e541-f680-4fc4-9efd-1497ee600954", Description = "Chua cac link")]
    public class LinkOfPageBlock : BlockData
    {
        [CultureSpecific]
        [Display(
            Name = "Name",
            Description = "Name field's description",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string Name { get; set; }

        [Display(
            Name = "Link",
            Description = "Link",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        public virtual string Link { get; set; }

    }
}