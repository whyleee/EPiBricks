using EPiServer;

namespace EPiBricks.Events
{
    public interface IDeletedContentHandler
    {
        void DeletedContent(object sender, DeleteContentEventArgs e);
    }
}