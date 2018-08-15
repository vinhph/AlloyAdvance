using AlloyTraining.Business.ExtensionMethods;
using AlloyTraining.Models.Pages;
using EPiServer.Core;

namespace AlloyTraining.Models.ViewModels
{
    public class DefaultPageViewModel<T> 
        : IPageViewModel<T> where T : SitePageData
    {
        public DefaultPageViewModel(T currentPage)
        {
            CurrentPage = currentPage;
            Section = currentPage.ContentLink.GetSection();
        }

        public T CurrentPage { get; set; }
        public IContent Section { get; set; }
    }
}