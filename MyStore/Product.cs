using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore
{
    public class Product
    {
        public string Name { get; }
        public decimal Price { get; }
        public int Antal { get; }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
        public Product(string name, decimal price, int antal)
        {
            Name = name;
            Price = price;
            Antal = antal;

        }
    }
}
