using Asp.Versioning;
using KhaiMarket.API.DTO;
using KhaiMarket.API.Features.Products;
using Microsoft.AspNetCore.Mvc;

namespace KhaiMarket.API.Controllers.V1;

[ApiVersion(1.0, Deprecated = true)]
[ApiController]
[Route("api/[controller]")]
[Route("api/v{Version:ApiVersion}/[controller]")]
public class ProductsController : ApiController
{
    // I Use Method Injection instead of constructor injection
    // for my service so the action only use what service it need

    [HttpGet]
    public async Task<IResult> Get([FromServices] GetProducts query)
    {
        var products = await query.GetProductsAsync();

        return products.Match(
            products => Results.Ok(products),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> GetById(int id, [FromServices] GetProductById query)
    {
        var product = await query.GetProductByIdAsync(id);

        if (product.IsError)
        {
            return Problem(product.Errors);
        }

        return Results.Ok(product.Value);
    }

    [HttpPost]
    public async Task<IResult> Create(ProductDTO productDTO, [FromServices] CreateProduct command)
    {
        var result = await command.Create(productDTO);
        if (result.IsError)
        {
            return Problem(result.Errors);
        }
        return Results.Created();
    }

    [HttpPut]
    public async Task<IResult> Update(int id, [FromBody] ProductDTO productDTO, [FromServices] UpdateProduct command)
    {
        var result = await command.UpdateProductById(id, productDTO);
        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        return Results.NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IResult> DeleteById(int id, [FromServices] DeleteProductById query)
    {
        var result = await query.DeleteProduct(id);

        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        return Results.NoContent();
    }
}
