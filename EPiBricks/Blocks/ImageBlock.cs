using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAnnotations;

namespace EPiBricks.Blocks
{
    [ContentType(GUID = "3C9FF500-D8BC-4E2B-A2D5-53547031EF86", AvailableInEditMode = false)]
    public class ImageBlock : BlockData
    {
        [CultureSpecific]
        [Display(Order = 10)]
        [UIHint(EPiServer.Web.UIHint.Image)]
        public virtual Url Url { get; set; }

        [CultureSpecific]
        [Searchable(false)]
        [Display(Order = 20)]
        public virtual string Description { get; set; }

        public virtual Image ToImage()
        {
            if (Url == null)
            {
                return null;
            }

            return new Image
            {
                Url = Url.ToString(),
                Alt = Description
            };
        }
    }
}
