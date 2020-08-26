using System;
using MediatR;
using RefactorProductApi.Application.Products.Models;

namespace RefactorProductApi.Application.Products.Queries
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