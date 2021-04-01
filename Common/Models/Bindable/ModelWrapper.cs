using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Domains.Common.Models.Bindable
{
    public class ModelWrapper<T> : BindableBase
    {
        public ModelWrapper(T model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Model = model;
            propertyInfo = Model.GetType().GetProperties();
        }

        public T Model { get; private set; }


        public bool Set<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            PropertyInfo pi = propertyInfo.FirstOrDefault(o => o.Name == propertyName);
            if (pi == null)
                throw new ArgumentException($"{propertyName} is not a valid property.");

            var currentValue = pi.GetValue(Model);

            if (Equals(currentValue, value))
                return false;

            pi.SetValue(Model, value);
            this.RaisePropertyChanged(propertyName);
            return true;

        }


        protected void RegisterCollection<TWrapper, TModel>(
            ObservableCollection<TWrapper> wrapperCollection,
            List<TModel> modelCollection) where TWrapper : ModelWrapper<TModel>
        {
            wrapperCollection.CollectionChanged += (s, e) =>
            {
                if (e.OldItems != null)
                {
                    foreach (var item in e.OldItems.Cast<TWrapper>())
                    {
                        modelCollection.Remove(item.Model);
                    }
                }
                if (e.NewItems != null)
                {
                    foreach (var item in e.NewItems.Cast<TWrapper>())
                    {
                        modelCollection.Add(item.Model);
                    }
                }
            };

        }

        private PropertyInfo[] propertyInfo;
    }
}
