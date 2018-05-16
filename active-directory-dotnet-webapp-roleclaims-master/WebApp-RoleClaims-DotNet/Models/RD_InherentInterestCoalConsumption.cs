using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_InherentInterestCoalConsumption
    {
        [Key]
        public int InherentInterestCoalConsumptionID { get; set; }

        public decimal CoalConsumptionPercentageShare { get; set; }

        public int CoalConsumptionScore { get; set; }

        public string CoalConsumptionBand { get; set; }

        public string CoalConsumptionTier { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}