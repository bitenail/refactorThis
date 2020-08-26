using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RefactorThisV2.Domain.Entities;
using RefactorThisV2.Persistence;
using RefactorThisV2.Service.Exceptions;
using RefactorThisV2.Service.Products.Models;

namespace RefactorThisV2.Service.Products.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly RefactorThisV2DbContext _context;

        private readonly IMediator _mediator;

        public UpdateProductCommandHandler(RefactorThisV2DbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = await _context.Products.FindAsync(request.Id);

            if (productEntity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            productEntity.Description = request.Description;
            productEntity.Name = request.Name;
            productEntity.Price = request.Price;
            productEntity.DeliveryPrice = request.Price;

            await _context.SaveChangesAsync(cancellationToken);

            return ProductDto.Create(productEntity);
        }
    }
}