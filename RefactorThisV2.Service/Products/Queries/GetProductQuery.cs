using System;
using MediatR;
using RefactorThisV2.Service.Products.Models;

namespace RefactorThisV2.Service.Products.Queries
{
    public class GetProductQuery : IRequest<ProductDto>
    {
        public GetProductQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}