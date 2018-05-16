using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GCCSI_CO2RE.DAL;
using GCCSI_CO2RE.Models;
using System.Security.Claims;
using Microsoft.Rest;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using GCCSI_CO2RE.Utils;

namespace GCCSI_CO2RE.Controllers
{
    /// <summary>
    /// 
    /// This controller will handle all of the dynamically shown content for the tabs and the tabbed content per page.
    /// Depending on a users authentication this controller will return different visualisations.
    /// 
    /// </summary>
    public class PowerBITabsController : Controller
    {

        private GCCSIContext db = new GCCSIContext();

        // --- TOP LEVEL TAB SELECTION SECTION ---

        /// <summary>
        /// 
        /// The generic tab list for all users - auth and unauth.
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetGenericTabList()
        {
            string facilitiesSection = @"
                    <li class=""nav-item dropdown"">
                        <a class=""nav-link dropdown-toggle"" data-toggle=""dropdown"" href=""#"">Facilities</a>
                        <div class=""dropdown-menu"">
                            <a class=""dropdown-item"" id=""facilityReport""  role=""tab"" data-toggle=""tab"" href=""#tabeight"">Facilities Report</a>
                        </div>
                    </li>";

            string firstHalf = @"  
                    <li class=""nav-item dropdown"">
                        <a class=""nav-link dropdown-toggle"" data-toggle=""dropdown"" href=""#"">Storage</a>
                        <div class=""dropdown-menu"">
                            <a class=""dropdown-item"" id=""storageIndicator"" role=""tab"" data-toggle=""tab"" href=""#tabfour"">Storage Indicator</a>
                            <a class=""dropdown-item"" id=""storageResources""  role=""tab"" data-toggle=""tab"" href=""#tabfive"">Storage Resources</a>
                        </div>
                    </li>
                    <li class=""nav-item"">
                        <a href="""" class=""nav-link"" data-toggle=""tab"" id=""legalAndReg"" data-target=""#tabsix"">Legal &amp; Regulatory</a>
                    </li>";

            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin") || User.IsInRole("InternalReader") || User.IsInRole("MemberReader"))
                {
                    facilitiesSection = @"
                    <li class=""nav-item dropdown"">
                        <a class=""nav-link dropdown-toggle"" data-toggle=""dropdown"" href=""#"">Facilities</a>
                        <div class=""dropdown-menu"">
                            <a class=""dropdown-item"" id=""facilityDetails"" role=""tab"" data-toggle=""tab"" href=""#tabseven"">Facilities Details</a>
                            <a class=""dropdown-item"" id=""facilityReport""  role=""tab"" data-toggle=""tab"" href=""#tabeight"">Facilities Report</a>
                        </div>
                    </li>
                    ";
                }
            }

          
            string secondHalf = @"
                    <li class=""nav-item dropdown"">
                        <a class=""nav-link dropdown-toggle"" data-toggle=""dropdown"" href=""#"">Climate Change</a>
                        <div class=""dropdown-menu"">
                            <a class=""dropdown-item"" id=""climateChange""  role=""tab"" data-toggle=""tab"" href=""#tabnine"">Climate Change Report</a>
                            <a class=""dropdown-item"" id=""inherentInterest""  role=""tab"" data-toggle=""tab"" href=""#tabten"">Inherent Interest</a>
                            <a class=""dropdown-item"" id=""ccsReady""  role=""tab"" data-toggle=""tab"" href=""#tabeleven"">CCS Readiness</a>
                        </div>
                    </li>"; 

            return firstHalf + facilitiesSection + secondHalf;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetAdminTabList()
        {
            //Create the content element.
            string htmlElementsOption = @"
                    <li class=""nav-item"">
                        <a href="""" class=""nav-link"" data-toggle=""tab"" id=""tabOneButton"" data-target=""#tabone"">Data Management</a>
                    </li>
                    <li class=""nav-item dropdown"">
                        <a class=""nav-link dropdown-toggle"" data-toggle=""dropdown"" href=""#"">Policies</a>
                        <div class=""dropdown-menu"">
                            <a class=""dropdown-item"" id=""policiesList"" role=""tab"" data-toggle=""tab"" href=""#tabtwo"">Policies List - Internal</a>
                            <a class=""dropdown-item"" id=""policyIndicatorInternal"" role=""tab"" data-toggle=""tab"" href=""#tabthreeInt"">Policy Indicator - Internal</a>
                            <a class=""dropdown-item"" id=""policyIndicatorPublic"" role=""tab"" data-toggle=""tab"" href=""#tabthreePub"">Policy Indicator - Public</a>
                        </div>
                    </li>
                    <li class=""nav-item dropdown"">
                        <a class=""nav-link dropdown-toggle"" data-toggle=""dropdown"" href=""#"">Storage</a>
                        <div class=""dropdown-menu"">
                            <a class=""dropdown-item"" id=""storageIndicatorInternal"" role=""tab"" data-toggle=""tab"" href=""#tabfourInt"">Storage Indicator - Internal</a>
                            <a class=""dropdown-item"" id=""storageIndicatorPublic"" role=""tab"" data-toggle=""tab"" href=""#tabfourPub"">Storage Indicator - Public</a>
                            <a class=""dropdown-item"" id=""storageResourcesInternal""  role=""tab"" data-toggle=""tab"" href=""#tabfiveInt"">Storage Resources - Internal</a>
                            <a class=""dropdown-item"" id=""storageResourcesPublic""  role=""tab"" data-toggle=""tab"" href=""#tabfivePub"">Storage Resources - Public</a>
                        </div>
                    </li>
                    <li class=""nav-item dropdown"">
                        <a class=""nav-link dropdown-toggle"" data-toggle=""dropdown"" href=""#"">Legal &amp; Regulatory</a>
                        <div class=""dropdown-menu"">
                            <a class=""dropdown-item"" id=""legalAndRegOwner"" role=""tab"" data-toggle=""tab"" href=""#tabsixOwn"">Legal &amp; Regulatory - Owner</a>
                            <a class=""dropdown-item"" id=""legalAndRegInternal"" role=""tab"" data-toggle=""tab"" href=""#tabsixInt"">Legal &amp; Regulatory - Internal</a>
                            <a class=""dropdown-item"" id=""legalAndRegMembers""  role=""tab"" data-toggle=""tab"" href=""#tabsixMem"">Legal &amp; Regulatory - Members</a>
                            <a class=""dropdown-item"" id=""legalAndRegPublic""  role=""tab"" data-toggle=""tab"" href=""#tabsixPub"">Legal &amp; Regulatory - Public</a>
                        </div>
                    </li>
                    <li class=""nav-item dropdown"">
                        <a class=""nav-link dropdown-toggle"" data-toggle=""dropdown"" href=""#"">Facilities</a>
                        <div class=""dropdown-menu"">
                            <a class=""dropdown-item"" id=""facilityDetails"" role=""tab"" data-toggle=""tab"" href=""#tabseven"">Facilities Details</a>
                            <a class=""dropdown-item"" id=""facilityReportInt""  role=""tab"" data-toggle=""tab"" href=""#tabeightInt"">Facilities Report - Internal</a>
                            <a class=""dropdown-item"" id=""facilityReportPub""  role=""tab"" data-toggle=""tab"" href=""#tabeightPub"">Facilities Report - Public</a>
                        </div>
                    </li>
                    <li class=""nav-item dropdown"">
                        <a class=""nav-link dropdown-toggle"" data-toggle=""dropdown"" href=""#"">Climate Change</a>
                        <div class=""dropdown-menu"">
                            <a class=""dropdown-item"" id=""climateChange""  role=""tab"" data-toggle=""tab"" href=""#tabnine"">Climate Change Report</a>
                            <a class=""dropdown-item"" id=""inherentInterestInt""  role=""tab"" data-toggle=""tab"" href=""#tabtenInt"">Inherent Interest - Internal</a>
                            <a class=""dropdown-item"" id=""inherentInterestPub""  role=""tab"" data-toggle=""tab"" href=""#tabtenPub"">Inherent Interest - Public</a>
                            <a class=""dropdown-item"" id=""ccsReadyInt""  role=""tab"" data-toggle=""tab"" href=""#tabelevenInt"">CCS Readiness - Internal</a>
                            <a class=""dropdown-item"" id=""ccsReadyPub""  role=""tab"" data-toggle=""tab"" href=""#tabelevenPub"">CCS Readiness - Public</a>
                        </div>
                    </li>
                ";

            return Content(htmlElementsOption);
        }

        [Authorize(Roles = "InternalReader")]
        public ActionResult GetInternalReaderTabList()
        {
            //Create the content element.
            string htmlElementsOption = @"
                    <li class=""nav-item dropdown"">
                        <a class=""nav-link dropdown-toggle"" data-toggle=""dropdown"" href=""#"">Policies</a>
                        <div class=""dropdown-menu"">
                            <a class=""dropdown-item"" id=""policiesList"" role=""tab"" data-toggle=""tab"" href=""#tabtwo"">Policies List</a>
                            <a class=""dropdown-item"" id=""policyIndicator"" role=""tab"" data-toggle=""tab"" href=""#tabthree"">Policy Indicator</a>
                        </div>
                    </li>
                ";
            htmlElementsOption = htmlElementsOption + GetGenericTabList();
            return Content(htmlElementsOption);
        }

        public ActionResult GetPublicAndMemberUserTabList()
        {
            //Create the content element.
            string htmlElementsOption = @"
                        <li class=""nav-item dropdown"">
                            <a class=""nav-link dropdown-toggle"" data-toggle=""dropdown"" href=""#"">Policies</a>
                            <div class=""dropdown-menu"">
                                <a class=""dropdown-item"" id=""policyIndicator"" role=""tab"" data-toggle=""tab"" href=""#tabthree"">Policy Indicator</a>
                            </div>
                        </li>" + GetGenericTabList();
            return Content(htmlElementsOption);
        }


        // --- POWER BI CONTENT SECTION ---

        //Defined here is the 'id' for initial tab selection.

        /// HTML Route Values (These represent the website sections)
        /// 1. Climate Change
        /// 2. Facility Data
        /// 3. Legal Reg Data
        /// 4. Policies
        /// 5. Storage Data
        /// 
        private string GetSelectedTabSection(string id, bool isAdmin)
        {
            //Add logic to set the initial tab selection according to ID mappings.
            string selectedTabScript = "";

            if (id == "1") //Climate Change
            {
                selectedTabScript = "$('#climateChange').trigger('click');";
            }
            else if (id == "2")//Facility Data
            {
                selectedTabScript = isAdmin ? "$('#tabOneButton').trigger('click');" : "$('#facilityReport').trigger('click');";
            }
            else if (id == "3")//Legal Reg Data
            {
                selectedTabScript = isAdmin ? "$('#tabOneButton').trigger('click');" : "$('#legalAndReg').trigger('click');";
            }
            else if (id == "4")//Policies
            {
                selectedTabScript = isAdmin ? "$('#tabOneButton').trigger('click');" : "$('#policyIndicator').trigger('click');";
            }
            else if (id == "5")//Storage Data
            {
                selectedTabScript = isAdmin ? "$('#tabOneButton').trigger('click');" : "$('#storageIndicator').trigger('click');";
            }

            return selectedTabScript;
        }


        public ActionResult GetPublicPowerBIContent(string id)
        {
            string[] reportArray = new string[10];

            try
            {
                //Get the embedded URLs here --> Fetch from database.
                var allReports = db.PowerBIReportSet;
                
                reportArray[0] = allReports.FirstOrDefault(a => a.ReportName == ConfigHelper.PolicyIndicatorSummaryPublic).Url;
                reportArray[1] = allReports.FirstOrDefault(a => a.ReportName == ConfigHelper.StorageIndicatorReportPublic).Url;
                reportArray[2] = allReports.FirstOrDefault(a => a.ReportName == ConfigHelper.StorageResourcesReportPublic).Url;
                reportArray[3] = allReports.FirstOrDefault(a => a.ReportName == ConfigHelper.LegalAndRegRepPublic).Url;
                reportArray[4] = allReports.FirstOrDefault(a => a.ReportName == ConfigHelper.FacilitiesReportPublic).Url;
                reportArray[5] = allReports.FirstOrDefault(a => a.ReportName == ConfigHelper.ClimateChangeReport).Url;
                reportArray[6] = allReports.FirstOrDefault(a => a.ReportName == ConfigHelper.InherentInterestPublic).Url;
                reportArray[7] = allReports.FirstOrDefault(a => a.ReportName == ConfigHelper.CCSReadinessIndicatorPublic).Url;


                string htmlElements = String.Format(@"
                            <div class=""tab-pane fade"" id=""tabthree"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabthreediv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabfour"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabfourdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabfive"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabfivediv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabsix"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabsixdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabeight"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabeightdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabnine"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabninediv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabten"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabtendiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabeleven"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabelevendiv"" style=""height:1000px"">
                                </div>
                            </div>


                        <script>
                             $(document).ready(function () {{
                                // Grab the reference to the div HTML element that will host the report.
                                var reportContainerThree = document.getElementById('tabthreediv');
                                var reportContainerFour = document.getElementById('tabfourdiv');
                                var reportContainerFive = document.getElementById('tabfivediv');
                                var reportContainerSix = document.getElementById('tabsixdiv');
                                var reportContainerEight = document.getElementById('tabeightdiv');
                                var reportContainerNine = document.getElementById('tabninediv');
                                var reportContainerTen = document.getElementById('tabtendiv');
                                var reportContainerEleven = document.getElementById('tabelevendiv');

                                $('[data-toggle=""tab""]').click(function(e) {{
                                    var $this = $(this),
                                    id = $this.attr('id');

                                    if(id == 'policyIndicator'){{
                                        reportContainerThree.innerHTML = '<iframe style=""width:100%; height:1000px"" src=""{0}"" frameborder=""0"" allowFullScreen=""true""></iframe>';
                                        document.getElementById('indexBreadcrum').innerHTML = 'Policies';
                                        document.title = 'Policies - Global CCS Institute';
                                    }}
                                    else if(id == 'storageIndicator'){{
                                        reportContainerFour.innerHTML = '<iframe style=""width:100%; height:1000px"" src=""{1}"" frameborder=""0"" allowFullScreen=""true""></iframe>';
                                        document.getElementById('indexBreadcrum').innerHTML = 'Storage Indicator';
                                        document.title = 'Storage - Global CCS Institute';
                                    }}
                                    else if(id == 'storageResources'){{
                                        reportContainerFive.innerHTML = '<iframe style=""width:100%; height:1000px"" src=""{2}"" frameborder=""0"" allowFullScreen=""true""></iframe>';
                                        document.getElementById('indexBreadcrum').innerHTML = 'Storage Resources';
                                        document.title = 'Storage - Global CCS Institute';
                                    }}
                                    else if(id == 'legalAndReg'){{
                                        reportContainerSix.innerHTML = '<iframe style=""width:100%; height:1000px"" src=""{3}"" frameborder=""0"" allowFullScreen=""true""></iframe>';
                                        document.getElementById('indexBreadcrum').innerHTML = 'Legal & Regulatory';
                                        document.title = 'Legal & Regulatory - Global CCS Institute';
                                    }}
                                    else if(id == 'facilityReport'){{
                                        reportContainerEight.innerHTML = '<iframe style=""width:100%; height:1000px"" src=""{4}"" frameborder=""0"" allowFullScreen=""true""></iframe>';
                                        document.getElementById('indexBreadcrum').innerHTML = 'Facilities';
                                        document.title = 'Facilities - Global CCS Institute';
                                    }}
                                    else if(id == 'climateChange'){{
                                        reportContainerNine.innerHTML = '<iframe style=""width:100%; height:1000px"" src=""{5}"" frameborder=""0"" allowFullScreen=""true""></iframe>';
                                        document.getElementById('indexBreadcrum').innerHTML = 'Climate Change';
                                        document.title = 'Climate Change - Global CCS Institute';
                                    }}
                                    else if(id == 'inherentInterest'){{
                                        reportContainerTen.innerHTML = '<iframe style=""width:100%; height:1000px"" src=""{6}"" frameborder=""0"" allowFullScreen=""true""></iframe>';
                                        document.getElementById('indexBreadcrum').innerHTML = 'Climate Change - Inherent Interest';
                                        document.title = 'Climate Change - Global CCS Institute';
                                    }}
                                    else if(id == 'ccsReady'){{
                                        reportContainerEleven.innerHTML = '<iframe style=""width:100%; height:1000px"" src=""{7}"" frameborder=""0"" allowFullScreen=""true""></iframe>';
                                        document.getElementById('indexBreadcrum').innerHTML = 'Climate Change - CCS Readiness';
                                        document.title = 'Climate Change - Global CCS Institute';
                                    }}
                                    
                                    $this.tab('show');
                                    return false;
                                }});

                                " + GetSelectedTabSection(id, false)+@"

                            }});
                            
                        </script>
                        ",
                reportArray[0],//Policy Indicator
                reportArray[1],//Storage Indicator
                reportArray[2],//Storage Resources
                reportArray[3],//Legal and Reg 
                reportArray[4],//Facility Report
                reportArray[5],//Climate Change
                reportArray[6],//Inherent Interest
                reportArray[7]//CCS Ready
                );

                return Content(htmlElements);
            }
            catch (HttpOperationException ex)
            {
                //Bad Request
                return Content(ex.Response.Content);
            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult GetAdminPowerBIContent(string id)
        {
            if (User.Identity.IsAuthenticated) //&& User.IsInRole("Admin"))
            {
                var ci = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
                string rawTokenVal = ci.FindFirst("powerBIToken").Value;
                var tokenCredentials = new TokenCredentials(ci.FindFirst("powerBIToken").Value, "Bearer");

                string[] reportArray = new string[19];

                try
                {
                    using (var client = new PowerBIClient(new Uri("https://api.powerbi.com/"), tokenCredentials))
                    {
                        ODataResponseListReport allReports = client.Reports.GetReportsInGroup(ConfigHelper.PowerBiGroupID);

                        //Get the embedded URLs here...
                        reportArray[0] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.PoliciesListReportInternal).EmbedUrl;
                        reportArray[1] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.PolicyIndicatorSummaryInternal).EmbedUrl;
                        reportArray[2] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.PolicyIndicatorSummaryPublic).EmbedUrl;
                        reportArray[3] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.StorageIndicatorReportInternal).EmbedUrl;
                        reportArray[4] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.StorageIndicatorReportPublic).EmbedUrl;
                        reportArray[5] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.StorageResourcesReportInternal).EmbedUrl;
                        reportArray[6] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.StorageResourcesReportPublic).EmbedUrl;
                        reportArray[7] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.LegalAndRegRepOwners).EmbedUrl;
                        reportArray[8] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.LegalAndRegRepInternal).EmbedUrl;
                        reportArray[9] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.LegalAndRegRepMembers).EmbedUrl;
                        reportArray[10] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.LegalAndRegRepPublic).EmbedUrl;
                        reportArray[11] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.FacilitiesDetailsInternal).EmbedUrl;
                        reportArray[12] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.FacilitiesReportInternal).EmbedUrl;
                        reportArray[13] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.FacilitiesReportPublic).EmbedUrl;
                        reportArray[14] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.ClimateChangeReport).EmbedUrl;
                        reportArray[15] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.InherentInterestInternal).EmbedUrl;
                        reportArray[16] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.InherentInterestPublic).EmbedUrl;
                        reportArray[17] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.CCSReadinessIndicatorInternal).EmbedUrl;
                        reportArray[18] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.CCSReadinessIndicatorPublic).EmbedUrl;

                        string htmlElements = String.Format(@"
                            <div class=""tab-pane fade"" id=""tabtwo"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabtwodiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabthreeInt"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabthreeIntdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabthreePub"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabthreePubdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabfourInt"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabfourIntdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabfourPub"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabfourPubdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabfiveInt"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabfiveIntdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabfivePub"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabfivePubdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabsixOwn"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabsixOwndiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabsixInt"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabsixIntdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabsixMem"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabsixMemdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabsixPub"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabsixPubdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabseven"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabsevendiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabeightInt"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabeightIntdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabeightPub"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabeightPubdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabnine"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabninediv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabtenInt"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabtenIntdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabtenPub"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabtenPubdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabelevenInt"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabelevenIntdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabelevenPub"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabelevenPubdiv"" style=""height:1000px"">
                                </div>
                            </div>

                        <script>

                             $(document).ready(function () {{
                                // Grab the reference to the div HTML element that will host the report.
                                var reportContainerTwo = document.getElementById('tabtwodiv');
                                var reportContainerThreeInt = document.getElementById('tabthreeIntdiv');
                                var reportContainerThreePub = document.getElementById('tabthreePubdiv');
                                var reportContainerFourInt = document.getElementById('tabfourIntdiv');
                                var reportContainerFourPub = document.getElementById('tabfourPubdiv');
                                var reportContainerFiveInt = document.getElementById('tabfiveIntdiv');
                                var reportContainerFivePub = document.getElementById('tabfivePubdiv');
                                var reportContainerSixOwn = document.getElementById('tabsixOwndiv');
                                var reportContainerSixInt = document.getElementById('tabsixIntdiv');
                                var reportContainerSixMem = document.getElementById('tabsixMemdiv');
                                var reportContainerSixPub = document.getElementById('tabsixPubdiv');
                                var reportContainerSeven = document.getElementById('tabsevendiv');
                                var reportContainerEightInt = document.getElementById('tabeightIntdiv');
                                var reportContainerEightPub = document.getElementById('tabeightPubdiv');
                                var reportContainerNine = document.getElementById('tabninediv');
                                var reportContainerTenInt = document.getElementById('tabtenIntdiv');
                                var reportContainerTenPub = document.getElementById('tabtenPubdiv');
                                var reportContainerElevenInt = document.getElementById('tabelevenIntdiv');
                                var reportContainerElevenPub = document.getElementById('tabelevenPubdiv');

                                $('[data-toggle=""tab""]').click(function(e) {{
                                    var $this = $(this),
                                    id = $this.attr('id');

                                    if(id == 'tabOneButton'){{
                                        var pageName =  document.getElementById('gccsiSection').innerHTML;
                                        document.getElementById('indexBreadcrum').innerHTML = pageName;
                                        document.title = pageName+' - Global CCS Institute';
                                    }}
                                    else if(id == 'policiesList'){{
                                        var report2 = powerbi.embed(reportContainerTwo, {{ type: 'report',accessToken: '{0}', embedUrl: '{1}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Policies';
                                        document.title = 'Policies - Global CCS Institute';
                                    }}
                                    else if(id == 'policyIndicatorInternal'){{
                                        var report3 = powerbi.embed(reportContainerThreeInt, {{ type: 'report',accessToken: '{0}', embedUrl: '{2}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Policy Indicator';
                                        document.title = 'Policies - Global CCS Institute';
                                    }}
                                    else if(id == 'policyIndicatorPublic'){{
                                        var report4 = powerbi.embed(reportContainerThreePub, {{ type: 'report',accessToken: '{0}', embedUrl: '{3}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Policy Indicator';
                                        document.title = 'Policies - Global CCS Institute';
                                    }}
                                    else if(id == 'storageIndicatorInternal'){{
                                        var report5 = powerbi.embed(reportContainerFourInt, {{ type: 'report',accessToken: '{0}', embedUrl: '{4}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Storage Indicator';
                                        document.title = 'Storage - Global CCS Institute';
                                    }}
                                    else if(id == 'storageIndicatorPublic'){{
                                        var report6 = powerbi.embed(reportContainerFourPub, {{ type: 'report',accessToken: '{0}', embedUrl: '{5}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Storage Indicator';
                                        document.title = 'Storage - Global CCS Institute';
                                    }}
                                    else if(id == 'storageResourcesInternal'){{
                                        var report7 = powerbi.embed(reportContainerFiveInt, {{ type: 'report',accessToken: '{0}', embedUrl: '{6}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Storage Resources';
                                        document.title = 'Storage - Global CCS Institute';
                                    }}
                                    else if(id == 'storageResourcesPublic'){{
                                        var report8 = powerbi.embed(reportContainerFivePub, {{ type: 'report',accessToken: '{0}', embedUrl: '{7}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Storage Resources';
                                        document.title = 'Storage - Global CCS Institute';
                                    }}
                                    else if(id == 'legalAndRegOwner'){{
                                        var report9 = powerbi.embed(reportContainerSixOwn, {{ type: 'report',accessToken: '{0}', embedUrl: '{8}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Legal & Regulatory';
                                        document.title = 'Legal & Regulatory - Global CCS Institute';
                                    }}
                                    else if(id == 'legalAndRegInternal'){{
                                        var report10 = powerbi.embed(reportContainerSixInt, {{ type: 'report',accessToken: '{0}', embedUrl: '{9}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Legal & Regulatory';
                                        document.title = 'Legal & Regulatory - Global CCS Institute';
                                    }}
                                    else if(id == 'legalAndRegMembers'){{
                                        var report11 = powerbi.embed(reportContainerSixMem, {{ type: 'report',accessToken: '{0}', embedUrl: '{10}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Legal & Regulatory';
                                        document.title = 'Legal & Regulatory - Global CCS Institute';
                                    }}
                                    else if(id == 'legalAndRegPublic'){{
                                        var report12 = powerbi.embed(reportContainerSixPub, {{ type: 'report',accessToken: '{0}', embedUrl: '{11}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Legal & Regulatory';
                                        document.title = 'Legal & Regulatory - Global CCS Institute';
                                    }}
                                    else if(id == 'facilityDetails'){{
                                        var report13 = powerbi.embed(reportContainerSeven, {{ type: 'report',accessToken: '{0}', embedUrl: '{12}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Facility Details';
                                        document.title = 'Facilities - Global CCS Institute';
                                    }}
                                    else if(id == 'facilityReportInt'){{
                                        var report14 = powerbi.embed(reportContainerEightInt, {{ type: 'report',accessToken: '{0}', embedUrl: '{13}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Facility Report';
                                        document.title = 'Facilities - Global CCS Institute';
                                    }}
                                    else if(id == 'facilityReportPub'){{
                                        var report15 = powerbi.embed(reportContainerEightPub, {{ type: 'report',accessToken: '{0}', embedUrl: '{14}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Facility Report';
                                        document.title = 'Facilities - Global CCS Institute';
                                    }}
                                    else if(id == 'climateChange'){{
                                        var report16 = powerbi.embed(reportContainerNine, {{ type: 'report',accessToken: '{0}', embedUrl: '{15}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Climate Change';
                                        document.title = 'Climate Change - Global CCS Institute';
                                    }}
                                    else if(id == 'inherentInterestInt'){{
                                        var report17 = powerbi.embed(reportContainerTenInt, {{ type: 'report',accessToken: '{0}', embedUrl: '{16}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Climate Change - Inherent Interest';
                                        document.title = 'Climate Change - Global CCS Institute';
                                    }}
                                    else if(id == 'inherentInterestPub'){{
                                        var report18 = powerbi.embed(reportContainerTenPub, {{ type: 'report',accessToken: '{0}', embedUrl: '{17}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Climate Change - Inherent Interest';
                                        document.title = 'Climate Change - Global CCS Institute';
                                    }}
                                    else if(id == 'ccsReadyInt'){{
                                        var report19 = powerbi.embed(reportContainerElevenInt, {{ type: 'report',accessToken: '{0}', embedUrl: '{18}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Climate Change - CCS Readiness';
                                        document.title = 'Climate Change - Global CCS Institute';
                                    }}
                                    else if(id == 'ccsReadyPub'){{
                                        var report20 = powerbi.embed(reportContainerElevenPub, {{ type: 'report',accessToken: '{0}', embedUrl: '{19}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Climate Change - CCS Readiness';
                                        document.title = 'Climate Change - Global CCS Institute';
                                    }}
                                    
                                    $this.tab('show');
                                    return false;
                                }});
                                    
                                " + GetSelectedTabSection(id, true) + @"

                            }});
                            
                        </script>
                        ",
                        rawTokenVal,
                        reportArray[0],
                        reportArray[1],
                        reportArray[2],
                        reportArray[3],
                        reportArray[4],
                        reportArray[5],
                        reportArray[6],
                        reportArray[7],
                        reportArray[8],
                        reportArray[9],
                        reportArray[10],
                        reportArray[11],
                        reportArray[12],
                        reportArray[13],
                        reportArray[14],
                        reportArray[15],
                        reportArray[16],
                        reportArray[17],
                        reportArray[18]);

                        return Content(htmlElements);

                    }
                }
                catch (HttpOperationException ex)
                {
                    //Bad Request
                    return Content(ex.Response.Content);
                }

            }
            else //Unauthenticated.
            {
                return Content("null");
            }
            
        }


        [Authorize(Roles = "InternalReader")]
        public ActionResult GetInternalReaderPowerBIContent(string id)
        {
            if (User.Identity.IsAuthenticated) //&& User.IsInRole("InternalReader"))
            {
                var ci = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
                string rawTokenVal = ci.FindFirst("powerBIToken").Value;
                var tokenCredentials = new TokenCredentials(ci.FindFirst("powerBIToken").Value, "Bearer");

                string[] reportArray = new string[10];

                try
                {
                    using (var client = new PowerBIClient(new Uri("https://api.powerbi.com/"), tokenCredentials))
                    {
                        
                        ODataResponseListReport allReports = client.Reports.GetReportsInGroup(ConfigHelper.PowerBiGroupID);

                        //Get the embedded URLs here...
                        reportArray[0] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.PoliciesListReportInternal).EmbedUrl;
                        reportArray[1] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.PolicyIndicatorSummaryInternal).EmbedUrl;
                        reportArray[2] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.StorageIndicatorReportInternal).EmbedUrl;
                        reportArray[3] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.StorageResourcesReportInternal).EmbedUrl;
                        reportArray[4] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.LegalAndRegRepInternal).EmbedUrl;
                        reportArray[5] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.FacilitiesDetailsInternal).EmbedUrl;
                        reportArray[6] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.FacilitiesReportInternal).EmbedUrl;
                        reportArray[7] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.ClimateChangeReport).EmbedUrl;
                        reportArray[8] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.InherentInterestInternal).EmbedUrl;
                        reportArray[9] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.CCSReadinessIndicatorInternal).EmbedUrl;
                        

                        string htmlElements = String.Format(@"
                            <div class=""tab-pane fade"" id=""tabtwo"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabtwodiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabthree"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabthreediv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabfour"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabfourdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabfive"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabfivediv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabsix"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabsixdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabseven"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabsevendiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabeight"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabeightdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabnine"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabninediv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabten"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabtendiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabeleven"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabelevendiv"" style=""height:1000px"">
                                </div>
                            </div>


                        <script>
                             $(document).ready(function () {{
                                // Grab the reference to the div HTML element that will host the report.
                                var reportContainerTwo = document.getElementById('tabtwodiv');
                                var reportContainerThree = document.getElementById('tabthreediv');
                                var reportContainerFour = document.getElementById('tabfourdiv');
                                var reportContainerFive = document.getElementById('tabfivediv');
                                var reportContainerSix = document.getElementById('tabsixdiv');
                                var reportContainerSeven = document.getElementById('tabsevendiv');
                                var reportContainerEight = document.getElementById('tabeightdiv');
                                var reportContainerNine = document.getElementById('tabninediv');
                                var reportContainerTen = document.getElementById('tabtendiv');
                                var reportContainerEleven = document.getElementById('tabelevendiv');

                                $('[data-toggle=""tab""]').click(function(e) {{
                                    var $this = $(this),
                                    id = $this.attr('id');

                                    if(id == 'policiesList'){{
                                        var report2 = powerbi.embed(reportContainerTwo, {{ type: 'report',accessToken: '{0}', embedUrl: '{1}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Policies';
                                        document.title = 'Policies - Global CCS Institute';
                                    }}
                                    else if(id == 'policyIndicator'){{
                                        var report3 = powerbi.embed(reportContainerThree, {{ type: 'report',accessToken: '{0}', embedUrl: '{2}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Policy Indicator';
                                        document.title = 'Policies - Global CCS Institute';
                                    }}
                                    else if(id == 'storageIndicator'){{
                                        var report4 = powerbi.embed(reportContainerThree, {{ type: 'report',accessToken: '{0}', embedUrl: '{3}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Storage Indicator';
                                        document.title = 'Storage - Global CCS Institute';
                                    }}
                                    else if(id == 'storageResources'){{
                                        var report5 = powerbi.embed(reportContainerFour, {{ type: 'report',accessToken: '{0}', embedUrl: '{4}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Storage Resources';
                                        document.title = 'Storage - Global CCS Institute';
                                    }}
                                    else if(id == 'legalAndReg'){{
                                        var report6 = powerbi.embed(reportContainerSix, {{ type: 'report',accessToken: '{0}', embedUrl: '{5}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Legal & Regulatory';
                                        document.title = 'Legal & Regulatory - Global CCS Institute';
                                    }}
                                    else if(id == 'facilityDetails'){{
                                        var report7 = powerbi.embed(reportContainerSeven, {{ type: 'report',accessToken: '{0}', embedUrl: '{6}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Facility Details';
                                        document.title = 'Facilities - Global CCS Institute';
                                    }}
                                    else if(id == 'facilityReport'){{
                                        var report8 = powerbi.embed(reportContainerEight, {{ type: 'report',accessToken: '{0}', embedUrl: '{7}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Facility Report';
                                        document.title = 'Facilities - Global CCS Institute';
                                    }}
                                    else if(id == 'climateChange'){{
                                        var report9 = powerbi.embed(reportContainerNine, {{ type: 'report',accessToken: '{0}', embedUrl: '{8}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Climate Change';
                                        document.title = 'Climate Change - Global CCS Institute';
                                    }}
                                    else if(id == 'inherentInterest'){{
                                        var report10 = powerbi.embed(reportContainerTen, {{ type: 'report',accessToken: '{0}', embedUrl: '{9}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Climate Change - Inherent Interest';
                                        document.title = 'Climate Change - Global CCS Institute';
                                    }}
                                    else if(id == 'ccsReady'){{
                                        var report10 = powerbi.embed(reportContainerEleven, {{ type: 'report',accessToken: '{0}', embedUrl: '{10}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Climate Change - CCS Readiness';
                                        document.title = 'Climate Change - Global CCS Institute';
                                    }}
                                    
                                    $this.tab('show');
                                    return false;
                                }});
                                " + GetSelectedTabSection(id, false) + @"
                            }});
                            
                        </script>
                        ",
                        rawTokenVal,
                        reportArray[0],//Policy List
                        reportArray[1],//Policy Indicator
                        reportArray[2],//Storage Indicator
                        reportArray[3],//Storage Resources
                        reportArray[4],//Legal and Reg
                        reportArray[5],//Facility Details
                        reportArray[6],//Facility Report
                        reportArray[7],//Climate Change
                        reportArray[8],//Inherent Interest
                        reportArray[9]//CCS Ready
                        );

                        return Content(htmlElements);

                    }
                }
                catch (HttpOperationException ex)
                {
                    //Bad Request
                    return Content(ex.Response.Content);
                }

            }
            else //Unauthenticated.
            {
                return Content("null");
            }

        }


        [Authorize(Roles = "MemberReader")]
        public ActionResult GetMemberReaderPowerBIContent(string id)
        {
            if (User.Identity.IsAuthenticated) //&& User.IsInRole("InternalReader"))
            {
                var ci = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
                string rawTokenVal = ci.FindFirst("powerBIToken").Value;
                var tokenCredentials = new TokenCredentials(ci.FindFirst("powerBIToken").Value, "Bearer");

                string[] reportArray = new string[9];

                try
                {
                    using (var client = new PowerBIClient(new Uri("https://api.powerbi.com/"), tokenCredentials))
                    {

                        ODataResponseListReport allReports = client.Reports.GetReportsInGroup(ConfigHelper.PowerBiGroupID);

                        //Get the embedded URLs here...
                        reportArray[0] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.PolicyIndicatorSummaryInternal).EmbedUrl;
                        reportArray[1] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.StorageIndicatorReportInternal).EmbedUrl;
                        reportArray[2] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.StorageResourcesReportInternal).EmbedUrl;
                        reportArray[3] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.LegalAndRegRepInternal).EmbedUrl;
                        reportArray[4] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.FacilitiesDetailsInternal).EmbedUrl;
                        reportArray[5] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.FacilitiesReportInternal).EmbedUrl;
                        reportArray[6] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.ClimateChangeReport).EmbedUrl;
                        reportArray[7] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.InherentInterestInternal).EmbedUrl;
                        reportArray[8] = allReports.Value.FirstOrDefault(a => a.Name == ConfigHelper.CCSReadinessIndicatorInternal).EmbedUrl;

                        string htmlElements = String.Format(@"
                            <div class=""tab-pane fade"" id=""tabthree"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabthreediv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabfour"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabfourdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabfive"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabfivediv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabsix"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabsixdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabseven"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabsevendiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabeight"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabeightdiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabnine"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabninediv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabten"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabtendiv"" style=""height:1000px"">
                                </div>
                            </div>
                            <div class=""tab-pane fade"" id=""tabeleven"" role=""tabpanel"">
                                <div class=""container-fluid"" id=""tabelevendiv"" style=""height:1000px"">
                                </div>
                            </div>


                        <script>
                             $(document).ready(function () {{
                                // Grab the reference to the div HTML element that will host the report.
                                var reportContainerThree = document.getElementById('tabthreediv');
                                var reportContainerFour = document.getElementById('tabfourdiv');
                                var reportContainerFive = document.getElementById('tabfivediv');
                                var reportContainerSix = document.getElementById('tabsixdiv');
                                var reportContainerSeven = document.getElementById('tabsevendiv');
                                var reportContainerEight = document.getElementById('tabeightdiv');
                                var reportContainerNine = document.getElementById('tabninediv');
                                var reportContainerTen = document.getElementById('tabtendiv');
                                var reportContainerEleven = document.getElementById('tabelevendiv');

                                $('[data-toggle=""tab""]').click(function(e) {{
                                    var $this = $(this),
                                    id = $this.attr('id');

                                    if(id == 'policyIndicator'){{
                                        var report3 = powerbi.embed(reportContainerThree, {{ type: 'report',accessToken: '{0}', embedUrl: '{1}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Policy Indicator';
                                        document.title = 'Policies - Global CCS Institute';
                                    }}
                                    else if(id == 'storageIndicator'){{
                                        var report4 = powerbi.embed(reportContainerFour, {{ type: 'report',accessToken: '{0}', embedUrl: '{2}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Storage Indicator';
                                        document.title = 'Storage - Global CCS Institute';
                                    }}
                                    else if(id == 'storageResources'){{
                                        var report5 = powerbi.embed(reportContainerFive, {{ type: 'report',accessToken: '{0}', embedUrl: '{3}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Storage Resources';
                                        document.title = 'Storage - Global CCS Institute';
                                    }}
                                    else if(id == 'legalAndReg'){{
                                        var report6 = powerbi.embed(reportContainerSix, {{ type: 'report',accessToken: '{0}', embedUrl: '{4}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Legal & Regulatory';
                                        document.title = 'Legal & Regulatory - Global CCS Institute';
                                    }}
                                    else if(id == 'facilityDetails'){{
                                        var report7 = powerbi.embed(reportContainerSeven, {{ type: 'report',accessToken: '{0}', embedUrl: '{5}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Facility Details';
                                        document.title = 'Facilities - Global CCS Institute';
                                    }}
                                    else if(id == 'facilityReport'){{
                                        var report8 = powerbi.embed(reportContainerEight, {{ type: 'report',accessToken: '{0}', embedUrl: '{6}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Facility Report';
                                        document.title = 'Facilities - Global CCS Institute';
                                    }}
                                    else if(id == 'climateChange'){{
                                        var report9 = powerbi.embed(reportContainerNine, {{ type: 'report',accessToken: '{0}', embedUrl: '{7}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Climate Change';
                                        document.title = 'Climate Change - Global CCS Institute';
                                    }}
                                    else if(id == 'inherentInterest'){{
                                        var report10 = powerbi.embed(reportContainerTen, {{ type: 'report',accessToken: '{0}', embedUrl: '{8}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Climate Change - Inherent Interest';
                                        document.title = 'Climate Change - Global CCS Institute';
                                    }}
                                    else if(id == 'ccsReady'){{
                                        var report10 = powerbi.embed(reportContainerEleven, {{ type: 'report',accessToken: '{0}', embedUrl: '{9}'}});
                                        document.getElementById('indexBreadcrum').innerHTML = 'Climate Change - CCS Readiness';
                                        document.title = 'Climate Change - Global CCS Institute';
                                    }}
                                    
                                    $this.tab('show');
                                    return false;
                                }});
                                " + GetSelectedTabSection(id, false) + @"
                            }});
                            
                        </script>
                        ",
                        rawTokenVal,    //0
                        reportArray[0], //1 - Policy Indicator Summary
                        reportArray[1], //2 - Storage Indicator
                        reportArray[2], //3 - Storage Resources
                        reportArray[3], //4 - Legal and Reg
                        reportArray[4], //5 - Facility Details
                        reportArray[5], //6 - Facility Report
                        reportArray[6], //7 - Climate Change
                        reportArray[7], //8 - Inherent Interest
                        reportArray[8]  //9 - CCS Ready
                        );

                        return Content(htmlElements);

                    }
                }
                catch (HttpOperationException ex)
                {
                    //Bad Request
                    return Content(ex.Response.Content);
                }

            }
            else //Unauthenticated.
            {
                return Content("null");
            }

        }

    }
}