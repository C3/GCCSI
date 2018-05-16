using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_InherentInterestOilProduction
    {
        [Key]
        public int InherentInterestOilProductionID { get; set; }

        public decimal OilProductionPercentageShare { get; set; }

        public int OilProductionScore { get; set; }

        public string OilProductionBand { get; set; }

        public string OilProductionTier { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}