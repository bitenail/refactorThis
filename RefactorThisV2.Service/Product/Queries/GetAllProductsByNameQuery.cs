using System.Collections.Generic;
using MediatR;
using RefactorProductApi.Application.Products.Models;

namespace RefactorProductApi.Application.Products.Queries
{
    public class GetAllProductsByNameQuery : IRequest<List<ProductDto>>
    {
        public GetAllProductsByNameQuery(string name)
        {
            Name = name;
        }
        
        public string Name { get; set; }
    }
}