using EPiServer.Core;

namespace EPiBricks.Content
{
    public interface IContentPage : IContent, INavigable, IMetaTagged
    {
        XhtmlString Content { get; set; }
    }
}