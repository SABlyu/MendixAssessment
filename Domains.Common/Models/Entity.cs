using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domains.Common.Models
{
    public class Entity : DbItem
    {
        public string Name { get; set; }

        public int X { get; set; }

        public int Y { get; set; }


        public List<DomainProperty> ExtraProperties { get; set; } = new List<DomainProperty>();


        public override void ClearNavigationProperties()
        {
            ExtraProperties = null;
        }
    }
}
