using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class PolicyListData
    {
        public int PolicyListDataID { get; set; }

        [DisplayName("Country")]
        public int? CountryID { get; set; }

        [DisplayName("Policy List Option")]
        public int? PolicyListOptionID { get; set; }

        [DisplayName("Name")]
        public string PolicyName { get; set; }

        [DisplayName("Description")]
        public string PolicyDescription { get; set; }

        [DisplayName("URL")]
        public string PolicyURL { get; set; }

        [DisplayName("Status")]
        public int? PolicyListStatusID { get; set; }

        [DisplayName("Summary")]
        public string Summary { get; set; }

        [DisplayName("Notes")]
        public string Note { get; set; }

        [DisplayName("Year")]
        public int? YearID { get; set; }

        public DateTime UpdateDateTime { get; set; }

        //Virtual Classes - Relational Data
        public virtual RD_Country Country { get; set; }

        [DisplayName("Policy Option")]
        public virtual RD_PolicyListOption PolicyListOption { get; set; }

        [DisplayName("Policy Status")]
        public virtual RD_PolicyListStatus PolicyListStatus { get; set; }
        public virtual RD_Year Year { get; set; }
    }

}