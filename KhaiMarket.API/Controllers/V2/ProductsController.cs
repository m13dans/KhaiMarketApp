using Asp.Versioning;
using KhaiMarket.API.Core.Entities;
using KhaiMarket.API.DTO;
using KhaiMarket.API.Features.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhaiMarket.API.Controllers.V2;

[ApiVersion(2.0)]
[ApiController]
[Route("api/v{Version:ApiVersion}/[controller]")]
public class ProductsController : ApiController
{
    // I Use Method Injection instead of constructor injection
    // for my service so the action only use what service it need
    [HttpGet]
    // [Authorize]
    public async Task<IResult> Get([FromQuery] SortFilterPageOption paging,
        [FromServices] GetProductWithPagination query)
    {
        var products = await query.GetProductsAsync(paging);

        return products.Match(
            products => Results.Ok(products),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
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
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IResult> Create([FromBody] UpsertProductDto upsertProductDto,
        [FromServices] CreateProduct command)
    {
        var result = await command.Create(upsertProductDto);
        if (result.IsError)
        {
            return Problem(result.Errors);
        }
        return Results.Created();
    }

    [HttpPut]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IResult> Update(int id, [FromBody] UpsertProductDto upsertProductDto, [FromServices] UpdateProduct command)
    {
        var result = await command.UpdateProductById(id, upsertProductDto);
        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        return Results.NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin, Manager")]
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
