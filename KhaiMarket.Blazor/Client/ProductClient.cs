using KhaiMarket.Blazor.Model;

namespace KhaiMarket.Blazor.Client;

public class ProductClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<ProductDTO>> GetProductsAsync() =>
        await _httpClient.GetFromJsonAsync<List<ProductDTO>>("") ?? [];

    public async Task<List<ProductDTO>> GetProductsWithSearch(string search) =>
        await _httpClient.GetFromJsonAsync<List<ProductDTO>>("Search=" + search) ?? [];
}