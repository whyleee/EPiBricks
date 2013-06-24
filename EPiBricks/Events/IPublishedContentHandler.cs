using EPiServer;

namespace EPiBricks.Events
{
    public interface IPublishedContentHandler
    {
        void PublishedContent(object sender, ContentEventArgs e);
    }
}