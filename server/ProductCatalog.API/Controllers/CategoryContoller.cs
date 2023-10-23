using IdentityServer4;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.Domain.Interfaces;
using ProductCatalog.API.DTO.Request;

namespace ProductCatalog.API.Controllers;

[Route("api/category/")]
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
    [Route("all")]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await _categoryService.GetAllCategories();
        return Ok(result);
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddCategory(CategoryRequest request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        await _categoryService.AddCategory(request);

        return Ok();
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryRequest categoryRequest)
    {
        await _categoryService.UpdateCategory(categoryRequest);

        return Ok();
    }

    [HttpDelete]
    [Route("delete/{categoryId:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid categoryId)
    {
        await _categoryService.DeleteCategory(categoryId);

        return Ok();
    }
}