using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_FacilityStatus
    {
        [Key]
        public int FacilityStatusID { get; set; }
        [DisplayName("Status")]
        public string FacilityStatusName { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}