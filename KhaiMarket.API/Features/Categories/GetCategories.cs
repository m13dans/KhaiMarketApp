using ErrorOr;
using KhaiMarket.API.Core.Entities;
using KhaiMarket.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KhaiMarket.API.Features.Categories
{
    public class GetCategories
    {
        private readonly AppDbContext _context;
        public GetCategories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<List<Category>>> GetAsync()
        {
            var categories = _context.Categories;
            if (!await categories.AnyAsync())
            {
                return Error.NotFound();
            }
            var result = await categories.ToListAsync();
            return result;
        }

    }
}