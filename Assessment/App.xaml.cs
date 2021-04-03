using Assignment.Views;
using Domains.Common;
using Domains.Common.Services;
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
            var db = new AppDbContext();
            db.Database.EnsureCreated();

            containerRegistry.RegisterInstance(db);
            containerRegistry.RegisterScoped<DomainsService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<DomainsViewerModule>();
        }
    }
}
