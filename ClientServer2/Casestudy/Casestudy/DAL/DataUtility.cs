using Casestudy.DAL.DomainClasses;
using System.Text.Json;


namespace Casestudy.DAL
{
    public class DataUtility
    {
        private readonly AppDbContext _db;
        public DataUtility(AppDbContext context)
        {
            _db = context;
        }

        public async Task<bool> LoadStoreInfoFromWebToDb(string stringJson)
        {
            bool categoriesLoaded = false;
            bool menuItemsLoaded = false;
            try
            {
                // an element that is typed as dynamic is assumed to support any operation
                dynamic? objectJson = JsonSerializer.Deserialize<Object>(stringJson);
                categoriesLoaded = await LoadBrands(objectJson);
                menuItemsLoaded = await LoadProducts(objectJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return categoriesLoaded && menuItemsLoaded;
        }

        private async Task<bool> LoadBrands(dynamic jsonObjectArray)
        {
            bool loadedCategories = false;
            try
            {
                // clear out the old rows
                _db.Brands?.RemoveRange(_db.Brands);
                await _db.SaveChangesAsync();
                List<String> allCategories = new();
                foreach (JsonElement element in jsonObjectArray.EnumerateArray())
                {
                    if (element.TryGetProperty("BRAND", out JsonElement menuItemJson))
                    {
                        allCategories.Add(menuItemJson.GetString()!);
                    }
                }
                IEnumerable<String> brands = allCategories.Distinct<String>();
                foreach (string brand_name in brands)
                {
                    Brand brand = new();
                    brand.Name = brand_name;
                    await _db.Brands!.AddAsync(brand);
                    await _db.SaveChangesAsync();
                }
                loadedCategories = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
            return loadedCategories;
        }

        private async Task<bool> LoadProducts(dynamic jsonObjectArray)
        {
            bool loadedItems = false;
            try
            {
                List<Brand> brands = _db.Brands!.ToList();
                // clear outthe old
                _db.Products?.RemoveRange(_db.Products);
                await _db.SaveChangesAsync();
                foreach (JsonElement element in jsonObjectArray.EnumerateArray())
                {
                    Product product = new();

                    string? brand_name = element.GetProperty("BRAND").GetString();
                    string? product_name = element.GetProperty("PRODUCT_NAME").GetString();

                    // setting properties
                    product.Id = brand_name + " " + product_name;
                    product.ProductName = product_name;
                    product.GraphicName = element.GetProperty("GRAPHIC_NAME").GetString();
                    product.CostPrice = Convert.ToDecimal(element.GetProperty("COST_PRICE").GetDecimal());
                    product.MSRP = Convert.ToDecimal(element.GetProperty("MSRP").GetDecimal());
                    product.QtyOnHand = Convert.ToInt32(element.GetProperty("QTY_HAND").GetInt32());
                    product.QtyOnBackOrder = Convert.ToInt32(element.GetProperty("QTY_BACKORDER").GetInt32());
                    product.Description = element.GetProperty("DESCRIPTION").GetString();

                    // add the FK here
                    foreach (Brand brand in brands)
                    {
                        if (brand.Name == brand_name)
                        {
                            product.Brand = brand;
                            break;
                        }
                    }
                    await _db.Products!.AddAsync(product);
                    await _db.SaveChangesAsync();
                }
                loadedItems = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
            return loadedItems;
        }// LoadMenuItems

    }
}