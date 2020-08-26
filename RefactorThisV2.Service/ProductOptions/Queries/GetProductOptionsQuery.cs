using System;
using System.Collections.Generic;
using MediatR;
using RefactorThisV2.Service.ProductOptions.Models;

namespace RefactorThisV2.Service.ProductOptions.Queries
{
    public class GetProductOptionsQuery : IRequest<List<ProductOptionDto>>
    {
        public GetProductOptionsQuery(Guid productId)
        {
            ProductId = productId;
        }
        
        public Guid ProductId { get; set; }
    }
}