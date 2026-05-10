using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Location.DAL;

namespace Location.Models
{    public class BauxRenewalVM
    {
        public int OriginalLeaseId { get; set; }
        public Baux NewLease { get; set; }
        public DateTime SuggestedStartDate { get; set; }
        public DateTime SuggestedEndDate { get; set; }
        public string NewLeaseNumber { get; set; }
        public bool CopyInclusions { get; set; }
        public bool KeepFinancialTerms { get; set; }
    }
}

