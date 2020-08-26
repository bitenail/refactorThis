using System.Collections.Generic;
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
    public class GetProductOptionsQueryHandler : IRequestHandler<GetProductOptionsQuery, List<ProductOptionDto>>
    {
        private readonly RefactorThisV2DbContext _context;

        public GetProductOptionsQueryHandler(RefactorThisV2DbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductOptionDto>> Handle(GetProductOptionsQuery request,
            CancellationToken cancellationToken)
        {
            var productEntity = await _context.Products.Where(p => p.Id == request.ProductId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (productEntity == null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId);
            }

            return await _context.ProductOptions
                .Where(po => po.ProductId == request.ProductId)
                .Select(ProductOptionDto.Projection)
                .ToListAsync(cancellationToken);
        }
    }
}