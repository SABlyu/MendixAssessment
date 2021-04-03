using DomainModelEditor;
using Domains.Common.Helpers;
using Domains.Common.Models.Bindable;
using Domains.Common.Services;
using Domains.Viewer.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Domains.Viewer.ViewModels
{
    public class DomainsViewerViewModel : BindableBase, INavigationAware
    {
        public DomainsViewerViewModel(DomainsService domainsService, IDialogService dialogService)
        {
            _domains = domainsService;
            _dialogService = dialogService;
        }


        public ObservableCollection<BindableEntity> Domains { get; } = new ObservableCollection<BindableEntity>();

        public DelegateCommand AddDomainCommand => new DelegateCommand(() => 
            _dialogService.ShowDialog(nameof(DomainEditorDialog)));


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
        private readonly IDialogService _dialogService;
    }
}
