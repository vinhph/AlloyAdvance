using AlloyDemo.Models.Pages;
using AlloyDemo.Models.ViewModels;
using EPiServer;
using EPiServer.Core;
using System.Web.Mvc;

namespace AlloyDemo.Controllers
{
    public class FAQListPageController : PageControllerBase<FAQListPage>
    {
        private readonly IContentRepository repo;

        public FAQListPageController(IContentRepository repo)
        {
            this.repo = repo;
        }

        public ActionResult Index(FAQListPage currentPage)
        {
            var viewmodel = PageViewModel.Create(currentPage);
            var faqs = repo.GetChildren<FAQItemPage>(currentPage.ContentLink);
            viewmodel.CurrentPage.FAQItems = faqs;
            return View(viewmodel);
        }

        public ActionResult CreateFAQItem(FAQListPage currentPage, string question)
        {
            var faqItem = repo.GetDefault<FAQItemPage>(currentPage.ContentLink);

            // if someone is logged in then CreatedBy and ChangedBy will be set,
            // otherwise they will be empty string which is shown as "installer"
            if (string.IsNullOrEmpty(faqItem.CreatedBy))
                faqItem.CreatedBy = "Anonymous";
            if (string.IsNullOrEmpty(faqItem.ChangedBy))
                faqItem.ChangedBy = "Anonymous";

            faqItem.Question = new XhtmlString(question);
            faqItem.Name = "Q. " + question;
            repo.Save(faqItem, 
                EPiServer.DataAccess.SaveAction.CheckOut, 
                EPiServer.Security.AccessLevel.Read);

            return RedirectToAction("Index");
        }
    }
}