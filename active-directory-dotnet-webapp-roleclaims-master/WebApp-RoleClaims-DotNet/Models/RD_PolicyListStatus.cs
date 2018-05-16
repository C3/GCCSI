using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_PolicyListStatus
    {
        [Key]
        public int PolicyListStatusID { get; set; }

        [DisplayName("Status")]
        public string PolicyListStatusName { get; set; }

        public DateTime UpdateDateTime { get; set; }


        //Relational
        public virtual ICollection<PolicyListData> PolicyData { get; set; }

    }

}