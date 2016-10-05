using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevProjectDataModels;

namespace DevProject.Models
{
    public class ReportViewModel : TransactionStatusModel
    {
        public List<ReportDTO> ReportDtoList { get; set; } // for retrieving all reports
    }
}