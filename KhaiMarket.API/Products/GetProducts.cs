using KhaiMarket.API.Products.Core.Entities;
using KhaiMarket.API.Products.DTO;
using KhaiMarket.API.Products.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KhaiMarket.API.Products;

public static class GetProducts
{
    public class GetProductsQuery(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<ProductDTO>> GetProductsAsync()
        {
            var products = await _context.Products.AsNoTracking()
                           .Include(p => p.Category)
                           .Include(p => p.ProductBrand)
                           .Include(p => p.Reviews)
                           .ToListAsync();

            var result = products.Select(p => new ProductDTO
            {
                Name = p.Name,
                Description = p.Description,
                Stock = p.Stock,
                Material = p.Material,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Category = p.Category,
                ProductBrand = p.ProductBrand,
                ReviewsDTO = p.Reviews.Select(x => new ReviewDTO
                {
                    VoterName = x.VoterName,
                    Comment = x.Comment,
                    NumStars = x.NumStars,
                }).ToList(),
                TotalStars = (float)p.Reviews.Sum(x => x.NumStars) / p.Reviews.Count

            }).ToList();

            return result;
        }

    }
}
