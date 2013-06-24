using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiBricks.Content;
using EPiServer;
using EPiServer.Core;
using EPiServer.SpecializedProperties;
using EPiServer.Web;

namespace EPiBricks
{
    public static class ContentExtensions
    {
        // Content items
        // ------------------------------------

        public static IEnumerable<T> Published<T>(this IEnumerable<T> items) where T : IContentData
        {
            return items.Where(item => item is IPublishable && ((IPublishable) item).IsPublished);
        }

        public static IEnumerable<T> Navigable<T>(this IEnumerable<T> items) where T : IContentData
        {
            return items.Published().Where(item => item is INavigable);
        }

        public static IEnumerable<T> Visible<T>(this IEnumerable<T> items) where T : IContentData
        {
            return items.Published().Where(item => item is IVisualizable && ((IVisualizable) item).IsVisible);
        }

        public static bool IsPublished(this PageData item)
        {
            return item.CheckPublishedStatus(PagePublishedStatus.Published);
        }

        public static IContent Content(this BlockData block)
        {
            return ((IContent) block);
        }

        public static IEnumerable<T> ToContentItems<T>(this LinkItemCollection links, IContentRepository repo) where T : ContentData
        {
            if (links == null)
            {
                yield break;
            }

            foreach (var link in links)
            {
                var linkUrl = new UrlBuilder(link.Href);

                if (!PermanentLinkMapStore.ToMapped(linkUrl))
                {
                    continue;
                }

                var contentLink = PermanentLinkUtility.GetContentReference(linkUrl);
                var item = repo.Get<T>(contentLink);

                if (item != null)
                {
                    yield return item;
                }
            }
        }

        public static bool IsAncestorOrSelfFor<T>(this T self, T item) where T : IContent, ITreeNode<T>
        {
            return self.ContentLink == item.ContentLink || item.Ancestors.Any(parent => parent.ContentLink == self.ContentLink);
        }

        // URLs
        // ------------------------------------

        public static string ToFriendlyUrl(this string url, ContentReference contentLink = null)
        {
            var urlBuilder = new UrlBuilder(url);
            EPiServer.Global.UrlRewriteProvider.ConvertToExternal(urlBuilder, contentLink, Encoding.UTF8);

            return urlBuilder.ToString();
        }

        public static string GetFriendlyUrl<T>(this T item) where T : IContent, IHttpResource
        {
            return item.Url.ToFriendlyUrl(item.ContentLink);
        }

        // Links
        // ------------------------------------

        public static Link GetLink<T>(this T item, string @class = null) where T : IContent, IHttpResource
        {
            return new Link
                {
                    Text = item.Name,
                    Url = item.GetFriendlyUrl(),
                    Class = @class
                };
        }

        public static IEnumerable<Link> ToLinks(this IEnumerable<LinkItem> linkItems)
        {
            return linkItems == null ? new List<Link>() : linkItems.Select(item => item.ToLink());
        }

        public static Link ToLink(this LinkItem link)
        {
            return new Link
            {
                Url = link.Href,
                Text = link.Text,
                Target = link.Target
            };
        }
    }
}
