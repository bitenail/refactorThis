using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorThisV2.Service.Products.Models;
using RefactorThisV2.Persistence;

namespace RefactorThisV2.Service.Products.Queries
{
    public class GetAllProductsByNameQueryHandler : IRequestHandler<GetAllProductsByNameQuery, List<ProductDto>>
    {
        private readonly RefactorThisV2DbContext _context;

        public GetAllProductsByNameQueryHandler(RefactorThisV2DbContext context)
        {
            _context = context;
        }
        
        public Task<List<ProductDto>> Handle(GetAllProductsByNameQuery request, CancellationToken cancellationToken)
        {
            return _context.Products
                .Where(p => p.Name == request.Name)
                .Select(ProductDto.Projection)
                .ToListAsync(cancellationToken);
        }
    }
}