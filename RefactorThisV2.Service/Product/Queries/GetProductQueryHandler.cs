using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorProductApi.Application.Exceptions;
using RefactorProductApi.Application.Products.Models;
using RefactorProductApi.Domain.Entities;
using RefactorProductApi.Persistence;

namespace RefactorProductApi.Application.Products.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {

        private readonly RefactorProductApiDbContext _context;
        
        public GetProductQueryHandler(RefactorProductApiDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
             var product = await _context.Products
                .Where(p => p.Id == request.Id)
                .Select(ProductDto.Projection)
                .SingleOrDefaultAsync((cancellationToken));

             if (product == null)
             {
                 throw new NotFoundException(nameof(Product), request.Id);
             }

             return product;
        }
    }
}