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
    
    public partial class PermitItem
    {
        public int ID { get; set; }
        public int PermitID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<System.DateTime> EnteredOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string Item { get; set; }
    
        public virtual MasterItem MasterItem { get; set; }
        public virtual PermitRequest PermitRequest { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
