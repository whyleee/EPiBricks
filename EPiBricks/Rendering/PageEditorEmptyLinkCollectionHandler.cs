using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiGlue.Framework;
using EPiGlue.Handlers;
using Perks;

namespace EPiBricks.Rendering
{
    public class PageEditorEmptyLinkCollectionHandler : PageEditorHtmlHandler
    {
        public override void Process(ModelPropertyContext context)
        {
            var editName = GetFieldEditName(context);
            var fakeLink = new Link
                {
                    Url = "#",
                    Text = "[" + editName.ToFriendlyString() + "]"
                };

            context.PropertyValue = new List<Link>(new[] {fakeLink});
        }

        protected override bool FilterByType(ModelPropertyContext context)
        {
            return context.Property.PropertyType.Is<IEnumerable<Link>>();
        }

        protected override bool FilterByValue(ModelPropertyContext context)
        {
            return context.PropertyValue == null || ((IEnumerable<Link>) context.PropertyValue).IsEmpty();
        }
    }
}
