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
using System.Dynamic;
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
	public partial class PermitController
	{

		public ActionResult ChangeFDCStatus(int id, int statusId, int? samplingpurpose, int? samplesource, string Remarks, string OtherSourceOfSample, string RejectionReason, bool? samplingrequired, bool? moresamplingrequired,string samplingRemarks)
		{
			if (Session[AppVariables.SessionUserRole] != null)
			{
				int roleid = AppUtil.getUserRole();
				//if (roleid == 1002 || roleid == 1)
				//{
				using (_Db = new WasteManageEntities())
				{
					var permitRequest =
					  _Db.PermitRequests.Where(e => e.ID == id).FirstOrDefault();
					if (permitRequest != null)
					{

						permitRequest.StatusID = GetPermitStatus(statusId, samplingrequired, null, moresamplingrequired);
						if (permitRequest.StatusID == 2)
						{
							permitRequest.PurposeOfSampling = samplingpurpose;
							permitRequest.SourceOfSample = samplesource;
							permitRequest.Othersource = OtherSourceOfSample;
						}
						if (permitRequest.StatusID == 14)
						{
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
							_Db.SaveChanges();
							/// Update AccountTransactions Data
							transaction.Status = true;
							_Db.AccountTransactions.Attach(transaction);
							_Db.Entry(transaction).State = System.Data.Entity.EntityState.Modified;
							_Db.SaveChanges();

						}
						if (permitRequest.StatusID == 7)
						{
							permitRequest.IssueDate = DateTime.Now;
							permitRequest.IssueBy = AppUtil.checkLogin();
							permitRequest.ApproveBy = AppUtil.checkLogin();
							//Permit Approve Add dates for it
							permitRequest.ValidTill = DateTime.Now.AddMonths(12);


						}
						if (permitRequest.StatusID == 12 || permitRequest.StatusID == 10 || permitRequest.StatusID == 4)
						{
							var SampleDetails = _Db.SamplingRequests.Where(e => e.PermitID == id).FirstOrDefault();
							try
							{
								if (SampleDetails != null)
								{
									if (SampleDetails.StatusID != 1)
									{
										SampleDetails.StatusID = 1;
										SampleDetails.UpdatedBy = AppUtil.checkLogin();
										SampleDetails.UpdatedOn = DateTime.Now;
										_Db.SamplingRequests.Attach(SampleDetails);
										_Db.Entry(SampleDetails).State = EntityState.Modified;
										_Db.SaveChanges();

										var SamplePaymentDetails = _Db.AccountTransactions.Where(e => e.PermitID == id && e.TransactionType == AppVariables.SamplingInvoiceType).FirstOrDefault();
										SamplePaymentDetails.Status = true;
										SamplePaymentDetails.UpdatedBy = AppUtil.checkLogin();
										SamplePaymentDetails.UpdatedOn = DateTime.Now;
										_Db.AccountTransactions.Attach(SamplePaymentDetails);
										_Db.Entry(SamplePaymentDetails).State = EntityState.Modified;
										_Db.SaveChanges();

										var SamplePaymentinvoice = _Db.Invoices.Where(e => e.ID == SamplePaymentDetails.InoviceID).FirstOrDefault();
										SamplePaymentinvoice.PaymentDate = SamplePaymentDetails.TransactionDate;
										SamplePaymentinvoice.PaymentMethod = SamplePaymentDetails.PaymentMethod;

										SamplePaymentinvoice.UpdatedBy = AppUtil.checkLogin();
										SamplePaymentinvoice.UpdatedOn = DateTime.Now;
										_Db.Invoices.Attach(SamplePaymentinvoice);
										_Db.Entry(SamplePaymentinvoice).State = EntityState.Modified;
										_Db.SaveChanges();
									}
								}
							}
							catch (Exception ex)
							{

								AppUtil.ExceptionLog(ex);
							}

						}
						if (permitRequest.StatusID == 12)
						{
							try
							{
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
								Data = _Db.Database.SqlQuery<SAPOrderData>(query).FirstOrDefault();
								string SalesOrder = CallinSAPI.GenerateOrder(Data);
								if (SalesOrder == null)
								{
									throw new Exception("Error in calling FDC SAP.");
								}
								else
								{
									var inv = _Db.Invoices.Where(x => x.PermitID == id && x.Invoice_Type == "Permit").FirstOrDefault();
									inv.SalesOrder = SalesOrder;
									_Db.Entry(inv).State = EntityState.Modified;
									_Db.SaveChanges();
								}
							}
							catch (Exception ex)
							{

								AppUtil.ExceptionLog(ex);
							}
						}
						if (permitRequest.StatusID == 5)
						{
							permitRequest.RejectionReason = RejectionReason;
						}
						permitRequest.UpdatedOn = DateTime.Now;
						if (AppUtil.getUserRole() != 3 && AppUtil.getUserRole() != 1003)
						{
							permitRequest.UpdatedBy = AppUtil.checkLogin();
						}
						permitRequest.IsRead = false;
            if (!String.IsNullOrEmpty(samplingRemarks)) {
							//weight bridge integration
							permitRequest.WBRemarks = samplingRemarks;
            }
						_Db.PermitRequests.Attach(permitRequest);
						_Db.Entry(permitRequest).State = System.Data.Entity.EntityState.Modified;

						_Db.SaveChanges();
						SendStatusChangeEmail(permitRequest,true);
						TempData["message"] = "4";
						return RedirectToAction("FDCList");
					}
					else
					{
						return RedirectToAction("FDCList");
					}

				}
				//}
				//else
				//{
				//	return RedirectToAction("Index");
				//}
			}
			else
			{
				return Redirect("/");
			}

		}

		public async Task<ActionResult> ChangePostFDCStatus(int id, int statusid) {
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
				return RedirectToAction("FDCList");

			} else {
				return Redirect("/");

			}

		}
		public async Task<ActionResult> FDCList(int? status, int? refid,
			string datefrom, string dateto)
		{

			if (Session[AppVariables.SessionUserRole] != null)
			{
				using (_Db = new WasteManageEntities())
				{


					List<FDCPermitPermitView> fdcpermitRequests = new List<FDCPermitPermitView>();
					var role = AppUtil.getUserRole();
					bool IsDelete = await AppUtil.checkIsPermission("DeletePermit", _Db);
					if (role == 1002)
					{
						IsDelete = true
							;
					}
					ViewBag.IsDelete = IsDelete;
					string query = @"select pr.ID, FORMAT (pr.EnteredOn, 'dd/MM/yyyy ') ApplyDate,ur.Name CompanyName, pt.PermitType PermitType,
							inv.TotalAmount TotalAmount,st.statusName Status,pr.StatusID, FORMAT (getdate(), 'dd/MM/yyyy ') ValidTill, np.DestructionReason

							From PermitRequest pr 
							inner join  fdcpermit np
								on pr.ID= np.PermitId
								inner join invoices inv on inv.PermitID=pr.ID
								left join PermitTypeMaster pt on pr.PermitType= pt.ID
								left join StatusMaster st on pr.StatusID=st.ID
left join Users ur on ur.Id=pr.UserId

					where 1=1 and inv.Invoice_Type='Permit' 
					";

					if (role == 3)
					{
						int userId = AppUtil.checkLogin();
						query += $" and  pr.EnteredBy={userId} ";
					}

					if (status.HasValue)
					{
						var statusquery = status.Value == 0 ? " 1=1 " : $" pr.StatusId ={status.Value} ";

						query += " and " + statusquery;


					}
					if (refid.HasValue)
					{
						var refquery = refid.Value == 0 ? " 1=1 " : $" pr.ID ={refid.Value} ";


						query += " and " + refquery;

					}

					if (!String.IsNullOrEmpty(datefrom))
					{
						var datefromquery = $" pr.EnteredOn >= cast ('{datefrom}' as datetime) ";


						query += " and " + datefromquery;

					}
					if (!String.IsNullOrEmpty(dateto))
					{
						var datetoquery = $" pr.EnteredOn <= cast ('{dateto}' as datetime) ";

						query += " and " + datetoquery;



					}
					query += " order by pr.UpdatedOn desc";
					fdcpermitRequests = await _Db.Database.SqlQuery<FDCPermitPermitView>(query).ToListAsync();






					return View(fdcpermitRequests);
				}
			}
			else
			{
				return Redirect("/");
			}

		}

		public ActionResult CreateFDC()
		{
			using (_Db = new WasteManageEntities())
			{
				var res = _Db.Entities.Where(e => e.ID == 1).FirstOrDefault();
				ViewBag.r_dcharges = string.Format("{0:n2}", res.r_dcharges);
				ViewBag.servicecharges = string.Format("{0:n2}", res.servicecharges);
				ViewBag.vatcharges = string.Format("{0:n2}", res.vatcharges);
				CreateEditFDCRequest permitViewModel = new CreateEditFDCRequest();
				ViewBag.itemslist = new List<SelectListItem>();
				foreach (var item in _Db.MasterItems.Where(e => e.status == true))
				{

					ViewBag.itemslist.Add(new SelectListItem()
					{
						Text = item.ItemName,
						Value = item.Amount.ToString() + "~" + item.ID.ToString()

					});
				}
				ViewBag.countrylist = _Db.LocationMasters.ToList();
				PrepareViewBagForFDC();
				int userid = AppUtil.checkLogin();
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

    private void PrepareViewBagForFDC() {
			var typeOfPakaging = new List<SelectListItem>();
	
		
			typeOfPakaging.Add(new SelectListItem() {
				Text = "Drums/Barrels",
				Value = "Drums/Barrels"
			});
			typeOfPakaging.Add(new SelectListItem() {
				Text = "Buckets",
				Value = "Buckets"
			});
			typeOfPakaging.Add(new SelectListItem() {
				Text = "Cans",
				Value = "Cans"
			});
			typeOfPakaging.Add(new SelectListItem() {
				Text = "Boxes",
				Value = "Boxes"
			});
			typeOfPakaging.Add(new SelectListItem() {
				Text = "IBC",
				Value = "IBC"
			});
			typeOfPakaging.Add(new SelectListItem() {
				Text = "Bags/Jumbo Bags",
				Value = "Bags/Jumbo Bags"
			});
			typeOfPakaging.Add(new SelectListItem() {
				Text = "Loose/NA",
				Value = "Loose/NA"
			});
			typeOfPakaging.Add(new SelectListItem() {
				Text = "Other",
				Value = "Other"
			});
			ViewBag.TypeOfPakaging = typeOfPakaging;
		}

    [HttpPost]
		public ActionResult CreateFDC(CreateEditFDCRequest viewModel)
		{
			long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			PermitRequest permitRequest = new PermitRequest();
			FDCPermit permitDetail = new FDCPermit();
			string inspectionform = "";
			using (TransactionScope scope = new TransactionScope())
			{
				using (_Db = new WasteManageEntities())
				{
					try
					{
						int userid = AppUtil.checkLogin();
						if (viewModel?.inspection_form?.ContentLength > 0)
						{
							string _FileName = Path.GetFileName(viewModel.inspection_form.FileName);
							string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), "inspform" + timeStamp + "_" + _FileName);
							viewModel.inspection_form.SaveAs(_path);
							inspectionform = "inspform" + timeStamp + "_" + _FileName;

						}
						permitRequest.PermitType = false; ///Indicat it is a FDCPermit
						permitRequest.TotalWeight = viewModel.TotalWeight;
						permitRequest.WeightUnit = viewModel.UnitOfMeasure;
						permitRequest.ApplicantName = viewModel.ApplicantName;
						permitRequest.PurposeOfSampling = 1;
						permitRequest.StatusID = 1;
						permitRequest.ApplyDate = DateTime.Now;
						permitRequest.UserID = userid;
						permitRequest.PermitReferenceID =  GenerateRefrenceNumberNormal(false, _Db);
						permitRequest.EnteredBy = userid;
						permitRequest.UpdatedBy = 1;
						permitRequest.EnteredOn = DateTime.Now;
						permitRequest.UpdatedOn = DateTime.Now;



						_Db.PermitRequests.Add(permitRequest);
						_Db.SaveChanges();


						permitDetail.CountryOfOrigin = viewModel.origin_country;
						permitDetail.ProductionDate = viewModel.pro_date;
						permitDetail.ExpiryDate = viewModel.expiry_date;
						permitDetail.PackageType = viewModel.PackageType;
						permitDetail.PackageCount = viewModel.PackageCount;
						permitDetail.PackageWeight = viewModel.PackageWeight.ToString();
						permitDetail.PackageWeightUnit = viewModel.total_package_weight_unit;
						permitDetail.DestructionReason = viewModel.destruction_reason;
						permitDetail.InspectionForm = inspectionform;
						permitDetail.FacilityLocation = viewModel.facility_location;
						permitDetail.PermitId = permitRequest.ID;

						_Db.FDCPermits.Add(permitDetail);

						List<PermitItem> permitItemsList = new List<PermitItem>();
						var check = Request.Form["itemslist[]"].ToString().Split(',');
						//viewModel.itemslist = Request.Form["itemslist[]"].ToString().Split(',');
						
						foreach (var item in viewModel.itemslist)
						{
							permitItemsList.Add(
							  new PermitItem()
							  {
								  EnteredBy = Convert.ToInt32(Session[AppVariables.SessionUserId]),
								  EnteredOn = DateTime.Now,
								  ItemID = 1,
									Item=item,
								  PermitID = permitRequest.ID,
								  UpdatedBy = Convert.ToInt32(Session[AppVariables.SessionUserId]),
								  UpdatedOn = DateTime.Now


							  }
							  );


						}
						_Db.PermitItems.AddRange(
						  permitItemsList

						  );
						_Db.SaveChanges();

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
						_Db.SaveChanges();


						//Helpers.EmailNotification NewEmail = new EmailNotification();
						//if (Convert.ToInt32(Session[AppVariables.SessionUserRole]) != 1002)
						//{
						//    var user = _Db.Users.Where(e => e.ID == 1).FirstOrDefault();
						//    NewEmail.Send(user.EmailID, "New Permit Request", "A new FDC Permit created in Portal, Please login and Review.", true, "");
						//}

						SendStatusChangeEmail(permitRequest);
						TempData["message"] = 1;
						scope.Complete();
						return RedirectToAction("FDCList");
					}
					catch (DbEntityValidationException ex)
					{
						scope.Dispose();
						foreach (var errors in ex.EntityValidationErrors)
						{
							foreach (var validationError in errors.ValidationErrors)
							{
								// get the error message 
								string errorMessage = validationError.ErrorMessage;
							}
						}
						ViewBag.itemslist = new List<SelectListItem>();
						foreach (var item in _Db.MasterItems.Where(e => e.status == true))
						{

							ViewBag.itemslist.Add(new SelectListItem()
							{
								Text = item.ItemName,
								Value = item.Amount.ToString() + "~" + item.ID.ToString()

							});
						}
						ViewBag.countrylist = _Db.LocationMasters.ToList();
						ViewBag.error = ex.ToString();
						AppUtil.ExceptionLog(ex);
						return View(viewModel);
					}
				}
			}





		}

	

		public async Task<ActionResult> FDCDetail(int id)
		{

			using (_Db = new WasteManageEntities())
			{
				FDCRequestDetailModel model = new
					FDCRequestDetailModel();
				model = GetFDCDetail(id);
				var role = AppUtil.getUserRole();
				model.IsSamplingRequest = role == 1002 ? true : await AppUtil.checkIsPermission(AppVariables.SamplingRequest, _Db);
				model.IsFDCApproval = role == 1002 ? true : await AppUtil.checkIsPermission(AppVariables.FDCApproval, _Db);
				model.IsPaymentApproval = role == 1002 ? true : await AppUtil.checkIsPermission(AppVariables.PaymentApproval, _Db);
				//if (model.StatusID == 13) //later Hard coded status ID
				//{
				ViewBag.PaymentDetails = _Db.AccountTransactions.Where(e => e.PermitID == id && e.TransactionType != "Sampling").FirstOrDefault();
				//}
				var invoice = await _Db.Invoices.Where(e => e.PermitID == id).FirstOrDefaultAsync();
				if (invoice != null)
				{
					model.InvoiceId = invoice.InvoiceNo;
					switch (invoice.PaymentMethod)
					{
						case "Cheque":
							model.PaymentMethod = "Cheque";
							break;
						case "CreditCard":
							model.PaymentMethod = "Credit Card";
							break;
						case "AccountBalance":
							model.PaymentMethod = "Wallet";
							break;


						default:
							break;
					}
				}

				ViewBag.SampleDetails = _Db.SamplingRequests.Where(e => e.PermitID == id).OrderByDescending(e => e.ID).FirstOrDefault();
				ViewBag.SamplePaymentDetails = _Db.AccountTransactions.Where(e => e.PermitID == id && e.TransactionType == AppVariables.SamplingInvoiceType).OrderByDescending(e => e.ID).FirstOrDefault();

				model.History = new List<TimelineViewModel>();
				model.History =
				await _Db.Database.SqlQuery<TimelineViewModel>($"select Table_Name,Old_Value,New_Value,Action_Type,us.UserName,st.Description," +
					   $" ad.Created_Timestamp From Audit_trail ad inner join Users us on ad.Created_By=us.Id" +
					   $" left join StatusMaster st on  ad.New_Value= st.ID where Table_Name='PermitRequest'" +
					   $" and Primary_Key={id} and Column_Name='StatusID' order by Created_Timestamp Desc").ToListAsync();

				var sampling = await _Db.SamplingRequests
						  .Where(e => e.PermitID == id).OrderBy(e => e.ID).ToListAsync();
				AssociateSamplingWithTimeLine(model.History, sampling);


				PopulateDropDownListForPermit();
				return View(model);
			}
		}

		private FDCRequestDetailModel GetFDCDetail(int id)
		{

			FDCRequestDetailModel permitViewModel = new
			FDCRequestDetailModel();
			var permit = _Db.PermitRequests.Where(e => e.ID == id).FirstOrDefault();
			if (permit != null)
			{
				int userid = AppUtil.checkLogin();
				var userInfo = _Db.Users.Where(e => e.ID == permit.EnteredBy).FirstOrDefault();
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
				permitViewModel.ValidTill = permit.ValidTill;
				permitViewModel.IssueDate = permit.IssueDate;
				permitViewModel.WBRemarks = permit.WBRemarks;
				permitViewModel.company
				  .Address = userInfo.Address;
				int counter = 0;
				permitViewModel.StatusID = permit.StatusID.HasValue ? permit.StatusID.Value : 0;
				permitViewModel.ID = id;
				string items = "";

				foreach (var item in permit.PermitItems)
				{
					if (String.IsNullOrEmpty(item.Item)) {
						if (counter == 0)
							items += item.MasterItem != null ?
						item.MasterItem.ItemName : "";
						else
							items += "," + item.MasterItem != null ?
						item.MasterItem.ItemName : "";
						counter++;
					} else {
						if (counter == 0)
							items += item.Item;
						else
							items += "," + item.Item;
						counter++;
					}
					
				}
				permitViewModel.ItemNames = items;

				permitViewModel.PurposeOfSampling = permit.PurposeOfSampling;
				permitViewModel.SourceSampling = permit.SourceOfSample;
				if (permit.PurposeOfSamplingMaster != null)
					permitViewModel.PurposeOfSamplingName = permit.PurposeOfSamplingMaster.PurposeOfSampling ?? "";
				if (permit.SourceOfSampleMaster != null)
					permitViewModel.SourceSamplingName = permit.SourceOfSampleMaster.SourceOfSample ?? "";
				permitViewModel.OtherSource = permit.Othersource;

				permitViewModel.TotalWeight = permit.TotalWeight;
				permitViewModel.UnitOfMeasure = permit.WeightUnit;
				permitViewModel.RejectionReason = permit.RejectionReason;
				var permitDetail = _Db.FDCPermits.Where(e => e.PermitId == id).FirstOrDefault();
				if (permitDetail != null)
				{
					permitViewModel.origin_country = permitDetail.CountryOfOrigin;
					permitViewModel.pro_date = permitDetail.ProductionDate.HasValue ?
						  permitDetail.ProductionDate.Value : new DateTime(01, 01, 1900);
					permitViewModel.expiry_date = permitDetail.ExpiryDate;
					permitViewModel.PackageType = permitDetail.PackageType;
					permitViewModel.PackageCount = permitDetail.PackageCount;
					try { permitViewModel.PackageWeight = Convert.ToDecimal(permitDetail.PackageWeight); } catch (Exception ex) { AppUtil.ExceptionLog(ex); }

					permitViewModel.total_package_weight_unit = permitDetail.PackageWeightUnit;
					permitViewModel.DestructionReason = permitDetail.DestructionReason;
				}
			}
			permit.IsRead = true;
			_Db.PermitRequests.Attach(permit);
			_Db.Entry(permit).State = System.Data.Entity.EntityState.Modified;
			_Db.SaveChanges();
			try
			{
				var countryID = Convert.ToInt32(permitViewModel.origin_country);
				var coutnryName =
							 _Db.LocationMasters.Where(e => e.ID == countryID).FirstOrDefault();
				if (coutnryName != null)
				{
					permitViewModel.origin_country = coutnryName.Location;
				}

			}
			catch (Exception ex)
			{
				AppUtil.ExceptionLog(ex);

			}
			return permitViewModel;
		}

		public ActionResult DeleteFDC(int id)
		{

			return View();

		}

		[HttpGet]
		public ActionResult GenerateFDCCertificatePDF(int id)
		{
			ViewBag.SuccessMessage = string.Empty;
			var newPermitRequest = new FDCRequestDetailModel();
			using (_Db = new WasteManageEntities())
			{

				newPermitRequest = GetFDCDetail(id);
			}

			ViewData.Model = newPermitRequest;
			using (var sw = new StringWriter())
			{
				var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "~/Views/Permit/_FDCCertificate.cshtml");
				var viewContext = new ViewContext(ControllerContext, viewResult.View,
							   ViewData, TempData, sw);
				viewResult.View.Render(viewContext, sw);
				viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
				TempData["PDFCertificate"] = sw.GetStringBuilder().ToString();
				return
				   RedirectToAction("DownloadCertificatePDF");
			}
		}
	}
}