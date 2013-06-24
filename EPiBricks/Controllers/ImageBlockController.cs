using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EPiBricks.Blocks;
using EPiServer.Web.Mvc;

namespace EPiBricks.Controllers
{
    public class ImageBlockController : BlockController<ImageBlock>
    {
        public override ActionResult Index(ImageBlock currentContent)
        {
            var image = currentContent.ToImage();
            var cssClass = ControllerContext.ParentActionViewContext.ViewData["cssClass"] as string;

            if (image == null)
            {
                return Content(string.Format("<span class=\"{0}\">[No image]</span>", cssClass)); // TODO: revert to default state (probably using 'data-rendersettings'...
            }

            image.Attributes["class"] = cssClass;

            return Content(image.ToHtmlString());
        }
    }
}
