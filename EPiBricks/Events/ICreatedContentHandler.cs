using EPiServer;

namespace EPiBricks.Events
{
    public interface ICreatedContentHandler
    {
        void CreatedContent(object sender, ContentEventArgs e); 
    }
}