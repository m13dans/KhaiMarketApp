using ErrorOr;
using KhaiMarket.API.DTO;
using KhaiMarket.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KhaiMarket.API.Features.Products;

public class GetProducts(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<ErrorOr<List<ProductDTO>>> GetProductsAsync()
    {
        var products = await _context.Products.AsNoTracking()
                       .AsSplitQuery()
                       .Include(p => p.Category)
                       .Include(p => p.ProductBrand)
                       .Include(p => p.Reviews)
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
                Id = x.Id,
                VoterName = x.VoterName,
                Comment = x.Comment,
                NumStars = x.NumStars,
            }).ToList(),
            TotalStars = p.Reviews.Count != 0
                ? (float)p.Reviews.Select(x => x.NumStars).Average()
                : 0

        }).ToList();

        return result;
    }
}