using System.Collections.Generic;
using EPiServer.Core;
using AlloyAdvanced.Models.Pages;
using AlloyAdvanced.Models.NorthwindEntities;

namespace AlloyAdvanced.Models.ViewModels
{
    // This view model is passed to BOTH an instance of a NorthwindPage to
    // display a list of categories, AND to a Category detail view. 
    public class NorthwindPageViewModel : IPageViewModel<NorthwindPage>
    {
        public NorthwindPageViewModel(NorthwindPage currentPage)
        {
            this.CurrentPage = currentPage;
            this.CategoryLinks = new Dictionary<string, string>();
        }

        // this property is used for showing a list of categories
        // in ~/Views/NorthwindPage/Index.cshtml
        public Dictionary<string, string> CategoryLinks { get; set; }

        public NorthwindPage CurrentPage { get; set; }

        // this property is used for showing details of a category
        // in ~/Views/Category/Index.cshtml
        public Category Category { get; set; }

        public LayoutModel Layout { get; set; }

        public IContent Section { get; set; }
    }
}