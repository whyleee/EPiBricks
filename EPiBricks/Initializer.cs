using System.Linq;
using System.Web.Mvc;
using EPiBricks.Configuration;
using EPiBricks.Controllers;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace EPiBricks
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class Initializer : IInitializableModule
    {
        public virtual void Initialize(InitializationEngine context)
        {
            ControllerBuilder.Current.DefaultNamespaces.Add(typeof(LinkBlockController).Namespace);

            // Additional content value providers
            ValueProviderFactories.Factories.Add(new ShortNamesContentDataValueProviderFactory());
            ValueProviderFactories.Factories.Add(new HomePageValueProviderFactory());
            ValueProviderFactories.Factories.Add(new ChildActionContentDataValueProviderFactory());
        }

        public virtual void Uninitialize(InitializationEngine context)
        {
        }

        public virtual void Preload(string[] parameters)
        {
        }
    }
}