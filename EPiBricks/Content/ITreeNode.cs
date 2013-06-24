using System.Collections.Generic;

namespace EPiBricks.Content
{
    public interface ITreeNode<out T>
    {
        T Parent { get; }

        IEnumerable<T> Ancestors { get; }

        IEnumerable<T> Children { get; }

        IEnumerable<T> Descendants { get; }
    }
}