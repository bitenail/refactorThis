using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorProductApi.Application.Products.Models;
using RefactorProductApi.Persistence;

namespace RefactorProductApi.Application.Products.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly RefactorProductApiDbContext _context;

        public GetAllProductsQueryHandler(RefactorProductApiDbContext context)
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