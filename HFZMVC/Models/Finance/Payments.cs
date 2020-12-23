using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFZMVC.Models.Finance
{
    public class Payments
    {
        public Nullable<decimal> TotalAmount { get; set; }
    }
    public class CardPayments
    {
        public int PermitID { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string SecurityCode { get; set; }
        public string ExpiryDate { get; set; }
        public string GrandTotal { get; set; }

    }
    public class ManualPayments
    {
        public int PermitID { get; set; }
        public HttpPostedFileBase ReferenceFile { get; set; }
        public string ReferenceNo { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Remarks { get; set; }
        public decimal GrandTotal { get; set; }

    }
    public class InvoiceViewModel
    {
        public int ID { get; set; }
        public string InvoiceNo { get; set; }
        public bool PermitType { get; set; }
        public string PermitCategory { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public int PermitID { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Remarks { get; set; }
    }
    public class TransactionViewModel
    {
        public int ID { get; set; }
        public string InvoiceNo { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal? AmountPayable { get; set; }
        public decimal? AmountPaid { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionType { get; set; }
    }
    public class InvoiceReceipt
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CardNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string InvoiceNo { get; set; }
        public decimal? PermitFees { get; set; }
        public decimal? ServicesFees { get; set; }
        public decimal? RnDFees { get; set; }
        public decimal? Vat { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? DueDate { get; set; }
        public string PaymentMethod { get; set; }
        public string PermitCategory { get; set; }
        public string Remarks { get; set; }
    }

    public class SAPOrderData
    {
        public int PermitID { get; set; }
        public string SAPCustomerID { get; set; }
        public string ProductCode { get; set; }
        public int OrderQty { get; set; }
        public string Notes { get; set; }
        public decimal Total { get; set; }
        public decimal PermitFees { get; set; }
        public decimal ServiceFees { get; set; }
        public decimal RDFees { get; set; }
        public decimal VAT { get; set; }
    }
}