using ErrorOr;
using KhaiMarket.API.DTO;
using KhaiMarket.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KhaiMarket.API.Features.Products;

public class GetProductById(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<ErrorOr<ProductDTO>> GetProductByIdAsync(int id)
    {
        //var product = await _context.Products
        //    .Include(x => x.Category)
        //    .Include(x => x.ProductBrand)
        //    .Include(x => x.Reviews)
        //    .SingleAsync();

        var product = await _context.Products.Where(x => x.Id == id).Select(x => new ProductDTO
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            Category = x.Category!.Name,
            Description = x.Description,
            ImageUrl = x.ImageUrl,
            ProductBrand = x.ProductBrand!.Name,
            ReviewsDTO = x.Reviews.Select(x => new ReviewDTO
            {
                VoterName = x.VoterName,
                Comment = x.Comment,
                NumStars = x.NumStars,
            }),
            Material = x.Material,
            Stock = x.Stock,
            TotalStars = x.Reviews.Count != 0
                ? x.Reviews.Select(x => x.NumStars).Average()
                : 0
        }).SingleOrDefaultAsync();

        if (product is null)
        {
            return ProductError.NotFound;
        }

        return product;
    }
}