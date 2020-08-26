using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorThisV2.Domain.Entities;
using RefactorThisV2.Persistence;
using RefactorThisV2.Service.Exceptions;
using RefactorThisV2.Service.ProductOptions.Models;

namespace RefactorThisV2.Service.ProductOptions.Queries
{
    public class GetProductOptionQueryHandler : IRequestHandler<GetProductOptionQuery, ProductOptionDto>
    {
        private readonly RefactorThisV2DbContext _context;

        public GetProductOptionQueryHandler(RefactorThisV2DbContext context)
        {
            _context = context;
        }

        public async Task<ProductOptionDto> Handle(GetProductOptionQuery request,
            CancellationToken cancellationToken)
        {
            var productEntity = await _context.Products.Where(p => p.Id == request.ProductId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (productEntity == null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId);
            }

            var productOption = await _context.ProductOptions
                .Where(po => po.ProductId == request.ProductId && po.Id == request.ProductOptionId )
                .Select(ProductOptionDto.Projection)
                .SingleOrDefaultAsync(cancellationToken);

            if (productOption == null)
            {
                throw new NotFoundException(nameof(ProductOption), request.ProductOptionId);
            }

            return productOption;
        }
    }
}