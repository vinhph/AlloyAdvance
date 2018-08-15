using EPiServer.Framework.Web;
using EPiServer.Web;
using System.Web;

namespace AlloyTraining.Business.DisplayChannels
{
    public class MobileDisplayChannel : DisplayChannel
    {
        // this must match a tag on a template
        public override string ChannelName => RenderingTags.Mobile;

        // this is shown to editors as a menu option
        public override string DisplayName => "Mobile";

        public override bool IsActive(HttpContextBase context)
        {
            return context.Request.Browser.IsMobileDevice;
        }

        public override string ResolutionId => "iphone5";
    }

    public class iPhone5 : IDisplayResolution
    {
        public string Id => "iphone5";
        public string Name => "iPhone 5 (320 x 568)";
        public int Width => 320;
        public int Height => 568;
    }

    public class iPhone4 : IDisplayResolution
    {
        public string Id => "iphone4";
        public string Name => "iPhone 4 (320 x 480)";
        public int Width => 320;
        public int Height => 480;
    }
}