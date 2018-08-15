using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace AlloyDemo.Models.Pages
{
    [ContentType(DisplayName = "FAQ Item", 
        Description = "A data page for an FAQ item (cannot be created by editors).",
        AvailableInEditMode = false)]
    public class FAQItemPage : PageData
    {
        [Display(Name = "Question", Order = 10)]
        public virtual XhtmlString Question { get; set; }

        [Display(Name = "Answer", Order = 20)]
        public virtual XhtmlString Answer { get; set; }
    }
}