using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_FacilityCaptureType
    {
        [Key]
        public int FacilityCaptureTypeID { get; set; }
        [DisplayName("Capture Type")]
        public string FacilityCaptureTypeName { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}