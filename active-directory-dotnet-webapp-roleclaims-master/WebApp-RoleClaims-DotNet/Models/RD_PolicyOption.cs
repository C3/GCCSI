using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_PolicyOption
    {
        [Key]
        public int PolicyOptionID { get; set; }

        public string PolicyOptionName { get; set; }

        public int? PolicyTypeID { get; set; }

        public DateTime UpdateDateTime { get; set; }

        //Relational Table Data
        public virtual RD_PolicyType PolicyType { get; set; }
    }

}