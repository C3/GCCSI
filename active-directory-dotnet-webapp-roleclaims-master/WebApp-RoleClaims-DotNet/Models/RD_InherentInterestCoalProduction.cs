using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_InherentInterestCoalProduction
    {
        [Key]
        public int InherentInterestCoalProductionID { get; set; }

        public decimal CoalProductionPercentageShare { get; set; }

        public int CoalProductionScore { get; set; }

        public string CoalProductionBand { get; set; }

        public string CoalProductionTier { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}