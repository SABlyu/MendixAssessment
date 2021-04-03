using Domains.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domains.Common.Services
{
    public class DomainPropertyService : DbItemService<DomainProperty>
    {
        public DomainPropertyService(AppDbContext db) : base(db)
        {
        }

        protected override IQueryable<DomainProperty> IncludeProperties(IQueryable<DomainProperty> query)
        {
            return query; // do nothing
        }
    }
}
