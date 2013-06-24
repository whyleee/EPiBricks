using EPiServer;

namespace EPiBricks.Events
{
    public interface IDeletingContentHandler
    {
        void DeletingContent(object sender, DeleteContentEventArgs e);
    }
}