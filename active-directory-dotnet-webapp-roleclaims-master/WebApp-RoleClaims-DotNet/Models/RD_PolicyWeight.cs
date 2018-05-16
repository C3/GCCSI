using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_PolicyWeight
    {
        [Key]
        public int PolicyWeightID { get; set; }

        public string PolicyWeightName { get; set; }

        public decimal PolicyWeightPercentage { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}