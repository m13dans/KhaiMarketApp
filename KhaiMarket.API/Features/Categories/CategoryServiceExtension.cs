namespace KhaiMarket.API.Features.Categories;

public static class CategoryServiceExtension
{
    public static IServiceCollection AddCategoryServices(this IServiceCollection services)
    {
        services.AddTransient<GetCategories>();

        return services;
    }
}