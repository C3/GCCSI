using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class StorageEstimatesData
    {
        [Key]
        public int StorageEstimatesDataID { get; set; }

        [ForeignKey("RegionID")]
        public virtual RD_Region Region { get; set; }
        public int? RegionID { get; set; }

        [ForeignKey("CountryID")]
        public virtual RD_Country Country { get; set; }
        public int? CountryID { get; set; }

        [DisplayName("National Resource Estimates")]
        public string NationalResourceEstimates { get; set; }
        [DisplayName("Confidence In Estimates")]
        public decimal? ConfidenceInEstimates { get; set; }
        [DisplayName("Prospective Basins")]
        public string ProspectiveBasins { get; set; }

        public string Summary { get; set; }

        public string Note { get; set; }

        [ForeignKey("YearID")]
        public virtual RD_Year Year { get; set; }
        public int? YearID { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}