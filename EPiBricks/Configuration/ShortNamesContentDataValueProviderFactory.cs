using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer.Web.Mvc;

namespace EPiBricks.Configuration
{
    public class ShortNamesContentDataValueProviderFactory : ContentDataValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            var provider = new ContentDataValueProvider(controllerContext.RequestContext)
                {
                    CurrentPageKey = "page",
                    CurrentBlockKey = "block",
                    CurrentContentKey = "content"
                };

            return provider;
        }
    }
}