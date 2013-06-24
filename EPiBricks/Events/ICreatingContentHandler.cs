using EPiServer;

namespace EPiBricks.Events
{
    public interface ICreatingContentHandler
    {
        void CreatingContent(object sender, ContentEventArgs e);
    }
}