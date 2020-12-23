using HFZMVC.Helpers;
using HFZMVC.Models.EntityFramework;
using HFZMVC.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace HFZMVC.Controllers
{
  [HFZMVC.CustomBinders.HFZAuthorization]
  public class CompanyController : Controller
  {
    #region --------------------------Properties-------------------------------------
    WasteManageEntities db;
    #endregion

    // GET: Company
    public ActionResult Index() {
      using (db = new WasteManageEntities()) {

        string query = "Select ID, CompanyName, Name, PhoneNumber, EmailID, Address, LicenseNumber, VATNumber," +
             " CASE WHEN Status = 1 THEN 'Active' ELSE 'InActive' END AS Status from Users where CompanyName IS NOT NULL  order by CreatedOn desc";
        var Data = db.Database.SqlQuery<CompanyMaster>(query).ToList();
        return View(Data);
      }
    }

    public ActionResult View(int ID) {
      using (db = new WasteManageEntities()) {
        var user = db.Users.Where(x => x.ID == ID).FirstOrDefault();
        var adminuser = db.Users.Where(x => x.ID == 1).FirstOrDefault();
        string query = "Select pr.ID, st.statusName from dbo.PermitRequest pr join StatusMaster st on pr.StatusID = st.ID where UserID = " + ID + " order by ID desc OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY";
        var Data = db.Database.SqlQuery<Permitlist>(query).ToList();
        CompanyView companyview = new CompanyView();
        companyview.Users = user;
        companyview.Admindetail = adminuser;
        companyview.permitlist = Data;
        
        return View(companyview);
      }
    }
    public ActionResult Edit(int ID) {
      using (db = new WasteManageEntities()) {
        var Data = db.Users.Where(x => x.ID == ID).FirstOrDefault();
        var clientDocuments = db.ClientDocumentDetails.Where(e => e.ClientID == ID).ToList();
        foreach (var item in clientDocuments) {
          switch (item.DocTypeID) {
            case 1:
              ViewBag.processflow= item.DocumentPath;
              break;
            case 2:
              ViewBag.onsitetreatment = item.DocumentPath;
              break;

            default:
              break;
          }

        }
        return View(Data);
      }
    }

    [HttpGet]
    public new ActionResult Profile() {
      int ID = (int)Session[AppVariables.SessionUserId];
      using (db = new WasteManageEntities()) {
        var user = db.Users.Where(x => x.ID == ID).FirstOrDefault();
        user.Password = AppUtil.DecryptPassword(user.Password);
        if (user == null) {
          return RedirectToAction("Index");
        }

        var Viewmodel =
   new RegisterViewModel() {
     Address = user.Address,
     Address2 = user.Address2,
     CompanyName = user.CompanyName,
     EmailID = user.EmailID,
     ID = user.ID,
     Name = user.Name,
     Password = user.Password,
     PhoneNumber = user.PhoneNumber,
     Status = user.Status.HasValue ? user.Status.Value : false,
     TradelicenseNo = user.LicenseNumber,
     Username = user.UserName,
     VatlicenseNo = user.VATNumber,
     TradeLicenseFile = user.TradeLicenseFile,
     VATLicenseFile = user.VATLicenseFile

   };
        var documents =
          db.ClientDocumentDetails.Where(e => e.ClientID
          == user.ID).ToList();
        foreach (var item in documents) {
          switch (item.DocTypeID) {
            case 1:
              Viewmodel.ProcessFlowFilePath =
                 item.DocumentPath;
              break;
            case 2:
              Viewmodel.WasteTreatementFilePath = item.DocumentPath;
              break;
            default:
              break;
          }
        }
        return View(Viewmodel);
      }
    }

    public ActionResult UpdateProfile(RegisterViewModel RegisterData) {
      long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
      string CompanyLicensedoc = "";
      string VatCertificatedoc = "";
      string password = "";
      if (RegisterData?.VatCertificateFile?.ContentLength > 0) {
        string _FileName = Path.GetFileName(RegisterData.VatCertificateFile.FileName);
        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), "vat" + timeStamp + "_" + _FileName);
        RegisterData.VatCertificateFile.SaveAs(_path);
        VatCertificatedoc = "vat" + timeStamp + "_" + _FileName;

      }
      if (RegisterData?.CompanyLicenseFile?.ContentLength > 0) {
        string _FileName = Path.GetFileName(RegisterData.CompanyLicenseFile.FileName);
        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), "Lic" + timeStamp + "_" + _FileName);
        RegisterData.CompanyLicenseFile.SaveAs(_path);
        CompanyLicensedoc = "Lic" + timeStamp + "_" + _FileName;
      }
      if (RegisterData?.ProcessFlowFile?.ContentLength > 0) {
        string _FileName = Path.GetFileName(RegisterData.ProcessFlowFile.FileName);
        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), "Process" + timeStamp + "_" + _FileName);
        RegisterData.ProcessFlowFile.SaveAs(_path);
        RegisterData.ProcessFlowFilePath = "Processs" + timeStamp + "_" + _FileName;
      }
      if (RegisterData?.WasteTreatementFile?.ContentLength > 0) {
        string _FileName = Path.GetFileName(RegisterData.WasteTreatementFile.FileName);
        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), "WasteTreat" + timeStamp + "_" + _FileName);
        RegisterData.WasteTreatementFile.SaveAs(_path);
        RegisterData.WasteTreatementFilePath = "WasteTreat" + timeStamp + "_" + _FileName;
      }
      if (!string.IsNullOrEmpty(RegisterData.Password)) {
        password = AppUtil.Encrypt(RegisterData.Password);
      }
      using (TransactionScope scope = new TransactionScope()) {
        try {
          using (db = new WasteManageEntities()) {
            var user = db.Users.Where(x => x.ID == RegisterData.ID).FirstOrDefault();

            user.Name = RegisterData.Name;
            user.PhoneNumber = RegisterData.PhoneNumber;
            user.CompanyName = RegisterData.CompanyName;
            user.EmailID = RegisterData.EmailID;
            user.LicenseNumber = RegisterData.TradelicenseNo;
            user.VATNumber = RegisterData.VatlicenseNo;
            if (CompanyLicensedoc != "") {
              user.TradeLicenseFile = CompanyLicensedoc;
            }
            if (VatCertificatedoc != "") {
              user.VATLicenseFile = VatCertificatedoc;
            }
            if (password != "") {
              user.Password = password;
            }
            user.Address = RegisterData.Address;
            user.Address2 = RegisterData.Address2;
            user.UpdatedBy = (int)Session[AppVariables.SessionUserId]; //later
            user.UpdatedOn = DateTime.Now;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            if (!String.IsNullOrEmpty(RegisterData.ProcessFlowFilePath)) {
              var filePathinDB
               = db.ClientDocumentDetails.Where(e => e.ClientID == user.ID && e.DocTypeID == 1).FirstOrDefault();
              if (filePathinDB == null) {
                db.ClientDocumentDetails.Add(
                  new ClientDocumentDetail() {
                    ClientID = user.ID,
                    UpdatedBy = AppUtil.checkLogin(),
                    CreatedBy = AppUtil.checkLogin(),
                    UpdatedOn = DateTime.Now,
                    DocumentPath = RegisterData.ProcessFlowFilePath,
                    CreatedOn = DateTime.Now,
                    DocTypeID = 1
                  });


              } else {
                filePathinDB.DocumentPath = RegisterData.ProcessFlowFilePath;
                filePathinDB.UpdatedBy = AppUtil.checkLogin();
                filePathinDB.UpdatedOn = DateTime.Now;
                db.Entry(filePathinDB).State = EntityState.Modified;
              }

              db.SaveChanges();
            }

            if (!String.IsNullOrEmpty(RegisterData.WasteTreatementFilePath)) {
              var filePathinDB
               = db.ClientDocumentDetails.Where(e => e.ClientID == user.ID && e.DocTypeID == 2).FirstOrDefault();
              if (filePathinDB == null) {
                db.ClientDocumentDetails.Add(
                  new ClientDocumentDetail() {
                    ClientID = user.ID,
                    UpdatedBy = AppUtil.checkLogin(),
                    CreatedBy = AppUtil.checkLogin(),
                    UpdatedOn = DateTime.Now,
                    DocumentPath = RegisterData.WasteTreatementFilePath,
                    CreatedOn = DateTime.Now,
                    DocTypeID = 2
                  });


              } else {
                filePathinDB.DocumentPath = RegisterData.WasteTreatementFilePath;
                filePathinDB.UpdatedBy = AppUtil.checkLogin();
                filePathinDB.UpdatedOn = DateTime.Now;
                db.Entry(filePathinDB).State = EntityState.Modified;
              }

              db.SaveChanges();
            }
            scope.Complete();

          }
        } catch (Exception ex) {
          scope.Dispose();

          throw ex;
        }
      }

      return RedirectToAction("Profile");
    }
    public ActionResult CompanyUpdate(RegisterViewModel RegisterData) {
      long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
      string CompanyLicensedoc = "";
      string VatCertificatedoc = "";
      if (RegisterData?.VatCertificateFile?.ContentLength > 0) {
        string _FileName = Path.GetFileName(RegisterData.VatCertificateFile.FileName);
        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), "vat" + timeStamp + "_" + _FileName);
        RegisterData.VatCertificateFile.SaveAs(_path);
        VatCertificatedoc = "vat" + timeStamp + "_" + _FileName;

      }
      if (RegisterData?.CompanyLicenseFile?.ContentLength > 0) {
        string _FileName = Path.GetFileName(RegisterData.CompanyLicenseFile.FileName);
        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), "Lic" + timeStamp + "_" + _FileName);
        RegisterData.CompanyLicenseFile.SaveAs(_path);
        CompanyLicensedoc = "Lic" + timeStamp + "_" + _FileName;
      }

      using (TransactionScope scope = new TransactionScope()) {
        try {
          using (db = new WasteManageEntities()) {
            var user = db.Users.Where(x => x.ID == RegisterData.ID).FirstOrDefault();

            user.Name = RegisterData.Name;
            user.PhoneNumber = RegisterData.PhoneNumber;
            user.EmailID = RegisterData.EmailID;
            user.CompanyName = RegisterData.CompanyName;
            user.LicenseNumber = RegisterData.TradelicenseNo;
            user.VATNumber = RegisterData.VatlicenseNo;
            if (CompanyLicensedoc != "") {
              user.TradeLicenseFile = CompanyLicensedoc;
            }
            if (VatCertificatedoc != "") {
              user.VATLicenseFile = VatCertificatedoc;
            }
            user.Address = RegisterData.Address;
            user.Address2 = RegisterData.Address2;
            user.Status = (RegisterData.Status == true) ? true : false;
            user.UpdatedBy = (int)Session[AppVariables.SessionUserId]; //later
            user.UpdatedOn = DateTime.Now;
            if (String.IsNullOrEmpty( user.SAPCustomerId)) {
              if (!String.IsNullOrEmpty(RegisterData.SAPCustomerId)) {
                user.SAPCustomerId = RegisterData.SAPCustomerId;
              }
            }
            db.Entry(user).State = EntityState.Modified;
            
            if (RegisterData.AmountToReturn!=0) {
              var account =  db.Accounts.Where(e => e.UserID == RegisterData.ID).FirstOrDefault();
              account.Balance += RegisterData.AmountToReturn;
              account.UpdatedBy = AppUtil.checkLogin();
              account.UpdatedOn = DateTime.Now;
              db.Entry(account).State = EntityState.Modified;

            }

            db.SaveChanges();
            scope.Complete();
          }
        } catch (Exception ex) {
          scope.Dispose();

          throw ex;
        }
      }

      return RedirectToAction("Edit", new { id = RegisterData.ID });
    }
    public int changeStatus(int ID) {
      int res = 0;
      using (db = new WasteManageEntities()) {
        var user = db.Users.Where(x => x.ID == ID).FirstOrDefault();
        if (user != null) {
          user.Status = !(user.Status);
          user.UpdatedBy = (int)Session[AppVariables.SessionUserId]; //later
          user.UpdatedOn = DateTime.Now;
          db.Entry(user).State = EntityState.Modified;
          db.SaveChanges();
          res = 1;
        }
      }
      return res;
    }

    public async  Task<int> deleteCompany(int id) {
      ///0 means some exception
      /////1 means success
      ///2 means permit exist for the record and hence it cannot be deleted
      ///
      try {
        using (db = new WasteManageEntities()) {
          var permits = await
                db.PermitRequests.Where(e => e.EnteredBy == id).AnyAsync();
          if (permits) {
            return 2;
          }

          using (var scope= db.Database.BeginTransaction()) {
            try {
              string SQL = "delete from AccountTransactions where AccountID in (select ID from Accounts where Userid=@userid)";
              string SQL2 = "delete from UserRoleMapping where UserID=@userid";
              string SQL3 = "delete from Accounts where UserID=@userid";
              string SQL4 = "delete from Users where id=@userid";
              var execute = await db.Database.ExecuteSqlCommandAsync(SQL, new System.Data.SqlClient.SqlParameter("@userid", id));
               execute= await db.Database.ExecuteSqlCommandAsync(SQL2, new System.Data.SqlClient.SqlParameter("@userid", id));
              execute= await db.Database.ExecuteSqlCommandAsync(SQL3, new System.Data.SqlClient.SqlParameter("@userid", id));
              execute= await db.Database.ExecuteSqlCommandAsync(SQL4, new System.Data.SqlClient.SqlParameter("@userid", id));

              scope.Commit();
              return 1;
            } catch (Exception) {
              scope.Dispose();
              throw;
            }
          }

          return 0;
        }
      } catch (Exception ex) {
        AppUtil.ExceptionLog(ex);
        return 0;
      }
    
    }

    public int ResetPassword(int ID) {
      int res = 0;
      Helpers.EmailNotification NewEmail = new EmailNotification();
      using (db = new WasteManageEntities()) {
        var user = db.Users.Where(x => x.ID == ID).FirstOrDefault();
        if (user != null) {
          // Link Construction
          string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
          string dat = DateTime.Now.ToString();
          string attributes = "user_ID=" + @Uri.EscapeDataString(user.ID.ToString()) + "&Name=" + @Uri.EscapeDataString(user.Name) + "&Username=" + @Uri.EscapeDataString(user.UserName) + "&datetime=" + @Uri.EscapeDataString(dat) + "&Email=" + @Uri.EscapeDataString(user.EmailID);
          attributes = Base64Encode(attributes);

          url = string.Format(url + "User/ResetPassword?r=" + attributes);

          string sendmailhtml = System.IO.File.ReadAllText(Server.MapPath(@"~/Assets/mails/forgot_password.html")); //Getting Html

          string EmailBody = sendmailhtml.Replace("{ActivationUrl}", url);
          //
          //string Password = AppUtil.DecryptPassword(user.Password);
          NewEmail.Send(user.EmailID, "Forget Password: (" + user.UserName + ")", EmailBody, true, "");
          res = 1;
        }
      }
      return res;
    }

    //public int DeleteCompany(int ID) 
    //{
    //    int res = 0;
    //    using (db = new WasteManageEntities())
    //    {
    //        var userrole = db.UserRoleMappings.Where(x => x.UserID == ID).FirstOrDefault();
    //        var user = db.Users.Where(x => x.ID == ID).FirstOrDefault();
    //        if (user != null)
    //        {
    //            db.UserRoleMappings.Remove(userrole);
    //            db.Users.Remove(user);
    //            db.SaveChanges();
    //            res = 1;
    //        }
    //    }
    //    return res;
    //}


    // encode Function
    public static string Base64Encode(string plainText) {
      var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
      return System.Convert.ToBase64String(plainTextBytes);
    }

  }
}