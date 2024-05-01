
using ErrorOr;
using KhaiMarket.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KhaiMarket.API.Features.Products;

public class DeleteProductById
{
    private readonly AppDbContext _context;

    public DeleteProductById(AppDbContext context)
    {
        _context = context;
    }

    internal async Task<ErrorOr<Deleted>> DeleteProduct(int id)
    {
        var product = await _context.Products
            .Include(x => x.Category)
            .Include(x => x.ProductBrand)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (product is null)
        {
            return ProductError.NotFound;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return Result.Deleted;
    }
}