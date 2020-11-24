using BreakDanceBattles.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakDanceBattles.Data.Seeding
{
    public class CountrySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Countries.Any())
            {
                return;
            }
            await dbContext.Countries.AddAsync(new Country { Name = "Bulgaria" });
            await dbContext.Countries.AddAsync(new Country { Name = "Greece" });
            await dbContext.Countries.AddAsync(new Country { Name = "USA" });
           
            await dbContext.SaveChangesAsync();
        }
    }
}
