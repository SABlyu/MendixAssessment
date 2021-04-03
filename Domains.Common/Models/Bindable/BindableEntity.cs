using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Domains.Common.Models.Bindable
{
    public class BindableEntity : ModelWrapper<Entity>
    {
        public BindableEntity(Entity model) : base(model)
        {
            var items = model.ExtraProperties.Select(p => new BindableDomainProperty(p));
            ExtraProperties = new ObservableCollection<BindableDomainProperty>(items);
            RegisterCollection(ExtraProperties, model.ExtraProperties);
        }

        public string Name
        {
            get => Model.Name;
            set => Set(value);
        }

        public int X
        {
            get => Model.X;
            set => Set(value);
        }

        public int Y
        {
            get => Model.Y;
            set => Set(value);
        }


        public ObservableCollection<BindableDomainProperty> ExtraProperties { get; }


        public string FullName => $"{Name} ({X}:{Y}) {ExtraProperties.Aggregate("", (p, p1) => p + $"\n{p1.Key}: {p1.Value}")}";
    }
}
