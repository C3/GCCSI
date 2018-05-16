using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Utils
{
    public class ConfigHelper
    {
        // The AAD Instance is the instance of Azure, for example public Azure or Azure China.
        // The Client ID is used by the application to uniquely identify itself to Azure AD.
        // The App Key is a credential used to authenticate the application to Azure AD.  Azure AD supports password and certificate credentials.
        // The GraphResourceId the resource ID of the AAD Graph API.  We'll need this to request a token to call the Graph API.
        // The GraphApiVersion specifies which version of the AAD Graph API to call.
        // The Post Logout Redirect Uri is the URL where the user will be redirected after they sign out.
        // The Authority is the sign-in URL of the tenant.

        private static readonly string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string appKey = ConfigurationManager.AppSettings["ida:AppKey"];
        private static string graphResourceId = ConfigurationManager.AppSettings["ida:GraphUrl"];
        private static string appTenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string powerBiAPI = ConfigurationManager.AppSettings["ida:PowerBiAPI"];
        private static string powerBIGroupID = ConfigurationManager.AppSettings["powerBIGroupId"];
        private static string graphApiVersion = ConfigurationManager.AppSettings["ida:GraphApiVersion"];
        private static readonly string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
        private static string commonAuthority = String.Format(CultureInfo.InvariantCulture, aadInstance, "common/");

        public static string ClientId { get { return clientId; } }
        internal static string AppKey { get { return appKey; } }
        internal static string GraphResourceId { get { return graphResourceId; } }
        internal static string GraphApiVersion { get { return graphApiVersion; } }
        internal static string AadInstance { get { return aadInstance; } }
        internal static string PostLogoutRedirectUri { get { return postLogoutRedirectUri; } }
        internal static string CommonAuthority { get { return commonAuthority; } }
        internal static string Tenant { get { return appTenant; } }
        internal static string PowerBiAPI { get { return powerBiAPI; } }
        internal static string PowerBiGroupID { get { return powerBIGroupID; } }

        
        internal static string CCSReadinessIndicatorInternal = "CCS Readiness Indicator Summary Report - Internal_Members";
        internal static string CCSReadinessIndicatorPublic = "CCS Readiness Indicator Summary Report - Public";
        internal static string ClimateChangeReport = "Climate Change Report";
        internal static string FacilitiesDetailsInternal = "Facilities Details Report - Internal_Members";
        internal static string FacilitiesReportInternal = "Facilities Report - Internal_Members";
        internal static string FacilitiesReportPublic = "Facilities Report - Public";
        internal static string InherentInterestInternal = "Inherent Interest Report - Internal_Members";
        internal static string InherentInterestPublic = "Inherent Interest Report - Public";
        internal static string LegalAndRegRepOwners = "Legal and Regulatory Report - Data Owners";
        internal static string LegalAndRegRepInternal = "Legal and Regulatory Report - Internal";
        internal static string LegalAndRegRepMembers = "Legal and Regulatory Report - Members";
        internal static string LegalAndRegRepPublic = "Legal and Regulatory Report - Public";
        internal static string PoliciesListReportInternal = "Policies List Report - Internal";
        internal static string PolicyIndicatorSummaryInternal = "Policy Indicator Summary Report - Internal_Members";
        internal static string PolicyIndicatorSummaryPublic = "Policy Indicator Summary Report - Public";
        internal static string StorageIndicatorReportPublic = "Storage Indicator Report - Public";
        internal static string StorageIndicatorReportInternal = "Storage Indicator Report - Internal_Members";
        internal static string StorageResourcesReportInternal = "Storage Resources Report - Internal_Members";
        internal static string StorageResourcesReportPublic = "Storage Resources Report - Public";
    }
}