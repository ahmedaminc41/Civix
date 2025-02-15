using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Civix.App.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Civix.App.Repositories.Data
{
    public static class CivixDbSeed
    {
        public async static Task SeedAsync(CivixDbContext _context)
        {
            if (_context.Categories.Count() == 0)
            {
                var categoryData = File.ReadAllText("../Civix.App.Repositories/Data/DataSeed/categories.json");

                var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);


                // 3. Seed At The Database
                if (categories?.Count() > 0)
                {
                    foreach (var category in categories)
                    {
                       await _context.Categories.AddAsync(category);
                    }
                    await _context.SaveChangesAsync();
                }

            }




        }
    }
}
