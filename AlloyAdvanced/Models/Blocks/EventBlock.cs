using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace AlloyAdvanced.Models.Blocks
{
    [SiteContentType(
        DisplayName = "Event",
        Description = "Events have a title and description, and where and when they take place.")]
    [SiteImageUrl]
    public class EventBlock : BlockData
    {
        [CultureSpecific]
        [Display(
            Name = "Title",
            Description = "The title should be short.",
            Order = 10)]
        public virtual string Title { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Description",
            Description = "The description should be longer.",
            Order = 20)]
        public virtual string Description { get; set; }

        [Display(
            Name = "Where",
            Description = "This is the location of the event.",
            Order = 30)]
        public virtual string Where { get; set; }

        [Display(
            Name = "When",
            Description = "This is when the event happens.",
            Order = 40)]
        public virtual DateTime? When { get; set; }
    }
}