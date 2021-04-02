using DomainModelEditor;
using Domains.Common.Helpers;
using Domains.Common.Models.Bindable;
using Domains.Common.Services;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Domains.Viewer.ViewModels
{
    public class DomainsViewerViewModel : BindableBase, INavigationAware
    {
        public DomainsViewerViewModel(DomainsService domainsService)
        {
            _domains = domainsService;
        }


        public ObservableCollection<BindableEntity> Domains { get; } = new ObservableCollection<BindableEntity>();


        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            var items = await _domains.GetItems();
            items.ForEach(e => Domains.Add(new BindableEntity(e)));
        }


        private readonly DomainsService _domains;
    }
}
