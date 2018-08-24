using AlloyAdvanced.Models.Pages;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ContentQuery;
using PowerSlice;
using System.Collections.Generic;

namespace AlloyAdvanced.Business.PowerSlices
{
    [ServiceConfiguration(typeof(IContentQuery)),
    ServiceConfiguration(typeof(IContentSlice))]
    public class ProductPageSlice : ContentSliceBase<ProductPage>
    {
        public override string Name
        {
            get { return "Product Pages"; }
        }
        public override IEnumerable<CreateOption> CreateOptions
        {
            get
            {
                var contentType = ContentTypeRepository.Load(
                    typeof(ProductPage));

                yield return new CreateOption(
                    label: contentType.LocalizedName,
                    parent: ContentReference.StartPage,
                    contentTypeId: contentType.ID);
            }
        }
    }
}