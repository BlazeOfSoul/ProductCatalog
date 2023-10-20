using ProductCatalog.API.Data.Entities.Categories;

namespace ProductCatalog.API.Data.Entities.Products;

public class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public double PriceInRubles { get; set; }

    public string? GeneralNote { get; set; }

    public string? SpecialNote { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}