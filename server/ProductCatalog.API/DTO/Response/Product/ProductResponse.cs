﻿namespace ProductCatalog.API.DTO.Response;

public class ProductResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal PriceInRubles { get; set; }

    public decimal PriceInDollars { get; set; }

    public string GeneralNote { get; set; }

    public string SpecialNote { get; set; }

    public string CategoryName { get; set; }
}