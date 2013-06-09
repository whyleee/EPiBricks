using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiBricks.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace EPiBricks.PropertyTypes
{
    [EditorDescriptorRegistration(TargetType = typeof(string), UIHint = UIHint.SelectList)]
    public class SelectListEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            SelectionFactoryType = typeof(SelectListFactory);
            ClientEditingClass = "epi.cms.contentediting.editors.SelectionEditor";

            base.ModifyMetadata(metadata, attributes);
        }

        public class SelectListFactory : ISelectionFactory
        {
            public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
            {
                var optionsAttr = metadata.Attributes
                    .FirstOrDefault(attr => attr.GetType() == typeof(SelectOptionsAttribute));

                if (optionsAttr == null)
                {
                    return new List<ISelectItem>();
                }

                var options = ((SelectOptionsAttribute) optionsAttr).Options.Split('|');

                return options
                    .Select(option => new SelectItem
                    {
                        Value = option.Split(':').First(),
                        Text = option.Split(':').Last()
                    })
                    .ToList();
            }
        }
    }
}
