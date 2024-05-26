using System.Text.Json;

namespace Backend.BLL.DataSeed
{
    public class AppContextSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            try
            {
                if(context.Products != null && !context.Brands.Any())
                {
                    var brandsData = File.ReadAllText("../Backend.Repository/DataSeed/Data/Brands.json");
                    var brands = JsonSerializer.Deserialize<List<Brand>>(brandsData);
                    if(brandsData != null)
                    {
                        await context.Brands.AddRangeAsync(brands);
                        await context.SaveChangesAsync();
                    }
                }

                if(context.Categories != null && !context.Categories.Any())
                {
                    var categoriesData = File.ReadAllText("../Backend.Repository/DataSeed/Data/Categories.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
                    if(categoriesData != null)
                    {
                        await context.Categories.AddRangeAsync(categories);
                        await context.SaveChangesAsync();
                    }

                }

                if (context.SubCategories != null && !context.SubCategories.Any())
                {
                    var subCategoriesData = File.ReadAllText("../Backend.Repository/DataSeed/Data/SubCategories.json");
                    var subCategories = JsonSerializer.Deserialize<List<SubCategory>>(subCategoriesData);
                    if (subCategoriesData != null)
                    {
                        await context.SubCategories.AddRangeAsync(subCategories);
                        await context.SaveChangesAsync();
                    }
                }

                if(context.Products != null && !context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Backend.Repository/DataSeed/Data/Products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if(products != null)
                    {
                        await context.Products.AddRangeAsync(products);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                 Console.WriteLine(ex);
            }
        }
    }
}
