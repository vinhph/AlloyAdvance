using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using System;
using System.ComponentModel.DataAnnotations;

namespace AlloyAdvanced.Models.Blocks
{
    [SiteContentType(DisplayName = "Person",
        Description = "A block of information about a person.")]
    [SiteImageUrl]
    public class PersonBlock : BlockData
    {
        [Display(
            Name = "First Name",
            Description = "The person's given name, e.g. Bob.",
            Order = 10)]
        public virtual string FirstName { get; set; }

        [Display(
            Name = "Last Name",
            Description = "The person's surname name, e.g. Smith.",
            Order = 20)]
        public virtual string LastName { get; set; }

        [Display(
            Name = "Birth Date",
            Description = "The person's date of birth.",
            Order = 30)]
        public virtual DateTime? BirthDate { get; set; }

        [Display(
            Name = "Summary",
            Description = "A brief synopsis of the person's background or accomplishments.",
            Order = 40)]
        [UIHint(UIHint.Textarea)]
        public virtual string Summary { get; set; }
    }
}