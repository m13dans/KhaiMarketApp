using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KhaiMarket.API.DTO;

namespace KhaiMarket.API.Features.Products;

public class ProductQueryObject
{

}
public enum OrderByOptions
{
    SimpleOrder = 0,
    ByRating,
    ByPriceLowestFirst,
    ByPriceHigestFirst
}

public enum ProductFilterByOptions
{
    NoFilter = 0,
    ByCategory,
    ByMaterial,
    ByBrand,
    ByPrice,
    ByRating
}
public record struct NumberFilter(
    decimal LowestPrice,
    decimal HigestPrice,
    double Rating
);

public static class ProductListDtoSort
{
    public static IQueryable<ProductDTO> OrderProductBy(
        this IQueryable<ProductDTO> products,
        OrderByOptions orderByOptions) =>

        orderByOptions switch
        {
            OrderByOptions.SimpleOrder => products.OrderByDescending(x => x.Id),
            OrderByOptions.ByRating => products.OrderByDescending(x => x.TotalStars),
            OrderByOptions.ByPriceLowestFirst => products.OrderBy(x => x.Price),
            OrderByOptions.ByPriceHigestFirst => products.OrderByDescending(x => x.Price),
            _ => products.OrderByDescending(x => x.Name)
        };
}

public static class ProductListDtoFilter
{
    public static IQueryable<ProductDTO> FilterProductBy(
        this IQueryable<ProductDTO> products,
        ProductFilterByOptions options, string filterValue,
        NumberFilter numberFilter
    ) =>

    string.IsNullOrEmpty(filterValue) ? products :

    options switch
    {
        ProductFilterByOptions.NoFilter => products,
        ProductFilterByOptions.ByBrand =>
            products.Where(x =>
                x.ProductBrand.Equals(
                filterValue, StringComparison.OrdinalIgnoreCase)),
        ProductFilterByOptions.ByCategory =>
            products.Where(x =>
                x.Category.Equals(
                filterValue, StringComparison.OrdinalIgnoreCase)),
        ProductFilterByOptions.ByMaterial =>
            products.Where(x =>
                x.Material.Equals(
                filterValue, StringComparison.OrdinalIgnoreCase)),
        ProductFilterByOptions.ByPrice =>
            products.Where(x =>
                x.Price >= numberFilter.LowestPrice
                && x.Price <= numberFilter.HigestPrice),
        ProductFilterByOptions.ByRating =>
            products.Where(x =>
                x.TotalStars >= numberFilter.Rating),
        _ => products
    };
}


