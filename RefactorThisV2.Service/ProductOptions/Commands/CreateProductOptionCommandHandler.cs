using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorThisV2.Domain.Entities;
using RefactorThisV2.Persistence;
using RefactorThisV2.Service.Exceptions;
using RefactorThisV2.Service.ProductOptions.Models;
using RefactorThisV2.Service.ProductOptions.Queries;

namespace RefactorThisV2.Service.ProductOptions.Commands
{
    public class CreateProductOptionCommandHandler : IRequestHandler<CreateProductOptionCommand, ProductOptionDto>
    {
        private readonly RefactorThisV2DbContext _context;

        private readonly IMediator _mediator;

        public CreateProductOptionCommandHandler(RefactorThisV2DbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ProductOptionDto> Handle(CreateProductOptionCommand request, CancellationToken cancellationToken)
        {
            var productEntity = await _context.Products.Where(p => p.Id == request.ProductId)
                .FirstOrDefaultAsync(cancellationToken);

            if (productEntity == null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId);
            }
            
            var productOptionEntity = new ProductOption
            {
                Name = request.Name,
                Description = request.Description,
                ProductId = request.ProductId
            };

            await _context.ProductOptions.AddAsync(productOptionEntity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(new GetProductOptionQuery(request.ProductId, productOptionEntity.Id), cancellationToken);
        }
    }
}