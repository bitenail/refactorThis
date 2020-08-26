using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefactorThisV2.Domain.Entities;
using RefactorThisV2.Persistence;
using RefactorThisV2.Service.Exceptions;
using RefactorThisV2.Service.Products.Models;

namespace RefactorThisV2.Service.Products.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {

        private readonly RefactorThisV2DbContext _context;
        
        public GetProductQueryHandler(RefactorThisV2DbContext context)
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