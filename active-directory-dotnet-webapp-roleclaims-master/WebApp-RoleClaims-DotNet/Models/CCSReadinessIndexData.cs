using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class CCSReadinessIndexData
    {
        [Key]
        public int ReadinessIndexDataID { get; set; }

        [ForeignKey("RegionID")]
        public virtual RD_Region Region { get; set; }
        public int? RegionID { get; set; }

        [ForeignKey("CountryID")]
        public virtual RD_Country Country { get; set; }
        public int? CountryID { get; set; }

        [DisplayName("Policy Score")]
        public decimal? PolicyScore { get; set; }
        [DisplayName("Policy Score / 100")]
        public decimal? PolicyScore_100 { get; set; }
        [DisplayName("Storage Score")]
        public decimal? StorageScore { get; set; }
        [DisplayName("Legal & Regulatory Score")]
        public decimal? LegalRegulatoryScore { get; set; }
        [DisplayName("Legal & Regulatory Score / 100")]
        public decimal? LegalRegulatoryScore_100 { get; set; }
        [DisplayName("CCS Readiness Index Score")]
        public decimal? CCSReadinessIndexScore { get; set; }
        [DisplayName("Summary")]
        public string Summary { get; set; }
        [DisplayName("Notes")]
        public string Note { get; set; }
        [ForeignKey("YearID")]
        public virtual RD_Year Year { get; set; }
        public int? YearID { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}