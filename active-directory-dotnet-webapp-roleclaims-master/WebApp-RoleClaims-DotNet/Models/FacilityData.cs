using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class FacilityData
    {
        public int FacilityDataID { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Category")]
        public int? FacilityCategoryID { get; set; }
        [ForeignKey("FacilityCategoryID")]
        public virtual RD_FacilityCategory FacilityCategory { get; set; }
        [DisplayName("Status")]
        public int? FacilityStatusID { get; set; }
        [ForeignKey("FacilityStatusID")]
        public virtual RD_FacilityStatus FacilityStatus { get; set; }
        [DisplayName("Country")]
        public int? CountryID { get; set; }
        public virtual RD_Country Country { get; set; }
        [DisplayName("Operation Date")]
        public string OperationDate { get; set; }
        [DisplayName("Industry")]
        public int? FacilityIndustryID { get; set; }
        [ForeignKey("FacilityIndustryID")]
        public virtual RD_FacilityIndustry FacilityIndustry { get; set; }
        [DisplayName("Capture Capacity (min)")]
        public decimal? CaptureCapacityMin { get; set; }
        [DisplayName("Capture Capacity (max)")]
        public decimal? CaptureCapacityMax { get; set; }
        [DisplayName("Capture Capacity")]
        public string CaptureCapacity { get; set; }
        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }
        [DisplayName("Proponents")]
        public string Proponents { get; set; }
        [DisplayName("Location")]
        public string Location { get; set; }
        [DisplayName("Industry")]
        public string Industry_Feedstock { get; set; }
        [DisplayName("Capture Type")]
        public int? FacilityCaptureTypeID { get; set; }
        [ForeignKey("FacilityCaptureTypeID")]
        public virtual RD_FacilityCaptureType FacilityCaptureType { get; set; }
        [DisplayName("Capture Source")]
        public string CaptureSource { get; set; }
        [DisplayName("Capture Method")]
        public string CaptureMethod { get; set; }
        [DisplayName("New Build or Retrofit")]
        public string NewBuildOrRetrofit { get; set; }
        [DisplayName("Storage Type")]
        public string StorageType { get; set; }
        [DisplayName("Storage Formation and Depth")]
        public string StorageFormationAndDepth { get; set; }
        [DisplayName("Utilisation Type")]
        public string UtilisationType { get; set; }
        [DisplayName("Transportation Type")]
        public string TransportationType { get; set; }
        [DisplayName("Transportation Distance")]
        public string TransportationDistance { get; set; }
        [DisplayName("Facility Description")]
        public string FacilityDescription { get; set; }
        [DisplayName("Key Milestone")]
        public string KeyMilestone { get; set; }
        [DisplayName("Currency Date")]
        [DataType(DataType.Date)]
        public DateTime? CurrencyDate { get; set; }
        [DisplayName("Reference Link")]
        public string ReferenceLink { get; set; }
        [DisplayName("Storage Code")]
        public int? FacilityStorageCodeID { get; set; }
        [ForeignKey("FacilityStorageCodeID")]
        public virtual RD_FacilityStorageCode FacilityStorageCode { get; set; }
        [DisplayName("Transport Code")]
        public int? FacilityTransportCodeID { get; set; }
        [ForeignKey("FacilityTransportCodeID")]
        public virtual RD_FacilityTransportCode FacilityTransportCode { get; set; }
        [DisplayName("Region")]
        public int? RegionID { get; set; }
        public virtual RD_Region Region { get; set; }
        [DisplayName("Exclude From Map")]
        public bool? ExcludeFromMap_HideInformation { get; set; }
        [DisplayName("Capture Latitude")]
        public decimal? CaptureLatitude { get; set; }
        [DisplayName("Capture Longitude")]
        public decimal? CaptureLongitude { get; set; }
        [DisplayName("Storage Latitude")]
        public decimal? StorageLatitude { get; set; }
        [DisplayName("Storage Longitude")]
        public decimal? StorageLongitude { get; set; }
        [DisplayName("Summary")]
        public string Summary { get; set; }
        [DisplayName("Notes")]
        public string Note { get; set; }
        [DisplayName("Year")]
        public int? YearID { get; set; }
        public virtual RD_Year Year { get; set; }
        public DateTime UpdateDateTime { get; set; }      

    }

}