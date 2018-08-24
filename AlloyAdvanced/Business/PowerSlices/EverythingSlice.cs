using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ContentQuery;
using PowerSlice;

namespace AlloyAdvanced.Business.PowerSlices
{
    [ServiceConfiguration(typeof(IContentQuery)),
    ServiceConfiguration(typeof(IContentSlice))]
    public class EverythingSlice : ContentSliceBase<IContent>
    {
        public override string Name
        {
            get { return "Everything"; }
        }
    }
}