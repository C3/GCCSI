using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using GCCSI_CO2RE.Models;

namespace GCCSI_CO2RE.DAL
{
    //public class GCCSIInitializer : System.Data.Entity.DropCreateDatabaseAlways<GCCSIContext>
    public class GCCSIInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<GCCSIContext>
    {
        protected override void Seed(GCCSIContext context)
        {

            //getPolicyStatusList().ForEach(r => context.RD_PolicyStatusSet.Add(r));
            //context.SaveChanges();

            //getYearList().ForEach(r => context.RD_YearDataSet.Add(r));
            //context.SaveChanges();

            //getRegionList().ForEach(r => context.RD_RegionSet.Add(r));
            //context.SaveChanges();

            //getCountryList().ForEach(r => context.RD_CountrySet.Add(r));
            //context.SaveChanges();

            //getStorageCriteriaList().ForEach(r => context.RD_StorageCriteriaSet.Add(r));
            //context.SaveChanges();

            //var dummyStorageData = new List<StorageData>
            //{
            //    new StorageData { StorageDataID=76, RegionID=1, CountryID = 3,
            //        ConventionalStoragePotential = true, RegionalPotential = 1, RegionalAssessment = 1,
            //    Dataset = 3, AssessmentMaturity = 2, Injection = 5, CommercialScaleInjection = 5, KnowledgeDissemination = 3,
            //    Summary = "Summ123", Note = "Note123", YearID = 1, UpdateDateTime = DateTime.Now}
            //};
            

            //dummyStorageData.ForEach(s => context.StorageDataSet.Add(s));
            context.SaveChanges();
        }


        protected List<RD_Country> getCountryList()
        {
            return new List<RD_Country>
            {
                new RD_Country { CountryID=1, CountryName="Afghanistan", RegionID=3, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=2, CountryName="Albania", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=3, CountryName="Algeria", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=4, CountryName="Argentina", RegionID=7, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=5, CountryName="Armenia", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=6, CountryName="Australia", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=7, CountryName="Austria", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=8, CountryName="Azerbaijan", RegionID=3, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=9, CountryName="Bahrain", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=10, CountryName="Bangladesh", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=11, CountryName="Belarus", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=12, CountryName="Belgium", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=13, CountryName="Benin", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=14, CountryName="Bolivia", RegionID=7, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=15, CountryName="Bosnia and Herzegovina", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=16, CountryName="Botswana", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=17, CountryName="Brazil", RegionID=7, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=18, CountryName="Brunei Darussalam", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=19, CountryName="Bulgaria", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=20, CountryName="Cambodia", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=21, CountryName="Cameroon", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=22, CountryName="Canada", RegionID=6, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=23, CountryName="Central African Republic", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=24, CountryName="Chad", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=25, CountryName="Chile", RegionID=7, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=26, CountryName="China", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=27, CountryName="Colombia", RegionID=7, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=28, CountryName="Congo", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=29, CountryName="Cote d'Ivoire", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=30, CountryName="Croatia", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=31, CountryName="Cyprus", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=32, CountryName="Czech Republic", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=33, CountryName="Democratic Republic of the Congo", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=34, CountryName="Denmark", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=35, CountryName="Djibouti", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=36, CountryName="Ecuador", RegionID=7, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=37, CountryName="Egypt", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=38, CountryName="El Salvador", RegionID=7, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=39, CountryName="Equatorial Guinea", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=40, CountryName="Eritrea", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=41, CountryName="Estonia", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=42, CountryName="Ethiopia", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=43, CountryName="European Union (28)", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=44, CountryName="Finland", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=45, CountryName="France", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=46, CountryName="Gabon", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=47, CountryName="Gambia", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=48, CountryName="Georgia", RegionID=3, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=49, CountryName="Germany", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=50, CountryName="Ghana", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=51, CountryName="Greece", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=52, CountryName="Greenland", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=53, CountryName="Guinea", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=54, CountryName="Guinea-Bissau", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=55, CountryName="Guyana", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=56, CountryName="Hong Kong", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=57, CountryName="Hungary", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=58, CountryName="Iceland", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=59, CountryName="India", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=60, CountryName="Indonesia", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=61, CountryName="Iran", RegionID=5, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=62, CountryName="Iraq", RegionID=5, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=63, CountryName="Ireland", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=64, CountryName="Israel", RegionID=5, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=65, CountryName="Italy", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=66, CountryName="Japan", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=67, CountryName="Jordan", RegionID=5, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=68, CountryName="Kazakhstan", RegionID=3, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=69, CountryName="Kenya", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=70, CountryName="Kuwait", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=71, CountryName="Kyrgyzstan", RegionID=5, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=72, CountryName="Laos", RegionID=3, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=73, CountryName="Latvia", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=74, CountryName="Lebanon", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=75, CountryName="Lesotho", RegionID=5, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=76, CountryName="Liberia", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=77, CountryName="Libya", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=78, CountryName="Lithuania", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=79, CountryName="Luxembourg", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=80, CountryName="Macedonia", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=81, CountryName="Madagascar", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=82, CountryName="Malawi", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=83, CountryName="Malaysia", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=84, CountryName="Mali", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=85, CountryName="Malta", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=86, CountryName="Mexico", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=87, CountryName="Mongolia", RegionID=6, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=88, CountryName="Morocco", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=89, CountryName="Mozambique", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=90, CountryName="Myanmar", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=91, CountryName="Namibia", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=92, CountryName="Netherlands", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=93, CountryName="New Zealand", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=94, CountryName="Nicaragua", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=95, CountryName="Niger", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=96, CountryName="Nigeria", RegionID=7, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=97, CountryName="Norway", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=98, CountryName="Oman", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=99, CountryName="Pakistan", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=100, CountryName="Paraguay", RegionID=5, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=101, CountryName="Peru", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=102, CountryName="Philippines", RegionID=7, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=103, CountryName="Poland", RegionID=7, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=104, CountryName="Portugal", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=105, CountryName="Qatar", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=106, CountryName="Republic of Korea", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=107, CountryName="Republic of Moldova", RegionID=5, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=108, CountryName="Romania", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=109, CountryName="Russian Federation", RegionID=3, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=110, CountryName="Rwanda", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=111, CountryName="Saudi Arabia", RegionID=5, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=112, CountryName="Senegal", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=113, CountryName="Serbia and Montenegro", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=114, CountryName="Singapore", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=115, CountryName="Slovakia", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=116, CountryName="Slovenia", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=117, CountryName="Somalia", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=118, CountryName="South Africa", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=119, CountryName="Spain", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=120, CountryName="Sri Lanka", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=121, CountryName="Suriname", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=122, CountryName="Swaziland", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=123, CountryName="Sweden", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=124, CountryName="Switzerland", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=125, CountryName="Syrian Arab Republic", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=126, CountryName="Tajikistan", RegionID=5, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=127, CountryName="Thailand", RegionID=3, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=128, CountryName="Timor-Leste", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=129, CountryName="Togo", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=130, CountryName="Trinidad and Tobago", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=131, CountryName="Tunisia", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=132, CountryName="Turkey", RegionID=7, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=133, CountryName="Turkmenistan", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=134, CountryName="Uganda", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=135, CountryName="Ukraine", RegionID=3, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=136, CountryName="United Arab Emirates", RegionID=5, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=137, CountryName="United Kingdom", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=138, CountryName="United Republic of Tanzania", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=139, CountryName="United States of America", RegionID=4, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=140, CountryName="Uruguay", RegionID=7, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=141, CountryName="Uzbekistan", RegionID=6, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=142, CountryName="Venezuela", RegionID=3, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=143, CountryName="Viet Nam", RegionID=7, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=144, CountryName="Western Sahara", RegionID=2, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=145, CountryName="World", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=146, CountryName="Yemen", RegionID=8, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=147, CountryName="Zambia", RegionID=5, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=148, CountryName="Zimbabwe", RegionID=1, UpdateDateTime=DateTime.Parse("3/04/2018 16:14")},
new RD_Country { CountryID=149, CountryName="Multiple", RegionID=2, UpdateDateTime=DateTime.Parse("5/04/2018 14:28")},
new RD_Country { CountryID=150, CountryName="Multiple", RegionID=4, UpdateDateTime=DateTime.Parse("5/04/2018 14:28")},
new RD_Country { CountryID=151, CountryName="Multiple", RegionID=6, UpdateDateTime=DateTime.Parse("5/04/2018 14:28")},
new RD_Country { CountryID=152, CountryName="Multiple", RegionID=8, UpdateDateTime=DateTime.Parse("5/04/2018 14:28")},
new RD_Country { CountryID=153, CountryName="Angola", RegionID=1, UpdateDateTime=DateTime.Parse("6/04/2018 9:24")},
new RD_Country { CountryID=154, CountryName="Sudan.South Sudan", RegionID=1, UpdateDateTime=DateTime.Parse("6/04/2018 9:24")}


            };
        }

        protected List<RD_Year> getYearList()
        {
            return new List<RD_Year>
            {
                new RD_Year { YearID=1, YearName="2010", YearStartDate=DateTime.Parse("1/01/2010 0:00"), YearEndDate=DateTime.Parse("31/12/2010 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=2, YearName="2011", YearStartDate=DateTime.Parse("1/01/2011 0:00"), YearEndDate=DateTime.Parse("31/12/2011 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=3, YearName="2012", YearStartDate=DateTime.Parse("1/01/2012 0:00"), YearEndDate=DateTime.Parse("31/12/2012 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=4, YearName="2013", YearStartDate=DateTime.Parse("1/01/2013 0:00"), YearEndDate=DateTime.Parse("31/12/2013 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=5, YearName="2014", YearStartDate=DateTime.Parse("1/01/2014 0:00"), YearEndDate=DateTime.Parse("31/12/2014 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=6, YearName="2015", YearStartDate=DateTime.Parse("1/01/2015 0:00"), YearEndDate=DateTime.Parse("31/12/2015 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=7, YearName="2016", YearStartDate=DateTime.Parse("1/01/2016 0:00"), YearEndDate=DateTime.Parse("31/12/2016 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=8, YearName="2017", YearStartDate=DateTime.Parse("1/01/2017 0:00"), YearEndDate=DateTime.Parse("31/12/2017 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=9, YearName="2018", YearStartDate=DateTime.Parse("1/01/2018 0:00"), YearEndDate=DateTime.Parse("31/12/2018 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=10, YearName="2019", YearStartDate=DateTime.Parse("1/01/2019 0:00"), YearEndDate=DateTime.Parse("31/12/2019 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=11, YearName="2020", YearStartDate=DateTime.Parse("1/01/2020 0:00"), YearEndDate=DateTime.Parse("31/12/2020 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=12, YearName="2021", YearStartDate=DateTime.Parse("1/01/2021 0:00"), YearEndDate=DateTime.Parse("31/12/2021 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=13, YearName="2022", YearStartDate=DateTime.Parse("1/01/2022 0:00"), YearEndDate=DateTime.Parse("31/12/2022 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=14, YearName="2023", YearStartDate=DateTime.Parse("1/01/2023 0:00"), YearEndDate=DateTime.Parse("31/12/2023 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=15, YearName="2024", YearStartDate=DateTime.Parse("1/01/2024 0:00"), YearEndDate=DateTime.Parse("31/12/2024 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=16, YearName="2025", YearStartDate=DateTime.Parse("1/01/2025 0:00"), YearEndDate=DateTime.Parse("31/12/2025 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=17, YearName="2026", YearStartDate=DateTime.Parse("1/01/2026 0:00"), YearEndDate=DateTime.Parse("31/12/2026 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=18, YearName="2027", YearStartDate=DateTime.Parse("1/01/2027 0:00"), YearEndDate=DateTime.Parse("31/12/2027 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=19, YearName="2028", YearStartDate=DateTime.Parse("1/01/2028 0:00"), YearEndDate=DateTime.Parse("31/12/2028 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=20, YearName="2029", YearStartDate=DateTime.Parse("1/01/2029 0:00"), YearEndDate=DateTime.Parse("31/12/2029 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") },
 new RD_Year { YearID=21, YearName="2030", YearStartDate=DateTime.Parse("1/01/2030 0:00"), YearEndDate=DateTime.Parse("31/12/2030 0:00"), UpdateDateTime=DateTime.Parse("4/04/2018 15:49") }
 };
        }

        protected List<RD_Region> getRegionList()
        {
            return new List<RD_Region>
            {
                  new RD_Region { RegionID=1, RegionName="AFRICA", UpdateDateTime=DateTime.Parse("3/04/2018 16:14") },
 new RD_Region { RegionID=2, RegionName="ASIA PACIFIC", UpdateDateTime=DateTime.Parse("3/04/2018 16:14") },
 new RD_Region { RegionID=3, RegionName="EURASIA", UpdateDateTime=DateTime.Parse("3/04/2018 16:14") },
 new RD_Region { RegionID=4, RegionName="EUROPE", UpdateDateTime=DateTime.Parse("3/04/2018 16:14") },
 new RD_Region { RegionID=5, RegionName="MIDDLE EAST", UpdateDateTime=DateTime.Parse("3/04/2018 16:14") },
 new RD_Region { RegionID=6, RegionName="NORTH AMERICA", UpdateDateTime=DateTime.Parse("3/04/2018 16:14") },
 new RD_Region { RegionID=7, RegionName="SOUTH AMERICA", UpdateDateTime=DateTime.Parse("3/04/2018 16:14") },
 new RD_Region { RegionID=8, RegionName="WORLD", UpdateDateTime=DateTime.Parse("3/04/2018 16:14") }


            };
        }

        protected List<RD_StorageCriteria> getStorageCriteriaList()
        {
            return new List<RD_StorageCriteria>()
            {
                new RD_StorageCriteria { StorageCriteriaID=1, StorageCriteriaCode="A", StorageCriteriaValue=10, UpdateDateTime=DateTime.Parse("4/04/2018 19:23") },
                new RD_StorageCriteria { StorageCriteriaID=2, StorageCriteriaCode="B", StorageCriteriaValue=7.5, UpdateDateTime=DateTime.Parse("4/04/2018 19:23") },
                new RD_StorageCriteria { StorageCriteriaID=3, StorageCriteriaCode="C", StorageCriteriaValue=5, UpdateDateTime=DateTime.Parse("4/04/2018 19:23") },
                new RD_StorageCriteria { StorageCriteriaID=4, StorageCriteriaCode="D", StorageCriteriaValue=2.5, UpdateDateTime=DateTime.Parse("4/04/2018 19:23") },
                new RD_StorageCriteria { StorageCriteriaID=5, StorageCriteriaCode="E", StorageCriteriaValue=0, UpdateDateTime=DateTime.Parse("4/04/2018 19:23") },
                new RD_StorageCriteria { StorageCriteriaID=6, StorageCriteriaCode="No Storage Potential", StorageCriteriaValue=0, UpdateDateTime=DateTime.Parse("4/04/2018 19:23") },
                new RD_StorageCriteria { StorageCriteriaID=7, StorageCriteriaCode="None", StorageCriteriaValue=0, UpdateDateTime=DateTime.Parse("4/04/2018 19:23") }

            };
        }

        //protected List<RD_PolicyStatus> getPolicyStatusList()
        //{
        //    return new List<RD_PolicyStatus>()
        //    {
        //        new RD_PolicyStatus { PolicyStatusID = 1, PolicyStatusName = "Discussed", PolicyStatusScore =0.5, UpdateDateTime = DateTime.Parse("4/04/2018 19:20")},
        //        new RD_PolicyStatus { PolicyStatusID = 2, PolicyStatusName = "Moderate", PolicyStatusScore =3, UpdateDateTime = DateTime.Parse("4/04/2018 19:20")},
        //        new RD_PolicyStatus { PolicyStatusID = 3, PolicyStatusName = "No", PolicyStatusScore =0, UpdateDateTime = DateTime.Parse("4/04/2018 19:20")},
        //        new RD_PolicyStatus { PolicyStatusID = 4, PolicyStatusName = "Rejected", PolicyStatusScore =-1, UpdateDateTime = DateTime.Parse("4/04/2018 19:20")},
        //        new RD_PolicyStatus { PolicyStatusID = 5, PolicyStatusName = "Strong", PolicyStatusScore =5, UpdateDateTime = DateTime.Parse("4/04/2018 19:20")},
        //        new RD_PolicyStatus { PolicyStatusID = 6, PolicyStatusName = "Weak", PolicyStatusScore =1, UpdateDateTime = DateTime.Parse("4/04/2018 19:20")},
        //        new RD_PolicyStatus { PolicyStatusID = 7, PolicyStatusName = "Yes", PolicyStatusScore =5, UpdateDateTime = DateTime.Parse("4/04/2018 19:20")}
        //    };
        //}
    }
}