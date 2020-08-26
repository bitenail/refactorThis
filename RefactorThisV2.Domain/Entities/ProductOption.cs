using System;

namespace RefactorThisV2.Domain.Entities
{
    public class ProductOption
    {
        public ProductOption()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}