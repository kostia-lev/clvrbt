using System;
using System.Collections.Generic;

namespace Cleverbit.CodingTask.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}