using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RefactorProductApi.Application.Products.Models;
using RefactorProductApi.Application.Products.Queries;
using RefactorProductApi.Domain.Entities;
using RefactorProductApi.Persistence;

namespace RefactorProductApi.Application.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly RefactorProductApiDbContext _context;

        private readonly IMediator _mediator;

        public CreateProductCommandHandler(RefactorProductApiDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = new Product
            {
                Name = request.Name,
                Price = request.Price,
                DeliveryPrice = request.DeliveryPrice,
                Description = request.Description
            };

            await _context.Products.AddAsync(productEntity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(new GetProductQuery(productEntity.Id), cancellationToken);
        }
    }
}