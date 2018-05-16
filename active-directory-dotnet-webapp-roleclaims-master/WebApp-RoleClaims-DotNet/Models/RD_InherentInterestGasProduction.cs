using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_InherentInterestGasProduction
    {
        [Key]
        public int InherentInterestGasProductionID { get; set; }

        public decimal GasProductionPercentageShare { get; set; }

        public int GasProductionScore { get; set; }

        public string GasProductionBand { get; set; }

        public string GasProductionTier { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}