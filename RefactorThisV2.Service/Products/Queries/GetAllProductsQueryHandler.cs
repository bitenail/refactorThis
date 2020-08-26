using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorThisV2.Persistence;
using RefactorThisV2.Service.Products.Models;

namespace RefactorThisV2.Service.Products.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly RefactorThisV2DbContext _context;

        public GetAllProductsQueryHandler(RefactorThisV2DbContext context)
        {
            _context = context;
        }
        
        public Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return _context.Products
                    .Select(ProductDto.Projection)
                    .ToListAsync(cancellationToken);
        }
    }
}