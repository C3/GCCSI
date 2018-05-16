using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace GCCSI_CO2RE.Models
{
    public class RD_Year
    {
        [Key]
        public int YearID { get; set; }
        [DisplayName("Year")]
        public string YearName { get; set; }

        [DisplayName("Year Start Date")]
        [DataType(DataType.Date)]
        public DateTime YearStartDate { get; set; }

        [DisplayName("Year End Date")]
        [DataType(DataType.Date)]
        public DateTime YearEndDate { get; set; }

        [DisplayName("Year Has Data?")]
        public bool YearHasData { get; set; }

        public DateTime UpdateDateTime { get; set; }

        public virtual ICollection<StorageData> StorageData { get; set; }

        public virtual ICollection<PolicyListData> PolicyData { get; set; }

        public virtual ICollection<ConsolidatedPolicyIndex> PolicyIndex { get; set; }

    }

}