using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_PolicyListOption
    {
        [Key]
        public int PolicyListOptionID { get; set; }

        [DisplayName("Name")]
        public string PolicyListOptionName { get; set; }

        public DateTime UpdateDateTime { get; set; }

        //Relational
        public virtual ICollection<PolicyListData> PolicyData { get; set; }

    }

}