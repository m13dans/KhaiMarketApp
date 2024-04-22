using KhaiMarket.API.Products.Infrastructure.Data;
using KhaiMarket.API.Products.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhaiMarket.API.Extension
{
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