using HFZMVC.AppLogics;
using HFZMVC.Models.EntityFramework;
using HFZMVC.Models.Finance;
using HFZMVC.Models.SamplingRequest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace HFZMVC.Controllers
{
  [HFZMVC.CustomBinders.HFZAuthorization]
  public class SamplingController : Controller
  {

    public WasteManageEntities _Db;
    string API_URL = ConfigurationManager.AppSettings["API_URL"];
    string Customer = ConfigurationManager.AppSettings["Customer"];
    string UserName = ConfigurationManager.AppSettings["UserName"];
    string Password = ConfigurationManager.AppSettings["Password"];
    // GET: Sampling
    public ActionResult Index() {
      return View();
    }

    #region --------------------------Create Sample-------------------------------------
    public async  Task<ActionResult> Create(int id = 0) {
      var role = AppUtil.getUserRole();
      var userid = AppUtil.checkLogin();
      CreateSamplingViewModel viewModel = new
          CreateSamplingViewModel();
      using (_Db = new WasteManageEntities()) {
        var permitList = _Db.PermitRequests;
        var entity = AppUtil.getEntity();
        var Entities = _Db.Entities.Where(e => e.ID == entity).First();
        viewModel.SampleFees = 800;
        viewModel.rdfee = Entities.r_dcharges;
        viewModel.ServiceFee = Entities.servicecharges;
        decimal? total = 800 + viewModel.rdfee + viewModel.ServiceFee;
        viewModel.vat = (total * Entities.vatcharges) / 100;
        viewModel.grandtotal = total + viewModel.vat;
        await PopulateViewBags(_Db);

        if (role == 1002 || role == 1) {
          var permitLIst =
              permitList.Where(e => (e.StatusID == 2 || e.StatusID == 4 || e.StatusID==7)).ToList()
              .Select(
                  e =>
                  new SelectListItem() {
                    Text = e.ID.ToString(),
                    Value = e.ID.ToString()
                  }).ToList();
          //var completedPermits =

          //   (
          //    from permit in _Db.PermitRequests
          //    join sampling in _Db.SamplingRequests
          //     on permit.ID equals sampling.PermitID
          //    where permit.StatusID == 7
          //    select new SelectListItem() {
          //      Text = permit.ID.ToString(),
          //      Value = permit.ID.ToString()
          //    }

          //   ).Distinct().ToList();



          //permitLIst.AddRange(completedPermits);

          ViewBag.PermitList = permitLIst;
        } else {
          var permitLIst =
            permitList.Where(e => e.EnteredBy == userid && (e.StatusID == 2 || e.StatusID == 4|| e.StatusID==7)).ToList()
            .Select(
                e =>
                new SelectListItem() {
                  Text = e.ID.ToString(),
                  Value = e.ID.ToString()
                }).ToList();
          //var completedPermits =

          //       (
          //        from permit in _Db.PermitRequests
          //        join sampling in _Db.SamplingRequests
          //         on permit.ID equals sampling.PermitID
          //        where permit.StatusID == 7
          //         && permit.EnteredBy==userid
          //        select new SelectListItem() {
          //          Text = permit.ID.ToString(),
          //          Value = permit.ID.ToString()
          //        }

          //       ).Distinct().ToList();
          //permitLIst.AddRange(completedPermits);
          ViewBag.PermitList = permitLIst;
        }

        if (id != 0) {
          var permitdetail = _Db
              .NormalPermits.Where(e => e.PermitId == id)
              .FirstOrDefault();
          var permitRequest = permitList.Where(e => e.ID == id).FirstOrDefault();
          

          if (permitRequest != null) {

           
              var sourceofsample = permitRequest.SourceOfSample;
            viewModel.samplesource = sourceofsample == null ? String.Empty : sourceofsample.ToString();
            var purposeofsample = permitRequest.PurposeOfSampling;
            viewModel.samplingpurpose =
               purposeofsample == null ? String.Empty : purposeofsample.ToString();
            viewModel.OtherSourceOfSample =
               permitRequest.Othersource;



          }
          if (permitdetail != null) {

            /* Sampling fees hardcoding */
            try {
              PermitWasteMapping
                        wasteMapping = _Db.PermitWasteMappings.Where(e => e.ID == permitdetail.WasteCategory)
                        .FirstOrDefault();

              if (wasteMapping != null && wasteMapping.IsHazardous.HasValue) {

                if (wasteMapping.IsHazardous.Value)
                  viewModel.SampleFees = 1360;
              }
            } catch (Exception ex) {

              AppUtil.ExceptionLog(ex);
            }


            /* Sampling fees hardcoding */
            viewModel.PermitId = viewModel.permitIdLabel
            = id;
            PermitTypeMaster permitTypeMaster = _Db.PermitTypeMasters
    .Where(e => e.ID == permitdetail.WasteType).FirstOrDefault();
            if (permitTypeMaster != null) {
              viewModel.WasteDescription = viewModel.WasteDescriptoinLabel = permitTypeMaster.PermitType;
            }



          } else {
            viewModel.PermitId = viewModel.permitIdLabel = id;
          }
        }

        return View(viewModel);
      }
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateSamplingViewModel createSampling) {
      using (_Db = new WasteManageEntities()) {


        using (var scope= _Db.Database.BeginTransaction()) {
          try {
            var permitRequest = await _Db.PermitRequests.
                    Where(e => e.ID == createSampling.PermitId)
                    .FirstOrDefaultAsync();
            if (permitRequest != null) {
              int userid = AppUtil.checkLogin();
              var samplerequest =
                new SamplingRequest() {
                  PermitID = createSampling.PermitId,
                  RequestDate = permitRequest.UpdatedOn,
                  SamplingAddress = createSampling.SamplingAddress,
                  SamplingDescription = createSampling.WasteDescription,
                  RecievedDate = DateTime.Now,
                  StatusID = 0,
                  CreatedBy = userid,
                  CreatedOn = DateTime.Now,
                  UpdatedBy = userid,
                  UpdatedOn = DateTime.Now
                };
              _Db.SamplingRequests.Add(samplerequest);
              await _Db.SaveChangesAsync();

              //permitRequest.StatusID = 3;
              permitRequest.IsSamplingRequired = true;
              try {
                permitRequest.SourceOfSample = Convert.ToInt32(createSampling.samplesource);
                permitRequest.PurposeOfSampling = Convert.ToInt32(createSampling.samplingpurpose);
              } catch (Exception) {

                throw;
              }
              permitRequest.Othersource = createSampling.OtherSourceOfSample;
              _Db.PermitRequests.Attach(permitRequest);
              _Db.Entry(permitRequest).State = System.Data.Entity.EntityState.Modified;
              await _Db.SaveChangesAsync();

              var permitInvoice = new Invoice();
              permitInvoice.Invoice_Type = AppVariables.SamplingInvoiceType;
              permitInvoice.InvoiceNo = "SMP000" + permitRequest.ID+"00"+ samplerequest.ID;
              permitInvoice.PermitFees = createSampling.SampleFees;
              permitInvoice.ServicesFees = createSampling.ServiceFee;
              permitInvoice.RnDFees = createSampling.rdfee;
              permitInvoice.Vat = createSampling.vat;
              permitInvoice.TotalAmount = createSampling.grandtotal;
              permitInvoice.DueDate = DateTime.Now.AddDays(7);
              permitInvoice.PaymentMethod = "N/A";
              permitInvoice.PermitID = permitRequest.ID;
              permitInvoice.Remarks = "Sample Required for Permit: " + permitRequest.ID;
              permitInvoice.EnteredBy = userid;
              permitInvoice.EnteredOn = DateTime.Now;
              permitInvoice.UpdatedBy = userid;
              permitInvoice.UpdatedOn = DateTime.Now;

              _Db.Invoices.Add(permitInvoice);
              await _Db.SaveChangesAsync();

              ///this.SAPEntry(permitRequest.ID);
             

              //TempData["message"] = "4";
              scope.Commit();
              //scope.Dispose();
              return RedirectToAction("SamplePayment", new { samplerequest.ID });
            } else {
              TempData["message"] = "2";
              PopulateViewBags(_Db);
              scope.Dispose();
              return View(createSampling);
            }
          } catch (Exception ex) {

            AppUtil.ExceptionLog(ex);
            TempData["message"] = "2";
             PopulateViewBags(_Db);
            scope.Dispose();
            
            return View(createSampling);
          } 
        }
      }

    }



    public async Task<bool> PopulateViewBags(WasteManageEntities _Db) {
      ViewBag.samplingpurpose = new List<SelectListItem>();
      foreach (var item in _Db.PurposeOfSamplingMasters.Where(e => e.status == true)) {

        ViewBag.samplingpurpose.Add(new SelectListItem() {
          Text = item.PurposeOfSampling,
          Value = item.ID.ToString()

        });
      }
      ViewBag.samplesource = new List<SelectListItem>();
      foreach (var item in _Db.SourceOfSampleMasters.Where(e => e.status == true)) {

        ViewBag.samplesource.Add(new SelectListItem() {
          Text = item.SourceOfSample,
          Value = item.ID.ToString()

        });
      }
      return true;
    }
    #endregion
    #region --------------------------SAP Entry -----------------------------------------
    public void SAPEntry(int id) {
      try {
        SAPOrderData Data = new SAPOrderData(); //later
        string query = @"select inv.PermitID, 
                                SAPCustomerId as SAPCustomerID,
                                '7000000517' as ProductCode,
                                1 as OrderQty,
                                inv.Remarks as Notes,
                                (PermitFees + ServicesFees + RnDFees + Vat) as Total,
                                PermitFees, 
                                ServicesFees as ServiceFees,
                                RnDFees as RDFees,
                                Vat as VAT
                              From Invoices inv 
                              join PermitRequest pr on inv.PermitID = pr.ID
                              join Users u on pr.UserID = u.ID
                              where inv.PermitID = " + id + " and Invoice_Type = 'Sampling' ";
        Data = _Db.Database.SqlQuery<SAPOrderData>(query).FirstOrDefault();
        string SalesOrder = CallinSAPI.GenerateOrder(Data);
        if (SalesOrder == null) {
          throw new Exception("Unable to process sales on SAP for sampling.");
        } else {
          var inv = _Db.Invoices.Where(x => x.PermitID == id && x.Invoice_Type == "Sampling").FirstOrDefault();
          inv.SalesOrder = SalesOrder;
          _Db.Entry(inv).State = EntityState.Modified;
          _Db.SaveChanges();
        }
      } catch (Exception ex) {

        AppUtil.ExceptionLog(ex);
      }
    }
    #endregion
    #region --------------------------Sample Payment-------------------------------------
    public ActionResult SamplePayment(int id) {
      using (_Db = new WasteManageEntities()) {
        var SampleData = _Db.SamplingRequests.Where(e => e.ID == id).First();
        var userid = AppUtil.checkLogin();
        var Accounts = _Db.Accounts.Where(e => e.UserID == userid);
        decimal walletbalance = 0;
        if (Accounts.Count() > 0) {
          var acc = Accounts.First();

          walletbalance = (decimal)acc.Balance;
        }
        ViewBag.balance = walletbalance;

        ViewBag.id = SampleData.PermitID;
        ViewBag.Sampleid = SampleData.ID;
        var invoiceData = _Db.Invoices.Where(e => e.PermitID == SampleData.PermitID && e.Invoice_Type == AppVariables.SamplingInvoiceType).First();
        ViewBag.invoiceData = invoiceData;

        return View();
      }
    }
    public JsonResult GetPermitVal(int id) {
      using (_Db = new WasteManageEntities()) {
        var permitDetail = _Db.PermitRequests.Where(e => e.ID == id).FirstOrDefault();
        var transaction = _Db.AccountTransactions.Where(e => e.PermitID == id).OrderByDescending(e=>e.TransactionDate).FirstOrDefault();
        var invoiceData = _Db.Invoices.Where(e => e.PermitID == id && e.PaymentDate == null && e.Invoice_Type == AppVariables.SamplingInvoiceType).OrderByDescending(e=>e.ID).FirstOrDefault();
        if (transaction != null && invoiceData == null) {
          var result = new Dictionary<string, object>();
          result.Add("Success", false);
          if (transaction.Status == false) {
            result.Add("msg", "The Payment for this Sample request is already Under Verification");
          } else if (transaction.Status == true) {
            result.Add("msg", "The Payment for this Sample request is already Done");
          } else {
            result.Add("msg", "The Payment unavailable for this Sample request");
          }
          return Json(result);
        } else {
          var entity = _Db.Entities.Where(e => e.ID == 1).FirstOrDefault();
          decimal? grandtotal = 0;
          decimal? vatcharges = 0;

          if (invoiceData != null) {
            grandtotal = invoiceData.PermitFees + invoiceData.RnDFees + invoiceData.ServicesFees;
            vatcharges = invoiceData.Vat;
            var userid = Convert.ToInt32(Session[AppVariables.SessionUserId]);

            var result = new Dictionary<string, object>();
            result.Add("Success", true);
            result.Add("totalAmount", grandtotal);
            result.Add("vatcharges", vatcharges);

            result.Add("grandtotal", (grandtotal + vatcharges));// - walletbalance


            return Json(result);
          } else {
            var result = new Dictionary<string, object>();
            result.Add("Success", false);
            result.Add("msg", "The Payment unavailable for this Sample request");
            return Json(result);
          }
        }
      }
    }
    #endregion

    #region -------------------------- Manual Payment -------------------------------------
    [HttpPost]
    public ActionResult ManualPayment(ManualPayments payment) {
      using (TransactionScope scope = new TransactionScope()) {
        using (_Db = new WasteManageEntities()) {
          try {
            long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string ReferenceFile = "";
            if (payment?.ReferenceFile?.ContentLength > 0) {
              string _FileName = Path.GetFileName(payment.ReferenceFile.FileName);
              string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), "cheque" + timeStamp + "_" + _FileName);
              payment.ReferenceFile.SaveAs(_path);
              ReferenceFile = "cheque" + timeStamp + "_" + _FileName;

            }
            var userid = Convert.ToInt32(Session[AppVariables.SessionUserId]);
            var InVoiceData = _Db.Invoices.Where(e => e.PermitID == payment.PermitID && e.PaymentDate == null && e.Invoice_Type == AppVariables.SamplingInvoiceType).First();
            var AccountData = _Db.Accounts.Where(e => e.UserID == userid).First();
            _Db.AccountTransactions.Add(new AccountTransaction() {
              InoviceID = InVoiceData.ID,
              AccountID = AccountData.ID,
              PaymentMethod = "Cheque",
              ReferenceNo = payment.ReferenceNo,
              ReferenceFIle = ReferenceFile,
              TransactionType = "Sampling",
              PermitID = payment.PermitID,
              AmountPayable = payment.GrandTotal,
              AmountPaid = payment.AmountPaid,
              Remarks = payment.Remarks,
              TransactionDate = payment.PaymentDate.HasValue ? payment.PaymentDate : new DateTime(1900, 01, 01),
              Status = false,
              EnteredBy = userid,
              EnteredOn = DateTime.Now,
              UpdatedBy = userid,
              UpdatedOn = DateTime.Now
            });
            _Db.SaveChanges();
            var fdcPermit = _Db.FDCPermits.Where(e => e.PermitId == payment.PermitID).FirstOrDefault();
            //
            string redirectLink = "ChangePermitStatus";

            if (fdcPermit != null) {
              redirectLink = "ChangeFDCStatus";
            }
            scope.Complete();
            return RedirectToAction(redirectLink, "Permit", new { id = payment.PermitID, statusId = 3 }); //later
          } catch (DbEntityValidationException ex) {
            scope.Dispose();
            foreach (var errors in ex.EntityValidationErrors) {
              foreach (var validationError in errors.ValidationErrors) {
                // get the error message 
                string errorMessage = validationError.ErrorMessage;
              }
            }
            ViewBag.error = ex.ToString();
            AppUtil.ExceptionLog(ex);
            return View(payment);
          }
        }
      }
    }
    #endregion

    #region -------------------------- Wallet Payment -------------------------------------
    [HttpPost]
    public ActionResult WalletPayment(ManualPayments payment) {
      using (TransactionScope scope = new TransactionScope()) {
        using (_Db = new WasteManageEntities()) {
          try {
            long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var userid = Convert.ToInt32(Session[AppVariables.SessionUserId]);
            var InVoiceData = _Db.Invoices.Where(e => e.PermitID == payment.PermitID && e.PaymentDate == null && e.Invoice_Type == AppVariables.SamplingInvoiceType).First();
            var AccountData = _Db.Accounts.Where(e => e.UserID == userid).First();
            _Db.AccountTransactions.Add(new AccountTransaction() {
              InoviceID = InVoiceData.ID,
              AccountID = AccountData.ID,
              PaymentMethod = "AccountBalance",
              ReferenceNo = "SMP-" + payment.PermitID,
              TransactionType = "Sampling",
              PermitID = payment.PermitID,
              AmountPayable = payment.GrandTotal,
              AmountPaid = payment.AmountPaid,
              Remarks = payment.Remarks,
              TransactionDate = DateTime.Now,
              Status = true,
              EnteredBy = userid,
              EnteredOn = DateTime.Now,
              UpdatedBy = userid,
              UpdatedOn = DateTime.Now
            });
            _Db.SaveChanges();
            // Update Account
            AccountData.Balance -= payment.AmountPaid;
            _Db.Accounts.Attach(AccountData);
            _Db.Entry(AccountData).State = System.Data.Entity.EntityState.Modified;
            _Db.SaveChanges();

            var permit = _Db.PermitRequests.Where(e => e.ID == payment.PermitID).FirstOrDefault();
            if (permit != null) {
              string redirectLink = "ChangePermitStatus";
              if (!permit.PermitType) {
                redirectLink = "ChangeFDCStatus";
              }
              scope.Complete();
              return RedirectToAction(redirectLink, "Permit", new { id = payment.PermitID, statusId = 3 }); //later
            } else {
              TempData["message"] = "3";
              return RedirectToAction("Index", "Permit", new { id = payment.PermitID, statusId = 3 });
            }

          } catch (DbEntityValidationException ex) {
            scope.Dispose();
            foreach (var errors in ex.EntityValidationErrors) {
              foreach (var validationError in errors.ValidationErrors) {
                // get the error message 
                string errorMessage = validationError.ErrorMessage;
              }
            }
            ViewBag.error = ex.ToString();
            return View(payment);
          }
        }
      }
    }
    #endregion

    #region -------------------------- CreditCard Payment -------------------------------------
    public class TransactionData
    {
      public int PermitID { get; set; }
      public string Amount { get; set; }
      public string OrderID { get; set; }
      public string OrderName { get; set; }
      public string OrderInfo { get; set; }
    }
    public async Task<string> GetTransactionID(TransactionData OData) {
      string Transaction = "";
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      string AppURL = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
      AppURL = string.Format(AppURL + "User/AddSampleCallBack/" + OData.PermitID);
      string URL = API_URL;
      string InputJSON = "{\"Registration\":"
          + "{\"Customer\":\"" + Customer + "\"," +
          "\"Channel\":\"Web\"," +
          "\"Amount\":\"" + OData.Amount + "\"," +
          "\"Currency\":\"AED\"," +
          "\"OrderID\":\"" + OData.OrderID + "\"," +
          "\"OrderName\":\"" + OData.OrderName + "\"," +
          "\"OrderInfo\":\"" + OData.OrderInfo + "\"," +
          "\"TransactionHint\":\"CPT:Y;VCC:Y;\"," +
          "\"UserName\":\"" + UserName + "\"," +
          "\"Password\":\"" + Password + "\"," +
          "\"ReturnPath\":\"" + AppURL + "\"}}";

      try {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(URL));
        byte[] lbPostBuffer = ASCIIEncoding.ASCII.GetBytes(InputJSON);
        //request.ClientCertificates.Add(certificate);               
        request.UserAgent = "Sample Payment For PermitID: " + OData.PermitID;
        request.Accept = "text/xml-standard-api";
        request.Method = "POST";
        request.ContentLength = lbPostBuffer.Length;
        request.ContentType = "application/json";
        request.Timeout = 600000;



        HttpWebResponse response;

        Stream loPostData = await request.GetRequestStreamAsync();
        loPostData.Write(lbPostBuffer, 0, lbPostBuffer.Length);
        loPostData.Close();

        response = (HttpWebResponse)request.GetResponse();
        Encoding enc = Encoding.GetEncoding(1252);
        StreamReader loResponseStream = new StreamReader(response.GetResponseStream(), enc);
        string result = await loResponseStream.ReadToEndAsync();
        response.Close();
        loResponseStream.Close();
        //dynamic deserializedProduct = JsonConvert.DeserializeObject<dynamic>(result);
        //TransactionID = deserializedProduct.Transaction.TransactionID;
        Transaction = result;

      } catch (Exception e) {
        AppUtil.ExceptionLog(e);
        Transaction = e.ToString();
      }

      return Transaction;
    }

    public async Task<ActionResult> VerifyPayment(int id, string TransactionID) {
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      string URL = API_URL;
      string InputJSON = "{\"Finalization\":"
          + "{\"TransactionID\":\"" + TransactionID + "\"," +
          "\"Customer\":\"" + Customer + "\"," +
          "\"UserName\":\"" + UserName + "\"," +
          "\"Password\":\"" + Password + "\"}}";

      try {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(URL));
        byte[] lbPostBuffer = ASCIIEncoding.ASCII.GetBytes(InputJSON);
        request.UserAgent = "Amount Verification";
        request.Accept = "text/xml-standard-api";
        request.Method = "POST";
        request.ContentLength = lbPostBuffer.Length;
        request.ContentType = "application/json";
        request.Timeout = 600000;

        HttpWebResponse response;

        Stream loPostData = await request.GetRequestStreamAsync();
        loPostData.Write(lbPostBuffer, 0, lbPostBuffer.Length);
        loPostData.Close();

        response = (HttpWebResponse)request.GetResponse();
        Encoding enc = Encoding.GetEncoding(1252);
        StreamReader loResponseStream = new StreamReader(response.GetResponseStream(), enc);
        string result = await loResponseStream.ReadToEndAsync();
        response.Close();
        loResponseStream.Close();
        dynamic data = JsonConvert.DeserializeObject<dynamic>(result);

        if (data.Transaction.ResponseCode == "0") {
          using (TransactionScope scope = new TransactionScope()) {
            using (_Db = new WasteManageEntities()) {
              try {
                long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                var userid = Convert.ToInt32(Session[AppVariables.SessionUserId]);
                var InVoiceData = _Db.Invoices.Where(e => e.PermitID == id && e.PaymentDate == null && e.Invoice_Type == AppVariables.SamplingInvoiceType).First();
                var AccountData = _Db.Accounts.Where(e => e.UserID == userid).FirstOrDefault();
                _Db.AccountTransactions.Add(new AccountTransaction() {
                  InoviceID = InVoiceData.ID,
                  AccountID = AccountData.ID,
                  PaymentMethod = "CreditCard",
                  ReferenceNo = TransactionID,
                  TransactionType = "Sampling",
                  PermitID = id,
                  AmountPayable = InVoiceData.TotalAmount,
                  AmountPaid = data.Transaction.Amount.Value,
                  Remarks = data.Transaction.ResponseDescription,
                  TransactionDate = DateTime.Now,
                  Status = true,
                  EnteredBy = userid,
                  EnteredOn = DateTime.Now,
                  UpdatedBy = userid,
                  UpdatedOn = DateTime.Now
                });
                _Db.SaveChanges();

                var permit = _Db.PermitRequests.Where(e => e.ID == id).FirstOrDefault();
                if (permit != null) {
                  string redirectLink = "ChangePermitStatus";
                  if (!permit.PermitType) {
                    redirectLink = "ChangeFDCStatus";
                  }
                  scope.Complete();
                  return RedirectToAction(redirectLink, "Permit", new { id = id, statusId = 3 }); //later
                } else {
                  TempData["message"] = "3";
                  return RedirectToAction("Index", "Permit", new { id = id, statusId = 3 });
                }

              } catch (DbEntityValidationException ex) {
                scope.Dispose();
                var SampleID = _Db.SamplingRequests.Where(e => e.PermitID == id).First().ID;
                Rejected(SampleID);
                string errorMessage = "";

                foreach (var errors in ex.EntityValidationErrors) {
                  foreach (var validationError in errors.ValidationErrors) {
                    // get the error message 
                    errorMessage = validationError.ErrorMessage;
                  }
                }
                TempData["message"] = errorMessage + "<br>" + ex.ToString();
                AppUtil.ExceptionLog(ex);
                return RedirectToAction("PermitPayment", "Payments", new { id });
              }
            }
          }
        } else {
          var SampleID = _Db.SamplingRequests.Where(e => e.PermitID == id).First().ID;
          Rejected(SampleID);
          TempData["message"] = data.Transaction.ResponseDescription;
          return RedirectToAction("PermitPayment", "Payments", new { id });
        }
      } catch (Exception ex) {
        var SampleID = _Db.SamplingRequests.Where(e => e.PermitID == id).First().ID;
        Rejected(SampleID);
        TempData["message"] = ex.ToString();
        AppUtil.ExceptionLog(ex);
        return RedirectToAction("PermitPayment", "Payments", new { id });
      }
    }
    #endregion

    #region -------------------------- Sample Rejected -------------------------------------
    public int Rejected(int id) {
      int response = 0;
      try {
        using (_Db = new WasteManageEntities()) {
          var SamplingRequests = _Db.SamplingRequests.Where(e => e.ID == id).First();// Employ { Id = 1 };
          var PermitID = SamplingRequests.PermitID;                                                                           // Invoice First
          var Invoices = _Db.Invoices.Where(e => e.ID == SamplingRequests.PermitID && e.Invoice_Type == AppVariables.SamplingInvoiceType && e.PaymentDate == null).First();// Employ { Id = 1 };
          _Db.Invoices.Attach(Invoices);
          _Db.Invoices.Remove(Invoices);
          _Db.SaveChanges();
          //Sample record
          _Db.SamplingRequests.Attach(SamplingRequests);
          _Db.SamplingRequests.Remove(SamplingRequests);
          _Db.SaveChanges();
          response = 1;
        }
      } catch (DbEntityValidationException ex) {
        foreach (var errors in ex.EntityValidationErrors) {
          foreach (var validationError in errors.ValidationErrors) {
            // get the error message 
            string errorMessage = validationError.ErrorMessage;
          }
        }
        ViewBag.error = ex.ToString();
        AppUtil.ExceptionLog(ex);
        response = 0;
      }

      return response;
    }
    #endregion




    #region ---------------------------Sample Certificate Upload---------------------------------
    [HttpPost]
    public async Task<JsonResult> UploadSampleCert(int id) {
      // Checking no of files injected in Request object  
      if (Request.Files.Count > 0) {
        try {
          var Expirydate = DateTime.ParseExact(Request.Form["Expirydate"], "MM/dd/yyyy", null);
          //  Get all files from Request object  
          HttpFileCollectionBase files = Request.Files;
          var samplingFileName = "Sampling_PermitNo_" + id;
          string fname = "";
          for (int i = 0; i < files.Count; i++) {
            //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
            //string filename = Path.GetFileName(Request.Files[i].FileName);  

            HttpPostedFileBase file = files[i];


            // Checking for Internet Explorer  
            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER") {
              string[] testfiles = file.FileName.Split(new char[] { '\\' });
              fname = testfiles[testfiles.Length - 1];
            } else {
              fname = file.FileName;
            }
            fname = samplingFileName + "_" + fname;
            // Get the complete folder path and store the file inside it.  
            var serverpathToSave = Server.MapPath("~/UploadedFiles/");
            if (!Directory.Exists(serverpathToSave)) {
              Directory.CreateDirectory(serverpathToSave);

            }
            serverpathToSave = Path.Combine(serverpathToSave, fname);
            file.SaveAs(serverpathToSave);

          }

          using (_Db = new WasteManageEntities()) {
            var sampling = await _Db.SamplingRequests.Where(e => e.PermitID == id).OrderByDescending(e => e.ID).FirstOrDefaultAsync();
            if (sampling != null) {
              sampling.SampleReportFile = fname;
              sampling.ExpiryDate = Expirydate;
              _Db.Entry(sampling).State = EntityState.Modified;
              await _Db.SaveChangesAsync();
            } else {
              return Json("Error occurred. Error details: Record cannot be found, please refresh and try again");

            }

          }


          // Returns message that successfully uploaded  
          return Json("File Uploaded Successfully!");
        } catch (Exception ex) {
          AppUtil.ExceptionLog(ex);
          return Json("Error occurred. Error details: " + ex.Message);
        }
      } else {
        return Json("No files selected.");
      }


    }
    #endregion
  }


}