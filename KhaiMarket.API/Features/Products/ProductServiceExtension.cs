namespace KhaiMarket.API.Features.Products;

public static class ProductServiceExtension
{
    public static IServiceCollection AddProductServices(this IServiceCollection services)
    {
        services.AddTransient<GetProducts>();
        services.AddTransient<GetProductById>();
        services.AddTransient<CreateProduct>();
        services.AddTransient<UpdateProduct>();
        services.AddTransient<DeleteProductById>();
        services.AddTransient<GetProductWithPagination>();

        return services;
    }
}