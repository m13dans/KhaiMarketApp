using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using KhaiMarket.Blazor.Model;

namespace KhaiMarket.Blazor.Client
{
    public class ProductClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<ProductDTO>> GetProductsAsync() =>
            await _httpClient.GetFromJsonAsync<List<ProductDTO>>("products") ?? [];
    }
}