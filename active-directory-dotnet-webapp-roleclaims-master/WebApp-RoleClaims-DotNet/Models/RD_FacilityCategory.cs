using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_FacilityCategory
    {
        [Key]
        public int FacilityCategoryID { get; set; }
        [DisplayName("Category")]
        public string FacilityCategoryName { get; set; }

        public DateTime UpdateDateTime { get; set; }

        public int CustomSortOrder { get; set; }
    }

}