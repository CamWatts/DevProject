using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevProjectDataModels
{
    public class TransactionDTO
    {
        public string TransactionId { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
        public double TotalPrice { get; set; }
        public List<SaleDTO> SaleDtoList { get; set; }
    }
}
