using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GCCSI_CO2RE.Models
{
    public class RD_Country
    {
        [Key]
        public int CountryID { get; set; }

        [DisplayName("Country Name")]
        public string CountryName { get; set; }

        public int? RegionID { get; set; }

        public DateTime UpdateDateTime { get; set; }

        public virtual RD_Region Region { get; set; }

        public virtual ICollection<StorageData> StorageData { get; set; }

        public virtual ICollection<PolicyListData> PolicyData { get; set; }

        public virtual ICollection<ConsolidatedPolicyIndex> PolicyIndex { get; set; }

    }

}