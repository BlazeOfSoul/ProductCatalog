using ProductCatalog.API.Data.Entities.Products;

namespace ProductCatalog.API.Data.Entities.Categories;

public class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public List<Product> Products { get; set; }
}