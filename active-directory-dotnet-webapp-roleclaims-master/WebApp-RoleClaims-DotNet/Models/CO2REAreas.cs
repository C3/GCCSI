using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class CO2REAreas
    {
        [Key]
        public int AreaID { get; set; }

        [DisplayName("CO2RE Area")]
        public string AreaName { get; set; }
    }
}