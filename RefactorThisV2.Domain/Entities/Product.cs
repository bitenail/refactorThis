using System;
using System.Collections.Generic;

namespace RefactorThisV2.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public ICollection<ProductOption> ProductOptions { get; set; }
    }
}
