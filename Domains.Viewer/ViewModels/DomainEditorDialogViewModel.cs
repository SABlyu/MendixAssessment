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
        public DomainEditorDialogViewModel(DomainsService domainsService)
        {
            _domains = domainsService;
        }


        public DelegateCommand CancelCommand => new DelegateCommand(Cancel).ObservesCanExecute(() => IsIdle);
        public DelegateCommand SaveCommand => new DelegateCommand(SaveChanges).ObservesCanExecute(() => IsIdle);


        public string Title
        {
            get => _title;
            private set => SetProperty(ref _title, value);
        }


        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public int X
        {
            get => _x;
            set => SetProperty(ref _x, value);
        }

        public int Y
        {
            get => _y;
            set => SetProperty(ref _y, value);
        }


        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => IsIdle;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("entity"))
            {
                Title = "Edit entity";
                _entity = parameters.GetValue<BindableEntity>("entity");
            }
            else
            {
                Title = "Create new entity";
                _entity = new BindableEntity(new Entity());
            }

            Name = _entity.Name;
            X = _entity.X;
            Y = _entity.Y;
        }


        private async void SaveChanges()
        {
            IsBusy = true;

            _entity.Name = Name;
            _entity.X = X;
            _entity.Y = Y;

            var item = await _domains.UpdateItem(_entity.Model);

            IsBusy = false;
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        private void Cancel()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }


        private string _title;

        private string _name;
        private int _x;
        private int _y;

        private BindableEntity _entity;

        private readonly DomainsService _domains;
    }
}
