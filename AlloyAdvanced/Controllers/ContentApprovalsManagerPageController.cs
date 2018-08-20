using AlloyAdvanced.Models.Pages;
using AlloyAdvanced.Models.ViewModels;
using EPiServer;
using EPiServer.Approvals;
using EPiServer.Approvals.ContentApprovals;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAccess;
using EPiServer.Security;
using EPiServer.Shell.Security;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AlloyAdvanced.Controllers
{
    public class ContentApprovalsManagerPageController : PageControllerBase<ContentApprovalsManagerPage>
    {
        private const string admins = "WebAdmins";
        private const string editors = "WebEditors";

        private const string userName1 = "Alice";
        private const string userName2 = "Bob";
        private const string userName3 = "Eve";

        private const string password = "Pa$$w0rd";
        private const string emailBase = "@alloy.com";
        
        private readonly IApprovalDefinitionRepository repoDefinitions;
        private readonly IContentRepository repoContent;
        private readonly IApprovalRepository repoApprovals;
        private readonly IApprovalEngine engine;

        public ContentApprovalsManagerPageController(
            IApprovalDefinitionRepository repoDefinitions,
            IContentRepository repoContent,
            IApprovalRepository repoApprovals,
            IApprovalEngine engine,
            UIRoleProvider roles,
            UIUserProvider users,
            IContentSecurityRepository repoSecurity)
        {
            this.repoDefinitions = repoDefinitions;
            this.repoContent = repoContent;
            this.repoApprovals = repoApprovals;
            this.engine = engine;

            // if the editors role does not exist, create it and assign access rights
            if (!roles.RoleExists(editors))
            {
                roles.CreateRole(editors);

                var permissions = repoSecurity.Get(ContentReference.RootPage).CreateWritableClone() as IContentSecurityDescriptor;
                permissions.AddEntry(new AccessControlEntry(editors, 
                    AccessLevel.Create | AccessLevel.Edit | AccessLevel.Delete | AccessLevel.Read | AccessLevel.Publish));
                repoSecurity.Save(ContentReference.RootPage, permissions, SecuritySaveType.Replace);
            }

            // create three users and add them to roles

            UIUserCreateStatus status;
            IEnumerable<string> errors = Enumerable.Empty<string>();

            if (users.GetUser(userName1) == null)
            {
                users.CreateUser(
                    userName1, password, 
                    email: userName1.ToLower() + emailBase,
                    passwordQuestion: null, passwordAnswer: null, 
                    isApproved: true, status: out status, errors: out errors);

                roles.AddUserToRoles(userName1, new string[] { admins });
            }

            if (users.GetUser(userName2) == null)
            {
                users.CreateUser(
                    userName2, password, userName2.ToLower() + emailBase,
                    null, null, true, out status, out errors);

                roles.AddUserToRoles(userName2, new string[] { editors });
            }

            if (users.GetUser(userName3) == null)
            {
                users.CreateUser(
                    userName3, password, userName3.ToLower() + emailBase,
                    null, null, true, out status, out errors);

                roles.AddUserToRoles(userName3, new string[] { editors });
            }
        }

        public async Task<ActionResult> Index(
            ContentApprovalsManagerPage currentPage, 
            string task, int? stepIndex, string user, string decision)
        {
            var viewmodel = new ContentApprovalsManagerPageViewModel(currentPage);

            if (!string.IsNullOrWhiteSpace(task))
            {
                switch (task)
                {
                    case "createDefinition":

                        var langEN = new[] { CultureInfo.GetCultureInfo("en") };
                        var langSV = new[] { CultureInfo.GetCultureInfo("sv") };

                        var def = new ContentApprovalDefinition
                        {
                            ContentLink = ContentReference.StartPage,
                            Steps = new List<ApprovalDefinitionStep>
                            {
                                new ApprovalDefinitionStep(
                                    "Alice reviews English, Bob reviews Swedish", new[]
                                {
                                    new ApprovalDefinitionReviewer(userName1, langEN),
                                    new ApprovalDefinitionReviewer(userName2, langSV),
                                }),
                                new ApprovalDefinitionStep(
                                    "Eve reviews both languages", new[]
                                {
                                    new ApprovalDefinitionReviewer(userName3, langEN.Union(langSV))
                                })
                            },
                            RequireCommentOnReject = true
                        };

                        await repoDefinitions.SaveAsync(def);

                        break;

                    case "modifyStart":

                        var start = repoContent.Get<StartPage>(ContentReference.StartPage)
                            .CreateWritableClone() as StartPage;

                        start.Name += "X";

                        repoContent.Save(content: start,
                            action: SaveAction.RequestApproval,
                            access: AccessLevel.NoAccess);

                        break;

                    case "processStep":

                        var approval = await repoApprovals.GetAsync(ContentReference.StartPage);
                        
                        if (decision == "Approve")
                        {
                            await engine.ApproveStepAsync(
                                id: approval.ID,
                                username: user,
                                stepIndex: stepIndex.Value,
                                comment: "I approve: the page looks great!");
                        }
                        else
                        {
                            await engine.RejectStepAsync(
                               id: approval.ID,
                               username: user,
                               stepIndex: stepIndex.Value,
                               comment: "I decline: the page looks horrible!");
                        }
                        break;
                }
            }

            // GetAsync(ContentReference) extension methods need
            // using EPiServer.Approvals.ContentApprovals
            viewmodel.ApprovalDefinition = await repoDefinitions.GetAsync(ContentReference.StartPage);
            viewmodel.Approval = await repoApprovals.GetAsync(ContentReference.StartPage);

            return View(viewmodel);
        }
    }
}