using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RefactorProductApi.Application.Exceptions;
using RefactorProductApi.Application.Products.Models;
using RefactorProductApi.Application.Products.Queries;
using RefactorProductApi.Domain.Entities;
using RefactorProductApi.Persistence;

namespace RefactorProductApi.Application.Products.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly RefactorProductApiDbContext _context;

        private readonly IMediator _mediator;

        public UpdateProductCommandHandler(RefactorProductApiDbContext context, IMediator mediator)
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