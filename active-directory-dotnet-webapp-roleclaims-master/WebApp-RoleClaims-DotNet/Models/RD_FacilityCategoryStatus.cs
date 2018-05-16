using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_FacilityCategoryStatus
    {
        [Key]
        public int FacilityCategoryStatusID { get; set; }

        public int FacilityCategoryID { get; set; }

        public int FacilityStatusID { get; set; }

        public DateTime UpdateDateTime { get; set; }
    }
}