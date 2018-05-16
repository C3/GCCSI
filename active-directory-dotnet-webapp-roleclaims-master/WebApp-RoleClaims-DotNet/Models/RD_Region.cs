using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_Region
    {
        [Key]
        public int RegionID { get; set; }
        [DisplayName("Region Name")]
        public string RegionName { get; set; }

        public DateTime UpdateDateTime { get; set; }

        public virtual ICollection<StorageData> StorageData { get; set; }
        public virtual ICollection<RD_Country> Country { get; set; }
        public virtual ICollection<ConsolidatedPolicyIndex> PolicyIndex { get; set; }

    }
}