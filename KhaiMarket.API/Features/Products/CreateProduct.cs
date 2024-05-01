using ErrorOr;
using KhaiMarket.API.Core.Entities;
using KhaiMarket.API.DTO;
using KhaiMarket.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KhaiMarket.API.Features.Products;

public class CreateProduct
{
    private readonly AppDbContext _context;

    public CreateProduct(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Created>> Create(ProductDTO request)
    {
        var product = MapToProduct(request);

        await AssignCategory(request, product);
        await AssignBrand(request, product);

        await _context.AddAsync(product);
        await _context.SaveChangesAsync();

        return Result.Created;
    }

    private static Product MapToProduct(ProductDTO request)
    {
        return new Product
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Stock = request.Stock,
            Material = request.Material,
            ImageUrl = request.ImageUrl,
            Price = request.Price,
            Reviews = request.ReviewsDTO.Select(x => new Review
            {
                Id = x.Id,
                VoterName = x.VoterName,
                NumStars = x.NumStars,
                Comment = x.Comment,
            }).ToList()
        };
    }

    private async Task<Product> AssignCategory(ProductDTO request, Product product)
    {
        if (string.IsNullOrEmpty(request.Category))
        {
            return product;
        }

        var existingCategory = await _context.Categories
            .SingleOrDefaultAsync(c => c.Name.ToLower()
            == request.Category.ToLower());

        product.Category = existingCategory ?? new Category { Name = request.Category };

        return product;
    }

    private async Task<Product> AssignBrand(ProductDTO request, Product product)
    {
        if (string.IsNullOrEmpty(request.ProductBrand))
        {
            return product;
        }

        var existingBrand = await _context.ProductBrands
                                    .SingleOrDefaultAsync(c => c.Name.ToLower()
                                                          == request.ProductBrand.ToLower());
        product.ProductBrand = existingBrand ?? new ProductBrand { Name = request.ProductBrand };

        return product;
    }
}
