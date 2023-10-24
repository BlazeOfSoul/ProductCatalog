using ProductCatalog.API.Data.Entities.Categories;

namespace ProductCatalog.API.Data.Entities.Products;

public class Product
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal PriceInRubles { get; set; }

    public string? GeneralNote { get; set; }

    public string? SpecialNote { get; set; }

    public Guid CategoryId { get; set; }

    public Category Category { get; set; }
}