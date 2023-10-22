namespace ProductCatalog.API.DTO.Request;

public class ProductRequest
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double PriceInRubles { get; set; }

    public string GeneralNote { get; set; }

    public string SpecialNote { get; set; }

    public string CategoryName { get; set; }
}