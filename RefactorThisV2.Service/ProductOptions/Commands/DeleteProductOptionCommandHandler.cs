using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RefactorThisV2.Domain.Entities;
using RefactorThisV2.Persistence;
using RefactorThisV2.Service.Exceptions;

namespace RefactorThisV2.Service.ProductOptions.Commands
{
    public class DeleteProductOptionCommandHandler : IRequestHandler<DeleteProductOptionCommand>
    {
        private readonly RefactorThisV2DbContext _context;

        public DeleteProductOptionCommandHandler(RefactorThisV2DbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProductOptionCommand request, CancellationToken cancellationToken)
        {
            var productEntity = await _context.Products.FindAsync(request.ProductId);

            if (productEntity == null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId);
            }

            var productOptionEntity = await _context.ProductOptions.FindAsync(request.Id);

            if (productOptionEntity == null)
            {
                throw new NotFoundException(nameof(ProductOption), request.Id);
            }

            _context.ProductOptions.Remove(productOptionEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}