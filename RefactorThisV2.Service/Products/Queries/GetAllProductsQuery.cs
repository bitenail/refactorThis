using System;
using System.Collections.Generic;
using MediatR;
using RefactorThisV2.Service.Products.Models;

namespace RefactorThisV2.Service.Products.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}