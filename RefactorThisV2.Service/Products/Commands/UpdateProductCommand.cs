using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using RefactorThisV2.Service.Products.Models;

namespace RefactorThisV2.Service.Products.Commands
{
    public class UpdateProductCommand : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        [Range(0.01, 99999, ErrorMessage = "Product Price needs to be range from 0.01 to 99999")]
        public decimal Price { get; set; }
        
        [Range(0, 99999, ErrorMessage = "Product Price needs to be range from 0 to 99999")]
        public decimal DeliveryPrice { get; set; }
    }
}