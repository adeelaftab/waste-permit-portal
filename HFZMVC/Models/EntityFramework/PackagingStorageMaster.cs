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
    
    public partial class PackagingStorageMaster
    {
        public int ID { get; set; }
        public string PackagingStorageMaster1 { get; set; }
        public Nullable<bool> status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
