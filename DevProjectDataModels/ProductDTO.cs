using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevProjectDataModels
{
    public class ProductDTO
    {
        public string ProductId { get; set; } //product id code
        public string Name { get; set; } // product name
        public string Description { get; set; } // product description
        public int StockAmount { get; set; } // amount of stock held for product
        public int CheckAmount { get; set; } // check stock against this value to determine if low stock warning necessary
        public double SellPrice { get; set; } // price the product is being sold at
     }
}
