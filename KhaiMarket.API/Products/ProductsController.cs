using KhaiMarket.API.Products.DTO;
using KhaiMarket.API.Products.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static KhaiMarket.API.Products.GetProducts;

namespace KhaiMarket.API.Products;

[Route("api/Products")]
[ApiController]
public class ProductsController : ControllerBase
{
    // private readonly GetProductsQuery _query;
    // public ProductsController(GetProductsQuery query)
    // {
    //     _query = query;
    // }
    [HttpGet]
    public async Task<ActionResult> Get(GetProductsQuery query)
    {
        var products = await query.GetProductsAsync();
        if (products is null )
        {
            return NotFound(products);
        }

        return Ok(products);
    }
}