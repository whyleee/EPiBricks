using EPiServer;

namespace EPiBricks.Events
{
    public interface ISavedContentHandler
    {
        void SavedContent(object sender, ContentEventArgs e);
    }
}