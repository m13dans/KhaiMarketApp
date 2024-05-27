using KhaiMarket.Blazor.Model;

namespace KhaiMarket.Blazor.Model;

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; }
    public string Material { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public string ProductBrand { get; set; } = string.Empty;
    public IEnumerable<ReviewDTO>? ReviewsDTO { get; set; }
    public double? TotalStars { get; set; }
}
