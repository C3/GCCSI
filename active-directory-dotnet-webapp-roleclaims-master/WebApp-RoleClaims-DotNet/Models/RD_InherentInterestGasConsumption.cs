using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_InherentInterestGasConsumption
    {
        [Key]
        public int InherentInterestGasConsumptionID { get; set; }

        public decimal GasConsumptionPercentageShare { get; set; }

        public int GasConsumptionScore { get; set; }

        public string GasConsumptionBand { get; set; }

        public string GasConsumptionTier { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}