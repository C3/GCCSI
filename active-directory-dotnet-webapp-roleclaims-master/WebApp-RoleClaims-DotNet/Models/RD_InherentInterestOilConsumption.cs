using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_InherentInterestOilConsumption
    {
        [Key]
        public int InherentInterestOilConsumptionID { get; set; }

        public decimal OilConsumptionPercentageShare { get; set; }

        public int OilConsumptionScore { get; set; }

        public string OilConsumptionBand { get; set; }

        public string OilConsumptionTier { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}