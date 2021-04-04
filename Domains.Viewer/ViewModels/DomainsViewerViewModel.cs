using Domains.Common.Models.Bindable;
using Domains.Common.Services;
using Domains.Viewer.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
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
            _dialogService.ShowDialog(nameof(DomainEditorDialog), OnEditorClosed));

        public DelegateCommand<BindableEntity> EditDomainCommand => new DelegateCommand<BindableEntity>(d =>
            _dialogService.ShowDialog(nameof(DomainEditorDialog), new DialogParameters($"id={d.Model.Id}"), OnEditorClosed));


        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            var items = await _domains.GetItems();
            items.ForEach(e => Domains.Add(new BindableEntity(e)));
        }


        private async void OnEditorClosed(IDialogResult result)
        {
            if (result.Result != ButtonResult.OK)
                return;
            if (!result.Parameters.ContainsKey("id"))
                return;

            var id = result.Parameters.GetValue<int>("id");
            if (id == 0)
                return;

            var savedItem = await _domains.GetItem(id);
            var oldItem = Domains.FirstOrDefault(d => d.Model.Id == id);

            if (oldItem != null)
                Domains.Remove(oldItem);
            Domains.Add(new BindableEntity(savedItem));
        }


        private readonly DomainsService _domains;
        private readonly IDialogService _dialogService;
    }
}
