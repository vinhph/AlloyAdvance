using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using System.Web.Routing;
using EPiServer.Web.Routing;
using AlloyAdvanced.Business.PartialRouters;

namespace AlloyAdvanced.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class NorthwindToCategoryPartialRouterRegistration : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            RouteTable.Routes.RegisterPartialRouter(
                new NorthwindToCategoryPartialRouter());
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}