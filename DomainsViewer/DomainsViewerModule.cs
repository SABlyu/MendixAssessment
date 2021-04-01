using Common.Helpers;
using DomainsViewerModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace DomainsViewerModule
{
    public class DomainsViewerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var region = containerProvider.Resolve<IRegionManager>();
            region.RequestNavigate(RegionNames.ContentRegion, nameof(DomainsViewer));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DomainsViewer>();
        }
    }
}