using Aspose.Pdf;
using HFZMVC.AppLogics;
using HFZMVC.Helpers;
using HFZMVC.Models.EntityFramework;
using HFZMVC.Models.Finance;
using HFZMVC.Models.PermitRequest;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace HFZMVC.Controllers
{
  [HFZMVC.CustomBinders.HFZAuthorization]
  public partial class PermitController : Controller
  {
    WasteManageEntities _Db;

    // GET: Permit



    #region Permit CRUD -----------------------------------------------------------------------------
    public async Task<ActionResult> Index(string section, int? status, int? permittype, int? refid,
  string datefrom, string dateto) {
      bool whereconditionadded = false;
      ViewBag.section = section;
      var role = Convert.ToInt32(Session[AppVariables.SessionUserRole]);

      bool IsDelete = await AppUtil.checkIsPermission("DeletePermit");
      if (role == 1002) {
        IsDelete = true
          ;
      }
      ViewBag.IsDelete = IsDelete;

      string cond = ""; //For Different screens
      {
        if (section == "InProgress") {
          cond = " StatusID Not IN (5,7,8,9)";
        } else if (section == "Issued") {
          cond = " StatusID IN (7,8)";
        } else if (section == "Active") {
          cond = " StatusID IN (7,8) and ValidTill >= getdate()";
        } else if (section == "Expired") {
          cond = " StatusID IN (7,8) and ValidTill< getdate()";
        } else {
          RedirectToAction("Index");
        }
      } // Condition of status selection
      string query = @"select pr.ID,pr.ApplyDate,pt.PermitType PermitType,pr.ID permitID,wc.WasteType as Category, pr.StatusID,
				(select top 1 TotalAmount from Invoices inv where inv.PermitID=pr.ID and Invoice_Type='Permit')  TotalAmount,st.statusName Status, getDate() ValidTill, PermitreferenceiD,
ur.CompanyName +' ('+ur.SAPCustomerId+')' CompanyName,np.Remarks,cast('false' as bit) IsSampling,
cast (totalweight as decimal)TotalWeight ,cast( COALESCE(consumedweight,0 ) as decimal)ConsumedWeight
				From PermitRequest pr 
				inner join NormalPermit np
					on pr.ID= np.PermitId

					left join PermitTypeMaster pt on pt.ID= np.WasteType
					left join PermitWasteMapping wc on wc.ID= np.WasteCategory
					left join StatusMaster st on pr.StatusID=st.ID
left join Users ur on ur.Id=pr.UserId
					{WhereCondition}
					";
      string query2 = @" select sp.ID,pr.ID permitID ,sp.RequestDate ApplyDate,pt.PermitType PermitType,wc.WasteType as Category, pr.StatusID,
				(select top 1 TotalAmount from Invoices inv where inv.PermitID=pr.ID and Invoice_Type='Sampling')
				TotalAmount,st.statusName Status, getDate() ValidTill,NULL PermitreferenceiD,
ur.CompanyName +' ('+ur.SAPCustomerId+')' CompanyName,np.Remarks,cast('true' as bit) IsSampling,
 cast (totalweight as decimal)TotalWeight ,cast( COALESCE(consumedweight,0 ) as decimal)ConsumedWeight
				From SamplingRequests sp 
				inner join PermitRequest pr
				on sp.PermitID=pr.id
				inner join NormalPermit np
					on pr.ID= np.PermitId

					left join PermitTypeMaster pt on pt.ID= np.WasteType
					left join PermitWasteMapping wc on wc.ID= np.WasteCategory
					left join StatusMaster st on pr.StatusID=st.ID
left join Users ur on ur.Id=pr.UserId
		{WhereCondition}
";
      if (Session[AppVariables.SessionUserRole] != null) {
        using (_Db = new WasteManageEntities()) {
          List<PermitRequestIndexViewModel> permitRequests = new List<PermitRequestIndexViewModel>();


          var userid = Convert.ToInt32(Session[AppVariables.SessionUserId]);

          if (role == 3) {
            query = query.Replace("{WhereCondition}", " where pr.UserID=@EnteredBy ");
            query2 = query2.Replace("{WhereCondition}", " where pr.UserID=@EnteredBy ");
            whereconditionadded = true;

          } else {
            string condition = " ";
            bool check = await AppUtil.checkIsPermission(AppVariables.HazardApproval, _Db);

            if (check) {

              if (whereconditionadded) {
                condition = " and pr.StatusID = 11 ";
                query += condition;
                query2 += condition;
              } else {
                condition = " where pr.StatusID = 11 ";
                whereconditionadded = true;
                query = query.Replace("{WhereCondition}", condition);
                query2 = query2.Replace("{WhereCondition}", condition);
              }





            }

          }
          if (status.HasValue) {
            var statusquery = status.Value == 0 ? " 1=1 " : $" pr.StatusId ={status.Value} ";

            if (whereconditionadded) {
              query += " and " + statusquery;
              query2 += " and " + statusquery;
            } else {
              whereconditionadded = true;
              query = query.Replace("{WhereCondition}", " where " + statusquery);
              query2 = query2.Replace("{WhereCondition}", " where " + statusquery);
            }

          }


          if (permittype.HasValue) {

            var permitquery = permittype.Value == 0 ? " 1=1 " : $" np.WasteType ={permittype.Value} ";

            if (whereconditionadded) {
              query += " and " + permitquery;
              query2 += " and " + permitquery;
            } else {
              whereconditionadded = true;
              query = query.Replace("{WhereCondition}", " where " + permitquery);
              query2 = query2.Replace("{WhereCondition}", " where " + permitquery);
            }
          }
          if (refid.HasValue) {
            var refquery = refid.Value == 0 ? " 1=1 " : $" pr.ID ={refid.Value} ";

            if (whereconditionadded) {
              query += " and " + refquery;
              query2 += " and " + refquery;
            } else {
              whereconditionadded = true;
              query = query.Replace("{WhereCondition}", " where " + refquery);
              query2 = query2.Replace("{WhereCondition}", " where " + refquery);
            }
          }
          if (!String.IsNullOrEmpty(datefrom)) {
            var datefromquery = $" pr.EnteredOn >= cast ('{datefrom}' as datetime) ";

            if (whereconditionadded) {
              query += " and " + datefromquery;
              query2 += " and " + datefromquery;
            } else {
              whereconditionadded = true;
              query = query.Replace("{WhereCondition}", " where " + datefromquery);
              query2 = query2.Replace("{WhereCondition}", " where " + datefromquery);
            }
          }
          if (!String.IsNullOrEmpty(dateto)) {
            var datetoquery = $" pr.EnteredOn <= cast ('{dateto}' as datetime) ";

            if (whereconditionadded) {
              query += " and " + datetoquery;
              query2 += " and " + datetoquery;
            } else {
              whereconditionadded = true;
              query = query.Replace("{WhereCondition}", " where " + datetoquery);
              query2 = query2.Replace("{WhereCondition}", " where " + datetoquery);
            }
          }
          if (cond != "") {
            if (whereconditionadded) {
              query += " and " + cond;
              query2 += " and " + cond;
            } else {
              whereconditionadded = true;
              query = query.Replace("{WhereCondition}", " where " + cond);
              query2 = query2.Replace("{WhereCondition}", " where " + cond);
            }
          }

          query += "   order by pr.UpdatedOn desc";
          query2 += "   order by pr.UpdatedOn desc";
          if (!whereconditionadded) {
            query = query = query.Replace("{WhereCondition}", "");
            query2 = query2 = query2.Replace("{WhereCondition}", "");
          }
          if (role == 3) {
            permitRequests = _Db.Database.SqlQuery<PermitRequestIndexViewModel>(query, new SqlParameter("@EnteredBy", userid)).ToList();
            permitRequests.AddRange(_Db.Database.SqlQuery<PermitRequestIndexViewModel>(query2, new SqlParameter("@EnteredBy", userid)).ToList());
          } else {
            permitRequests = _Db.Database.SqlQuery<PermitRequestIndexViewModel>(query).ToList();
            permitRequests.AddRange(_Db.Database.SqlQuery<PermitRequestIndexViewModel>(query2).ToList());
          }


          return View(permitRequests.OrderByDescending(e => e.ApplyDate));
        }
      } else {
        return Redirect("/");
      }

    }

    public ActionResult Create() {
      using (_Db = new WasteManageEntities()) {


        PopulateDropDownListForPermit();
        int userid = AppUtil.checkLogin();
        CreateEditPermitRequestModel permitViewModel = new CreateEditPermitRequestModel();
        var userInfo = _Db.Users.Where(e => e.ID == userid).FirstOrDefault();
        permitViewModel.company
          .CompanyName = userInfo.CompanyName;
        permitViewModel.company.PhoneNumber =
          userInfo.PhoneNumber;
        permitViewModel.company
          .EmailID = userInfo.EmailID;
        permitViewModel.company
          .LicenseNumber = userInfo.LicenseNumber;
        permitViewModel.company
          .VATNumber = userInfo.VATNumber;
        permitViewModel.company.Name = userInfo.Name;

        permitViewModel.company
          .Address = userInfo.Address;
        var clientDocuments = _Db.ClientDocumentDetails.Where(e => e.ClientID == userid).ToList();
        foreach (var item in clientDocuments) {
          switch (item.DocTypeID) {
            case 1:
              permitViewModel.ProcessFlowChartUploadedFromClient = item.DocumentPath;
              break;
            case 2:
              permitViewModel.OnsiteWasteTreamentUploadedFromClient = item.DocumentPath;
              break;

            default:
              break;
          }

        }
        return View(permitViewModel);
      }

    }

    private void PopulateDropDownListForPermit() {
      var wasteCategory = new List<SelectListItem>();
      foreach (var item in _Db.PermitTypeMasters.Where(e=>e.PermitType!= "Lab Analysis & Samplings")) {
        wasteCategory.Add(
          new SelectListItem() {
            Text = item.PermitType,
            Value = item.Amount + "~" + item.ID.ToString()
          }
          );
      }
      var wasteType = new List<SelectListItem>();
      foreach (var item in _Db.PermitWasteMappings.Where(e => e.PermitTypeMasterID == 1)) {
        wasteType.Add(new SelectListItem() {
          Text = item.WasteType,
          Value = item.ID.ToString()

        });
      }
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

      var res = _Db.Entities.Where(e => e.ID == 1).FirstOrDefault();
      ViewBag.r_dcharges = string.Format("{0:n2}", res.r_dcharges);
      ViewBag.servicecharges = string.Format("{0:n2}", res.servicecharges);
      ViewBag.vatcharges = string.Format("{0:n2}", res.vatcharges);

      ViewBag.WasteCategory = wasteCategory;
      ViewBag.WasteTypeId = wasteType;
      ViewBag.PackagingStorage = new List<SelectListItem>();
      foreach (var item in _Db.PackagingStorageMasters.Where(e => e.status == true)) {

        ViewBag.PackagingStorage.Add(new SelectListItem() {
          Text = item.PackagingStorageMaster1,
          Value = item.PackagingStorageMaster1

        });
      }
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateEditPermitRequestModel viewModel) {
      PermitRequest permitRequest = new PermitRequest();
      NormalPermit permitDetail = new NormalPermit();// FOr storing permit details



      using (_Db = new WasteManageEntities()) {
        using (var scope = _Db.Database.BeginTransaction()) {


          try {
            int userid = AppUtil.checkLogin();
            var adminId = AppUtil.GetUserIdByRole(1, _Db);
            //permitRequest.AmountDetails = viewModel.AmountDetails;


            permitRequest.ApplyDate = DateTime.Now;

            permitRequest.TotalWeight = viewModel.TotalWeight;
            permitRequest.WeightUnit = viewModel.UnitOfMeasure;


            permitRequest.PurposeOfSampling = viewModel.samplingpurpose;
            permitRequest.SourceOfSample = viewModel.samplesource;
            permitRequest.Othersource = viewModel.OtherSourceOfSample;
            permitRequest.ApplicantName = viewModel.ApplicantName;

            permitRequest.PermitReferenceID = await GenerateRefrenceNumber(false, _Db);

            permitRequest.EnteredBy = userid;
            permitRequest.UpdatedBy = adminId;
            permitRequest.PermitType = true;
            permitRequest.IsPaymentDone = false;
            permitRequest.StatusID = 1;
            permitRequest.ApplyDate = DateTime.Now;
            permitRequest.UserID = userid;
            permitRequest.EnteredOn = DateTime.Now;
            permitRequest.UpdatedOn = DateTime.Now;

            _Db.PermitRequests.Add(permitRequest);
            await _Db.SaveChangesAsync();


            viewModel.ID = permitRequest.ID;
            permitDetail.WasteLocation = viewModel.facility_location;
            permitDetail.WasteCategory = viewModel.WasteTypeId;

            permitDetail.WasteType = viewModel.WasteCategory;



            permitDetail.SourceProcess = viewModel.SourceProcess;
            permitDetail.PackagingStorage = viewModel.Storage;
            if (string.IsNullOrEmpty(viewModel.Remarks)) {
              viewModel.Remarks = "-";
            }
            permitDetail.Remarks = viewModel.Remarks;
            permitDetail.PermitId = permitRequest.ID;


            _Db.NormalPermits.Add(permitDetail);
            await _Db.SaveChangesAsync();

            var permitInvoice = new Invoice();
            permitInvoice.Invoice_Type = AppVariables.PermitInvoiceType;
            permitInvoice.PermitFees = viewModel.permitfees;
            permitInvoice.InvoiceNo = "INV000" + permitRequest.ID;
            permitInvoice.ServicesFees = viewModel.servicefee;
            permitInvoice.RnDFees = viewModel.rdfee;
            permitInvoice.Vat = viewModel.vat;
            permitInvoice.TotalAmount = viewModel.grandtotal;
            permitInvoice.DueDate = DateTime.Now.AddDays(7);
            permitInvoice.PaymentMethod = "N/A";
            permitInvoice.PermitID = permitRequest.ID;
            permitInvoice.Remarks = "Added from new permit";
            permitInvoice.EnteredBy = userid;
            permitInvoice.EnteredOn = DateTime.Now;
            permitInvoice.UpdatedBy = userid;
            permitInvoice.UpdatedOn = DateTime.Now;

            _Db.Invoices.Add(permitInvoice);
            await _Db.SaveChangesAsync();

            //Helpers.EmailNotification NewEmail = new EmailNotification();
            //if (Convert.ToInt32(Session[AppVariables.SessionUserRole]) != 1002)
            //{
            //    var user = _Db.Users.Where(e => e.ID == 1).FirstOrDefault();
            //    NewEmail.Send(user.EmailID, "New Permit Request", "A new Permit created in Portal, Please login and Review.", true, "");
            //}
            var isSaved = await SavePermitFilesAsync(viewModel);
            if (isSaved) {

              var isSent = SendStatusChangeEmail(permitRequest);

              TempData["message"] = 1;


            } else {

              PopulateDropDownListForPermit();
              ViewBag.error = "Error attaching files! please re-upload and try again!";

              return View(viewModel);
            }

            scope.Commit();
            return RedirectToAction("Index");
          } catch (DbEntityValidationException ex) {

            foreach (var errors in ex.EntityValidationErrors) {
              foreach (var validationError in errors.ValidationErrors) {
                // get the error message 
                string errorMessage = validationError.ErrorMessage;
              }
            }
            PopulateDropDownListForPermit();
            ViewBag.error = ex.ToString();
            AppUtil.ExceptionLog(ex);
            scope.Dispose();
            return View(viewModel);
          }
        }
      }

    }

    private async Task<int> GenerateRefrenceNumber(bool issampling, WasteManageEntities _Db) {
      //string first2digit = "14";
      //string second2digit = DateTime.Now.ToString("yy");
      //string thir2digit = "01";
      //int countofPermit = await _Db.PermitRequests.CountAsync();
      //countofPermit += 1;

      //string lastFourDigit = countofPermit.ToString().PadLeft(4, '0');
      //var refnumber = Convert.ToInt32(first2digit + second2digit + thir2digit + lastFourDigit);
      var refnumber = await _Db.Database.SqlQuery<int>("select case when max(PermitReferenceID) is null then 1420000000 else max(PermitReferenceID)+1 end From PermitRequest").FirstOrDefaultAsync();

      return refnumber;
    }

    private int GenerateRefrenceNumberNormal(bool issampling, WasteManageEntities _Db) {
      //string first2digit = "14";
      //string second2digit = DateTime.Now.ToString("yy");
      //string thir2digit = "01";
      //int countofPermit = await _Db.PermitRequests.CountAsync();
      //countofPermit += 1;

      //string lastFourDigit = countofPermit.ToString().PadLeft(4, '0');
      //var refnumber = Convert.ToInt32(first2digit + second2digit + thir2digit + lastFourDigit);
      var refnumber = _Db.Database.SqlQuery<int>("select case when max(PermitReferenceID) is null then 1420000000 else max(PermitReferenceID)+1 end From PermitRequest").FirstOrDefault();

      return refnumber;
    }

    public async Task<ActionResult> Detail(int id) {

      try {
        using (_Db = new WasteManageEntities()) {
          var statuses = await
             _Db.StatusMasters
             .Where(e => e.status == true)
             .ToListAsync();

          ViewBag.Status = statuses.Select(e =>
           new SelectListItem() {
             Text = e.statusName,
             Value = e.ID.ToString()
           });
          //ViewBag.InvoiceDetails = _Db.Invoices.Where(e => e.PermitID == id).FirstOrDefault();
          int role = ViewBag.Role = AppUtil.getUserRole();
          var permitRequestDB =
            _Db.PermitRequests
              .Where(e => e.ID == id).FirstOrDefault();
          var permitDetailDB =
          await _Db.NormalPermits.Where(e => e.PermitId == id).FirstOrDefaultAsync();

          var permitViewModel = new PermitRequetDetailModel();
          permitViewModel.ID = permitRequestDB.ID;
          permitViewModel.HazardAppNo = permitRequestDB.HazardSAP;
          permitViewModel.WasteDescription = permitRequestDB.WasteGenerationDescription;
          permitViewModel.DisposalFrequency = permitRequestDB.FrequencyOfDisposal;
          permitViewModel.transporter = permitRequestDB.Transporter;
          permitViewModel.transporterDetails = permitRequestDB.HazardTransporter;
          permitViewModel.RejectionReason = permitRequestDB.RejectionReason;
          permitViewModel.StatusID = permitRequestDB.StatusID.HasValue ? permitRequestDB.StatusID.Value : 0;
          permitViewModel.CreatedBy = permitRequestDB.EnteredBy.HasValue ? permitRequestDB.EnteredBy.Value : 0;
          permitViewModel.HazardRemarks = permitRequestDB.HazardRemarks;
          permitViewModel.ConsumedWeight = permitRequestDB.ConsumedWeight;

          int userid = AppUtil.checkLogin();
          var userInfo = await _Db.Users.Where(e => e.ID == permitRequestDB.EnteredBy).FirstOrDefaultAsync();
          permitViewModel.company
          .CompanyName = userInfo.CompanyName;
          permitViewModel.company.PhoneNumber =
            userInfo.PhoneNumber;
          permitViewModel.company
            .EmailID = userInfo.EmailID;
          permitViewModel.company
            .LicenseNumber = userInfo.LicenseNumber;
          permitViewModel.company
            .VATNumber = userInfo.VATNumber;
          permitViewModel.company.Name = userInfo.Name;

          permitViewModel.company
            .Address = userInfo.Address;


          permitViewModel.WasteType = permitDetailDB.WasteType ?? 0;
          var wasteCategoryName =
             _Db.PermitTypeMasters.Where(e => e.ID == permitDetailDB.WasteType).FirstOrDefault();
          if (wasteCategoryName != null) {
            permitViewModel.WasteCategoryName = wasteCategoryName.PermitType;

          }

          var PermitWasteMappingDB =
             _Db.PermitWasteMappings.Where(e => e.ID == permitDetailDB.WasteCategory).FirstOrDefault();
          if (PermitWasteMappingDB != null)
            permitViewModel.WasteTypeName = PermitWasteMappingDB.WasteType;
          permitViewModel.IsHazardousWaste = PermitWasteMappingDB.IsHazardous ?? false;
          permitViewModel.TotalWeight = permitRequestDB.TotalWeight;
          permitViewModel.UnitOfMeasure = permitRequestDB.WeightUnit;
          permitViewModel.SourceProcess = permitDetailDB.SourceProcess;
          permitViewModel.Storage = permitDetailDB.PackagingStorage;
          permitViewModel.Remarks = permitDetailDB.Remarks;
          permitViewModel.facility_location = permitDetailDB.WasteLocation;

          try {
            permitViewModel.PurposeOfSampling = permitRequestDB.PurposeOfSampling;
            permitViewModel.SourceSampling = permitRequestDB.SourceOfSample;
            if (permitRequestDB.PurposeOfSamplingMaster != null)
              permitViewModel.PurposeOfSamplingName = permitRequestDB.PurposeOfSamplingMaster.PurposeOfSampling ?? "";
            if (permitRequestDB.SourceOfSampleMaster != null)
              permitViewModel.SourceSamplingName = permitRequestDB.SourceOfSampleMaster.SourceOfSample ?? "";
            permitViewModel.OtherSource = permitRequestDB.Othersource;
          } catch (Exception ex) {
            AppUtil.ExceptionLog(ex);

          }

          if (permitRequestDB.StatusID.Value == 11) {
            ViewBag.id = id;
            HazardCertificateModel permitRequests = new HazardCertificateModel();

            string query = @"select UserID, WasteGenerationDescription as WasteDescription, FrequencyOfDisposal as DisposalFrequency, Transporter From PermitRequest where ID = " + id;
            ViewBag.hazardsdetail = _Db.Database.SqlQuery<HazardCertificateModel>(query).FirstOrDefault();

            var hazardFiles =
               await _Db.PermitDocumentDetails.Where(e => e.PermitID == id && (e.DocTypeID == 6 || e.DocTypeID == 7)).ToListAsync();
            ViewBag.hazardsdetail.HPhotos = new List<string>();
            ViewBag.hazardsdetail.HMSDS = new List<string>();


            foreach (var item in hazardFiles) {
              if (item.DocTypeID == 6) {
                ViewBag.hazardsdetail.HPhotos.Add(item.DocumentPath);

              } else if (item.DocTypeID == 7) {
                ViewBag.hazardsdetail.HMSDS.Add(item.DocumentPath);
              }
            }
            string haz = @"select Name as name, Quantity as quantity, State as state, ContainerType,QuantityItem quantityItem  From HazardContractItems where PermitID = " + id;
            ViewBag.hazards = _Db.Database.SqlQuery<HazardItems>(haz).ToList();

          }

          permitViewModel.IsPermitApproval = role == 1002 ? true : await AppUtil.checkIsPermission(AppVariables.PermitApproval, _Db);
          permitViewModel.IsSamplingRequest = role == 1002 ? true : await AppUtil.checkIsPermission(AppVariables.SamplingRequest, _Db);
          permitViewModel.IsHazardApproval = role == 1002 ? true : await AppUtil.checkIsPermission(AppVariables.HazardApproval, _Db);
          permitViewModel.IsHazardRequest = role == 1002 ? true : await AppUtil.checkIsPermission(AppVariables.HazardRequest, _Db);
          permitViewModel.IsPaymentApproval = role == 1002 ? true : await AppUtil.checkIsPermission(AppVariables.PaymentApproval, _Db);
          ViewBag.PaymentDetails = _Db.AccountTransactions.Where(e => e.PermitID == id && e.TransactionType != "Sampling").FirstOrDefault();
          //if (permitRequestDB.StatusID == 13) //later Hard coded status ID
          //{
          //	ViewBag.PaymentDetails = _Db.AccountTransactions.Where(e => e.PermitID == id).FirstOrDefault();
          //}

          //if (permitViewModel.IsSamplingRequest) //later Hard coded status ID
          //{
          ViewBag.SampleDetails = _Db.SamplingRequests.Where(e => e.PermitID == id).
            OrderByDescending(e => e.RequestDate)
            .FirstOrDefault();
          if (ViewBag.SampleDetails != null) {
            permitViewModel.SamplingId = ViewBag.SampleDetails.ID;
          }
          ViewBag.SamplePaymentDetails = _Db.AccountTransactions.Where(e => e.PermitID == id && e.TransactionType == AppVariables.SamplingInvoiceType).OrderByDescending(e => e.ID).FirstOrDefault();
          //}
          var invoice = await _Db.Invoices.Where(e => e.PermitID == id).FirstOrDefaultAsync();
          if (invoice != null) {
            permitViewModel.InvoiceId = invoice.InvoiceNo;
            switch (invoice.PaymentMethod) {
              case "Cheque":
                permitViewModel.PaymentMethod = "Cheque";
                break;
              case "CreditCard":
                permitViewModel.PaymentMethod = "Credit Card";
                break;
              case "AccountBalance":
                permitViewModel.PaymentMethod = "Wallet";
                break;


              default:
                break;
            }
          }

          ////Populate TimelineVIew
          ///
          permitViewModel.History = new List<TimelineViewModel>();
          permitViewModel.History =
          await _Db.Database.SqlQuery<TimelineViewModel>($"select Table_Name,Old_Value,New_Value,Action_Type,us.UserName,st.Description," +
               $" ad.Created_Timestamp From Audit_trail ad inner join Users us on ad.Created_By=us.Id" +
               $" left join StatusMaster st on  ad.New_Value= st.ID where Table_Name='PermitRequest'" +
               $" and Primary_Key={id} and Column_Name='StatusID' order by Created_Timestamp Desc").ToListAsync();

          var sampling = await _Db.SamplingRequests
                .Where(e => e.PermitID == id).OrderBy(e => e.ID).ToListAsync();

          AssociateSamplingWithTimeLine(permitViewModel.History, sampling);
          var permitImages = await _Db.PermitDocumentDetails.Where(e => e.PermitID == id).ToListAsync();
          if (permitImages.Any()) {
            foreach (var item in permitImages.Where(e => e.DocTypeID == 3)) {
              permitViewModel.PermitImages.Add(item.DocumentPath);
            }
            foreach (var item in permitImages.Where(e => e.DocTypeID == 5)) {
              permitViewModel.MSDS = item.DocumentPath;
            }
          }

          permitRequestDB.IsRead = true;
          _Db.PermitRequests.Attach(permitRequestDB);
          _Db.Entry(permitRequestDB).State = System.Data.Entity.EntityState.Modified;
          _Db.SaveChanges();
          PopulateDropDownListForPermit();
          return View(permitViewModel);
        }
      } catch (DbEntityValidationException ex) {
        string errorMessage = "";
        foreach (var errors in ex.EntityValidationErrors) {
          foreach (var validationError in errors.ValidationErrors) {
            // get the error message 
            errorMessage = validationError.ErrorMessage;
          }
        }
        TempData["actual"] = errorMessage;
        TempData["message"] = "3";
        AppUtil.ExceptionLog(ex);
        return RedirectToAction("Index");
      }

    }


    public async Task<ActionResult> ChangePermitStatus(int id, int statusId, int? samplingpurpose, int? samplesource, string Remarks, string OtherSourceOfSample, string RejectionReason, bool? samplingrequired, bool? hazardrequired, bool? moresamplingrequired, string hazardRemarks,string samplingRemarks) {
      if (Session[AppVariables.SessionUserRole] != null) {
        int roleid = Convert.ToInt16(Session[AppVariables.SessionUserRole]);
        //if (roleid == 1002 || roleid == 1)
        //{
        using (_Db = new WasteManageEntities()) {
          var permitRequest =
            await _Db.PermitRequests.Where(e => e.ID == id).FirstOrDefaultAsync();
          if (permitRequest != null) {
            permitRequest.StatusID = GetPermitStatus(statusId, samplingrequired, hazardrequired, moresamplingrequired);
            if (permitRequest.StatusID == 2) {
              permitRequest.PurposeOfSampling = samplingpurpose;
              permitRequest.SourceOfSample = samplesource;
              permitRequest.Othersource = OtherSourceOfSample;
            }
            if (permitRequest.StatusID == 14) {
              var Invoice = _Db.Invoices.Where(e => e.PermitID == id).FirstOrDefault();
              var InvoiceID = Invoice.ID;
              var transaction = _Db.AccountTransactions.Where(e => e.InoviceID == InvoiceID).FirstOrDefault();
              /// Update PermitRequests Data
              permitRequest.IsPaymentDone = true;
              /// Update Invoices Data
              Invoice.PaymentDate = DateTime.Now;
              Invoice.PaymentMethod = transaction.PaymentMethod;
              _Db.Invoices.Attach(Invoice);
              _Db.Entry(Invoice).State = System.Data.Entity.EntityState.Modified;
             // _Db.SaveChanges();
              /// Update AccountTransactions Data
              transaction.Status = true;
              _Db.AccountTransactions.Attach(transaction);
              _Db.Entry(transaction).State = System.Data.Entity.EntityState.Modified;
             // await _Db.SaveChangesAsync();

            }

            if (permitRequest.StatusID == 7) {
              try {


                SAPOrderData Data = new SAPOrderData();
                string query = @"select inv.PermitID, 
								SAPCustomerId as SAPCustomerID,
								CASE When pr.PermitType = 1 
			  Then (
			  Select ptm.ItemCode from NormalPermit np 
			  Join PermitWasteMapping ptm on np.WasteCategory = ptm.ID
			  where np.PermitId = inv.PermitID
			  )
			  When pr.PermitType != 1 
			  Then 
			  (
			  Select top 1 mi.ItemCode AS csv from PermitItems np 
			  Join MasterItems mi on np.ItemID = mi.ID
			  where np.PermitID = inv.PermitID
			  )
			  End as ProductCode,
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
			  where inv.PermitID = " + id + " and Invoice_Type = 'Permit' ";
                Data = await _Db.Database.SqlQuery<SAPOrderData>(query).FirstOrDefaultAsync();
                string SalesOrder = CallinSAPI.GenerateOrder(Data);
                if (SalesOrder == null) {
                  throw new Exception("Unable to store sales order in SAP");
                } else {
                  var inv = _Db.Invoices.Where(x => x.PermitID == id && x.Invoice_Type== "Permit").OrderByDescending(e => e.ID).FirstOrDefault();
                  inv.SalesOrder = SalesOrder;
                  _Db.Entry(inv).State = EntityState.Modified;
                  //await _Db.SaveChangesAsync();
                }
              } catch (Exception ex) {

                AppUtil.ExceptionLog(ex);
              }
              permitRequest.IssueDate = DateTime.Now;
              permitRequest.IssueBy = AppUtil.checkLogin();
              permitRequest.ApproveBy = AppUtil.checkLogin();
              //Permit Approve Add dates for it
              if (permitRequest.PermitType == false)//FDC later
              {
                var permitdetails = _Db.FDCPermits.Where(e => e.PermitId == id).First();
              } else if (permitRequest.PermitType == true) //Normal 
                {
                int Validity = 12;

                var normalPermit = await _Db.NormalPermits.Where(e => e.PermitId == id).FirstOrDefaultAsync();
                if (normalPermit != null) {
                  var isHazardous =
                     await _Db.PermitWasteMappings.Where(e => e.ID == normalPermit.WasteCategory && e.IsHazardous.Value).AnyAsync();
                  if (normalPermit.WasteType == 4) {
                    Validity = 999;
                  } else if (isHazardous) {
                    Validity = 3;

                  } else {
                    Validity =
                  _Db.PermitTypeMasters.Where(e => e.ID == normalPermit.WasteType).First().Vallidity ?? 12;
                  }

                }

                if (Validity == 999) {
                  permitRequest.ValidTill = new DateTime(1900, 1, 1);
                } else {
                  permitRequest.ValidTill = DateTime.Now.AddMonths(Validity);

                }

              }
            }

            if (permitRequest.StatusID == 12 || permitRequest.StatusID == 10 || permitRequest.StatusID == 4) {
              var SampleDetails = _Db.SamplingRequests.Where(e => e.PermitID == id).FirstOrDefault();
              if (SampleDetails != null) {
                if (SampleDetails.StatusID != 1) {
                  SampleDetails.StatusID = 1;
                  SampleDetails.UpdatedBy = AppUtil.checkLogin();
                  SampleDetails.UpdatedOn = DateTime.Now;
                  _Db.SamplingRequests.Attach(SampleDetails);
                  _Db.Entry(SampleDetails).State = EntityState.Modified;
                 // await _Db.SaveChangesAsync();

                  var SamplePaymentDetails = _Db.AccountTransactions.Where(e => e.PermitID == id && e.TransactionType == AppVariables.SamplingInvoiceType).FirstOrDefault();
                  SamplePaymentDetails.Status = true;
                  SamplePaymentDetails.UpdatedBy = AppUtil.checkLogin();
                  SamplePaymentDetails.UpdatedOn = DateTime.Now;
                  _Db.AccountTransactions.Attach(SamplePaymentDetails);
                  _Db.Entry(SamplePaymentDetails).State = EntityState.Modified;
                //  await _Db.SaveChangesAsync();

                  var SamplePaymentinvoice = _Db.Invoices.Where(e => e.ID == SamplePaymentDetails.InoviceID).FirstOrDefault();
                  SamplePaymentinvoice.PaymentDate = SamplePaymentDetails.TransactionDate;
                  SamplePaymentinvoice.PaymentMethod = SamplePaymentDetails.PaymentMethod;

                  SamplePaymentinvoice.UpdatedBy = AppUtil.checkLogin();
                  SamplePaymentinvoice.UpdatedOn = DateTime.Now;
                  _Db.Invoices.Attach(SamplePaymentinvoice);
                  _Db.Entry(SamplePaymentinvoice).State = EntityState.Modified;
                 // await _Db.SaveChangesAsync();
                }
              }
            }
            if (!string.IsNullOrEmpty(Remarks)) {
              var permitDetailDB = _Db.NormalPermits.Where(e => e.PermitId == id).First();
              permitDetailDB.Remarks = Remarks;

              _Db.NormalPermits.Attach(permitDetailDB);
              _Db.Entry(permitDetailDB).State = EntityState.Modified;
              //await _Db.SaveChangesAsync();
            }
            if (permitRequest.StatusID == 12) {
              
            }
            if (permitRequest.StatusID == 5) {
              permitRequest.RejectionReason = RejectionReason;
            }

            if (permitRequest.StatusID == 17) {
              permitRequest.HazardRemarks = hazardRemarks;
              permitRequest.UpdatedBy = AppUtil.checkLogin();

            }
            if (!String.IsNullOrEmpty(samplingRemarks)) {
              //Weight bridge remarks
              permitRequest.WBRemarks = samplingRemarks;
            }
            permitRequest.UpdatedOn = DateTime.Now;
            if (AppUtil.getUserRole() != 3 && AppUtil.getUserRole() != 1003) {
              permitRequest.UpdatedBy = AppUtil.checkLogin();
            }

            permitRequest.IsRead = false;
            _Db.PermitRequests.Attach(permitRequest);
            _Db.Entry(permitRequest).State = EntityState.Modified;

            await _Db.SaveChangesAsync();
            SendStatusChangeEmail(permitRequest);
            TempData["message"] = "4";
            return RedirectToAction("Index");

          } else {
            return RedirectToAction("Index");
          }

        }
        //}
        //else
        //{
        //	return RedirectToAction("Index");
        //}
      } else {
        return Redirect("/");
      }

    }


    public async Task<ActionResult> ChangePostPermitStatus(int id, int statusid) {
      if (Session[AppVariables.SessionUserRole] != null) {
        int roleid = Convert.ToInt16(Session[AppVariables.SessionUserRole]);
        int userid = AppUtil.checkLogin();
        using (_Db = new WasteManageEntities()) {
          var permitRequest =
          await _Db.PermitRequests.Where(e => e.ID == id).FirstOrDefaultAsync();
          switch (statusid) {
            case 18:
              //Meaning the sampling is accepted

              SendStatusChangeEmailForPostPermit(permitRequest, 18);

              break;
            case 19:
              SendStatusChangeEmailForPostPermit(permitRequest, 19);
              break;
            default:
              break;
          }
          // check what is the status
          permitRequest.StatusID = 7;
          permitRequest.UpdatedBy = userid;
          permitRequest.UpdatedOn = DateTime.Now;


          _Db.Entry(permitRequest).State = EntityState.Modified;
          await _Db.SaveChangesAsync();

          // else reject 


        }
        TempData["message"] = "4";
        return RedirectToAction("Index");

      } else {
        return Redirect("/");

      }

    }


    private int? GetPermitStatus(int statusId, bool? samplingrequired, bool? hazardrequired, bool? moresamplingrequired) {

      if (samplingrequired.HasValue && hazardrequired.HasValue) {
        if (samplingrequired.Value) {
          return 2;
        } else if (hazardrequired.Value) {
          return 10;

        } else {
          return statusId;
        }
      }
      if (samplingrequired.HasValue) {
        if (samplingrequired.Value) {
          return 2;
        } else
          return statusId;
      } else if (hazardrequired.HasValue && hazardrequired.Value) {
        if (hazardrequired.Value)
          return 10;
        else
          return statusId;
      } else if (moresamplingrequired.HasValue) {
        if (moresamplingrequired.Value)
          return 4;
        //return 12;
        else
          return statusId;
      } else {
        return statusId;
      }
    }

    public async Task<ActionResult> DeletePermit(int id, string type) {

      using (_Db = new WasteManageEntities()) {

        if (id != 0) {
          switch (type) {
            case "Permit":

              var permit = await
                _Db.PermitRequests.Where(e => e.ID == id).FirstOrDefaultAsync();
              if (permit != null) {
                var accountTransactions = await _Db.AccountTransactions.Where(e => e.PermitID == id).ToListAsync();
                if (accountTransactions != null) {
                  foreach (var item in accountTransactions) {
                    _Db.Entry(item).State = EntityState.Deleted;
                  }

                }
                var invoices = await _Db.Invoices.Where(e => e.PermitID == id).ToListAsync();
                if (invoices != null) {
                  foreach (var item in invoices) {
                    _Db.Entry(item).State = EntityState.Deleted;
                  }


                }
                var sampling = await _Db.SamplingRequests.Where(e => e.PermitID == id).ToListAsync();
                foreach (var item in sampling) {
                  _Db.Entry(item).State = EntityState.Deleted;
                }

                var hazardItems = await _Db.HazardContractItems.Where(e => e.PermitID == id).ToListAsync();
                if (hazardItems != null) {
                  foreach (var item in hazardItems) {
                    _Db.Entry(item).State = EntityState.Deleted;

                  }
                }

                _Db.Entry(permit).State = EntityState.Deleted;
                await _Db.SaveChangesAsync();
                TempData["message"] = 5;
                return RedirectToAction("Index");
              }

              break;



            case "FDC":
              var permitRequest = await
              _Db.PermitRequests.Where(e => e.ID == id).FirstOrDefaultAsync();
              if (permitRequest != null) {
                var accountTransactions = await _Db.AccountTransactions.Where(e => e.PermitID == id).ToListAsync();
                if (accountTransactions != null) {
                  foreach (var item in accountTransactions) {
                    _Db.Entry(item).State = EntityState.Deleted;

                  }
                }
                var invoices = await _Db.Invoices.Where(e => e.PermitID == id).ToListAsync();
                if (invoices != null) {
                  foreach (var item in invoices) {
                    _Db.Entry(item).State = EntityState.Deleted;
                  }


                }
                var permitItems = await _Db.PermitItems.Where(e => e.PermitID == id).ToListAsync();
                if (permitItems != null) {
                  foreach (var item in permitItems) {
                    _Db.Entry(item).State = EntityState.Deleted;
                  }

                }
                var sampling = await _Db.SamplingRequests.Where(e => e.PermitID == id).ToListAsync();
                foreach (var item in sampling) {
                  _Db.Entry(item).State = EntityState.Deleted;
                }
                _Db.Entry(permitRequest).State = EntityState.Deleted;
                await _Db.SaveChangesAsync();
                TempData["message"] = 5;
                return RedirectToAction("FDCList");
              }
              break;


            default:
              break;
          }


        }
      }
      string redirec = "Index";
      TempData["message"] = 3;
      if (type == "FDC") {

        redirec = "FDCList";
      }
      return RedirectToAction(redirec);

    }
    #endregion








    #region Other Methods-----------------------------------------------------------

    public async Task<ActionResult>TestWeighBridge (){
      using (var client =new HttpClient()) {
        client.BaseAddress = new Uri("http://83.111.22.127:8070");
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new  MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage Res = await client.GetAsync("api/GetWeight/911");
        if (Res.IsSuccessStatusCode) {
          //Storing the response details recieved from web api   
          var EmpResponse = Res.Content.ReadAsStringAsync().Result;

          //Deserializing the response recieved from web api and storing into the Employee list  
          

        }

      }


      return View();
    }


    #region Upload Files for Permit
    [HttpPost]
    public ActionResult UploadPermitFiles() {
      //
      //Code credits: https://www.c-sharpcorner.com/UploadFile/manas1/upload-files-through-jquery-ajax-in-Asp-Net-mvc/


      if (Request.Files.Count > 0) {
        try {
          //  Get all files from Request object  
          HttpFileCollectionBase files = Request.Files;
          List<string> FilePaths = new List<string>();
          for (int i = 0; i < files.Count; i++) {
            HttpPostedFileBase file = files[i];
            string fname;
            // Checking for Internet Explorer  
            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER") {
              string[] testfiles = file.FileName.Split(new char[] { '\\' });
              fname = testfiles[testfiles.Length - 1];
            } else {
              fname = file.FileName;
            }
            fname = "permit" + DateTimeOffset.UtcNow.ToUnixTimeSeconds() + "_" + fname;
            var fullname = Path.Combine(Server.MapPath("~/UploadedFiles/Temp"), fname);
            file.SaveAs(fullname);
            FilePaths.Add(fname);
          }

          return Json(FilePaths);

        } catch (Exception ex) {

          return Json("error-" + ex.ToString());
        }
        return Json("FIlesname");

      } else {
        return Json("error-No files found");
      }

    }
    private async Task<bool> SavePermitFilesAsync(CreateEditPermitRequestModel viewModel) {
      try {
        List<string> documentToMove = new List<string>();
        if (viewModel.PermitImages != null) {
          foreach (var item in viewModel.PermitImages) {
            documentToMove.Add(item);
            var documentDetail = new PermitDocumentDetail() {

              CreatedBy = AppUtil.checkLogin(),
              CreatedOn = DateTime.Now,
              DocTypeID = 3,
              DocumentPath = item,
              PermitID = viewModel.ID,
              UpdatedBy = AppUtil.checkLogin(),
              UpdatedOn = DateTime.Now
            };


            _Db.PermitDocumentDetails.Add(documentDetail);
            await _Db.SaveChangesAsync();

          }
        }

        if (!String.IsNullOrEmpty(viewModel.MSDS)) {
          var documentDetail = new PermitDocumentDetail() {

            CreatedBy = AppUtil.checkLogin(),
            CreatedOn = DateTime.Now,
            DocTypeID = 5,
            DocumentPath = viewModel.MSDS,
            PermitID = viewModel.ID,
            UpdatedBy = AppUtil.checkLogin(),
            UpdatedOn = DateTime.Now
          };
          documentToMove.Add(viewModel.MSDS);
          _Db.PermitDocumentDetails.Add(documentDetail);
          await _Db.SaveChangesAsync();
        }

        foreach (var item in documentToMove) {
          string Destinationdirectory = Server.MapPath(string.Format("~/UploadedFiles/Permit/{0}/", viewModel.ID));

          if (!Directory.Exists(Destinationdirectory)) {
            Directory.CreateDirectory(Destinationdirectory);
          }
          string source = Path.Combine(Server.MapPath("~/UploadedFiles/Temp"), item);
          string destination = Path.Combine(Destinationdirectory, item);

          if (!System.IO.File.Exists(destination)) {
            System.IO.File.Move(source, destination);
          }
        }
        return true;
      } catch (Exception ex) {
        AppUtil.ExceptionLog
          (ex);
        return false;
      }
    }
    #endregion


    #region Hazard Waste Contract


    #endregion

    public async Task<PermitRequetDetailModel> GetPermitDetails(int id) {

      var permitRequestDB = await
        _Db.PermitRequests
        .Where(e => e.ID == id).FirstOrDefaultAsync();
      var permitDetailDB = await
         _Db.NormalPermits.Where(e => e.PermitId == id).FirstOrDefaultAsync();

      var permitViewModel = new PermitRequetDetailModel();
      permitViewModel.ID = permitRequestDB.ID;
      permitViewModel.StatusID = permitRequestDB.StatusID.HasValue ? permitRequestDB.StatusID.Value : 0;
      permitViewModel.CreatedBy = permitRequestDB.EnteredBy.HasValue ? permitRequestDB.EnteredBy.Value : 0;
      permitViewModel.PermitRefNo = permitRequestDB.PermitReferenceID.HasValue?
        "0": permitRequestDB.PermitReferenceID.Value.ToString();

      int userid = AppUtil.checkLogin();
      var userInfo = await _Db.Users.Where(e => e.ID == permitRequestDB.EnteredBy).FirstOrDefaultAsync();
      permitViewModel.company
      .CompanyName = userInfo.CompanyName;
      permitViewModel.company.PhoneNumber =
        userInfo.PhoneNumber;
      permitViewModel.company
        .EmailID = userInfo.EmailID;
      permitViewModel.company
        .LicenseNumber = userInfo.LicenseNumber;
      permitViewModel.company
        .VATNumber = userInfo.VATNumber;
      permitViewModel.company.Name = userInfo.Name;

      permitViewModel.company
        .Address = userInfo.Address;
      var permitTypeMasterDB = await
         _Db.PermitTypeMasters.Where(e => e.ID == permitDetailDB.WasteType).FirstOrDefaultAsync();
      if (permitTypeMasterDB != null) {
        permitViewModel.WasteCategoryName = permitTypeMasterDB.PermitType;

      }

      var PermitWasteMappingDB
        = _Db.PermitWasteMappings.Where(e => e.ID == permitDetailDB.WasteCategory).FirstOrDefault();

      if (PermitWasteMappingDB != null) {
        permitViewModel.WasteTypeName = PermitWasteMappingDB.WasteType;

      }

      permitViewModel.TotalWeight = permitRequestDB.TotalWeight;
      permitViewModel.UnitOfMeasure = permitRequestDB.WeightUnit;
      permitViewModel.SourceProcess = permitDetailDB.SourceProcess;
      permitViewModel.Storage = permitDetailDB.PackagingStorage;
      permitViewModel.Remarks = permitDetailDB.Remarks;
      permitViewModel.facility_location = permitDetailDB.WasteLocation;
      permitViewModel.ValidTill = permitRequestDB.ValidTill;
      permitViewModel.IssueDate = permitRequestDB.IssueDate;
      permitViewModel.WBRemarks = permitRequestDB.WBRemarks;

      try {
        permitViewModel.PurposeOfSampling = permitRequestDB.PurposeOfSampling;
        permitViewModel.SourceSampling = permitRequestDB.SourceOfSample;
        if (permitRequestDB.PurposeOfSamplingMaster != null)
          permitViewModel.PurposeOfSamplingName = permitRequestDB.PurposeOfSamplingMaster.PurposeOfSampling ?? "";
        if (permitRequestDB.SourceOfSampleMaster != null)
          permitViewModel.SourceSamplingName = permitRequestDB.SourceOfSampleMaster.SourceOfSample ?? "";
        permitViewModel.OtherSource = permitRequestDB.Othersource;
      } catch (Exception ex) {

        AppUtil.ExceptionLog(ex);
      }

      //if (permitRequestDB.StatusID.Value == 11) {
      //  ViewBag.id = id;
      //  HazardCertificateModel permitRequests = new HazardCertificateModel();

      //  string query = @"select UserID, WasteGenerationDescription as WasteDescription, FrequencyOfDisposal as DisposalFrequency, Transporter From PermitRequest where ID = " + id;
      //  ViewBag.hazardsdetail = _Db.Database.SqlQuery<HazardCertificateModel>(query).FirstOrDefault();

      //  string haz = @"select Name as name, Quantity as quantity, State as state, ContainerType From HazardContractItems where PermitID = " + id;
      //  ViewBag.hazards = _Db.Database.SqlQuery<HazardItems>(haz).ToList();

      //}

      return permitViewModel;

    }
    private void AssociateSamplingWithTimeLine(List<TimelineViewModel> history, List<SamplingRequest> sampling) {
      foreach (var item in history) {
        if (item.New_Value == "3") {
          if (sampling != null && sampling.Count > 0) {
            item.attachedSampling =
                     sampling.Last();
            sampling.Remove(item.attachedSampling);
          }

        }
      }
    }

    [HttpGet]
    public async Task<ActionResult> CertficateTestView(int id) {
      var newPermitRequest = new PermitRequetDetailModel();
      using (_Db = new WasteManageEntities()) {

        newPermitRequest = await GetPermitDetails(id);
      }

      ViewData.Model = newPermitRequest;

      return View("~/Views/Permit/_Certificate.cshtml", newPermitRequest);

    }
    [HttpGet]
    public ActionResult CertficateFDCTestView(int id) {
      var newPermitRequest = new FDCRequestDetailModel();
      using (_Db = new WasteManageEntities()) {

        newPermitRequest = GetFDCDetail(id);
      }

      ViewData.Model = newPermitRequest;

      return View("~/Views/Permit/_FDCCertificate.cshtml", newPermitRequest);

    }
    public bool SendStatusChangeEmail(PermitRequest permitRequest,bool isFDC=false) {
      Helpers.EmailNotification NewEmail = new EmailNotification();
      //
      // Getting Html Files for Email
      var StatusMasters = _Db.StatusMasters.Where(e => e.ID == permitRequest.StatusID).FirstOrDefault();
      var FileForUserEmail = StatusMasters.UserEmailFileLink;
      var FileForClientEmail = StatusMasters.ClientEmailFileLink;


      //Data Required ApplicantName, PermitID, statusName
      var StatusName = StatusMasters.Description;
      string ApplicantName = permitRequest.ApplicantName;
      int permitID = permitRequest.ID;
      string ActivationUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));


      /// Setting Up Email for Client
      if (FileForClientEmail != "" && fileexist("Assets/mails/" + FileForClientEmail) == true) {
        var ClientMasters = _Db.Users.Where(e => e.ID == permitRequest.UserID).FirstOrDefault(); // Getting Client Data For emailing

        string EmailHtmlBody = System.IO.File.ReadAllText(Server.MapPath(@"~/Assets/mails/" + FileForClientEmail)); //Getting Html

        string EmailBody = EmailHtmlBody.Replace("{StatusName}", StatusName);
        EmailBody = EmailBody.Replace("{ApplicantName}", ApplicantName);
        EmailBody = EmailBody.Replace("{ActivationUrl}", ActivationUrl);
        EmailBody = EmailBody.Replace("{permitID}", permitID.ToString());
        if (isFDC) {
          EmailBody = EmailBody.Replace("Permit", "FDC Permit");
          EmailBody = EmailBody.Replace("permit", "FDC Permit");

        }

        NewEmail.Send(ClientMasters.EmailID, "Status Changed To " + StatusName, EmailBody, true, "", permitID); //New User verification Email
      }

      /// Setting Up Email for Beea'h User
      if (FileForUserEmail != "" && fileexist("Assets/mails/" + FileForUserEmail) == true) {
        var userList = new List<string>();
        if (StatusMasters.ReceiverRoles == null) {
          userList.Add("1");
        } else {
          if (StatusMasters.ReceiverRoles.Contains(",")) {
            userList =
               StatusMasters.ReceiverRoles.Split(',').ToList<string>();
          } else {
            userList.Add(StatusMasters.ReceiverRoles);
          }
        }
        StringBuilder query = new StringBuilder();

        query.Append(@"select ur.Id,
						UserName,Password,Name,
						PhoneNumber,EmailID,
						CompanyName,LicenseNumber,
						TradeLicenseFile,VATNumber,
						VATLicenseFile,Address,Address2,Status,BlockChainCustomerId,
						SAPCustomerId,EntityID,ur.CreatedBy,ur.CreatedOn
						,ur.UpdatedBy,ur.UpdatedOn
						 from Users ur left join  UserRoleMapping role on ur.ID=role.UserID 
					where role.RoleID in 		");
        int counter = 0;
        foreach (var item in userList) {
          if (counter == 0) {
            query.Append("(");
          } else {
            query.Append(",");
          }
          query.Append("'" + item + "'");
          counter++;
        }
        query.Append(")");
        try {
          var queryValue = query.ToString();
          var UsersMasters = _Db.Database.SqlQuery<User>(queryValue)
        .ToList();// Getting User Data For emailing


          string EmailHtmlBody = "";
          string EmailBody;

          foreach (var item in UsersMasters) {
            EmailHtmlBody = System.IO.File.ReadAllText(Server.MapPath(@"~/Assets/mails/" + FileForUserEmail)); //Getting Html

            EmailBody = EmailHtmlBody.Replace("{StatusName}", StatusName);
            EmailBody = EmailBody.Replace("{ApplicantName}", ApplicantName);
            EmailBody = EmailBody.Replace("{ActivationUrl}", ActivationUrl);
            EmailBody = EmailBody.Replace("{Name}", item.Name);
            EmailBody = EmailBody.Replace("{permitID}", permitID.ToString());
            if (isFDC) {
              EmailBody = EmailBody.Replace("Permit", "FDC Permit");
              EmailBody = EmailBody.Replace("permit", "FDC Permit");

            }
            NewEmail.Send(item.EmailID, "Status Changed To " + StatusName, EmailBody, true, "", permitID); //New User verification Email
          }
        } catch (Exception ex) {

          AppUtil.ExceptionLog(ex);
        }



      }

      return true;
    }


    public bool SendStatusChangeEmailForPostPermit(PermitRequest permitRequest, int statusid) {
      Helpers.EmailNotification NewEmail = new EmailNotification();
      //
      // Getting Html Files for Email

      var FileForUserEmail = "";
      var FileForClientEmail = "";
      var StatusName = "";
      switch (statusid) {
        case 18:
          FileForClientEmail = "UserSamplingAccepted.html";
          StatusName = "Sampling Request Accepted";
          break;
          ;
        case 19:
          FileForClientEmail = "UserSampleRejected.html";
          StatusName = "Sampling Request Rejected";
          break;
        default:
          break;
      }
      //Data Required ApplicantName, PermitID, statusName

      string ApplicantName = permitRequest.ApplicantName;
      int permitID = permitRequest.ID;
      string ActivationUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));


      /// Setting Up Email for Client
      if (FileForClientEmail != "" && fileexist("Assets/mails/" + FileForClientEmail) == true) {
        var ClientMasters = _Db.Users.Where(e => e.ID == permitRequest.UserID).FirstOrDefault(); // Getting Client Data For emailing

        string EmailHtmlBody = System.IO.File.ReadAllText(Server.MapPath(@"~/Assets/mails/" + FileForClientEmail)); //Getting Html

        string EmailBody = EmailHtmlBody.Replace("{StatusName}", StatusName);
        EmailBody = EmailBody.Replace("{ApplicantName}", ApplicantName);
        EmailBody = EmailBody.Replace("{ActivationUrl}", ActivationUrl);
        EmailBody = EmailBody.Replace("{permitID}", permitID.ToString());
        NewEmail.Send(ClientMasters.EmailID, "Status Changed To " + StatusName, EmailBody, true, "", permitID); //New User verification Email
      }

      /// Setting Up Email for Beea'h User
      if (FileForUserEmail != "" && fileexist("Assets/mails/" + FileForUserEmail) == true) {
        //var userList = new List<string>();
        //if (StatusMasters.ReceiverRoles == null) {
        //	userList.Add("1");
        //} else {
        //	if (StatusMasters.ReceiverRoles.Contains(",")) {
        //		userList =
        //			 StatusMasters.ReceiverRoles.Split(',').ToList<string>();
        //	} else {
        //		userList.Add(StatusMasters.ReceiverRoles);
        //	}
        //}
        //StringBuilder query = new StringBuilder();

        //query.Append(@"select ur.Id,
        //		UserName,Password,Name,
        //		PhoneNumber,EmailID,
        //		CompanyName,LicenseNumber,
        //		TradeLicenseFile,VATNumber,
        //		VATLicenseFile,Address,Address2,Status,BlockChainCustomerId,
        //		SAPCustomerId,EntityID,ur.CreatedBy,ur.CreatedOn
        //		,ur.UpdatedBy,ur.UpdatedOn
        //		 from Users ur left join  UserRoleMapping role on ur.ID=role.UserID 
        //	where role.RoleID in 		");
        //int counter = 0;
        //foreach (var item in userList) {
        //	if (counter == 0) {
        //		query.Append("(");
        //	} else {
        //		query.Append(",");
        //	}
        //	query.Append("'" + item + "'");
        //	counter++;
        //}
        //query.Append(")");
        //try {
        //	var queryValue = query.ToString();
        //	var UsersMasters = _Db.Database.SqlQuery<User>(queryValue)
        //.ToList();// Getting User Data For emailing


        //	string EmailHtmlBody = "";
        //	string EmailBody;

        //	foreach (var item in UsersMasters) {
        //		EmailHtmlBody = System.IO.File.ReadAllText(Server.MapPath(@"~/Assets/mails/" + FileForUserEmail)); //Getting Html

        //		EmailBody = EmailHtmlBody.Replace("{StatusName}", StatusName);
        //		EmailBody = EmailBody.Replace("{ApplicantName}", ApplicantName);
        //		EmailBody = EmailBody.Replace("{ActivationUrl}", ActivationUrl);
        //		EmailBody = EmailBody.Replace("{Name}", item.Name);
        //		EmailBody = EmailBody.Replace("{permitID}", permitID.ToString());
        //		NewEmail.Send(item.EmailID, "Status Changed To " + StatusName, EmailBody, true, "", permitID); //New User verification Email
        //	}
        //} catch (Exception ex) {

        //	AppUtil.ExceptionLog(ex);
        //}



      }

      return true;
    }

    public bool fileexist(string link) {
      string urls = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
      var url = urls + link;
      HttpWebResponse response = null;
      var request = (HttpWebRequest)WebRequest.Create(url);
      request.Method = "HEAD";
      bool resp = false;

      try {
        response = (HttpWebResponse)request.GetResponse();
        resp = true;
      } catch (WebException ex) {
        AppUtil.ExceptionLog(ex);
        resp = false;
        /* A WebException will be thrown if the status of the response is not `200 OK` */
      } finally {
        // Don't forget to close your response.
        if (response != null) {
          response.Close();
        }
      }
      return resp;
    }




    public JsonResult GetWasteDescription(int id) {
      using (_Db = new WasteManageEntities()) {

        var permitDetail = _Db.NormalPermits.Where(e => e.PermitId == id).FirstOrDefault();
        var permit = _Db.PermitRequests.Where(p => p.ID == id).FirstOrDefault();
        var isFDC = !permit.PermitType;
        var isHazard = false;
        var permittype = "";
        if (permitDetail != null || isFDC) {
          if (isFDC) {
            permittype = "FDC";

          } else {
            var PermitTypeMasterDB = isFDC ? null : _Db.PermitTypeMasters.Where(e => e.ID == permitDetail.WasteType).FirstOrDefault();
            var permitwastempping = isFDC ? null : _Db.PermitWasteMappings.Where(e => e.ID == permitDetail.WasteCategory).FirstOrDefault();
            if (permitwastempping != null) {
              if (permitwastempping.IsHazardous.HasValue) {
                isHazard = permitwastempping.IsHazardous.Value;
              }
            }
            permittype = PermitTypeMasterDB.PermitType;
          }

          var SourceofSample = permit.SourceOfSample;
          var purposeofsample = permit.PurposeOfSampling;
          var otherSourceofSample =
            permit.Othersource;



          return Json(new {
            PermitType = permittype,
            SourceSample = SourceofSample == null ? "" : SourceofSample.ToString(),
            PurposeOfSample = purposeofsample == null ? "" : purposeofsample.ToString(),
            OtherSource = otherSourceofSample,
            IsHazard = isHazard
          }, JsonRequestBehavior.AllowGet);


        } else {
          return Json("No Description found", JsonRequestBehavior.AllowGet);
        }
      }
    }

    public string GetTypeD(int id) {
      string result = "<option value=\"\">Select Waste Type</option>";

      using (_Db = new WasteManageEntities()) {
        var permitDetail = _Db.PermitWasteMappings.Where(e => e.PermitTypeMasterID == id).ToList();
        foreach (var item in permitDetail) {
          result = result + "<option value=\"" + item.ID + "\">" + item.WasteType + "</option>";
        }
      }
      return result;
    }

    [HttpGet]
    public async Task<ActionResult> GenerateCertificatePDF(int id) {
      ViewBag.SuccessMessage = string.Empty;
      var newPermitRequest = new PermitRequetDetailModel();
      using (_Db = new WasteManageEntities()) {

        newPermitRequest = await GetPermitDetails(id);
      }

      ViewData.Model = newPermitRequest;
      using (var sw = new StringWriter()) {
        var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "~/Views/Permit/_Certificate.cshtml");
        var viewContext = new ViewContext(ControllerContext, viewResult.View,
                 ViewData, TempData, sw);
        viewResult.View.Render(viewContext, sw);
        viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
        TempData["PDFCertificate"] = sw.GetStringBuilder().ToString();
        return
           RedirectToAction("DownloadCertificatePDF");
      }
    }



    [HttpGet]
    public void DownloadCertificatePDF() {
      Response.ClearContent();
      Response.Buffer = true;
      Response.AddHeader("content-disposition", "attachment;filename = PermitCertificate_" + DateTime.Now.Ticks + ".pdf");
      Response.ContentType = "application/pdf";
      Response.Charset = "";

      var options = new HtmlLoadOptions();
      //options.PageInfo.Margin.Left = 10;
      //options.PageInfo.Margin.Right = 10;
      //options.PageInfo.Margin.Top = 10;
      //options.PageInfo.Margin.Bottom = 10;
      // var attchmentString = body.ToString();
      var attchmentString = TempData["PDFCertificate"] as string;
      var byteArray = Encoding.UTF8.GetBytes(attchmentString);
      var ms = new MemoryStream(byteArray);

      var liscencePath = Server.MapPath("~/Helpers/Aspose.Total.lic");

      License objlicense = new License();

      objlicense.SetLicense(liscencePath);

      var pdfDocument = new Document(ms, options);

      var stream = new MemoryStream();

      pdfDocument.FitWindow = true;
      pdfDocument.Save(stream, SaveFormat.Pdf);

      stream.WriteTo(Response.OutputStream);

      Response.Flush();
      Response.End();
    }

    #endregion


  }
}