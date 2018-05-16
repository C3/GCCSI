using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class LegalRegulatoryData
    {
        [DisplayName("Legal Regulatory ID")]
        public int LegalRegulatoryDataID { get; set; }

        [DisplayName("Region")]
        public int? RegionID { get; set; }

        [DisplayName("Country")]
        public int? CountryID { get; set; }

        [DisplayName("Clarity and Efficiency of Administractive Process (Total Score: 12)")]
        public decimal ClarityAndEfficiencyOfAdministrativeProcess { get; set; }

        [DisplayName("Comprehensiveness of Frameworks (Total: 36)")]
        public decimal ComprehensivenessOfFrameworks { get; set; }

        [DisplayName("Siting and EIA (Total: 18)")]
        public decimal SitingAndEIA { get; set; }

        [DisplayName("Stakeholder/Public Consultation (Total: 9)")]
        public decimal StakeholderPublicConsultation { get; set; }

        [DisplayName("Liability and Closure (Total: 12)")]
        public decimal LiabilityAndClosure { get; set; }

        [DisplayName("Total out of 87")]
        public decimal? TotalScore { get; set; }

        [DisplayName("Band (A, B, C)")]
        public int? LRBandID { get; set; }

        [DisplayName("List of Laws?")]
        public string ListOfLaws { get; set; }

        [DisplayName("Attachments")]
        public int? Attachment { get; set; }

        [DisplayName("Summary")]
        public string Summary { get; set; }

        [DisplayName("Notes")]
        public string Note { get; set; }

        public int? YearID { get; set; }

        public DateTime UpdateDateTime { get; set; }


        //Virtual Classes -- Relational Data.
        public virtual RD_Year Year { get; set; }
        public virtual RD_Region Region { get; set; }
        public virtual RD_Country Country { get; set; }

        [ForeignKey("LRBandID")]
        public virtual RD_LRBand LRBand { get; set; }

    }

}