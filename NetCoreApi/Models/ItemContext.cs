using Microsoft.EntityFrameworkCore;

namespace NetCoreApi.Models
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {

        }

        public DbSet<ItemModel> Items { get; set; }
    }
}