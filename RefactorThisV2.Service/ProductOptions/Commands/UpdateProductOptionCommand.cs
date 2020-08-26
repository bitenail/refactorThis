using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using RefactorThisV2.Service.ProductOptions.Models;

namespace RefactorThisV2.Service.ProductOptions.Commands
{
    public class UpdateProductOptionCommand : IRequest<ProductOptionDto>
    {
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public Guid Id { get; set; }
        
        public Guid ProductId { get; set; }
    }
}