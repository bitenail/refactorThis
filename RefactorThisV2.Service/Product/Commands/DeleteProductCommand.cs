using System;
using MediatR;

namespace RefactorProductApi.Application.Products.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; set; }
    }
}