using Domains.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Common.Services
{
    public abstract class DbItemService<T> where T: DbItem
    {
        public DbItemService(AppDbContext db)
        {
            _db = db;
        }


        public virtual bool Any() => _db.Set<T>().Any();

        public virtual Task<T> GetItem(int id) 
            => IncludeProperties(_db.Set<T>().AsNoTracking())
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

        public virtual Task<List<T>> GetItems()
            => IncludeProperties(_db.Set<T>().AsNoTracking())
                .ToListAsync();

        public virtual async Task<T> InsertItem(T item)
        {
            item.ClearNavigationProperties();
            await _db.AddAsync<T>(item);
            await _db.SaveChangesAsync();

            return await GetItem(item.Id);
        }

        public virtual async Task InsertItemNoSave(T item)
        {
            item.ClearNavigationProperties();
            await _db.AddAsync<T>(item);
        }

        public virtual async Task UpdateItemNoSave(T entity)
        {
            entity.ClearNavigationProperties();

            var item = await _db.FindAsync<T>(entity.Id);
            var entry = _db.Entry(item);
            entry.CurrentValues.SetValues(entity);
            entry.State = EntityState.Modified;
        }

        public virtual async Task<T> UpdateItem(T entity)
        {
            entity.ClearNavigationProperties();

            var item = await _db.FindAsync<T>(entity.Id);
            var entry = _db.Entry(item);
            entry.CurrentValues.SetValues(entity);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return await this.GetItem(entity.Id);
        }

        public virtual async Task UpdateItems(List<T> items, bool save = true)
        {
            foreach (var item in items)
            {
                item.ClearNavigationProperties();

                if (item.Id == 0)
                    await _db.AddAsync<T>(item);
                else
                    await UpdateItemNoSave(item);
            }

            if (save)
                await _db.SaveChangesAsync();
        }

        public virtual async Task RemoveItem(T item)
        {
            if (item == null || item.Id == 0)
                return;
            var entity = await _db.Set<T>().FindAsync(item.Id);
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        protected abstract IQueryable<T> IncludeProperties(IQueryable<T> query);


        protected readonly AppDbContext _db;
    }
}
