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
    
    public partial class AccountTransaction
    {
        public int ID { get; set; }
        public Nullable<int> InoviceID { get; set; }
        public int AccountID { get; set; }
        public string PaymentMethod { get; set; }
        public string ReferenceNo { get; set; }
        public string ReferenceFIle { get; set; }
        public string TransactionType { get; set; }
        public Nullable<int> PermitID { get; set; }
        public Nullable<decimal> AmountPayable { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<System.DateTime> EnteredOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual PermitRequest PermitRequest { get; set; }
    }
}
