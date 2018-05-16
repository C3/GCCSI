using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_LeadershipStatus
    {
        [Key]
        public int LeadershipStatusID { get; set; }
        
        public string LeadershipStatusName { get; set; }

        public decimal LeadershipStatusScore { get; set; }

        public DateTime UpdateDateTime { get; set; }
    }
}