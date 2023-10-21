using AutoMapper;
using ProductCatalog.API.Data.Entities.Products;
using ProductCatalog.API.DTO.Request;
using ProductCatalog.API.DTO.Response;

namespace ProductCatalog.API.Domain.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>();

        CreateMap<ProductRequest, Product>()
            .ForMember(p => p.Name, map => map.MapFrom(userInfoResponse => userInfoResponse.Name))
            .ForMember(p => p.Description, map => map.MapFrom(userInfoResponse => userInfoResponse.Description))
            .ForMember(p => p.PriceInRubles, map => map.MapFrom(userInfoResponse => userInfoResponse.PriceInRubles))
            .ForMember(p => p.GeneralNote, map => map.MapFrom(userInfoResponse => userInfoResponse.GeneralNote))
            .ForMember(p => p.SpecialNote, map => map.MapFrom(userInfoResponse => userInfoResponse.SpecialNote))
            .IgnoreAllPropertiesWithAnInaccessibleSetter();
    }
}