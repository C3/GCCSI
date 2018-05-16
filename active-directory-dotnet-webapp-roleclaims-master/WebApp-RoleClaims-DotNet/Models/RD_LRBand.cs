using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_LRBand
    {
        [Key]
        public int LRBandID { get; set; }

        public string BandName { get; set; }

        public string BandValue { get; set; }

        public DateTime UpdateDateTime { get; set; }

    }

}