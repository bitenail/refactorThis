using System;
using System.Collections.Generic;
using MediatR;
using RefactorProductApi.Application.Products.Models;

namespace RefactorProductApi.Application.Products.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}