using System;
using MediatR;

namespace RefactorThisV2.Service.Products.Commands
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