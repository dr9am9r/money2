using System;

namespace Money2.Domain.Products
{
    public class Product : IEntity
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public Int32 TypeId { get; set; }

        public Int32 UserId { get; set; }
    }
}
