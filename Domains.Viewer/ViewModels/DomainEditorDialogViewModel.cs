using Domains.Common.Models;
using Domains.Common.Models.Bindable;
using Domains.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Viewer.ViewModels
{
    public class DomainEditorDialogViewModel : BusyViewModel, IDialogAware
    {
        public DomainEditorDialogViewModel(
            DomainPropertyService domainPropertyService,
            DomainsService domainsService)
        {
            _domains = domainsService;
            _domainPropertyService = domainPropertyService;
        }


        public DelegateCommand CancelCommand => new DelegateCommand(Cancel).ObservesCanExecute(() => IsIdle);
        public DelegateCommand SaveCommand => new DelegateCommand(SaveChanges).ObservesCanExecute(() => IsIdle);

        public DelegateCommand AddPropertyCommand => new DelegateCommand(AddProperty).ObservesCanExecute(() => IsIdle);
        public DelegateCommand<BindableDomainProperty> DeletePropertyCommand
            => new DelegateCommand<BindableDomainProperty>(DeleteProperty);

        public string Title
        {
            get => _title;
            private set => SetProperty(ref _title, value);
        }


        public BindableEntity Entity
        {
            get => _entity;
            set => SetProperty(ref _entity, value);
        }


        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => IsIdle;

        public void OnDialogClosed()
        {
        }

        public async void OnDialogOpened(IDialogParameters parameters)
        {
            IsBusy = true;

            if (parameters.ContainsKey("id"))
            {
                Title = "Edit domain";
                var id = parameters.GetValue<int>("id");
                Entity = new BindableEntity(await _domains.GetItem(id));
            }
            else
            {
                Title = "Create new domain";
                Entity = new BindableEntity(new Entity());
            }

            IsBusy = false;
        }


        private async void SaveChanges()
        {
            IsBusy = true;

            var item = Entity.Model.Id == 0
                ? await _domains.InsertItem(Entity.Model)
                : await _domains.UpdateItem(Entity.Model);

            IsBusy = false;
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, new DialogParameters($"id={item.Id}")));
        }

        private void Cancel()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        private void AddProperty()
        {
            this.Entity.ExtraProperties.Add(new BindableDomainProperty(new DomainProperty() { DomainId = Entity.Model.Id }));
        }

        private async void DeleteProperty(BindableDomainProperty property)
        {
            if (property.Model.Id != 0)
                await _domainPropertyService.RemoveItem(property.Model);
            this.Entity.ExtraProperties.Remove(property);
        }


        private string _title;

        private BindableEntity _entity;

        private readonly DomainsService _domains;
        private readonly DomainPropertyService _domainPropertyService;
    }
}
