using HFZMVC.Models.EntityFramework;
using HFZMVC.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HFZMVC.Models.PermitRequest
{
    public class CreateEditPermitRequestModel
    {
        public CreateEditPermitRequestModel()
        {
            company = new CompanyMaster();
            company.Address = "";
            company.CompanyName = "";
            company.EmailID = "";
            company.ID = 0;
            company.LicenseNumber = "";
            company.Name = "";
            company.PhoneNumber = "";
            company.Status = "active";
            company.VATNumber = "";

        }
        const string RegexSpecialCharactesAllowed = "^[a-zA-Z0-9 ,.!?-]+$";
        public decimal? servicefee { get; set; }
        public decimal? permitfees { get; set; }
        public decimal? rdfee { get; set; }
        public decimal? vat { get; set; }
        public decimal? grandtotal { get; set; }

        public CompanyMaster company { get; set; }
        public int ID { get; set; }
        [Required(ErrorMessage = "Total Weight / Volume is required")]
        [MaxLength(10, ErrorMessage = "Total Weight / Volume cannot be longer than 10 characters")]
        [MinLength(0)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Total Weight / Volume must be numeric")]
        public string TotalVolume { get; set; }
    public string ConsumedWeight { get; set; }
    public List<HttpPostedFileBase> Files { get; set; }
        [Required(ErrorMessage = "Waste category is required")]
        public int PermitTypeID { get; set; }
        [Required(ErrorMessage = "Waste type is required")]
        public int WasteTypeId { get; set; }
        [Required(ErrorMessage = "Waste Location is required")]
        [DataType(DataType.Text)]
        [StringLength(200, ErrorMessage = "Waste Location cannot be longer than 200 characters")]
        [Display(Name = "Waste Location")]
        public string facility_location { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Manager Name cannot be longer than 200 characters")]
        [Display(Name = "Manager Name")]
        public string ManagerName { get; set; }

        [Required(ErrorMessage = "Other Source of Sample is required")]
        [StringLength(200, ErrorMessage = "Other Source of Sample cannot be longer than 200 characters")]
        [Display(Name = "Other Source of Sample")]
        public string OtherSourceOfSample { get; set; }

        [Required(ErrorMessage = "Source Process is required")]
        [DataType(DataType.Text)]
        //[RegularExpression(RegexSpecialCharactesAllowed, ErrorMessage = "Special charactes not allowed")]
        [StringLength(500, ErrorMessage = "Source Process cannot be longer than 500 characters")]
        [Display(Name = "Source Process")]
        public string SourceProcess { get; set; }
        [Required(ErrorMessage = "Packaging/Storage is required")]
        [DataType(DataType.Text)]
        //[RegularExpression(RegexSpecialCharactesAllowed, ErrorMessage = "Special charactes not allowed")]
        [StringLength(500, ErrorMessage = "Packaging/Storage cannot be longer than 500 characters")]
        [Display(Name = "Storage")]
        public string Storage { get; set; }
        //[RegularExpression(RegexSpecialCharactesAllowed, ErrorMessage = "Special charactes not allowed")]
        [Display(Name = "Remarks")]
        [StringLength(500, ErrorMessage = "Additional Remarks cannot be longer than 500 characters")]
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Purpose Of Sampling is required")]
        public int? samplingpurpose { get; set; }
        [Required(ErrorMessage = "Sampling Method is required")]
        public int SamplingMethodID { get; set; }
        public Nullable<bool> IsSampling { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PermitTypeAmount { get; set; }
        public int CurrencyID { get; set; }

        [Required(ErrorMessage = "Disclaimer Name is required")]
        [RegularExpression(@"[A-Za-z ]*", ErrorMessage = "Use characters only")]
        [DataType(DataType.Text)]
        [StringLength(200, ErrorMessage = "Name cannot be longer than 200 characters")]
        [Display(Name = "Disclaimer Name")]
        public string DisclaimerName { get; set; }
        [Display(Name = "Disclaimer Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DisclaimerDate { get; set; }
        public Nullable<bool> IsPayment { get; set; }
        public string PaymentRemarks { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public int PackageCount { get; set; }
        public string QueryString { get; set; }
        public string ApplicantName { get; set; }
        public string RejectionReason { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> ValidTill { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public int LocationID { get; set; }
        public string SourceOfSampleMasterIDs { get; set; }

        public string FolderPath { get; set; }
        public int AmountTypeMasterID { get; set; }
        public string StatusCode { get; set; }
        [Required]
        [Display(Name = "Disclaimer")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree with the Disclaimer")]
        public bool IsDisclaimerCertified { get; set; }
        [Required(ErrorMessage = "Disclaimer is required")]
        [Display(Name = "Disclaimer")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree with the Terms and Conditions")]
        public bool IsIAgreeTermsAndCondition { get; set; }


        public string CurrencyCode { get; set; }

        public List<PurposeOfSamplingMaster> PurposeOfSamplingMaster { get; set; }
        // public List<DocumentTypeMaster> DocumentTypeMaster { get; set; }


        // public ICollection<AmountDetail> AmountDetails { get; set; }
        // public ICollection<PermitDocument> PermitDocuments { get; set; }

        public List<PermitTypeMaster> PermitTypeMaster { get; set; }

        public List<PermitTypeMaster> WasteTypeMaster { get; set; }

        //  public ICollection<PermitSourceOfSample> PermitSourceOfSamples { get; set; }

        public List<SourceOfSampleMaster> SourceOfSampleMaster { get; set; }
        public Nullable<long> RefNumber { get; set; }

        public HttpPostedFileBase TradeLicenseDocument { get; set; }
        public HttpPostedFileBase ProcessFlowChart { get; set; }
        public HttpPostedFileBase OnsiteWasteTreament { get; set; }

        public string TradeLicenseDocumentUploadedFromClient { get; set; }
        public string ProcessFlowChartUploadedFromClient { get; set; }
        public string OnsiteWasteTreamentUploadedFromClient { get; set; }

        public List<PermitWasteMapping> PermitWasteType { get; set; }
        public string UnitOfMeasure { get; set; }
        public string DestructionReason { get; set; }
        public decimal PackageWeight { get; internal set; }
        public int PackageType { get; set; }
        public string TotalWeight { get; set; }
        public int? samplesource { get; set; }
        public int WasteCategory { get; set; }
    public List<string> PermitImages { get; set; }
    public string MSDS { get; set; }
  }





}