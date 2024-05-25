using ErrorOr;
using KhaiMarket.API.DTO;
using KhaiMarket.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KhaiMarket.API.Features.Products;

public class GetProductWithPagination(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<ErrorOr<List<ProductDTO>>> GetProductsAsync(SortFilterPageOption param)
    {
        var products = _context.Products.AsNoTracking()
                       .AsSplitQuery()
                       .Include(p => p.Category)
                       .Include(p => p.ProductBrand)
                       .Include(p => p.Reviews)
                       .MapProductToDto()
                       .OrderProductBy(param.OrderByOptions)
                       .SearchProduct(param.Search)
                       .FilterProductBy(param.ProductFilterByOptions, param.FilterValue)
                       .Where(x => x.Price >= param.LowestPrice && x.Price <= param.HigestPrice)
                       .Where(x => x.TotalStars >= param.Rating)
                       .Skip((param.PageNumber - 1) * param.PageSize)
                       .Take(param.PageSize);

        if (products is null)
        {
            return ProductError.NotFound;
        }

        return await products.ToListAsync();
    }
}