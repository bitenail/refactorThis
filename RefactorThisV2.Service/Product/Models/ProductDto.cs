using System;
using System.Linq.Expressions;
using RefactorProductApi.Domain.Entities;

namespace RefactorProductApi.Application.Products.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public static Expression<Func<Product, ProductDto>> Projection
        {
            get
            {
                return p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    DeliveryPrice = p.DeliveryPrice
                };
            }
        }

        public static ProductDto Create(Product product)
        {
            return Projection.Compile().Invoke(product);
        }
    }
}