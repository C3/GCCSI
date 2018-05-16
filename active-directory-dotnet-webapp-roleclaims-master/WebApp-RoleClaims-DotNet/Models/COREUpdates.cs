using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class COREUpdates
    {
        [Key]
        public int UpdateID { get; set; }

        [ForeignKey("AreaID")]
        public virtual CO2REAreas Area { get; set; }

        public int AreaID { get; set; }


        public string Title { get; set; }

        public string Description { get; set; }

        [DisplayName("Include on Homepage?")]
        public bool IncludeOnHome { get; set; }
        [DisplayName("Update Date/Time")]
        public DateTime UpdateDateTime { get; set; }
    }
}