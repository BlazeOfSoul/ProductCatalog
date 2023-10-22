using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.Domain.Interfaces;
using ProductCatalog.API.DTO.Request;

namespace ProductCatalog.API.Controllers;

[Route("api/product/")]
[ApiController]
public class ProductController : BaseController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [Route("all")]
    // [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public IActionResult GetAllProducts()
    {
        var result = _productService.GetAllProducts();
        return Ok(result);
    }

    [HttpGet]
    [Route("all-category-name")]
    // [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public IActionResult GetAllByCategoryName([FromQuery] string categoryName)
    {
        var result = _productService.GetAllProductsByCategoryName(categoryName);
        return Ok(result);
    }

    [HttpPost]
    [Route("add")]
    // [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public async Task<IActionResult> AddProduct([FromBody] ProductRequest request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        await _productService.AddProduct(request);

        return Ok();
    }

    [HttpPut]
    [Route("update")]
    // [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductRequest productRequest)
    {
        await _productService.UpdateProduct(productRequest);

        return Ok();
    }

    [HttpDelete]
    [Route("delete/{productId:guid}")]
    // [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        await _productService.DeleteProduct(productId);

        return Ok();
    }
}