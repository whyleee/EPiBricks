using EPiServer;

namespace EPiBricks.Events
{
    public interface IPublishingContentHandler
    {
        void PublishingContent(object sender, ContentEventArgs e);
    }
}