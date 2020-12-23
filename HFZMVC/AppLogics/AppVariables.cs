using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFZMVC
{
	public class AppVariables
	{

	#region ----------------- Session and Cookies ------------------------
	public const string SessionUsername = "username";
	public const string SessionUserId = "userid";
	public const string SessionUserRole = "roleid";
	public const string CookieUsername = "username";
		public const string SessionFullName = "SessionFullName";
		#endregion


		#region -------------------------Permit Permissions-------------------------------
		public const string PaymentApproval = "PaymentApproval";
	public const string HazardApproval = "HazardApproval";
	public const string SamplingRequest = "SamplingRequest";
	public const string HazardRequest = "HazardRequest";
	public const string PermitApproval = "PermitApproval";
	public const string FDCApproval = "FDCApproval";
		#endregion


		#region --------------------------Accounts and  Inovices-------------------------------------
		public const string PermitInvoiceType = "Permit";
		public const string SamplingInvoiceType = "Sampling";
		public const string FDCInvoiceType = "FDC";

	#endregion
  }
}