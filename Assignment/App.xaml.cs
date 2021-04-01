using Assignment.Views;
using Domains.Viewer;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace Assignment
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<DomainsViewerModule>();
        }

    }
}
