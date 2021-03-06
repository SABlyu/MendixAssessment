using Domains.Common.Helpers;
using Domains.Viewer.ViewModels;
using Domains.Viewer.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Domains.Viewer
{
    public class DomainsViewerModule: IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var region = containerProvider.Resolve<IRegionManager>();
            region.RequestNavigate(RegionNames.ContentRegion, nameof(DomainsViewer));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<DomainEditorDialog, DomainEditorDialogViewModel>();
            containerRegistry.RegisterForNavigation<DomainsViewer>();
        }
    }
}