using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_PolicyType
    {
        [Key]
        public int PolicyTypeID { get; set; }

        public string PolicyTypeName { get; set; }

        public decimal PreCommercialDemoPercentage { get; set; }

        public decimal DeploymentPercentage { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}