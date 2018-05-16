using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class InherentInterestData
    {
        [Key]
        public int InherentInterestID { get; set; }

        public int? RegionID { get; set; }
        public virtual RD_Region Region { get; set; }

        public int? CountryID { get; set; }
        public virtual RD_Country Country { get; set; }

        [DisplayName("Oil Production")]
        public decimal OilProduction { get; set; }

        [DisplayName("Gas Production")]
        public decimal GasProduction { get; set; }

        [DisplayName("Coal Production")]
        public decimal CoalProduction { get; set; }

        [DisplayName("Oil Consumption")]
        public decimal OilConsumption { get; set; }

        [DisplayName("Gas Consumption")]
        public decimal GasConsumption { get; set; }

        [DisplayName("Coal Consumption")]
        public decimal CoalConsumption { get; set; }

        [DisplayName("Summary")]
        public string Summary { get; set; }

        [DisplayName("Notes")]
        public string Note { get; set; }

        public int? YearID { get; set; }
        public virtual RD_Year Year { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}