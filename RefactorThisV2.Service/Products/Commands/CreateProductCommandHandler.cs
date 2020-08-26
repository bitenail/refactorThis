using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RefactorThisV2.Service.Products.Models;
using RefactorThisV2.Service.Products.Queries;
using RefactorThisV2.Persistence;
using RefactorThisV2.Domain.Entities;

namespace RefactorThisV2.Service.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly RefactorThisV2DbContext _context;

        private readonly IMediator _mediator;

        public CreateProductCommandHandler(RefactorThisV2DbContext context, IMediator mediator)
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