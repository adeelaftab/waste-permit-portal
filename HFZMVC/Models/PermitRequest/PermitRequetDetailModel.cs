using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFZMVC.Models.PermitRequest
{
	public class PermitRequetDetailModel:CreateEditPermitRequestModel
	{
    public string transporterDetails;

    public PermitRequetDetailModel() {
			PermitImages = new List<string>();
    }
		public string WasteCategoryName { get; set; }
    public string PermitRefNo { get; set; }
    public string WBRemarks { get; set; }
    public string WasteTypeName { get; set; }
		public int WasteType { get; set; }
		public int? PurposeOfSampling { get; set; }
		public string PurposeOfSamplingName { get; set; }
		public int? SourceSampling { get; set; }
		public string SourceSamplingName { get; set; }
		public string OtherSource { get; set; }
		public string WasteDescription { get; set; }
		public string DisposalFrequency { get; set; }
		public string transporter { get; set; }

		public bool IsPermitApproval { get; set; }
		public bool IsHazardRequest { get; set; }
		public bool IsSamplingRequest { get; set; }
		public bool IsPaymentApproval { get; set; }
		public bool IsHazardApproval { get; set; }
    public bool IsHazardousWaste { get; set; }

    public List<TimelineViewModel> History { get; set; }

		public String InvoiceId { get; set; }
		public string PaymentMethod { get; set; }
		public int SamplingId { get; set; }
    public string HazardRemarks { get; set; }
    public string HazardAppNo { get; set; }



  }

	
}