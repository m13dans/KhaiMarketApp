using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using KhaiMarket.API.Features.Brands;
using KhaiMarket.API.Features.Categories;
using Microsoft.AspNetCore.Mvc;

namespace KhaiMarket.API.Controllers.V2
{
    [ApiVersion(2.0)]
    [ApiController]
    [Route("api/v{Version:ApiVersion}/[controller]")]
    public class ProductBrandsController : ApiController
    {
        [HttpGet]
        public async Task<IResult> GetBrands([FromServices] GetProductBrands getBrands)
        {
            var brands = await getBrands.GetAsync();
            if (brands.IsError)
            {
                return Results.NotFound();
            }
            return Results.Ok(brands.Value);
        }

    }
}