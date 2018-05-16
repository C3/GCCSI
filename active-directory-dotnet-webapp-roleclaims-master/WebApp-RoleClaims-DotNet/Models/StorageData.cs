using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class StorageData
    {
        [DisplayName("Storage Data ID")]
        public int StorageDataID { get; set; }

        [DisplayName("Region ID")]
        public int? RegionID { get; set; }

        [DisplayName("Country ID")]
        public int? CountryID { get; set; }

        [DisplayName("Conventional Storage Potential")]
        public bool? ConventionalStoragePotential { get; set; }

        [DisplayName("Regional Potential")]
        public int? RegionalPotential { get; set; }

        [DisplayName("Regional Assessment")]
        public int? RegionalAssessment { get; set; }

        [DisplayName("Dataset")]
        public int? Dataset { get; set; }

        [DisplayName("Assessment Maturity")]
        public int? AssessmentMaturity { get; set; }

        [DisplayName("Injection")]
        public int? Injection { get; set; }

        [DisplayName("Commercial Scale Injection")]
        public int? CommercialScaleInjection { get; set; }

        [DisplayName("Knowledge Dissemination")]
        public int? KnowledgeDissemination { get; set; }

        [DisplayName("Summary")]
        public string Summary { get; set; }

        [DisplayName("Note")]
        public string Note { get; set; }

        [DisplayName("Year")]
        public int? YearID { get; set; }

        [DisplayName("Update DateTime")]
        public DateTime UpdateDateTime { get; set; }


        //Virtual Classes -- Relational Data.
        public virtual RD_Year Year { get; set; }
        public virtual RD_Region Region { get; set; }
        public virtual RD_Country Country { get; set; }

        [ForeignKey("RegionalPotential")]
        public virtual RD_StorageCriteria RegionalPotentialCriteria { get; set; }

        [ForeignKey("RegionalAssessment")]
        public virtual RD_StorageCriteria RegionalAssessmentCriteria { get; set; }

        [ForeignKey("Dataset")]
        public virtual RD_StorageCriteria DatasetCriteria { get; set; }

        [ForeignKey("AssessmentMaturity")]
        public virtual RD_StorageCriteria AssessmentMaturityCriteria { get; set; }

        [ForeignKey("Injection")]
        public virtual RD_StorageCriteria InjectionCriteria { get; set; }

        [ForeignKey("CommercialScaleInjection")]
        public virtual RD_StorageCriteria CommercialScaleInjectionCriteria { get; set; }

        [ForeignKey("KnowledgeDissemination")]
        public virtual RD_StorageCriteria KnowledgeDisseminationCriteria { get; set; }
    }

}