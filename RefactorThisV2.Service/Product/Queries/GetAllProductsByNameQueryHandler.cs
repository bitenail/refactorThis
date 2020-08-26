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
    public class GetAllProductsByNameQueryHandler : IRequestHandler<GetAllProductsByNameQuery, List<ProductDto>>
    {
        private readonly RefactorProductApiDbContext _context;

        public GetAllProductsByNameQueryHandler(RefactorProductApiDbContext context)
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