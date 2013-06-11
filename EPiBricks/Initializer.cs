using System.Web.Mvc;
using EPiBricks.Controllers;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace EPiBricks
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class Initializer : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            ControllerBuilder.Current.DefaultNamespaces.Add(typeof(LinkBlockController).Namespace);
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}