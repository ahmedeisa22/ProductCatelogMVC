using Microsoft.EntityFrameworkCore;
using ProductCatDAL.Context;
using ProductCatDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductCatDAL
{
    public static class StoreContextSeed
    {
        public static async Task SeedCategoriesAsync(ProductContext _context)
        {
            if (!_context.Category.Any())
            {
                try
                {
                    var CategoryData = File.ReadAllText("../ProductCatDAL/DataSeed/Category.json");
                    var CategoriesVM = JsonSerializer.Deserialize<List<CategoryVM>>(CategoryData);
                    List<Category> Categories = new List<Category>();

                    if (CategoriesVM?.Count > 0)
                    {

                        foreach (var catVM in CategoriesVM)
                        {
                            Category category = new Category
                            {
                                CategoryName = catVM.name
                            };
                            Categories.Add(category);
                        }

                        await _context.Category.AddRangeAsync(Categories);
                        await _context.SaveChangesAsync();
                    }

                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"JSON deserialization error: {ex.Message}");
                }

            }
        }
    }
}
