using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_StorageWeight
    {
        [Key]
        public int StorageWeightID { get; set; }

        public string StorageWeightName { get; set; }

        public decimal StorageWeightScore { get; set; }

        public DateTime UpdateDateTime { get; set; }
    }
}