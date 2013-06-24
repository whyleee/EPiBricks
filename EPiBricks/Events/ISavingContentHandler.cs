using EPiServer;

namespace EPiBricks.Events
{
    public interface ISavingContentHandler
    {
        void SavingContent(object sender, ContentEventArgs e);
    }
}