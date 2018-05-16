using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_FacilityIndustry
    {
        [Key]
        public int FacilityIndustryID { get; set; }
        [DisplayName("Industry")]
        public string FacilityIndustryName { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}