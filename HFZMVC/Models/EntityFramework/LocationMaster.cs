//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HFZMVC.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class LocationMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LocationMaster()
        {
            this.FDCPermits = new HashSet<FDCPermit>();
        }
    
        public int ID { get; set; }
        public string PrefixCode { get; set; }
        public string Location { get; set; }
        public Nullable<bool> status { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FDCPermit> FDCPermits { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
