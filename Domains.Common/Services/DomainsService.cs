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
        public DomainsService(AppDbContext db) : base(db)
        {
        }


        public async Task DeleteItem(int id)
        {
            var item = await _db.Domains.FirstOrDefaultAsync(i => i.Id == id);
            _db.Domains.Remove(item);
        }

        protected override IQueryable<Entity> IncludeProperties(IQueryable<Entity> query) 
            => query.Include(e => e.ExtraProperties);
    }
}
