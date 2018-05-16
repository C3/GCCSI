using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_StorageCriteria
    {
        [Key]
        public int StorageCriteriaID { get; set; }

        public string StorageCriteriaCode { get; set; }

        public double StorageCriteriaValue { get; set; }

        public DateTime UpdateDateTime { get; set; }

        public virtual ICollection<StorageData> StorageData { get; set; }
    }
}