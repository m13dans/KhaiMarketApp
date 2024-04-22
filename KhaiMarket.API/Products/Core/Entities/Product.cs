namespace KhaiMarket.API.Products.Core.Entities;

public class Product : BaseEntities
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; }
    public string Material {get; set;} = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Category? Category { get; set; }
    public int CategoryId { get; set; }
    public ProductBrand? ProductBrand { get; set; }
    public int ProductBrandId { get; set; }
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
