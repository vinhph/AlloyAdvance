using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Web;

namespace AlloyTraining.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class DisplayOptionsInitializationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var options = context.Locate.Advanced.GetInstance<DisplayOptions>();

            options.Add(id: SiteTags.Full, name: "Full", tag: SiteTags.Full);
            options.Add(id: SiteTags.Wide, name: "Wide", tag: SiteTags.Wide);
            options.Add(id: SiteTags.Narrow, name: "Narrow", tag: SiteTags.Narrow);
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}