using IdentityServer4;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.Domain.Interfaces;
using ProductCatalog.API.DTO.Request;
using ProductCatalog.API.DTO.Request.Product;

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
    [Authorize]
    public IActionResult GetAllProducts()
    {
        if (((System.Security.Claims.ClaimsIdentity)User.Identity).HasClaim("role", "Admin") || ((System.Security.Claims.ClaimsIdentity)User.Identity).HasClaim("role", "Moderator"))
        {
            var result = _productService.GetAllProductsFull();
            return Ok(result);
        }
        else
        {
            var result = _productService.GetAllProductsPartial();
            return Ok(result);
        }
    }

    [HttpGet]
    [Route("all-category-name")]
    [Authorize(Roles = "Admin,Moderator", AuthenticationSchemes = IdentityServerConstants.LocalApi.AuthenticationScheme)]
    public IActionResult GetAllByCategoryName([FromQuery] string categoryName)
    {
        var result = _productService.GetAllProductsByCategoryName(categoryName);
        return Ok(result);
    }

    [HttpPost]
    [Route("add")]
    [Authorize]
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
    [Authorize(Roles = "Admin,Moderator", AuthenticationSchemes = IdentityServerConstants.LocalApi.AuthenticationScheme)]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductRequest productRequest)
    {
        await _productService.UpdateProduct(productRequest);

        return Ok();
    }

    [HttpPut]
    [Route("update-user")]
    [Authorize]
    public async Task<IActionResult> UpdateProductUser([FromBody] ProductRequestUser productRequest)
    {
        await _productService.UpdateProductUser(productRequest);

        return Ok();
    }

    [HttpDelete]
    [Route("delete/{productId:guid}")]
    [Authorize(Roles = "Admin,Moderator", AuthenticationSchemes = IdentityServerConstants.LocalApi.AuthenticationScheme)]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        await _productService.DeleteProduct(productId);

        return Ok();
    }
}