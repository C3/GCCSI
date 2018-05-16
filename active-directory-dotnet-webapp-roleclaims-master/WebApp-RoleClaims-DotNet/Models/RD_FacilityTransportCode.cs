using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_FacilityTransportCode
    {
        [Key]
        public int FacilityTransportCodeID { get; set; }
        [DisplayName("Transport Code")]
        public string FacilityTransportCodeName { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}