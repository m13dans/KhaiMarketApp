using AutoMapper;
using KhaiMarket.API.Core.Entities;
using KhaiMarket.API.DTO;

namespace KhaiMarket.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(x => x.ImageUrl, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}