using System;
using MediatR;
using RefactorThisV2.Service.ProductOptions.Models;

namespace RefactorThisV2.Service.ProductOptions.Queries
{
    public class GetProductOptionQuery : IRequest<ProductOptionDto>
    {
        public GetProductOptionQuery(Guid productId, Guid productOptionId)
        {
            ProductId = productId;
            ProductOptionId = productOptionId;
        }
        
        public Guid ProductId { get; set; }
        public Guid ProductOptionId { get; set; }
    }
}