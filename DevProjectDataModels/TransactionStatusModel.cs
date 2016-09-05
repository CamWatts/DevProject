using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevProjectDataModels
{
    public class TransactionStatusModel
    {
        public TransactionStatusModel()
        {
            ReturnMessage = new List<String>();
            ReturnStatus = true;
            ValidationErrors = new Hashtable();
        }

        public bool ReturnStatus { get; set; }
        public List<String> ReturnMessage { get; set; }
        public Hashtable ValidationErrors;
        public int ReturnCount { get; set; }
        public int ReturnValue { get; set; }
    }
}
