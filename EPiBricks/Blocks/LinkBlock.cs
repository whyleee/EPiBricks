using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiBricks.DataAnnotations;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace EPiBricks.Blocks
{
    [ContentType(GUID = "F270F118-7ABF-4694-8A0C-88AE0AA35FED", AvailableInEditMode = false)]
    public class LinkBlock : BlockData
    {
        [CultureSpecific]
        [Display(Order = 10)]
        public virtual Url Url { get; set; }

        [CultureSpecific]
        [Display(Order = 20)]
        public virtual string Text { get; set; }

        [CultureSpecific]
        [Searchable(false)]
        [Display(Order = 30)]
        [UIHint(UIHint.SelectList)]
        [SelectOptions("_self:Same tab (default)|_blank:New window or tab")]
        public virtual string Target { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

            Target = "_self";
        }

        public virtual Link ToLink()
        {
            if (Url == null)
            {
                return null;
            }

            return new Link
            {
                Url = Url.ToString(),
                Text = Text,
                Target = Target
            };
        }
    }
}
