using ErrorOr;
using KhaiMarket.API.Core.Entities;
using KhaiMarket.API.DTO;
using KhaiMarket.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KhaiMarket.API.Features.Products;

public class UpdateProduct
{
    private readonly AppDbContext _context;

    public UpdateProduct(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Updated>> UpdateProductById(int id, UpsertProductDto request)
    {
        var product = await _context.Products
            .Include(x => x.Category)
            .Include(x => x.ProductBrand)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (product is null)
        {
            return Error.NotFound();
        }

        var updatedProduct = MapToProductUpdate(request, product);

        await AssignCategory(request, product);
        await AssignBrand(request, product);

        _context.Update(updatedProduct);
        await _context.SaveChangesAsync();

        return Result.Updated;
    }

    private static Product MapToProductUpdate(UpsertProductDto request, Product product)
    {
        product.Name = request.Name;
        product.Description = request.Description;
        product.Stock = request.Stock;
        product.Material = request.Material;
        product.ImageUrl = request.ImageUrl;
        product.Price = request.Price;
        product.Category = new Category { Name = request.Category };
        product.ProductBrand = new ProductBrand { Name = request.ProductBrand };

        return product;
    }

    private async Task<Product> AssignCategory(UpsertProductDto request, Product product)
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

    private async Task<Product> AssignBrand(UpsertProductDto request, Product product)
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