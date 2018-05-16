using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class EmissionsData
    {
        [Key]
        public int EmissionsDataID { get; set; }

        public int? RegionID { get; set; }
        public virtual RD_Region Region { get; set; }

        public int? CountryID { get; set; }
        public virtual RD_Country Country { get; set; }

        [DisplayName("Other Industrial Combustion")]
        public decimal? OtherIndustrialCombustion { get; set; }
        [DisplayName("Non-combustion")]
        public decimal? NonCombustion { get; set; }
        [DisplayName("Power Industry")]
        public decimal? PowerIndustry { get; set; }
        [DisplayName("Total")]
        public decimal? Total { get; set; }

        public string Summary { get; set; }
        [DisplayName("Notes")]
        public string Note { get; set; }

        public int? YearID { get; set; }
        public virtual RD_Year Year { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}