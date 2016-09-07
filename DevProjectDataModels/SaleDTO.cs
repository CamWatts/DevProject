using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevProjectDataModels
{
    public class SaleDTO
    {
        public string TransactionId { get; set; }
        public ProductDTO ProductDto { get; set; }
        public double TotalPrice { get; set; }
    }
}
