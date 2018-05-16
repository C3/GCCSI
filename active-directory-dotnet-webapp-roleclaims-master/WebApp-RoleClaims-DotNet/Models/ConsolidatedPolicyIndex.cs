using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class ConsolidatedPolicyIndex
    {
        [Key]
        public int CPIID { get; set; }

        public int? RegionID { get; set; }
        public virtual RD_Region Region { get; set; }

        public int? CountryID { get; set; }
        public virtual RD_Country Country { get; set; }

        public bool? Leader { get; set; }

        public bool? FastFollower { get; set; }

        public bool? Detached { get; set; }

        public int? FiT { get; set; }
        [ForeignKey("FiT")]
        public virtual RD_PolicyStatus FiTStatus { get; set; }

        public int? InfrastructureInvestments { get; set; }
        [ForeignKey("InfrastructureInvestments")]
        public virtual RD_PolicyStatus InfrastructureInvestmentsStatus { get; set; }

        public int? TaxRelief { get; set; }
        [ForeignKey("TaxRelief")]
        public virtual RD_PolicyStatus TaxReliefStatus { get; set; }

        public int? Taxes { get; set; }
        [ForeignKey("Taxes")]
        public virtual RD_PolicyStatus TaxesStatus { get; set; }

        public int? CarbonPricingArrangements { get; set; }
        [ForeignKey("CarbonPricingArrangements")]
        public virtual RD_PolicyStatus CarbonPricingArrangementsStatus { get; set; }

        public int? RegulatedPricing { get; set; }
        [ForeignKey("RegulatedPricing")]
        public virtual RD_PolicyStatus RegulatedPricingStatus { get; set; }

        public int? GrantsSubsidies { get; set; }
        [ForeignKey("GrantsSubsidies")]
        public virtual RD_PolicyStatus GrantsSubsidiesStatus { get; set; }

        public int? RD_D_Funding { get; set; }
        [ForeignKey("RD_D_Funding")]
        public virtual RD_PolicyStatus RD_D_FundingStatus { get; set; }

        public int? LoanGuarantees { get; set; }
        [ForeignKey("LoanGuarantees")]
        public virtual RD_PolicyStatus LoanGuaranteesStatus { get; set; }

        public int? CapAndTrade { get; set; }
        [ForeignKey("CapAndTrade")]
        public virtual RD_PolicyStatus CapAndTradeStatus { get; set; }

        public int? Offset { get; set; }
        [ForeignKey("Offset")]
        public virtual RD_PolicyStatus OffsetStatus { get; set; }

        public int? StatutoryClimateCCSBody { get; set; }
        [ForeignKey("StatutoryClimateCCSBody")]
        public virtual RD_PolicyStatus StatutoryClimateCCSBodyStatus { get; set; }

        public int? LegislatedEmissionTarget { get; set; }
        [ForeignKey("LegislatedEmissionTarget")]
        public virtual RD_PolicyStatus LegislatedEmissionTargetStatus { get; set; }

        public int? CarbonBudgetsWithReporting { get; set; }
        [ForeignKey("CarbonBudgetsWithReporting")]
        public virtual RD_PolicyStatus CarbonBudgetsWithReportingStatus { get; set; }

        public int? PolicyStatementsMinor { get; set; }
        [ForeignKey("PolicyStatementsMinor")]
        public virtual RD_PolicyStatus PolicyStatementsMinorStatus { get; set; }

        public int? PolicyStatementsMajor { get; set; }
        [ForeignKey("PolicyStatementsMajor")]
        public virtual RD_PolicyStatus PolicyStatementsMajorStatus { get; set; }

        public int? WhitePaper { get; set; }
        [ForeignKey("WhitePaper")]
        public virtual RD_PolicyStatus WhitePaperStatus { get; set; }

        public int? ImplementationAdvice { get; set; }
        [ForeignKey("ImplementationAdvice")]
        public virtual RD_PolicyStatus ImplementationAdviceStatus { get; set; }

        public int? KnowledgeSharing { get; set; }
        [ForeignKey("KnowledgeSharing")]
        public virtual RD_PolicyStatus KnowledgeSharingStatus { get; set; }

        public int? InternationalStandards { get; set; }
        [ForeignKey("InternationalStandards")]
        public virtual RD_PolicyStatus InternationalStandardsStatus { get; set; }

        public int? Roadmaps { get; set; }
        [ForeignKey("Roadmaps")]
        public virtual RD_PolicyStatus RoadmapsStatus { get; set; }

        public int? TechnologyNeedsAssessments { get; set; }
        [ForeignKey("TechnologyNeedsAssessments")]
        public virtual RD_PolicyStatus TechnologyNeedsAssessmentsStatus { get; set; }

        public int? InternationalFundsContributes { get; set; }
        [ForeignKey("InternationalFundsContributes")]
        public virtual RD_PolicyStatus InternationalFundsContributesStatus { get; set; }

        public int? MOU { get; set; }
        [ForeignKey("MOU")]
        public virtual RD_PolicyStatus MOUStatus { get; set; }

        public int? PublicInitiative { get; set; }
        [ForeignKey("PublicInitiative")]
        public virtual RD_PolicyStatus PublicInitiativeStatus { get; set; }

        public int? PrivateInitiative { get; set; }
        [ForeignKey("PrivateInitiative")]
        public virtual RD_PolicyStatus PrivateInitiativeStatus { get; set; }

        public int? ObligationSchemes { get; set; }
        [ForeignKey("ObligationSchemes")]
        public virtual RD_PolicyStatus ObligationSchemesStatus { get; set; }

        public int? EmissionsPerformanceStandards { get; set; }
        [ForeignKey("EmissionsPerformanceStandards")]
        public virtual RD_PolicyStatus EmissionsPerformanceStandardsStatus { get; set; }

        public int? CommercialScaleStorage { get; set; }
        [ForeignKey("CommercialScaleStorage")]
        public virtual RD_PolicyStatus CommercialScaleStorageStatus { get; set; }

        public int? EnergyIntensityTargets { get; set; }
        [ForeignKey("EnergyIntensityTargets")]
        public virtual RD_PolicyStatus EnergyIntensityTargetsStatus { get; set; }

        public int? OtherMandatoryRequirements { get; set; }
        [ForeignKey("OtherMandatoryRequirements")]
        public virtual RD_PolicyStatus OtherMandatoryRequirementsStatus { get; set; }

        public string Summary { get; set; }

        public string Note { get; set; }

        public int? YearID { get; set; }
        public virtual RD_Year Year { get; set; }

        public DateTime UpdateDateTime { get; set; }

        public string References { get; set; }

    }

}