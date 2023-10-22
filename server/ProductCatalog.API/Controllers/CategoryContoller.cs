using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.Domain.Interfaces;
using ProductCatalog.API.DTO.Request;

namespace ProductCatalog.API.Controllers;

[Route("api/category/")]
[ApiController]
public class CategoryContoller : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoryContoller(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [Route("all")]
    // [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public async Task<IActionResult> Get()
    {
        var result = await _categoryService.GetAllCategories();
        return Ok(result);
    }

    [HttpPost]
    [Route("add")]
    // [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public async Task<IActionResult> AddProduct(CategoryRequest request)
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
    // [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryRequest categoryRequest)
    {
        await _categoryService.UpdateCategory(categoryRequest);

        return Ok();
    }

    [HttpDelete]
    [Route("delete/{categoryId:guid}")]
    // [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public async Task<IActionResult> DeleteProduct(Guid categoryId)
    {
        await _categoryService.DeleteCategory(categoryId);

        return Ok();
    }
}