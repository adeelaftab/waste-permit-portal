using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFZMVC.Models.PermitRequest
{
    public class FDCRequestDetailModel : CreateEditFDCRequest
    {
        public string WasteCategoryName { get; set; }
        public string WasteTypeName { get; set; }
        public bool IsFDCApproval { get; set; }
        public int? PurposeOfSampling { get; set; }
        public string PurposeOfSamplingName { get; set; }
        public int? SourceSampling { get; set; }
        public string SourceSamplingName { get; set; }
        public string OtherSource { get; set; }
    public string WBRemarks { get; set; }
    public bool IsPaymentApproval { get; set; }

        public string ItemNames { get; set; }
        public bool IsSamplingRequest { get; set; }

        public List<TimelineViewModel> History { get; set; }
        public String InvoiceId { get; set; }
        public string PaymentMethod { get; set; }


    }
}