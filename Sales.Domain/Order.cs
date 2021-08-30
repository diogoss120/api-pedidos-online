using System;
using System.Collections.Generic;

namespace Sales.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
