namespace KhaiMarket.API.DTO;

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
    public ICollection<ReviewDTO> ReviewsDTO { get; set; } = new List<ReviewDTO>();
    public float? TotalStars { get; set; }
}
