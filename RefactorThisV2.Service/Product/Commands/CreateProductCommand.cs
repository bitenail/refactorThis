using System.ComponentModel.DataAnnotations;
using MediatR;
using RefactorProductApi.Application.Products.Models;

namespace RefactorProductApi.Application.Products.Commands
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        [Range(0.01, 99999, ErrorMessage = "Product Price needs to be range from 0.01 to 99999")]
        public decimal Price { get; set; }
        
        [Range(0, 99999, ErrorMessage = "Product Price needs to be range from 0 to 99999")]
        public decimal DeliveryPrice { get; set; }
    }
}