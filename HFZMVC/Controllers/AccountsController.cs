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
	public class AccountsController : Controller
	{
		private WasteManageEntities _Db;
		string API_URL = ConfigurationManager.AppSettings["API_URL"];
		string Customer = ConfigurationManager.AppSettings["Customer"];
		string UserName = ConfigurationManager.AppSettings["UserNameEtisalat"];
		string Password = ConfigurationManager.AppSettings["PasswordEtisalat"];
		// GET: Accounts
		public ActionResult AddBalance()
		{
			return View();
		}

		public ActionResult Wallet()
		{
			using (_Db = new WasteManageEntities())
			{
				var role = AppUtil.getUserRole();
				var userid = AppUtil.checkLogin();
				string FDCquery = @"select sum(AmountPaid) as TotalAmount From AccountTransactions at 
										join Accounts ac on at.AccountID = ac.ID where at.TransactionType = 'FDCPermit' and 
   PaymentMethod not in ('Cheque','CreditCard') and  ac.UserID=@EnteredBy";
				var FDCtransactions = _Db.Database.SqlQuery<Payments>(FDCquery, new SqlParameter("@EnteredBy", userid)).First().TotalAmount;


				string Normalquery = @"select sum(AmountPaid) as TotalAmount From AccountTransactions at 
										join Accounts ac on at.AccountID = ac.ID where at.TransactionType = 'Permit' and PaymentMethod not in ('Cheque','CreditCard') and ac.UserID=@EnteredBy";
				var Normaltransactions = _Db.Database.SqlQuery<Payments>(Normalquery, new SqlParameter("@EnteredBy", userid)).First().TotalAmount;


				string samplingquery = @"select sum(AmountPaid) as TotalAmount From AccountTransactions at 
										join Accounts ac on at.AccountID = ac.ID where at.TransactionType = 'Sampling'  and PaymentMethod not in ('Cheque','CreditCard') and ac.UserID=@EnteredBy";
				var Samplingtransactions = _Db.Database.SqlQuery<Payments>(samplingquery, new SqlParameter("@EnteredBy", userid)).First().TotalAmount;

				string Accountsquery = @"select * from Accounts where UserID=@EnteredBy";
				var Accounts = _Db.Database.SqlQuery<Account>(Accountsquery, new SqlParameter("@EnteredBy", userid)).First();

				string Accounttransactionsquery = @"select Top 8 * from AccountTransactions where AccountID = " + Accounts.ID + " and PaymentMethod not in ('Cheque','CreditCard') order by ID DESC";
				var Accounttransactions = _Db.Database.SqlQuery<AccountTransaction>(Accounttransactionsquery).ToList();

				ViewBag.Accounts = Accounts;
				var total = (FDCtransactions + Normaltransactions + Samplingtransactions);
				ViewBag.Totalpaid = (total==null)?"0": total?.ToString("0.0");
				ViewBag.NormalAmount = (Normaltransactions == null) ? "0" : Normaltransactions?.ToString("0.0");
				ViewBag.FDCAmount = (FDCtransactions == null) ? "0" : FDCtransactions?.ToString("0.0");
				ViewBag.SamplingAmount = (Samplingtransactions == null) ? "0" : Samplingtransactions?.ToString("0.0");
				return View(Accounttransactions);
			}
		}

		public async Task<ActionResult> VerifyPayment(string TransactionID)
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
				request.UserAgent = "Topup Verification";
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
							try
							{
								long timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
								var userid = Convert.ToInt32(Session[AppVariables.SessionUserId]);
								var AccountData = _Db.Accounts.Where(e => e.UserID == userid).FirstOrDefault();
								var CurrentBalance = AccountData.Balance;

								_Db.AccountTransactions.Add(new AccountTransaction()
								{
									//InoviceID = InVoiceData.ID,
									AccountID = AccountData.ID,
									PaymentMethod = "CreditCard",
									ReferenceNo = TransactionID,
									TransactionType = "Account Topup",
									//PermitID = id,
									AmountPayable = data.Transaction.Amount.Value,
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
								var newadded = (Decimal)data.Transaction.Amount.Value;
								// Update Account
								AccountData.Balance = CurrentBalance + newadded;
								AccountData.UpdatedBy = userid;
								AccountData.UpdatedOn = DateTime.Now;
								_Db.Accounts.Attach(AccountData);
								_Db.Entry(AccountData).State = System.Data.Entity.EntityState.Modified;
								_Db.SaveChanges();

								scope.Complete();
								TempData["successmessage"] = "Account Recharged Successfully";
								return RedirectToAction("Wallet", "Accounts");
							}
							catch (DbEntityValidationException ex)
							{
								scope.Dispose();
								string errorMessage = "";

								foreach (var errors in ex.EntityValidationErrors)
								{
									foreach (var validationError in errors.ValidationErrors)
									{
										// get the error message 
										errorMessage = validationError.ErrorMessage;
									}
								}
								TempData["errormessage"] = errorMessage + "<br>" + ex.ToString();
								return RedirectToAction("Wallet", "Accounts");
							}
						}
					}
				}
				else
				{
					TempData["errormessage"] = data.Transaction.ResponseDescription;
					return RedirectToAction("Wallet", "Accounts");
				}
			}
			catch (Exception ex)
			{
				TempData["errormessage"] = ex.ToString();
				return RedirectToAction("Wallet", "Accounts");
			}
		}
	}
}