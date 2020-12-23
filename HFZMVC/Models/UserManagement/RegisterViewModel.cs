using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace HFZMVC.Models.UserManagement
{
  public class RegisterViewModel
  {
    public int ID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string EmailID { get; set; }
    public string PhoneNumber { get; set; }
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public string TradelicenseNo { get; set; }
    public string TradeLicenseFile { get; set; }// the path to the file
    public string VATLicenseFile { get; set; }
    public string VatlicenseNo { get; set; }
    public string ProcessFlowFilePath { get; set; }
    public string WasteTreatementFilePath { get; set; }
    public bool Status { get; set; }
    public HttpPostedFileBase CompanyLicenseFile { get; set; }
    public HttpPostedFileBase VatCertificateFile { get; set; }
    public HttpPostedFileBase ProcessFlowFile { get; set; }
    public HttpPostedFileBase WasteTreatementFile { get; set; }
    public string Address2 { get; set; }
    public  decimal AmountToReturn { get; set; }
    public string SAPCustomerId { get;  set; }
  }

  public class forgetPasswordViewModel
  {
    public string EmailID { get; set; }
  }
  public class resetPasswordViewModel
  {
    public int ID { get; set; }
    public string password { get; set; }
    public string Email { get; set; }
  }
  public class registerdatavalidation
  {
    public string email { get; set; }
    public string UserName { get; set; }
    public string type { get; set; }
  }
}
