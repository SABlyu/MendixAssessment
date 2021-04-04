using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domains.Common.Models
{
    public class DomainProperty : DbItem
    {
        [ForeignKey("Domain")]
        public int DomainId { get; set; }
        public Entity Domain { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }


        public override void ClearNavigationProperties()
        {
            if (Domain != null && Domain.Id > 0)
                DomainId = Domain.Id;
            Domain = null;
        }
    }
}
