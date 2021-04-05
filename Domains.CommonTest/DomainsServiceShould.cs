using Domains.Common;
using Domains.Common.Models;
using Domains.Common.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Domains.CommonTest
{
    public class DomainServiceShould
    {
        public DomainServiceShould()
        {
        }


        [Fact]
        public async Task DomainCreated()
        {
            var domains = GetInMemoryDomainsService();

            var domain = new Entity
            {
                Name = "Test entity 1",
                X = 1,
                Y = 2
            };

            var item = await domains.InsertItem(domain);

            Assert.NotNull(item);
            Assert.NotEqual(0, item.Id);
            Assert.Equal(1, item.X);
            Assert.Equal(2, item.Y);
        }

        [Fact]
        public async Task DomainLoaded()
        {
            var domains = GetInMemoryDomainsService();
            var newItem = new Entity
            {
                Name = "Test entity 1",
                X = 1,
                Y = 2
            };
            var item = await domains.InsertItem(newItem);

            var domain = await domains.GetItem(1);
            Assert.NotNull(domain);
        }

        [Fact]
        public async Task DomainUpdated()
        {
            var domains = GetInMemoryDomainsService();
            var newItem = new Entity
            {
                Name = "Test entity 1",
                X = 1,
                Y = 2
            };
            var domain = await domains.InsertItem(newItem);

            var newName = "updated name";

            domain.Name = newName;
            domain.X = 3;
            domain.Y = 4;

            var item = await domains.UpdateItem(domain);

            Assert.NotNull(item);
            Assert.Equal(newName, item.Name);
            Assert.Equal(domain.X, item.X);
            Assert.Equal(domain.Y, item.Y);
        }


        private DomainsService GetInMemoryDomainsService()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("domains-test");
            options = builder.Options;
            var db = new AppDbContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var properties = new DomainPropertyService(db);
            var domains = new DomainsService(db, properties);

            return domains;
        }
    }
}
