using KhaiMarket.API.Features.Brands;

namespace KhaiMarket.API.Features.Categories;

public static class ProductBrandServiceExtension
{
    public static IServiceCollection AddProductBrandServices(this IServiceCollection services)
    {
        services.AddTransient<GetProductBrands>();

        return services;
    }
}