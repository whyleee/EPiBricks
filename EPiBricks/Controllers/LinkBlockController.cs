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
    public class LinkBlockController : BlockController<LinkBlock>
    {
        public override ActionResult Index(LinkBlock currentContent)
        {
            var link = currentContent.ToLink();
            var cssClass = ControllerContext.ParentActionViewContext.ViewData["cssClass"] as string;

            if (link == null)
            {
                return Content(string.Format("<span class=\"{0}\">[Empty link]</span>", cssClass)); // TODO: revert to default state (probably using 'data-rendersettings'...
            }

            link.Class = cssClass;

            return Content(link.ToHtmlString());
        }
    }
}
