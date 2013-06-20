using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using EPiGlue.Framework;
using EPiGlue.Helpers;
using Perks;

namespace EPiBricks.Rendering
{
    public class LinkFriendlyUrlRewriter : IModelPropertyHandler
    {
        protected readonly FriendlyUrlRewriter _urlRewriter;

        public LinkFriendlyUrlRewriter(FriendlyUrlRewriter urlRewriter)
        {
            Ensure.ArgumentNotNull(urlRewriter, "urlRewriter");

            _urlRewriter = urlRewriter;
        }

        public virtual bool CanHandle(ModelPropertyContext context)
        {
            return context.Property.PropertyType.Is<Link>();
        }

        public virtual void Process(ModelPropertyContext context)
        {
            var link = (Link) context.PropertyValue;

            if (link == null)
            {
                return;
            }

            link.Url = _urlRewriter.GetFriendlyUrl(link.Url, context.ExecutionContext.RequestContext, RouteTable.Routes);
        }
    }
}
