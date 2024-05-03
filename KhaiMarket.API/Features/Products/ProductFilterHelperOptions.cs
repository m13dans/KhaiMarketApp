using System.ComponentModel.DataAnnotations;
using KhaiMarket.API.Core.Entities;
using KhaiMarket.API.DTO;

namespace KhaiMarket.API.Features.Products;

public class SortFilterPageOption
{
    const int maxPageSize = 10;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 3;
    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
    }

    private decimal _lowestPrice = 0;
    public decimal LowestPrice
    {
        get { return _lowestPrice; }
        set { _lowestPrice = (value < 0 || value > HigestPrice) ? _lowestPrice : value; }
    }

    private decimal _higestPrice = 100_000_000;
    public decimal HigestPrice
    {
        get { return _higestPrice; }
        set { _higestPrice = (value < 0) ? _higestPrice : value; }
    }

    public double Rating { get; set; }


    public OrderByOptions OrderByOptions { get; set; }
    public ProductFilterByOptions ProductFilterByOptions { get; set; }
    public string FilterValue { get; set; } = string.Empty;

}

public static class ProductHelper
{
    public static IQueryable<ProductDTO> MapProductToDto(this IQueryable<Product> products)
    {
        return products.Select(p => new ProductDTO
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Stock = p.Stock,
            Material = p.Material,
            ImageUrl = p.ImageUrl,
            Price = p.Price,
            Category = p.Category!.Name,
            ProductBrand = p.ProductBrand!.Name,
            ReviewsDTO = p.Reviews.Select(x => new ReviewDTO
            {
                VoterName = x.VoterName,
                Comment = x.Comment,
                NumStars = x.NumStars,
            }),
            TotalStars = p.Reviews.Count != 0
                ? Math.Round(p.Reviews.Select(x => x.NumStars).Average(), 2)
                : 0

        });
    }
}


public static class ProductListDtoSort
{
    public static IQueryable<ProductDTO> OrderProductBy(
        this IQueryable<ProductDTO> products,
        OrderByOptions orderByOptions) =>

        orderByOptions switch
        {
            OrderByOptions.SimpleOrder => products.OrderByDescending(x => x.Id),
            OrderByOptions.ByRating => products.OrderByDescending(x => x.TotalStars),
            OrderByOptions.ByPriceLowestFirst => products.OrderBy(x => (double?)x.Price),
            OrderByOptions.ByPriceHigestFirst => products.OrderByDescending(x => (double?)x.Price),
            _ => products.OrderByDescending(x => x.Name)
        };
}

public static class ProductListDtoFilter
{
    public static IQueryable<ProductDTO> FilterProductBy(
        this IQueryable<ProductDTO> products,
        ProductFilterByOptions options, string filterValue
    // FilterByPriceOptions priceFilter
    ) =>

    string.IsNullOrWhiteSpace(filterValue) ? products :

    options switch
    {
        ProductFilterByOptions.NoFilter => products,

        ProductFilterByOptions.ByBrand =>
            products.Where(x => x.ProductBrand.ToLower().Contains(filterValue.ToLower())),

        ProductFilterByOptions.ByCategory =>
            products.Where(x => x.Category.ToLower().Contains(filterValue.ToLower())),

        ProductFilterByOptions.ByMaterial =>
            products.Where(x => x.Material.ToLower().Contains(filterValue.ToLower())),

        // ProductFilterByOptions.ByPrice =>
        //     products.Where(x =>
        //         x.Price >= priceFilter.LowestPrice
        //         && x.Price <= priceFilter.HigestPrice),

        // ProductFilterByOptions.ByRating =>
        //     products.Where(x =>
        //         x.TotalStars >= priceFilter.Rating),

        _ => products
    };
}

public enum OrderByOptions
{
    [Display(Name = "sort by...")] SimpleOrder = 0,
    [Display(Name = "Rating ↑")] ByRating,
    [Display(Name = "Lowest Price ↓")] ByPriceLowestFirst,
    [Display(Name = "Higest Price ↑")] ByPriceHigestFirst
}

public enum ProductFilterByOptions
{
    [Display(Name = "All")] NoFilter = 0,
    [Display(Name = "By Category...")] ByCategory,
    [Display(Name = "By Material...")] ByMaterial,
    [Display(Name = "By Brand...")] ByBrand,
    [Display(Name = "By Price...")] ByPrice,
    [Display(Name = "By Rating...")] ByRating
}
public record struct FilterByPriceOptions(
    [Display(Name = "Lowest Price")] decimal LowestPrice,
    [Display(Name = "Higest Price")] decimal HigestPrice
);