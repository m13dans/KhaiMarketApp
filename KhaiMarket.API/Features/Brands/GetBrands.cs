using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using KhaiMarket.API.Core.Entities;
using KhaiMarket.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KhaiMarket.API.Features.Brands;

public class GetProductBrands
{
    private readonly AppDbContext _context;
    public GetProductBrands(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<ProductBrand>>> GetAsync()
    {
        var productBrands = _context.ProductBrands;
        if (!await productBrands.AnyAsync())
        {
            return Error.NotFound();
        }
        var result = await productBrands.ToListAsync();
        return result;
    }

}