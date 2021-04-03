using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Common.Models.Bindable
{
    public class BindableEntity : ModelWrapper<Entity>
    {
        public BindableEntity(Entity model) : base(model)
        {
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

        public string FullName => $"{Name} ({X}:{Y})";
    }
}
