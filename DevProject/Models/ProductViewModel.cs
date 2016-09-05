using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevProjectDataModels;

namespace DevProject.Models
{
    public class ProductViewModel : TransactionStatusModel
    {
        public ProductDTO ProductDto { get; set; } // for creating / updating product
        public List<ProductDTO> ProductDtoList { get; set; } // for retrieving all products
    }
}