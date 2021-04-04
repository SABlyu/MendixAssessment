using Domains.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Common.Services
{
    public class DomainsService : DbItemService<Entity>
    {
        public DomainsService(AppDbContext db, DomainPropertyService domainPropertyService) : base(db)
        {
            _properties = domainPropertyService;
        }


        public override async Task<Entity> InsertItem(Entity item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
            return await GetItem(item.Id);
        }

        public override async Task<Entity> UpdateItem(Entity entity)
        {
            await _properties.UpdateItems(entity.ExtraProperties);
            return await base.UpdateItem(entity);
        }


        protected override IQueryable<Entity> IncludeProperties(IQueryable<Entity> query) 
            => query.Include(e => e.ExtraProperties);


        private readonly DomainPropertyService _properties;
    }
}
