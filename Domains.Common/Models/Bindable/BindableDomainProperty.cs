using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Common.Models.Bindable
{
    public class BindableDomainProperty : ModelWrapper<DomainProperty>
    {
        public BindableDomainProperty(DomainProperty model) : base(model)
        {
        }


        public string Key
        {
            get => Model.Key;
            set => Set(value);
        }

        public string Value
        {
            get => Model.Value;
            set => Set(value);
        }
    }
}
