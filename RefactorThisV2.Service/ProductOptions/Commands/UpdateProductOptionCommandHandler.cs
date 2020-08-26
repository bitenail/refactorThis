using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorThisV2.Domain.Entities;
using RefactorThisV2.Persistence;
using RefactorThisV2.Service.Exceptions;
using RefactorThisV2.Service.ProductOptions.Models;

namespace RefactorThisV2.Service.ProductOptions.Commands
{
    public class UpdateProductOptionCommandHandler : IRequestHandler<UpdateProductOptionCommand, ProductOptionDto>
    {
        private readonly RefactorThisV2DbContext _context;

        private readonly IMediator _mediator;

        public UpdateProductOptionCommandHandler(RefactorThisV2DbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ProductOptionDto> Handle(UpdateProductOptionCommand request, CancellationToken cancellationToken)
        {
            var productEntity = await _context.Products.Where(p => p.Id == request.ProductId)
                .FirstOrDefaultAsync(cancellationToken);

            if (productEntity == null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId);
            }
            
            var productOptionEntity = await _context.ProductOptions.Where(po => po.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (productOptionEntity == null)
            {
                throw new NotFoundException(nameof(ProductOption), request.Id);
            }

            productOptionEntity.Description = request.Description;
            productOptionEntity.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return ProductOptionDto.Create(productOptionEntity);
        }
    }
}