using ErrorOr;
using KhaiMarket.API.DTO;
using KhaiMarket.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KhaiMarket.API.Features.Products;

public class GetProductWithPagination(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<ErrorOr<List<ProductDTO>>> GetProductsAsync(ProductParameter param)
    {
        var products = await _context.Products.AsNoTracking()
                       .AsSplitQuery()
                       .Include(p => p.Category)
                       .Include(p => p.ProductBrand)
                       .Include(p => p.Reviews)
                       .OrderBy(p => p.Name)
                       .Skip((param.PageNumber - 1) * param.PageSize)
                       .Take(param.PageSize)
                       .ToListAsync();

        if (products.Count is 0)
        {
            return ProductError.NotFound;
        }

        var result = products.Select(p => new ProductDTO
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Stock = p.Stock,
            Material = p.Material,
            ImageUrl = p.ImageUrl,
            Price = p.Price,
            Category = p.Category!.Name,
            ProductBrand = p.ProductBrand!.Name,
            ReviewsDTO = p.Reviews.Select(x => new ReviewDTO
            {
                VoterName = x.VoterName,
                Comment = x.Comment,
                NumStars = x.NumStars,
            }),
            TotalStars = p.Reviews.Count != 0
                ? Math.Round(p.Reviews.Select(x => x.NumStars).Average(), 2)
                : 0

        });
        var resultWithFilter = result.Where(x => x.TotalStars >= param.MinRating);
        return resultWithFilter.ToList();
    }
}