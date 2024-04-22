using KhaiMarket.API.Products.Core.Entities;
using Microsoft.VisualBasic;

namespace KhaiMarket.API.Products.DTO;

public class ProductDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; }
    public string Material { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Category? Category { get; set; }
    public ProductBrand? ProductBrand { get; set; }
    public ICollection<ReviewDTO> ReviewsDTO { get; set; } = new List<ReviewDTO>();
    public float TotalStars { get; set; }
}
