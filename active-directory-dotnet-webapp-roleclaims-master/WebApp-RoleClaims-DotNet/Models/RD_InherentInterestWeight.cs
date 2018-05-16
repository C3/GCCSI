using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_InherentInterestWeight
    {
        [Key]
        public int InherentInterestWeightID { get; set; }

        public string InherentInterestWeightName { get; set; }

        public decimal ProductionWeightScore { get; set; }

        public decimal ConsumptionWeightScore { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}