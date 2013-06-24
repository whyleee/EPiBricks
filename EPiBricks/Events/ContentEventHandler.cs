using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace EPiBricks.Events
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class ContentEventHandler : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var eventRegistry = ServiceLocator.Current.GetInstance<IContentEvents>();

            eventRegistry.CreatingContent += OnCreatingContent;
            eventRegistry.CreatedContent += OnCreatedContent;
            eventRegistry.DeletingContent += OnDeletingContent;
            eventRegistry.DeletedContent += OnDeletedContent;
            eventRegistry.SavingContent += OnSavingContent;
            eventRegistry.SavedContent += OnSavedContent;
            eventRegistry.PublishingContent += OnPublishingContent;
            eventRegistry.PublishedContent += OnPublishedContent;
        }

        private void OnCreatingContent(object sender, ContentEventArgs e)
        {
            if (e.Content is ICreatingContentHandler)
            {
                ((ICreatingContentHandler) e.Content).CreatingContent(sender, e);
            }
        }

        private void OnCreatedContent(object sender, ContentEventArgs e)
        {
            if (e.Content is ICreatedContentHandler)
            {
                ((ICreatedContentHandler) e.Content).CreatedContent(sender, e);
            }
        }

        private void OnDeletingContent(object sender, DeleteContentEventArgs e)
        {
            if (e.Content is IDeletingContentHandler)
            {
                ((IDeletingContentHandler) e.Content).DeletingContent(sender, e);
            }
        }

        private void OnDeletedContent(object sender, DeleteContentEventArgs e)
        {
            if (e.Content is IDeletedContentHandler)
            {
                ((IDeletedContentHandler) e.Content).DeletedContent(sender, e);
            }
        }

        private void OnSavingContent(object sender, ContentEventArgs e)
        {
            if (e.Content is ISavingContentHandler)
            {
                ((ISavingContentHandler) e.Content).SavingContent(sender, e);
            }
        }

        private void OnSavedContent(object sender, ContentEventArgs e)
        {
            if (e.Content is ISavedContentHandler)
            {
                ((ISavedContentHandler) e.Content).SavedContent(sender, e);
            }
        }

        private void OnPublishingContent(object sender, ContentEventArgs e)
        {
            if (e.Content is IPublishingContentHandler)
            {
                ((IPublishingContentHandler) e.Content).PublishingContent(sender, e);
            }
        }

        private void OnPublishedContent(object sender, ContentEventArgs e)
        {
            if (e.Content is IPublishedContentHandler)
            {
                ((IPublishedContentHandler) e.Content).PublishedContent(sender, e);
            }
        }

        public void Uninitialize(InitializationEngine context)
        {
            var eventRegistry = ServiceLocator.Current.GetInstance<IContentEvents>();

            eventRegistry.CreatingContent -= OnCreatingContent;
            eventRegistry.CreatedContent -= OnCreatedContent;
            eventRegistry.DeletingContent -= OnDeletingContent;
            eventRegistry.DeletedContent -= OnDeletedContent;
            eventRegistry.SavingContent -= OnSavingContent;
            eventRegistry.SavedContent -= OnSavedContent;
            eventRegistry.PublishingContent -= OnPublishingContent;
            eventRegistry.PublishedContent -= OnPublishedContent;
        }

        public void Preload(string[] parameters)
        {
        }
    }
}
