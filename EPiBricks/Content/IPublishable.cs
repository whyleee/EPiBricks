using System;

namespace EPiBricks.Content
{
    public interface IPublishable
    {
        bool IsPublished { get; }

        DateTime StartPublish { get; }

        DateTime StopPublish { get; }
    }
}