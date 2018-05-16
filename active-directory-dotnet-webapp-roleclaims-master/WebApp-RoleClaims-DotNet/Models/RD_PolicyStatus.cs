using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_PolicyStatus
    {
        [Key]
        public int PolicyStatusID { get; set; }

        public string PolicyStatusName { get; set; }

        public double PolicyStatusScore { get; set; }

        public DateTime UpdateDateTime { get; set; }

        public virtual ICollection<ConsolidatedPolicyIndex> PolicyIndex { get; set; }
    }

}