using HFZMVC.Models.EntityFramework;
using HFZMVC.Models.Finance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
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
	public class PaymentsController : Controller
	{
		private WasteManageEntities _Db;
		bool IsUser = AppUtil.getUserRole()==3;
		string API_URL = ConfigurationManager.AppSettings["API_URL"];
		string Customer = ConfigurationManager.AppSettings["Customer"];
		string UserName = ConfigurationManager.AppSettings["UserNameEtisalat"];
		string Password = ConfigurationManager.AppSettings["PasswordEtisalat"];
		// GET: Payments
		public ActionResult Index( string status,string searchtype,string refid,string datefrom,string dateto)
		{
			using (_Db = new WasteManageEntities())
			{
				var role = AppUtil.getUserRole();
				var userid = AppUtil.checkLogin();
				string query = @"select *
									From AccountTransactions at 
									join Accounts ac on at.AccountID = ac.ID
									join Invoices inv on at.InoviceID = inv.ID
									where 1=1 ";

				//if (role == 3)
				if(IsUser==true)
				{
					query +=" and ac.UserID=@EnteredBy ";
					

				}

				if (!String.IsNullOrEmpty(status) ) {
					var searchquery = status == "0"? " 1=1 " : $" at.Status ='{status}' ";
					query += " and " + searchquery;
				}
				if (!String.IsNullOrEmpty(searchtype) ) {
					var searchquery =searchtype  == "0" ? " 1=1 " : $" at.PaymentMethod ='{searchtype}' ";
					query += " and " + searchquery;
				}



				if (!String.IsNullOrEmpty(refid)) {
					var searchquery = refid == "0" ? " 1=1 " : $" inv.invoiceno ='{refid}' ";
					query += " and " + searchquery;
				}
				if (!String.IsNullOrEmpty(datefrom)) {
					var searchquery = datefrom == "0" ? " 1=1 " : $" at.EnteredOn>= cast ('{datefrom}' as datetime)  ";
					query += " and " + searchquery;
				}

				if (!String.IsNullOrEmpty(dateto)) {
					var searchquery = dateto == "0" ? " 1=1 " : $" at.EnteredOn<= cast ('{dateto}' as datetime)  ";
					query += " and " + searchquery;
				}

				query += "   order by at.UpdatedOn desc";
				

				var transactions = _Db.Database.SqlQuery<TransactionViewModel>(query, new SqlParameter("@EnteredBy", userid)).ToList();
				ViewBag.count = transactions.Count();
				return View(transactions);
			}
		}

		public ActionResult CardPayment()
		{
			using (_Db = new WasteManageEntities())
			{
				var userid = Convert.ToInt32(Session[AppVariables.SessionUserId]);
				var Accounts = _Db.Accounts.Where(e => e.UserID == userid);
				decimal walletbalance = 0;
				if (Accounts.Count() > 0)
				{
					var acc = Accounts.First();

					walletbalance = (decimal)acc.Balance;
				}
				ViewBag.balance = walletbalance;
				return View();
			}
		}

		public JsonResult GetPermitVal(int id)
		{
			using (_Db = new WasteManageEntities())
			{
				var permitDetail = _Db.PermitRequests.Where(e => e.ID == id).FirstOrDefault();
				var transaction = _Db.AccountTransactions.Where(e => e.PermitID == id
				&& e.TransactionType.ToLower()!=AppVariables.SamplingInvoiceType.ToLower()
				).FirstOrDefault();
				if(transaction != null)
				{
					var result = new Dictionary<string, object>();
					result.Add("Success", false);
					if (transaction.Status == false)
					{
						result.Add("msg", "The Payment for this Permit is already Under Verification");
					}
					else if (transaction.Status == true)
					{
						result.Add("msg", "The Payment for this Permit is already Done");
					}
					else
					{
						result.Add("msg", "The Payment unavailable for this Permit");
					}
					return Json(result);
				}
				else
				{
					var entity = _Db.Entities.Where(e => e.ID == 1).FirstOrDefault();
					var amount = 0;
					if (permitDetail.PermitType == true) // Normal
					{
						var normalpermit = _Db.NormalPermits.Where(e => e.PermitId == id).FirstOrDefault();
						var wastetype = _Db.PermitTypeMasters.Where(e => e.ID == normalpermit.WasteType).FirstOrDefault();
						amount = (int)wastetype.Amount;

					}
					else //FDC
					{
						var permititems = _Db.PermitItems.Where(e => e.PermitID == id).ToList();
						//foreach (var item in permititems)
						//{
						//	var charges = _Db.MasterItems.Where(e => e.ID == item.ItemID).First().Amount;
						//	amount = amount + (int)charges;
						//}
						//Kepping Hardcoded for now
						amount = 250;
						entity.servicecharges = 0;
					}

					var grandtotal = amount + entity.r_dcharges + entity.servicecharges;
					var vatcharges = (grandtotal * entity.vatcharges) / 100;
					var userid = Convert.ToInt32(Session[AppVariables.SessionUserId]);
					//var Accounts = _Db.Accounts.Where(e => e.UserID == userid);
					//decimal walletbalance = 0;
					//if (Accounts.Count() > 0)
					//{
					//	var acc = Accounts.First();

					//	walletbalance = (decimal)acc.Balance;
					//}
					var result = new Dictionary<string, object>();
					result.Add("Success", true);
					result.Add("totalAmount", grandtotal);
					result.Add("vatcharges", vatcharges);
					//result.Add("walletbalance", walletbalance);
					result.Add("grandtotal", (grandtotal + vatcharges));// - walletbalance


					return Json(result);
				}                
			}
		}
		
		public ActionResult ManualPayment(int? id)
		{
			using (_Db = new WasteManageEntities())
			{
				var userid = Convert.ToInt32(Session[AppVariables.SessionUserId]);
				var Accounts = _Db.Accounts.Where(e => e.UserID == userid);
				decimal walletbalance = 0;
				if (Accounts.Count() > 0)
				{
					var acc = Accounts.First();

					walletbalance = (decimal)acc.Balance;
				}
				ViewBag.balance = walletbalance;
				if (id.HasValue)
				{
					ViewBag.id = id;
				}
				return View();
			}
		}
		
		public ActionResult PermitPayment(int id)
		{
			using (_Db = new WasteManageEntities())
			{
				var userid = AppUtil.checkLogin();
				var Accounts = _Db.Accounts.Where(e => e.UserID == userid);
				decimal walletbalance = 0;
				if (Accounts.Count() > 0)
				{
					var acc = Accounts.First();

					walletbalance = (decimal)acc.Balance;
				}
				ViewBag.balance = walletbalance;

				ViewBag.id = id;
				var invoiceData = _Db.Invoices.Where(e => e.PermitID == id && e.Invoice_Type == "Permit").First();
				ViewBag.invoiceData = invoiceData;

				return View();
			}
		}

		[HttpPost]
		public ActionResult CardPayment(CardPayments payment)
		{

			return RedirectToAction("Receipt", new { InvoiceNo = 99 });
		}

		[HttpPost]
		public ActionResult ManualPayment(ManualPayments payment)
		{
			using (TransactionScope scope = new TransactionScope())
			{
				using (_Db = new WasteManageEntities())
				{
					try
					{
						long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
						string ReferenceFile = "";
						if (payment?.ReferenceFile?.ContentLength > 0)
						{
							string _FileName = Path.GetFileName(payment.ReferenceFile.FileName);
							string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), "cheque" + timeStamp + "_" + _FileName);
							payment.ReferenceFile.SaveAs(_path);
							ReferenceFile = "cheque" + timeStamp + "_" + _FileName;

						}
						var userid = Convert.ToInt32(Session[AppVariables.SessionUserId]);
						var InVoiceData = _Db.Invoices.Where(e => e.PermitID == payment.PermitID && e.PaymentDate == null).First();
						var AccountData = _Db.Accounts.Where(e => e.UserID == userid).First();
						_Db.AccountTransactions.Add(new AccountTransaction()
						{
							InoviceID = InVoiceData.ID,
							AccountID = AccountData.ID,
							PaymentMethod = "Cheque",
							ReferenceNo = payment.ReferenceNo,
							ReferenceFIle = ReferenceFile,
							TransactionType = InVoiceData.Invoice_Type,
							PermitID = payment.PermitID,
							AmountPayable = payment.GrandTotal,
							AmountPaid = payment.AmountPaid,
							Remarks = payment.Remarks,
							TransactionDate = payment.PaymentDate.HasValue? payment.PaymentDate: new DateTime(1900,01,01) ,
							Status = false,
							EnteredBy = userid,
							EnteredOn = DateTime.Now,
							UpdatedBy = userid,
							UpdatedOn = DateTime.Now
						});
						_Db.SaveChanges();
						//var permitRequest = _Db.PermitRequests.Where(e => e.ID == payment.PermitID).First();
						//permitRequest.StatusID = 13;

						//_Db.PermitRequests.Attach(permitRequest);
						//_Db.Entry(permitRequest).State = System.Data.Entity.EntityState.Modified;

						//_Db.SaveChanges();

						//Check if its FDCpermit then redirect to FDC related Method
						var fdcPermit = _Db.FDCPermits.Where(e => e.PermitId == payment.PermitID).FirstOrDefault();
						//
						string redirectLink = "ChangePermitStatus";

						if (fdcPermit!=null) {
							redirectLink = "ChangeFDCStatus";
						}


						scope.Complete();
						return RedirectToAction(redirectLink, "Permit", new { id = payment.PermitID, statusId = 13 });
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
						ViewBag.error = ex.ToString();
						return View(payment);
					}
				}
			}
		}

		[HttpPost]
		public ActionResult WalletPayment(ManualPayments payment)
		{
			using (TransactionScope scope = new TransactionScope())
			{
				using (_Db = new WasteManageEntities())
				{
					try
					{
						long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
						var userid = Convert.ToInt32(Session[AppVariables.SessionUserId]);
						var InVoiceData = _Db.Invoices.Where(e => e.PermitID == payment.PermitID && e.PaymentDate == null).First();
						var AccountData = _Db.Accounts.Where(e => e.UserID == userid).First();
						_Db.AccountTransactions.Add(new AccountTransaction()
						{
							InoviceID = InVoiceData.ID,
							AccountID = AccountData.ID,
							PaymentMethod = "AccountBalance",
							ReferenceNo = "Acc-"+ AccountData.ID,
							TransactionType = InVoiceData.Invoice_Type,
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


						//var permitRequest = _Db.PermitRequests.Where(e => e.ID == payment.PermitID).First();
						//permitRequest.StatusID = 13;

						//_Db.PermitRequests.Attach(permitRequest);
						//_Db.Entry(permitRequest).State = System.Data.Entity.EntityState.Modified;

						//_Db.SaveChanges();

						
						var permitRequest = _Db.PermitRequests.Where(e => e.ID == payment.PermitID).FirstOrDefault();
						if (permitRequest != null) {
							scope.Complete();
							string redirecLink = "ChangePermitStatus";
              if (!permitRequest.PermitType) {
								redirecLink = "ChangeFDCStatus";

							}

								
								return RedirectToAction(redirecLink, "Permit", new { id = payment.PermitID, statusId = 14 });
							
						} else {

							TempData["message"] = "3";
							return RedirectToAction("Index", "Permit");
						}
						
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
						ViewBag.error = ex.ToString();
						return View(payment);
					}
				}
			}
		}          

		public ActionResult Invoices(string section)
		{
			bool whereconditionadded = false;
			ViewBag.section = section;

			string cond = ""; //For Different screens
			if(section != "")
			{
				if (section == "Pending")
				{
					cond = " PaymentDate is Null";
				}

				else if (section == "Paid")
				{
					cond = " PaymentDate is Not Null";
				}
				else
				{
					RedirectToAction("Index");
				}
			} // Condition of status selection


			string query = @"select inv.ID, pr.PermitType, inv.InvoiceNo, inv.DueDate, inv.PaymentDate, inv.PaymentMethod, inv.PermitID, inv.TotalAmount, inv.Remarks,
						CASE When pr.PermitType = 1 
						Then CONCAT(
						(
						Select ptm.PermitType from NormalPermit np 
						Join PermitTypeMaster ptm on np.WasteType = ptm.ID
						where np.PermitId = inv.PermitID
						), 
						' - ', 
						(
						Select ptm.WasteType from NormalPermit np 
						Join PermitWasteMapping ptm on np.WasteCategory = ptm.ID
						where np.PermitId = inv.PermitID
						) )
						When pr.PermitType != 1 
						Then 
						(
						Select STRING_AGG ( ISNULL(mi.ItemName,'N/A'), ',') AS csv from PermitItems np 
						Join MasterItems mi on np.ItemID = mi.ID
						where np.PermitID = inv.PermitID
						)
						End as PermitCategory
						From Invoices inv 
						join PermitRequest pr on inv.PermitID = pr.ID
						{WhereCondition}";

			using (_Db = new WasteManageEntities())
			{
				List<InvoiceViewModel> InvoiceView = new List<InvoiceViewModel>();
				var role = AppUtil.getUserRole();
				var userid = AppUtil.checkLogin();

				//if (role == 3)
				if(IsUser==true)
				{
					query = query.Replace("{WhereCondition}", " where pr.UserID=@EnteredBy ");
					whereconditionadded = true;

				}
				
				if (cond != "")
				{
					if (whereconditionadded)
					{
						query += " and " + cond;
					}
					else
					{
						whereconditionadded = true;
						query = query.Replace("{WhereCondition}", " where " + cond);
					}
				}

				query += "   order by inv.UpdatedOn desc";
				if (!whereconditionadded) query = query.Replace("{WhereCondition}", "");

				InvoiceView = _Db.Database.SqlQuery<InvoiceViewModel>(query, new SqlParameter("@EnteredBy", userid)).ToList();
				ViewBag.count = InvoiceView.Count();
				return View(InvoiceView);
			}
		}

		public ActionResult Receipt(string InvoiceNo)
		{
			bool whereconditionadded = false;
			string query = @"select inv.ID, Name,CompanyName, Address, PhoneNumber, EmailID, inv.EnteredOn as PaymentDate,
						InvoiceNo, PermitFees, ServicesFees, RnDFees, Vat, TotalAmount, DueDate, at.PaymentMethod, inv.Remarks,
						CASE When pr.PermitType = 1 
						Then CONCAT(
						(
						Select ptm.PermitType from NormalPermit np 
						Join PermitTypeMaster ptm on np.WasteType = ptm.ID
						where np.PermitId = inv.PermitID
						), 
						' - ', 
						(
						Select ptm.WasteType from NormalPermit np 
						Join PermitWasteMapping ptm on np.WasteCategory = ptm.ID
						where np.PermitId = inv.PermitID
						) )
						When pr.PermitType != 1 
						Then 
						(
						Select STRING_AGG ( ISNULL(mi.ItemName,'N/A'), ',') AS csv from PermitItems np 
						Join MasterItems mi on np.ItemID = mi.ID
						where np.PermitID = inv.PermitID
						)
						End as PermitCategory
						From Invoices inv 
						join PermitRequest pr on inv.PermitID = pr.ID
						join Users u on pr.UserID = u.ID
						join AccountTransactions at on at.InoviceID = inv.ID
						{WhereCondition}";
			using (_Db = new WasteManageEntities())
			{
				var role = AppUtil.getUserRole();
				var userid = AppUtil.checkLogin();

				//if (role == 3)
				if(IsUser==true)
				{
					query = query.Replace("{WhereCondition}", " where pr.UserID=@EnteredBy ");
					whereconditionadded = true;
				}

				if (whereconditionadded)
				{
					query += " and inv.InvoiceNo = '" + InvoiceNo + "'";
				}
				else
				{
					whereconditionadded = true;
					query = query.Replace("{WhereCondition}", " where inv.InvoiceNo = '" + InvoiceNo + "'");
				}

				if (!whereconditionadded) query = query.Replace("{WhereCondition}", "");
				var InvoiceReceipt = _Db.Database.SqlQuery<InvoiceReceipt>(query, new SqlParameter("@EnteredBy", userid)).FirstOrDefault();
				if (InvoiceReceipt == null)
				{
					return HttpNotFound();
				}
				else
				{
					ViewBag.InoiveData = InvoiceReceipt;
					return View(InvoiceReceipt);
				}
			}
		}

		public ActionResult Invoice(string InvoiceNo)
		{
			bool whereconditionadded = false;
			string query = @"select inv.ID, Name,CompanyName, Address, PhoneNumber, EmailID, inv.EnteredOn as PaymentDate,
						InvoiceNo, PermitFees, ServicesFees, RnDFees, Vat, (PermitFees + ServicesFees + RnDFees + Vat) as TotalAmount, DueDate, inv.Remarks,
						CASE When pr.PermitType = 1 
						Then CONCAT(
						(
						Select ptm.PermitType from NormalPermit np 
						Join PermitTypeMaster ptm on np.WasteType = ptm.ID
						where np.PermitId = inv.PermitID
						), 
						' - ', 
						(
						Select ptm.WasteType from NormalPermit np 
						Join PermitWasteMapping ptm on np.WasteCategory = ptm.ID
						where np.PermitId = inv.PermitID
						) )
						When pr.PermitType != 1 
						Then 
						(
						Select STRING_AGG ( ISNULL(mi.ItemName,'N/A'), ',') AS csv from PermitItems np 
						Join MasterItems mi on np.ItemID = mi.ID
						where np.PermitID = inv.PermitID
						)
						End as PermitCategory
						From Invoices inv 
						join PermitRequest pr on inv.PermitID = pr.ID
						join Users u on pr.UserID = u.ID
						{WhereCondition}";
			using (_Db = new WasteManageEntities())
			{
				var role = AppUtil.getUserRole();
				var userid = AppUtil.checkLogin();

				//if (role == 3)
				if(IsUser==true)
				{
					query = query.Replace("{WhereCondition}", " where pr.UserID=@EnteredBy ");
					whereconditionadded = true;
				}

				if (whereconditionadded)
				{
					query += " and inv.InvoiceNo = '" + InvoiceNo + "'";
				}
				else
				{
					whereconditionadded = true;
					query = query.Replace("{WhereCondition}", " where inv.InvoiceNo = '" + InvoiceNo + "'");
				}

				if (!whereconditionadded) query = query.Replace("{WhereCondition}", "");
				var InvoiceReceipt = _Db.Database.SqlQuery<InvoiceReceipt>(query, new SqlParameter("@EnteredBy", userid)).FirstOrDefault();
				if (InvoiceReceipt == null)
				{
					return HttpNotFound();
				}
				else
				{
					ViewBag.InvoiceData = InvoiceReceipt;
					return View(InvoiceReceipt);
				}
			}
		}

		public class TransactionData
		{
			public int PermitID { get; set; }
			public string Amount { get; set; }
			public string OrderID { get; set; }
			public string OrderName { get; set; }
			public string OrderInfo { get; set; }
		}

		public async Task<string> GetTransactionID(TransactionData OData)
		{
			string Transaction = "";
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			string AppURL = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
			if (OData.PermitID == -1)
			{
				AppURL = string.Format(AppURL + "User/AddBalanceCallBack");
			}
			else
			{
				AppURL = string.Format(AppURL + "User/PermitPaymentCallBack/" + OData.PermitID);
			}
			string URL = API_URL;
			string InputJSON = "{\"Registration\":"
				+ "{\"Customer\":\""+ Customer + "\"," +
				"\"Channel\":\"Web\"," +
				"\"Amount\":\""+ OData.Amount + "\"," +
				"\"Currency\":\"AED\"," +
				"\"OrderID\":\"" + OData.OrderID + "\"," +
				"\"OrderName\":\"" + OData.OrderName + "\"," +
				"\"OrderInfo\":\"" + OData.OrderInfo + "\"," +
				"\"TransactionHint\":\"CPT:Y;VCC:Y;\"," +
				"\"UserName\":\"" + UserName + "\"," +
				"\"Password\":\"" + Password + "\"," +
				"\"ReturnPath\":\""+ AppURL + "\"}}";

			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(URL));
				byte[] lbPostBuffer = ASCIIEncoding.ASCII.GetBytes(InputJSON);
				//request.ClientCertificates.Add(certificate);               
				request.UserAgent = ".NET Framework Test Client";
				request.Accept = "text/xml-standard-api";
				request.Method = "POST";
				request.ContentLength = lbPostBuffer.Length;
				request.ContentType = "application/json";
				request.Timeout = 600000;



				HttpWebResponse response;

				Stream loPostData =await request.GetRequestStreamAsync();
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

			}
			catch (Exception e)
			{
				AppUtil.ExceptionLog(e);
				Transaction = e.ToString();
			}

			return Transaction;
		}

		public async Task<ActionResult> VerifyPayment(int id, string TransactionID)
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			string URL = API_URL;
			string InputJSON = "{\"Finalization\":"
				+ "{\"TransactionID\":\"" + TransactionID + "\"," +
				"\"Customer\":\"" + Customer + "\"," + 
				"\"UserName\":\"" + UserName + "\"," +
				"\"Password\":\"" + Password + "\"}}";

			try
			{
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

				if (data.Transaction.ResponseCode == "0")
				{
					using (TransactionScope scope = new TransactionScope())
					{
						using (_Db = new WasteManageEntities())
						{
							try {
								long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
								var userid = Convert.ToInt32(Session[AppVariables.SessionUserId]);
								var InVoiceData = _Db.Invoices.Where(e => e.PermitID == id && e.PaymentDate == null).First();
								var AccountData = _Db.Accounts.Where(e => e.UserID == userid).FirstOrDefault();
								_Db.AccountTransactions.Add(new AccountTransaction() {
									InoviceID = InVoiceData.ID,
									AccountID = AccountData.ID,
									PaymentMethod = "CreditCard",
									ReferenceNo = TransactionID,
									TransactionType = InVoiceData.Invoice_Type,
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

								var permitRequest = _Db.PermitRequests.Where(e => e.ID == id).FirstOrDefault();
								if (permitRequest != null) {
									scope.Complete();
									string redirecLink = "ChangePermitStatus";
									if (!permitRequest.PermitType) {
										redirecLink = "ChangeFDCStatus";

									}


									return RedirectToAction(redirecLink, "Permit", new { id = id, statusId = 14 });


								} else {
									TempData["message"] = "3";
									return RedirectToAction("Index", "Permit");
								}
							} catch (DbEntityValidationException ex) {
								scope.Dispose();
								string errorMessage = "";

								foreach (var errors in ex.EntityValidationErrors) {
									foreach (var validationError in errors.ValidationErrors) {
										// get the error message 
										errorMessage = validationError.ErrorMessage;
									}
								}
								AppUtil.ExceptionLog(ex);
								TempData["message"] = errorMessage + "<br>" + ex.ToString();
								return RedirectToAction("PermitPayment", "Payments", new { id });
							}
						}
					}
				}
				else
				{
					TempData["message"] = data.Transaction.ResponseDescription;
					return RedirectToAction("PermitPayment", "Payments", new { id });
				}
			}
			catch (Exception ex)
			{
				TempData["message"] = ex.ToString();
				AppUtil.ExceptionLog(ex);
				return RedirectToAction("PermitPayment", "Payments", new { id });
			}
		}
	}
}
