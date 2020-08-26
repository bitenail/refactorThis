using System.Collections.Generic;
using MediatR;
using RefactorThisV2.Service.Products.Models;

namespace RefactorThisV2.Service.Products.Queries
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