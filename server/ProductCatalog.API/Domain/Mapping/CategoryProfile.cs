using AutoMapper;
using ProductCatalog.API.Data.Entities.Categories;
using ProductCatalog.API.DTO.Request;
using ProductCatalog.API.DTO.Response;

namespace ProductCatalog.API.Domain.Mapping;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryRequest, Category>();

        CreateMap<Category, CategoryResponse>();
    }
}