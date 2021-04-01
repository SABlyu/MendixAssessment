using Domains.Common.Helpers;
using Domains.Viewer.Views;
using Prism.Mvvm;
using Prism.Regions;

namespace Assignment.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Mendix Modeler";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(DomainsViewer));
        }
    }
}
