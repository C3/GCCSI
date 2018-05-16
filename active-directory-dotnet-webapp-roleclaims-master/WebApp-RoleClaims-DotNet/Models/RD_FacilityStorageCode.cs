using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_FacilityStorageCode
    {
        [Key]
        public int FacilityStorageCodeID { get; set; }
        [DisplayName("Storage Code")]
        public string FacilityStorageCodeName { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}