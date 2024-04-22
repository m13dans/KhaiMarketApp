using KhaiMarket.API.Products.Core.Entities;
using KhaiMarket.API.Products.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KhaiMarket.API.Products;

public static class CreateProduct
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public Task<ActionResult> CreateProduct()
        {
            throw new NotImplementedException();
        }
    }

    public record CreateProductCommand : IRequest<Product>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Category Category { get; set; } = new Category();
        public int CategoryId { get; set; }
        public ProductBrand ProductBrand { get; set; } = new ProductBrand();
        public int ProductBrandId { get; set; }

    }

    public class Handler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly AppDbContext _context;

        public Handler(AppDbContext context)
        {
            _context = context;
        }

        public Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
