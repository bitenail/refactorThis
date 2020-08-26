using System;
using System.Linq.Expressions;
using RefactorThisV2.Domain.Entities;

namespace RefactorThisV2.Service.ProductOptions.Models
{
    public class ProductOptionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public static Expression<Func<ProductOption, ProductOptionDto>> Projection
        {
            get
            {
                return po => new ProductOptionDto
                {
                    Id = po.Id,
                    Name = po.Name,
                    Description = po.Description,
                };
            }
        }

        public static ProductOptionDto Create(ProductOption productOption)
        {
            return Projection.Compile().Invoke(productOption);
        }
    }
}