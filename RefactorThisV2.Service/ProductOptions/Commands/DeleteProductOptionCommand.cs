using System;
using MediatR;

namespace RefactorThisV2.Service.ProductOptions.Commands
{
    public class DeleteProductOptionCommand : IRequest
    {
        public DeleteProductOptionCommand(Guid id, Guid productId)
        {
            Id = id;
            ProductId = productId;
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
    }
}