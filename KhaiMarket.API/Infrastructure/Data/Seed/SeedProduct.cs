using KhaiMarket.API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace KhaiMarket.API.Infrastructure.Data.Seed
{
    public static class SeedProduct
    {
        private readonly static List<ProductBrand> _listProductBrands =
        [
            new ProductBrand{ Id = 1, Name = "Khaela Hijab"},
            new ProductBrand{ Id = 2, Name = "Azara"},
            new ProductBrand{ Id = 3, Name = "Khaela"},
        ];

        private readonly static List<Category> _listCategories =
        [
            new Category{ Id = 1, Name = "Hijab"},
            new Category{ Id = 2, Name = "Hijab Segi Empat"}, new Category { Id = 3, Name = "Hijab Pasmina"},
            new Category{ Id = 4, Name = "Hijab Instan"}, new Category { Id = 5, Name = "Ciput"},
        ];

        private readonly static List<Review> _listReviews =
        [
            new() {VoterName = "Miya", NumStars = 5, Comment = "Sangat Bagus Hijabnya", ProductId = 1},
            new() {VoterName = "Masha", NumStars = 4, Comment = "Bagus Hijabnya", ProductId = 1},

            new() {VoterName = "Sasha", NumStars = 5, Comment = "Sangat Bagus Hijabnya", ProductId = 2},
            new() {VoterName = "Masha", NumStars = 5, Comment = "Sangat Bagus Hijabnya", ProductId = 2},

            new() {VoterName = "Sasha", NumStars = 4, Comment = "Bagus Hijabnya", ProductId = 3},
            new() {VoterName = "Masha", NumStars = 5, Comment = "Sangat Bagus Hijabnya", ProductId = 3},
        ];

        private readonly static List<Product> _listProducts =
        [
            new Product
            {
                Id = 1,
                Name = "Night Flower Hijab",
                Description = "Hijab Elegan dengan motif bunga dan perpaduan warna hitam",
                Stock = 1,
                Material = "Sublim",
                ImageUrl = "http://localhost:5018/images/hijab-code-1.jpg",
                Price = 38_000m,
                Category = _listCategories.Find(x => x.Id is 2),
                CategoryId = 2,
                ProductBrandId = 1,
                ProductBrand = _listProductBrands.Find(x => x.Id is 1),
                Reviews = _listReviews!.Where(x => x.ProductId is 1).ToList()
            },
            new Product
            {
                Id = 2,
                Name = "Antique Pink Hijab",
                Description = "Hijab Elegan Minimalis Pink",
                Stock = 1,
                Material = "Sublim",
                ImageUrl = "http://localhost:5018/images/hijab-code-2.jpg",
                Price = 38_000m,
                Category = _listCategories.Find(x => x.Id is 2),
                CategoryId = 2,
                ProductBrandId = 1,
                ProductBrand = _listProductBrands.Find(x => x.Id is 1),
                Reviews = _listReviews!.Where(x => x.ProductId is 2).ToList()
            },
            new Product
            {
                Id = 3,
                Name = "Burlywood Hijab",
                Description = "Hijab Elegan dengan corak bunga dan perpaduan warna yang menenangkan",
                Stock = 1,
                Material = "Sublim",
                ImageUrl = "http://localhost:5018/images/hijab-code-3.jpg",
                Price = 38_000m,
                Category = _listCategories.Find(x => x.Id is 2),
                CategoryId = 2,
                ProductBrandId = 1,
                ProductBrand = _listProductBrands.Find(x => x.Id is 1),
                Reviews = _listReviews!.Where(x => x.ProductId is 3).ToList()
            }
        ];

        public static void Seed(AppDbContext context)
        {
            context.Categories.AddRange(_listCategories);
            context.ProductBrands.AddRange(_listProductBrands);
            context.Reviews.AddRange(_listReviews);
            context.Products.AddRange(_listProducts);

            context.SaveChanges();
        }
    }

    public static class MigrationExtension
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            try
            {
                context.Database.Migrate();

                if (context.Products.FirstOrDefault() is null)
                {
                    SeedProduct.Seed(context);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}