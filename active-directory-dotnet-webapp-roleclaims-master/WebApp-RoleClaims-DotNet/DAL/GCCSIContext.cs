using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GCCSI_CO2RE.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web.Mvc;

namespace GCCSI_CO2RE.DAL
{
    public class GCCSIContext : DbContext
    {

        public GCCSIContext() : base ("GCCSIContext")
        {
            this.Database.CommandTimeout = 200;
        }

        //Generic Storage Sets
        public DbSet<RD_Year> RD_YearDataSet { get; set; }
        public DbSet<RD_Region> RD_RegionSet { get; set; }
        public DbSet<RD_Country> RD_CountrySet { get; set; }

        //Storage Data
        public DbSet<StorageData> StorageDataSet { get; set; }
        public DbSet<RD_StorageCriteria> RD_StorageCriteriaSet { get; set; }
        public DbSet<RD_StorageWeight> RD_StorageWeightSet { get; set; }

        //Policy Data
        public DbSet<PolicyListData> PolicyListDataSet { get; set; }
        public DbSet<RD_PolicyOption> RD_PolicyOptionSet { get; set; }
        public DbSet<RD_PolicyListOption> RD_PolicyListOptionSet { get; set; }
        public DbSet<RD_PolicyStatus> RD_PolicyStatusSet { get; set; }
        public DbSet<RD_PolicyListStatus> RD_PolicyListStatusSet { get; set; }
        public DbSet<RD_PolicyType> RD_PolicyTypeSet { get; set; }
        public DbSet<RD_PolicyWeight> RD_PolicyWeightSet { get; set; }
        public DbSet<ConsolidatedPolicyIndex> ConsolidatedPolicyIndex { get; set; }

        //Legal and Regulatory
        public DbSet<RD_LRBand> LRBandSet { get; set; }
        public DbSet<LegalRegulatoryData> LegalRegDataSet { get; set; }

        //Facility Data
        public DbSet<RD_FacilityIndustry> RD_FacilityIndustrySet { get; set; }
        public DbSet<RD_FacilityCategory> RD_FacilityCategorySet { get; set; }
        public DbSet<RD_FacilityStatus> RD_FacilityStatusSet { get; set; }
        public DbSet<RD_FacilityStorageCode> RD_FacilityStorageCodeSet { get; set; }
        public DbSet<RD_FacilityTransportCode> RD_FacilityTransportCodeSet { get; set; }
        public DbSet<RD_FacilityCaptureType> RD_FacilityCaptureTypeSet { get; set; }
        public DbSet<RD_FacilityCategoryStatus> RD_FacilityCategoryStatusSet { get; set; }
        public DbSet<FacilityData> FacilityData { get; set; }

        //Climate Change Initiative
        public DbSet<ClimateChangeData> ClimateChangeDataSet { get; set; }

        //Emissions
        public DbSet<EmissionsData> EmissionsDataSet { get; set; }

        //Inherent Interest
        public DbSet<InherentInterestData> InherentInterestDataSet { get; set; }


        //CCS Readiness Index
        public System.Data.Entity.DbSet<GCCSI_CO2RE.Models.CCSReadinessIndexData> CCSReadinessIndexDatas { get; set; }

        //Areas of CO2RE and Update System
        public System.Data.Entity.DbSet<GCCSI_CO2RE.Models.CO2REAreas> CO2REAreasSet { get; set; }
        public System.Data.Entity.DbSet<GCCSI_CO2RE.Models.COREUpdates> CO2REUpdateDataSet { get; set; }

        //Storage Data Estimates
        public System.Data.Entity.DbSet<GCCSI_CO2RE.Models.StorageEstimatesData> StorageEstimatesDatas { get; set; }

        //Inherent Interest Data Relational
        public System.Data.Entity.DbSet<GCCSI_CO2RE.Models.RD_InherentInterestCoalConsumption> InherentCoalConsumptionSet { get; set; }
        public System.Data.Entity.DbSet<GCCSI_CO2RE.Models.RD_InherentInterestCoalProduction> InherentCoalProductionSet { get; set; }
        public System.Data.Entity.DbSet<GCCSI_CO2RE.Models.RD_InherentInterestGasConsumption> InherentGasConsumptionSet { get; set; }
        public System.Data.Entity.DbSet<GCCSI_CO2RE.Models.RD_InherentInterestGasProduction> InherentGasProductionSet { get; set; }
        public System.Data.Entity.DbSet<GCCSI_CO2RE.Models.RD_InherentInterestOilConsumption> InherentOilConsumptionSet { get; set; }
        public System.Data.Entity.DbSet<GCCSI_CO2RE.Models.RD_InherentInterestOilProduction> InherentOilProductionSet { get; set; }
        public System.Data.Entity.DbSet<GCCSI_CO2RE.Models.RD_InherentInterestWeight> InherentWeightSet { get; set; }


        // Power BI Reports
        public System.Data.Entity.DbSet<GCCSI_CO2RE.Models.PowerBIReports> PowerBIReportSet { get; set; }

        //Extra data added after.
        public System.Data.Entity.DbSet<GCCSI_CO2RE.Models.RD_LeadershipStatus> LeadershipStatusSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


        public IEnumerable<SelectListItem> GetRegions()
        {
            var roles = this.RD_RegionSet
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.RegionID.ToString(),
                                    Text = x.RegionName
                                });

            return new SelectList(roles, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetCountries()
        {
            var roles = this.RD_CountrySet
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.CountryID.ToString(),
                                    Text = x.CountryName
                                });

            return new SelectList(roles, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetStorageCriteria()
        {
            var roles = this.RD_StorageCriteriaSet
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.StorageCriteriaID.ToString(),
                                    Text = x.StorageCriteriaCode
                                });

            return new SelectList(roles, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetYearList()
        {
            var roles = this.RD_YearDataSet
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.YearID.ToString(),
                                    Text = x.YearName
                                });

            return new SelectList(roles, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetTrueFalseNullList()
        {
            var roles = new List<SelectListItem>{
                new SelectListItem
                {
                    Value = "true",
                    Text = "Yes"
                },
                new SelectListItem
                {
                    Value = "false",
                    Text = "No"
                },
                new SelectListItem
                {
                    Value = "null",
                    Text = "N/A"
                }
            };

            return new SelectList(roles, "Value", "Text");
        }
    }
}