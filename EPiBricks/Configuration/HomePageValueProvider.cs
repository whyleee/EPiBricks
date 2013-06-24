using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.Editor;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;

namespace EPiBricks.Configuration
{
    public class HomePageValueProvider : IValueProvider
    {
        public HomePageValueProvider()
        {
            HomePageKey = "home";
        }

        public virtual string HomePageKey { get; set; }

        public virtual bool ContainsPrefix(string prefix)
        {
            return IsHomePageKey(prefix);
        }

        public virtual ValueProviderResult GetValue(string key)
        {
            if (ContainsPrefix(key))
            {
                var homeLink = (ContentReference) ContentReference.StartPage;

                if (PageEditing.PageIsInEditMode)
                {
                    homeLink = GetLastVersionOf(homeLink);
                }

                return new ValueProviderResult(homeLink, homeLink.ToString(), CultureInfo.InvariantCulture);
            }

            return null;
        }

        public static ContentReference GetLastVersionOf(ContentReference pageRef)
        {
            var versionRepo = ServiceLocator.Current.GetInstance<IContentVersionRepository>();
            return versionRepo.LoadCommonDraft(pageRef, ContentLanguage.PreferredCulture.Name).ContentLink;
        }

        private bool IsHomePageKey(string key)
        {
            return string.Equals(key, HomePageKey, StringComparison.OrdinalIgnoreCase);
        }
    }

    public class HomePageValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            return new HomePageValueProvider();
        }
    }
}