namespace EPiBricks.Content
{
    public interface IMetaTagged
    {
        string PageTitle { get; }

        string PageDescription { get; }

        string PageKeywords { get; }
    }
}