using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class ClimateChangeData
    {
        public int ClimateChangeDataID { get; set; }

        public int? RegionID { get; set; }
        public virtual RD_Region Region { get; set; }

        public int? CountryID { get; set; }
        public virtual RD_Country Country { get; set; }

        [DisplayName("Key Mechanisms in NDC")]
        public string KeyMechanismsInNDC { get; set; }
        [DisplayName("% Reduction")]
        public string PercentageReduction { get; set; }
        [DisplayName("Base Year")]
        public string BaseYear { get; set; }
        [DisplayName("Target Year")]
        public string TargetYear { get; set; }

        public string Source { get; set; }

        public string Summary { get; set; }
        [DisplayName("Notes")]
        public string Note { get; set; }

        public int? YearID { get; set; }
        public virtual RD_Year Year { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}