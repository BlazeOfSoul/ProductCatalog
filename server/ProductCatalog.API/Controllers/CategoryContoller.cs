using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.Controllers.Routes;
using ProductCatalog.API.Domain.Interfaces;
using ProductCatalog.API.DTO.Request;

namespace ProductCatalog.API.Controllers;

[ApiController]
[Authorize(Roles = "Admin,Moderator", AuthenticationSchemes = IdentityServerConstants.LocalApi.AuthenticationScheme)]
public class CategoryContoller : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoryContoller(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [Route(CategoryRoutes.GetAllCategories)]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await _categoryService.GetAllCategories();
        return Ok(result);
    }

    [HttpPost]
    [Route(CategoryRoutes.AddCategory)]
    public async Task<IActionResult> AddCategory(CategoryRequest request)
    {
        if (request == null) return BadRequest();

        await _categoryService.AddCategory(request);

        return Ok();
    }

    [HttpPut]
    [Route(CategoryRoutes.UpdateCategory)]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryRequest categoryRequest)
    {
        await _categoryService.UpdateCategory(categoryRequest);

        return Ok();
    }

    [HttpDelete]
    [Route(CategoryRoutes.DeleteCategory)]
    public async Task<IActionResult> DeleteProduct(Guid categoryId)
    {
        await _categoryService.DeleteCategory(categoryId);

        return Ok();
    }
}