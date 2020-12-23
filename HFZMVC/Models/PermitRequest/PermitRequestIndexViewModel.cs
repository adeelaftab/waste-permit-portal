using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFZMVC.Models.PermitRequest
{
	public class PermitRequestIndexViewModel
	{
		public int ID { get; set; }
		public DateTime ApplyDate { get; set; }
		public string PermitType { get; set; }
		public string Category { get; set; }
    public int? PermitreferenceiD { get; set; }
    public decimal? TotalAmount { get; set; }
		public string Status { get; set; }
		public DateTime ValidTill { get; set; }
		public string Notes { get; set; }
    public bool IsSampling { get; set; } = false;
		public int permitID { get; set; }
		public int UserID { get; set; }
	public int StatusId { get; set; }
		public string CompanyName { get; set; }
    public decimal TotalWeight { get; set; }
    public decimal ConsumedWeight { get; set; }
  }
	public class FDCPermitPermitView
	{
		public int ID { get; set; }
		
		public string PermitType { get; set; }
		public string TotalWeight { get; set; }
		public string ApplyDate { get; set; }
		public bool IsSamplingRequired { get; set; }
		public string PurposeOfSampling { get; set; }
		public string SourceOfSample { get; set; }
		public string Othersource { get; set; }
		public string IsPaymentDone { get; set; }
		public string RejectionReason { get; set; }
		public string Status { get; set; }
		public int StatusId { get; set; }
		public string IssueBy { get; set; }
		public string ValidTill { get; set; }
		public string ApproveBy { get; set; }
		public string CompanyName { get; set; }
		public string WasteGenerationDescription { get; set; }
		public string FrequencyOfDisposal { get; set; }
		public string Transporter { get; set; }
		public string remarks { get; set; }
		public int UserID { get; set; }

		// FDC Individual list
		public decimal? TotalAmount { get; set; }
		public string ApplyLocation { get; set; }
		public DateTime ProductionDate { get; set; }
		public DateTime ExpiryDate { get; set; }
		public string PackageType { get; set; }
		public string PackageCount { get; set; }
		public string PackageWeight { get; set; }
		public string DestructionReason { get; set; }
		public string InspectionForm { get; set; }
		public string FacilityLocation { get; set; }
		public string CountryOfOrigin { get; set; }
		public string itemslist { get; set; }

	}
	public class NormalPermitPermitView
	{
		public int ID { get; set; }
		public string PermitType { get; set; }
		public string TotalWeight { get; set; }
		public string ApplyDate { get; set; }
		public bool IsSamplingRequired { get; set; }
		public string PurposeOfSampling { get; set; }
		public string SourceOfSample { get; set; }
		public string Othersource { get; set; }
		public string IsPaymentDone { get; set; }
		public string RejectionReason { get; set; }
		public string Status { get; set; }
		public string IssueBy { get; set; }
		public string ValidTill { get; set; }
		public string ApproveBy { get; set; }
		public string CompanyName { get; set; }
		public string WasteGenerationDescription { get; set; }
		public string FrequencyOfDisposal { get; set; }
		public string Transporter { get; set; }
		public string remarks { get; set; }

		// Normal Individual list
		public decimal TotalAmount { get; set; }
		public string WasteCategory { get; set; }
		public string WasteType { get; set; }
		public string WasteLocation { get; set; }
		public string SourceProcess { get; set; }
		public string PackagingStorage { get; set; }
		public string NormalRemarks { get; set; }

	}
	public class HazardCertificateModel
	{
		public int ID { get; set; }
    public int PermitId { get; set; }
    public string WasteDescription { get; set; }
		public string DisposalFrequency { get; set; }
		public string transporter { get; set; }
    public string transporterDetails { get; set; }
    public string TypeOfIndustry { get; set; }
    public int UserID { get; set; }
		public List<HazardItems> hazard { get; set; }
    public List<string> HPhotos { get; set; }
    public List<string> HMSDS { get; set; }
  }
	public class HazardItems
	{
		public int ID { get; set; }
		public string name { get; set; }
		public string quantity { get; set; }
		public string quantityItem { get; set; }
		public string state { get; set; }
		public string ContainerType { get; set; }
		public bool IsDeleted { get; set; } = false;
  }
	
	public class FileModel
  {
    public int Id { get; set; }
    public string FilePath { get; set; }
		public bool IsDeleted { get; set; } = false;
  }
	public class HazardCertificateEditModel
	{
		public int ID { get; set; }
		public int PermitId { get; set; }
		public string WasteDescription { get; set; }
		public string DisposalFrequency { get; set; }
		public string transporter { get; set; }
		public string transporterDetails { get; set; }
		public string TypeOfIndustry { get; set; }
		public int UserID { get; set; }
		public List<HazardItems> hazard { get; set; }
		public List<FileModel> HPhotos { get; set; }
		public List<FileModel> HMSDS { get; set; }
    public string HazardSAPNo { get; set; }
  }

	public class HazardCertificateModel_Certificate:HazardCertificateEditModel {

    public HFZMVC.Models.EntityFramework.User Company { get; set; }
  }
	public class HazardSAPModel {
    public string WDC_APPNO { get; set; }
    public DateTime WDC_APPDT { get; set; }
    public string KUNNR { get; set; }
    public string WDC_NAME { get; set; }
    public string WDC_MOBILE { get; set; }
    public string WDC_EMAIL { get; set; }
    public string WDC_VALIDIY { get; set; }
    public string MSDS { get; set; }
    public string PICTUE { get; set; }
    public string APPLICATION { get; set; }

  }
}