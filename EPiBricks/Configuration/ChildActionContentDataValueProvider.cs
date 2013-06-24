using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPiServer.Web.Routing;
using Perks;

namespace EPiBricks.Configuration
{
    public class ChildActionContentDataValueProvider : IValueProvider
    {
        private readonly ControllerContext _controllerContext;

        public ChildActionContentDataValueProvider(ControllerContext controllerContext)
        {
            Ensure.ArgumentNotNull(controllerContext, "controllerContext");

            _controllerContext = controllerContext;
            CurrentPageKeys = new[] {"currentPage", "page"};
        }

        public virtual string[] CurrentPageKeys { get; set; }

        public virtual bool ContainsPrefix(string prefix)
        {
            return IsPageKey(prefix);
        }

        public virtual ValueProviderResult GetValue(string key)
        {
            if (_controllerContext.IsChildAction && ContainsPrefix(key))
            {
                var pageLink = _controllerContext.RequestContext.GetContentLink();

                if (pageLink != null)
                {
                    return new ValueProviderResult(pageLink, pageLink.ToString(), CultureInfo.InvariantCulture);
                }
            }

            return null;
        }

        private bool IsPageKey(string key)
        {
            return CurrentPageKeys.Any(k => string.Equals(k, key, StringComparison.OrdinalIgnoreCase));
        }
    }

    public class ChildActionContentDataValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            return new ChildActionContentDataValueProvider(controllerContext);
        }
    }
}