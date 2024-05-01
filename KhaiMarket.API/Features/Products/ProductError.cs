using ErrorOr;

namespace KhaiMarket.API.Features.Products
{
    public static class ProductError
    {
        public static Error NotFound => Error.NotFound(
            code: "Product.NotFound",
            description: "Product Not Found"
        );
    }
}