
using HFZMVC.Models.EntityFramework;
using HFZMVC.Models.UserManagement;
using HFZMVC.Helpers;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Transactions;
using System.Web;
using Newtonsoft.Json;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using HFZMVC.AppLogics;
using SAP.Middleware.Connector;

namespace HFZMVC.Controllers
{
  public class UserController : Controller
  {

    public class MailUser
    {
      public string UserName { get; set; }
      public string EmailID { get; set; }
    }

    #region --------------------------Properties-------------------------------------
    WasteManageEntities db;
    #endregion

    #region -------------------------LOGIN Methods-----------------------------------
    public ActionResult Login() {



      ViewBag.Success = TempData["Success"];
      return View();
    }

    public ActionResult SAPConnect() {
      try {
        /////////  CallinSAPI.CallingSAP();
        RfcDestination dest = null;
        dest = RfcDestinationManager.GetDestination("mySAPdestination");
        RfcRepository repo = dest.Repository;
        IRfcFunction func = repo.CreateFunction("ZSD_HFZ_CR_CUSTOMER");
        //prepare input parameters
        IRfcStructure impStruct = func.GetStructure("I_CUSTOMER");
        impStruct.SetValue("CUSTOMER_NAME", "Adeel");
        impStruct.SetValue("CITY", "Sharjah");
        impStruct.SetValue("LOCATION", "Sharjah");
        impStruct.SetValue("STREET_NO", "Sharjah");
        impStruct.SetValue("POSTL_COD1", "Sharjah");
        impStruct.SetValue("PO_BOX", "Sharjah");
        impStruct.SetValue("TEL1_NUMBR", "Sharjah");
        impStruct.SetValue("E_MAIL", "Sharjah@Sharjah.com");
        impStruct.SetValue("CP_FNAME", "Sharjah");
        impStruct.SetValue("CP_PHONE", "Sharjah");
        impStruct.SetValue("STCEG", "123121212");
        impStruct.SetValue("ENTITY", "1");

        func.Invoke(dest);
        RfcSessionManager.EndContext(dest);
        ViewBag.SAPDATA = func;
        ViewBag.SAPMessage = "Successfully connencted";
      } catch (Exception ex) {

        throw ex;
      }

      return View();
    }

    public ActionResult Index() {
      using (db = new WasteManageEntities()) {

        string query = "Select tbu.ID, UserName, Name, EmailID, PhoneNumber,CASE WHEN Status = 1 THEN 'Active' ELSE 'InAcive' END AS Status, " +
            "(Select RoleName from RoleMaster where RoleMaster.ID = tbur.RoleID) as Role from Users tbu inner join UserRoleMapping tbur on tbu.ID = tbur.UserID where tbur.RoleID != 3 order by tbu.CreatedOn desc";
        var Data = db.Database.SqlQuery<UserMaster>(query).ToList();
        return View(Data);
      }
    }

    [HttpPost]
    public ActionResult Register(RegisterViewModel RegisterData) {

      using (db = new WasteManageEntities()) {
        var newuserentry = db.Users.Where(x => x.UserName == RegisterData.Username).FirstOrDefault();
        if (newuserentry != null) {
          @ViewBag.Error = "UserName Already Exists..!!!";
          return View("Login");
        }
        var newuserentryemail = db.Users.Where(x => x.EmailID == RegisterData.EmailID).FirstOrDefault();
        if (newuserentryemail != null) {
          @ViewBag.Error = "Email Already Exists..!!!";
          return View("Login");
        }
      }
      long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
      bool upload = false;
      string CompanyLicensedoc = "";
      string VatCertificatedoc = "";
      try {
        if (RegisterData.CompanyLicenseFile.ContentLength > 0) {
          string _FileName = Path.GetFileName(RegisterData.CompanyLicenseFile.FileName);
          string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), "Lic" + timeStamp + "_" + _FileName);
          RegisterData.CompanyLicenseFile.SaveAs(_path);
          CompanyLicensedoc = "Lic" + timeStamp + "_" + _FileName;
        }
        if (RegisterData.VatCertificateFile.ContentLength > 0) {
          string _FileName = Path.GetFileName(RegisterData.VatCertificateFile.FileName);
          string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), "vat" + timeStamp + "_" + _FileName);
          RegisterData.VatCertificateFile.SaveAs(_path);
          VatCertificatedoc = "vat" + timeStamp + "_" + _FileName;
        }
        upload = true;
      } catch {
        upload = false;
        @ViewBag.Error = "Unable to Upload your documents.";
      }

      if (upload == true) {
        Helpers.EmailNotification NewEmail = new EmailNotification();
        string password = AppUtil.Encrypt(RegisterData.Password);
        string Email = RegisterData.EmailID;
        using (TransactionScope scope = new TransactionScope()) {
          try {
            using (db = new WasteManageEntities()) {
              db.Users.Add(new User() {
                UserName = RegisterData.Username,
                Password = password,
                Name = RegisterData.Name,
                PhoneNumber = RegisterData.PhoneNumber,
                EmailID = RegisterData.EmailID,
                CompanyName = RegisterData.CompanyName,
                LicenseNumber = RegisterData.TradelicenseNo,
                TradeLicenseFile = CompanyLicensedoc,
                VATNumber = RegisterData.VatlicenseNo,
                VATLicenseFile = VatCertificatedoc,
                Address = RegisterData.Address,
                Address2 = RegisterData.Address2,
                Status = false,
                EntityID = 1,
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                UpdatedBy = 1,
                UpdatedOn = DateTime.Now
              });
              db.SaveChanges();

              var newuser = db.Users.Where(x => x.UserName == RegisterData.Username).FirstOrDefault();

              db.UserRoleMappings.Add(new UserRoleMapping() {
                UserID = newuser.ID,
                RoleID = 3, // Client Role ID
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                UpdatedBy = 1,
                UpdatedOn = DateTime.Now
              });
              db.SaveChanges();

              db.Accounts.Add(new Account() {
                UserID = newuser.ID,
                Balance = 0, // Client Role ID
                Details = "New Account Created",
                status = true,
                EnteredBy = 1,
                EnteredOn = DateTime.Now,
                UpdatedBy = 1,
                UpdatedOn = DateTime.Now
              });
              //Calling SAP API
              string CustomerID = CallinSAPI.AddCustomer(RegisterData);
              if (CustomerID == null) {
                throw new Exception("Unable to process you request.");
              } else {
                newuser.SAPCustomerId = CustomerID;
                db.Entry(newuser).State = EntityState.Modified;
                db.SaveChanges();
              }
              string query = "Select UserName, EmailID from Users tbu inner join UserRoleMapping tbur on tbu.ID = tbur.UserID " +
                                              "where(Select RoleName from RoleMaster where RoleMaster.ID = tbur.RoleID) = 'admin'";
              var ApprovalMailData = db.Database.SqlQuery<MailUser>(query).FirstOrDefault();

              string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
              string dat = DateTime.Now.ToString();
              string attributes = "user_ID=" + @Uri.EscapeDataString(newuser.ID.ToString()) + "&Name=" + @Uri.EscapeDataString(RegisterData.Name) + "&Username=" + @Uri.EscapeDataString(RegisterData.Username) + "&datetime=" + @Uri.EscapeDataString(dat) + "&Email=" + @Uri.EscapeDataString(Email);
              attributes = Base64Encode(attributes);

              url = string.Format(url + "User/Activation?r=" + attributes);

              string sendmailhtml = System.IO.File.ReadAllText(Server.MapPath(@"~/Assets/mails/account_verification.html")); //Getting Html

              string EmailBody = sendmailhtml.Replace("{ActivationUrl}", url);

              NewEmail.Send(Email, "Welcome to Bee'ah", EmailBody, true, ""); //New User verification Email

              NewEmail.Send(ApprovalMailData.EmailID, "New User SignUp Alert", "A New User has been created with the Name of " + RegisterData.Name + " for the company \"" + RegisterData.CompanyName + "\"", true, ""); //New User Alert Email

              


              db.SaveChanges();
              scope.Complete();

            }
          } catch (DbEntityValidationException ex) {
            scope.Dispose();
            AppUtil.ExceptionLog(ex);
            foreach (var errors in ex.EntityValidationErrors) {
              foreach (var validationError in errors.ValidationErrors) {
                // get the error message 
                string errorMessage = validationError.ErrorMessage;
              }
            }
          }
        }
      }

      TempData["SuccessLogin"] = "Registration Successful, A verification email will be sent to registered email.";
      return RedirectToAction("Login", "User");
    }
    [HttpGet]
    public string Check() {
      if (AppUtil.checkLogin() > 0)
        return "1";
      return "0";
    }
    public int checkData(registerdatavalidation data) {
      int isexist = 0;
      string username = data.UserName;
      string email = data.email;
      string type = data.type;
      using (db = new WasteManageEntities()) {
        if (type == "UserName") {
          var newuserentry = db.Users.Where(x => x.UserName == username).FirstOrDefault();
          if (newuserentry != null) {
            isexist = 1;
          }
        } else if (type == "email") {
          var newuserentryemail = db.Users.Where(x => x.EmailID == email).FirstOrDefault();
          if (newuserentryemail != null) {
            isexist = 1;
          }
        }
      }
      return isexist;
    }

    public ActionResult Activation(string r) {
      if (string.IsNullOrEmpty(r)) {
        return HttpNotFound();
      } else {
        dynamic response = GetObject(r); // Parse Datastring to Json Object
        var ID = (int)response.user_ID;
        var Email = response.Email.ToString();



        Helpers.EmailNotification NewEmail = new EmailNotification();

        using (db = new WasteManageEntities()) {
          using (var scope= new TransactionScope()) {

            try {
              var user = db.Users.Where(x => x.ID == ID).FirstOrDefault();
              if (user.UpdatedBy == ID) {
                //return HttpNotFound();
              }
              user.UpdatedBy = ID;
              user.UpdatedOn = DateTime.Now;
              db.Entry(user).State = EntityState.Modified;
              db.SaveChanges();
              string sendmailhtml = System.IO.File.ReadAllText(Server.MapPath(@"~/Assets/mails/Activation.html")); //Getting Html

              #region Send a task
              //To send task in wbridge integration
              var apptask = new AppTask();
              var userDTO = new RegisterViewModel() {
                ID = user.ID,
                Address = user.Address,
                CompanyName = user.CompanyName,
                EmailID = user.EmailID,
                Name = user.Name,
                SAPCustomerId = user.SAPCustomerId,
                Status = Convert.ToBoolean(user.Status),
                PhoneNumber = user.PhoneNumber,

              };
              apptask.TaskTypeId = "NewWBCustomer";
              apptask.TaskBody =
                JsonConvert.SerializeObject(userDTO);
              apptask.CreatedBy=apptask.UpdatedBy = "Portal";
              apptask.CreatedOn =apptask.UpdatedOn= DateTime.Now;
              apptask.Retry = 0;
              apptask.Status = "New";
              apptask.TaskTable = "Users";
              apptask.TaskPrimaryKey = user.ID.ToString();
              db.AppTasks.Add(apptask);
              db.SaveChanges();
              #endregion
              NewEmail.Send(Email, "Email Verfied Successfully", sendmailhtml, true, "");
              scope.Complete();
            } catch (Exception ex) {

              scope.Dispose();
              AppUtil.ExceptionLog(ex);
              return HttpNotFound();
            } 
          }
        }
        return View(response);
      }
    }

    public ActionResult Forgetpassword() {
      return View();
    }

    [HttpPost]
    public ActionResult Forgetpassword(forgetPasswordViewModel Email) {
      Helpers.EmailNotification NewEmail = new EmailNotification();
      using (db = new WasteManageEntities()) {
        var user = db.Users.Where(e => e.EmailID == Email.EmailID && e.ID != 1).FirstOrDefault();
        if (user != null) {
          if (user.Status == false) {
            ViewBag.Error = "Your account is currently inactive. Please contact support for further information";
            return View();
          }
          // Link Construction
          string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
          string dat = DateTime.Now.ToString();
          string attributes = "user_ID=" + @Uri.EscapeDataString(user.ID.ToString()) + "&Name=" + @Uri.EscapeDataString(user.Name) + "&Username=" + @Uri.EscapeDataString(user.UserName) + "&datetime=" + @Uri.EscapeDataString(dat) + "&Email=" + @Uri.EscapeDataString(Email.EmailID);
          attributes = Base64Encode(attributes);

          url = string.Format(url + "User/ResetPassword?r=" + attributes);

          string sendmailhtml = System.IO.File.ReadAllText(Server.MapPath(@"~/Assets/mails/forgot_password.html")); //Getting Html

          string EmailBody = sendmailhtml.Replace("{ActivationUrl}", url);
          //
          //string Password = AppUtil.DecryptPassword(user.Password);
          NewEmail.Send(Email.EmailID, "Forget Password: (" + user.UserName + ")", EmailBody, true, "");

          ViewBag.Success = "A reset password request has been sent to your registered email.";

          return View();
        } else {//"Forgetpassword", "User"
          ViewBag.Error = "Error! Invalid Email";
          return View();
        }
      }
    }

    [HttpGet]
    public ActionResult ResetPassword(string r) {
      if (string.IsNullOrEmpty(r)) {
        return HttpNotFound();
      } else {
        dynamic response = GetObject(r); // Parse Datastring to Json Object
        return View(response);
      }
    }

    [HttpPost]
    public ActionResult ChangePassword(resetPasswordViewModel data) {
      Helpers.EmailNotification NewEmail = new EmailNotification();
      int ID = data.ID;
      string password = AppUtil.Encrypt(data.password);
      string Email = data.Email;

      using (db = new WasteManageEntities()) {
        var user = db.Users.Where(x => x.ID == ID).FirstOrDefault();
        user.Password = password;
        user.UpdatedBy = ID;
        user.UpdatedOn = DateTime.Now;
        db.Entry(user).State = EntityState.Modified;
        db.SaveChanges();

        string EmailBody = System.IO.File.ReadAllText(Server.MapPath(@"~/Assets/mails/changesuccess.html")); //Getting Html

        NewEmail.Send(Email, "Password Change For (" + user.UserName + ")", EmailBody, true, "");
      }
      TempData["Success"] = "The password Has Been changed Successfully.";
      return RedirectToAction("login");
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel logindata) {
      using (db = new WasteManageEntities()) {
        var user = await db.Users.Where(e => e.UserName == logindata.Username || e.EmailID == logindata.Username)

          .OrderByDescending(e=>e.ID)  .FirstOrDefaultAsync();
        if (user == null) {
          ViewBag.Error = "Error! Invalid username and password";
          return View(logindata);
        } else if (user.Status == false) {
          ViewBag.Error = "Your account is currently inactive. Please contact support for further information";
          return View(logindata);
        } else {
          string decryptedPassword = AppUtil.DecryptPassword(user.Password);
          bool isValidUser = (logindata.Password.Equals(decryptedPassword));
          if (!isValidUser) {
            ViewBag.Error = "Error! Invalid username and password";
            return View(logindata);
          }
          var UserRoleType = await db.UserRoleMappings.Where(e => e.UserID ==
              user.ID).FirstOrDefaultAsync();

          Session[AppVariables.SessionUsername] = user.UserName;
          Session[AppVariables.SessionUserId] = user.ID;
          Session[AppVariables.SessionUserRole] = UserRoleType.RoleID;
          Session[AppVariables.SessionFullName] = user.Name;

          return RedirectToAction("Index", "Dashboard");

        }

      }

    }
    #endregion
    public ActionResult Create() {
      using (db = new WasteManageEntities()) {

        string query = "Select * from RoleMaster where ID Not IN(3,1002);";
        var Data = db.Database.SqlQuery<RoleMaster>(query).ToList();
        return View(Data);
      }
    }

    [HttpPost]
    public ActionResult Add(User user) {
      string password = AppUtil.Encrypt(user.Password);
      using (TransactionScope scope = new TransactionScope()) {
        try {
          using (db = new WasteManageEntities()) {
            db.Users.Add(new User() {
              UserName = user.UserName,
              Password = password,
              Name = user.Name,
              PhoneNumber = user.PhoneNumber,
              EmailID = user.EmailID,
              Status = user.Status,
              EntityID = 1,
              CreatedBy = (int)Session[AppVariables.SessionUserId],
              CreatedOn = DateTime.Now,
              UpdatedBy = (int)Session[AppVariables.SessionUserId],
              UpdatedOn = DateTime.Now
            });
            db.SaveChanges();

            var newuser = db.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();

            db.UserRoleMappings.Add(new UserRoleMapping() {
              UserID = newuser.ID,
              RoleID = user.ID, // Client Role ID
              CreatedBy = (int)Session[AppVariables.SessionUserId],
              CreatedOn = DateTime.Now,
              UpdatedBy = (int)Session[AppVariables.SessionUserId],
              UpdatedOn = DateTime.Now
            });
            db.SaveChanges();
            scope.Complete();

          }
        } catch (DbEntityValidationException ex) {
          scope.Dispose();
          foreach (var errors in ex.EntityValidationErrors) {
            foreach (var validationError in errors.ValidationErrors) {
              // get the error message 
              string errorMessage = validationError.ErrorMessage;
            }
          }
        }
      }
      return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult EditUser(int ID) {
      using (db = new WasteManageEntities()) {
        var user = db.Users.Where(x => x.ID == ID).FirstOrDefault();
        user.Password = AppUtil.DecryptPassword(user.Password);
        if (user == null) {
          return RedirectToAction("Index");
        }
        return View(user);
      }
    }

    [HttpPost]
    public ActionResult EditUser(User user) {
      Helpers.EmailNotification NewEmail = new EmailNotification();
      string password = AppUtil.Encrypt(user.Password);
      string Email = user.EmailID;
      using (db = new WasteManageEntities()) {
        user.Password = password;
        user.UpdatedOn = DateTime.Now;
        db.Entry(user).State = EntityState.Modified;
        db.SaveChanges();
        NewEmail.Send(Email, "User Update", "Your Account has been Updated and Activated", true, "");
      }

      return RedirectToAction("Index");
    }

    public ActionResult View(int ID) {
      using (db = new WasteManageEntities()) {
        var user = db.Users.Where(x => x.ID == ID).FirstOrDefault();
        return View(user);
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
        return View(user);
      }
    }

    [HttpPost]
    public ActionResult UpdateProfile(User user) {
      try {
        string password = "";
        if (!string.IsNullOrEmpty(user.Password)) {
          password = AppUtil.Encrypt(user.Password);
        }
        string Email = user.EmailID;
        using (db = new WasteManageEntities()) {
          var userrec = db.Users.Where(x => x.ID == user.ID).FirstOrDefault();
          userrec.Name = user.Name;
          userrec.EmailID = user.EmailID;
          userrec.PhoneNumber = user.PhoneNumber;
          if (password != "") {
            userrec.Password = password;
          }
          userrec.UpdatedBy = (int)Session[AppVariables.SessionUserId];
          userrec.UpdatedOn = DateTime.Now;
          db.Entry(userrec).State = EntityState.Modified;
          db.SaveChanges();
        }
      } catch (DbEntityValidationException ex) {
        AppUtil.ExceptionLog(ex);
        foreach (var errors in ex.EntityValidationErrors) {
          foreach (var validationError in errors.ValidationErrors) {
            // get the error message 
            string errorMessage = validationError.ErrorMessage;
          }
        }
        TempData["message"] = "2";
      }
      TempData["message"] = "1";
      return RedirectToAction("Profile");
    }

    #region --------------------------------------LogOut Methods-------------------------------------

    public ActionResult Logout() {
      Session.Clear();
      Session.Abandon();
      return RedirectToAction("Login");
    }
    #endregion

    public ActionResult Terms() {
      return View();
    }
    public ActionResult Privacy() {
      return View();
    }

    public ActionResult AddBalanceCallBack(string TransactionID) {
      return RedirectToAction("VerifyPayment", "Accounts", new { TransactionID });
    }

    public ActionResult PermitPaymentCallBack(int id, string TransactionID) {
      return RedirectToAction("VerifyPayment", "Payments", new { id, TransactionID });
    }

    public ActionResult AddSampleCallBack(int id, string TransactionID) {
      return RedirectToAction("VerifyPayment", "Sampling", new { id, TransactionID });
    }


    // encode Function
    public static string Base64Encode(string plainText) {
      var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
      return System.Convert.ToBase64String(plainTextBytes);
    }

    // Decode Function
    public static string Base64Decode(string base64EncodedData) {
      var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
      return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static dynamic GetObject(string datastring) {
      string responseString = Base64Decode(datastring);
      var dict = HttpUtility.ParseQueryString(responseString);
      string json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v]));
      dynamic respObj = JsonConvert.DeserializeObject<dynamic>(json);
      return respObj;
    }
  }
}