using AutoMapper;
using KhaiMarket.API.Core.Entities;
using KhaiMarket.API.DTO;

namespace KhaiMarket.API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.ImageUrl))
            {
                return string.Empty;
            }

            return _config["ApiUrl"] + source.ImageUrl;
        }
    }
}