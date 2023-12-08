using CmsShoppingCard.Models;
using Microsoft.EntityFrameworkCore;

namespace CmsShoppingCard.Infrastructure
{
    public class CmsShoppingCardContext: DbContext
    {
        public CmsShoppingCardContext(DbContextOptions<CmsShoppingCardContext> options): base(options)
        {

        }
        public DbSet<Page> Pages { get; set; }

    }
}
