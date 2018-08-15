using AlloyTraining.Models.Pages;
using EPiServer.Core;

namespace AlloyTraining.Models.ViewModels
{
    public class PreviewPageViewModel : DefaultPageViewModel<SitePageData>
    {
        public PreviewPageViewModel(SitePageData currentPage, IContent contentToPreview) : base(currentPage)
        {
            this.PreviewArea = new ContentArea();
            this.PreviewArea.Items.Add(new ContentAreaItem { ContentLink = contentToPreview.ContentLink });
        }

        public ContentArea PreviewArea { get; set; }
    }
}