using CmsShoppingCard.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CmsShoppingCard.Models
{
    public class SeedData
    {
        public static void Initialze(IServiceProvider serviceProvider) {
            using (var context = new CmsShoppingCardContext(serviceProvider.GetRequiredService<DbContextOptions<CmsShoppingCardContext>>()))
            {
                if(context.Pages.Any())
                {
                    return;
                }

                context.AddRange(
                    new Page
                    {
                        Title = "Home",
                        Slug = "home",
                        Content = "Home Page",
                        Sorting = 100
                    },
                    new Page
                    {
                        Title = "About Us",
                        Slug = "about-us",
                        Content = "About Us Page",
                        Sorting = 100
                    },
                    new Page
                    {
                        Title = "Services",
                        Slug = "services",
                        Content = "Services Page",
                        Sorting = 100
                    }

                );
                context.SaveChanges();
            }
        }
    }
}
