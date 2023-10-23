using AutoMapper;
using ProductCatalog.API.Data.Entities.Products;
using ProductCatalog.API.DTO.Request;
using ProductCatalog.API.DTO.Request.Product;
using ProductCatalog.API.DTO.Response;

namespace ProductCatalog.API.Domain.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(pr => pr.CategoryName, map => map.MapFrom(p => p.Category.Name));

        CreateMap<ProductRequest, Product>()
            .ForMember(p => p.Name, map => map.MapFrom(pr => pr.Name))
            .ForMember(p => p.Description, map => map.MapFrom(pr => pr.Description))
            .ForMember(p => p.PriceInRubles, map => map.MapFrom(pr => pr.PriceInRubles))
            .ForMember(p => p.GeneralNote, map => map.MapFrom(pr => pr.GeneralNote))
            .ForMember(p => p.SpecialNote, map => map.MapFrom(pr => pr.SpecialNote))
            .IgnoreAllPropertiesWithAnInaccessibleSetter();

        CreateMap<ProductRequestUser, Product>()
           .ForMember(p => p.Name, map => map.MapFrom(pr => pr.Name))
           .ForMember(p => p.Description, map => map.MapFrom(pr => pr.Description))
           .ForMember(p => p.PriceInRubles, map => map.MapFrom(pr => pr.PriceInRubles))
           .ForMember(p => p.GeneralNote, map => map.MapFrom(pr => pr.GeneralNote))
           .IgnoreAllPropertiesWithAnInaccessibleSetter();
    }
}