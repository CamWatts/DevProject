using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevProjectDataModels;

namespace DevProject.Models
{
    public class TransactionViewModel : TransactionStatusModel
    {
        public TransactionDTO TransactionDto { get; set; }
    }
}