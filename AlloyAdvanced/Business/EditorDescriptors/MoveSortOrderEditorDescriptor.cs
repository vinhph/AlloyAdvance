using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;
using System.Collections.Generic;

namespace AlloyAdvanced.Business.EditorDescriptors
{
    [EditorDescriptorRegistration(TargetType = typeof(int?))]
    public class MoveSortOrderEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata,
            IEnumerable<Attribute> attributes)
        {
            //if (metadata.PropertyName == "PagePeerOrder")
            if (metadata.PropertyName == Global.SystemPropertyNames.SortIndex)
            {
                metadata.GroupName = EPiServer.DataAbstraction.SystemTabNames.Content;
            }
            base.ModifyMetadata(metadata, attributes);
        }
    }
}