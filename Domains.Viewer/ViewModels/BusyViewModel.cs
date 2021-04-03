using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Viewer.ViewModels
{
    public class BusyViewModel : BindableBase
    {
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (SetProperty(ref _isBusy, value))
                    RaisePropertyChanged(nameof(IsIdle));
            }
        }

        public bool IsIdle => !IsBusy;


        protected bool _isBusy;
    }
}
