using Domains.Common.Models;
using Domains.Common.Models.Bindable;
using Domains.Common.Services;
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

        }

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

        public bool CanCloseDialog() => !IsBusy;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("entity"))
            {
                Title = "Edit entity";
                Entity = parameters.GetValue<BindableEntity>("entity");
            }
            else
            {
                Title = "Create new entity";
                Entity = new BindableEntity(new Entity());
            }
        }


        private string _title;
        private BindableEntity _entity;
    }
}
