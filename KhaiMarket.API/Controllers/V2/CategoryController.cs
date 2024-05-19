using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using KhaiMarket.API.Features.Categories;
using Microsoft.AspNetCore.Mvc;

namespace KhaiMarket.API.Controllers.V2
{
    [ApiVersion(2.0)]
    [ApiController]
    [Route("api/v{Version:ApiVersion}/[controller]")]
    public class CategoryController : ApiController
    {
        public async Task<IResult> GetCategories([FromServices] GetCategories getCategories)
        {
            var categories = await getCategories.GetAsync();
            if (categories.IsError)
            {
                return Results.NotFound();
            }
            return Results.Ok();
        }

    }
}