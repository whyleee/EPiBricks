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
    public class LinkCollectionFriendlyUrlRewriter : IModelPropertyHandler
    {
        protected readonly FriendlyUrlRewriter _urlRewriter;

        public LinkCollectionFriendlyUrlRewriter(FriendlyUrlRewriter urlRewriter)
        {
            Ensure.ArgumentNotNull(urlRewriter, "urlRewriter");

            _urlRewriter = urlRewriter;
        }

        public virtual bool CanHandle(ModelPropertyContext context)
        {
            return context.PropertyValue is IEnumerable<Link>;
        }

        public virtual void Process(ModelPropertyContext context)
        {
            var links = (IEnumerable<Link>) context.PropertyValue;

            foreach (var link in links)
            {
                link.Url = _urlRewriter.GetFriendlyUrl(link.Url, context.ExecutionContext.RequestContext, RouteTable.Routes);
            }
        }
    }
}
