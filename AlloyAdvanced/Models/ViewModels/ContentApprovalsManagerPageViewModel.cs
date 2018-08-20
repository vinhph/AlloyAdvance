using AlloyAdvanced.Models.Pages;
using EPiServer.Approvals.ContentApprovals;

namespace AlloyAdvanced.Models.ViewModels
{
    public class ContentApprovalsManagerPageViewModel : PageViewModel<ContentApprovalsManagerPage>
    {
        public ContentApprovalDefinition ApprovalDefinition { get; set; }
        public ContentApproval Approval { get; set; }

        public ContentApprovalsManagerPageViewModel(ContentApprovalsManagerPage currentPage) : base(currentPage)
        {
        }
    }
}