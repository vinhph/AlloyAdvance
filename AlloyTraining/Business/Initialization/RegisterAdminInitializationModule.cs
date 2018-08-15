using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Security;
using System.Configuration;
using System.Web.Security;

namespace AlloyTraining.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class RegisterAdminInitializationModule : IInitializableModule
    {
        private const string roleName = "WebAdmins";
        private const string userName = "Admin";
        private const string password = "Pa$$w0rd";
        private const string email = "admin@alloy.com";

        public void Initialize(InitializationEngine context)
        {
            string enabledString = ConfigurationManager.AppSettings["alloy:RegisterAdmin"];
            bool enabled;
            if (bool.TryParse(enabledString, out enabled))
            {
                if (enabled)
                {
                    #region Use ASP.NET Membership classes to create the role and user

                    // if the role does not exist, create it
                    if (!Roles.RoleExists(roleName))
                    {
                        Roles.CreateRole(roleName);
                    }

                    // if the user already exists, delete it
                    MembershipUser user = Membership.GetUser(userName);
                    if (user != null)
                    {
                        Membership.DeleteUser(userName);
                    }

                    // create the user with password and add it to role
                    Membership.CreateUser(userName, password, email);
                    Roles.AddUserToRole(userName, roleName);

                    #endregion

                    #region Use EPiServer classes to give full access to root of content tree

                    var security = context.Locate.Advanced.GetInstance<IContentSecurityRepository>();

                    IContentSecurityDescriptor permissions = security
                        .Get(ContentReference.RootPage)
                        .CreateWritableClone() as IContentSecurityDescriptor;

                    permissions.AddEntry(new AccessControlEntry(roleName, AccessLevel.FullAccess));

                    security.Save(ContentReference.RootPage, permissions, SecuritySaveType.Replace);

                    permissions = security
                        .Get(ContentReference.WasteBasket)
                        .CreateWritableClone() as IContentSecurityDescriptor;

                    permissions.AddEntry(new AccessControlEntry(roleName, AccessLevel.FullAccess));

                    security.Save(ContentReference.WasteBasket, permissions, SecuritySaveType.Replace);

                    #endregion
                }
            }
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}
