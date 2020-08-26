using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorProductApi.Application.Exceptions;
using RefactorProductApi.Application.Products.Models;
using RefactorProductApi.Application.Products.Queries;
using RefactorProductApi.Domain.Entities;
using RefactorProductApi.Persistence;

namespace RefactorProductApi.Application.Products.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly RefactorProductApiDbContext _context;

        public DeleteProductCommandHandler(RefactorProductApiDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = await _context.Products.FindAsync(request.Id);

            if (productEntity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            var productOption = await _context.ProductOptions
                .Where(po => po.ProductId == request.Id).ToListAsync(cancellationToken);

            if (productOption.Count > 0)
            {
                _context.ProductOptions.RemoveRange(productEntity.ProductOptions);
            }

            _context.Products.Remove(productEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}