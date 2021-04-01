using DomainModelEditor;
using Domains.Common.Helpers;
using Domains.Common.Models.Bindable;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Domains.Viewer.ViewModels
{
    public class DomainsViewerViewModel : BindableBase, INavigationAware
    {
        public DomainsViewerViewModel()
        {

        }


        public ObservableCollection<BindableEntity> Domains { get; } = new ObservableCollection<BindableEntity>();


        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var entityStore = new EntityStore();
            var demoData = new[]
            {
                new Tuple<int, string, int, int>(1, "Order", 100, 100),
                new Tuple<int, string, int, int>(2, "OrderLine", 200, 200)
            };
            entityStore.Load(demoData);

            entityStore
                .Select(d => new BindableEntity(d))
                .ForEach(bd => Domains.Add(bd));
        }
    }
}
