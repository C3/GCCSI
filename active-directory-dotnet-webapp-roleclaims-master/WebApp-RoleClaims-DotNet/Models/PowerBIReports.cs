using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class PowerBIReports
    {
        [Key]
        public int PowerBIReportID { get; set; }

        public string ReportName { get; set; }

        public string Url { get; set; }
    }
}