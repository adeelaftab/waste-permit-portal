using HFZMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace HFZMVC.Helpers
{
  public class PermitEmails
  {
    WasteManageEntities _Db;

    public  bool SendPermitStatusChangeEmail
      ( PermitRequest permitRequest, WasteManageEntities db) {
			Helpers.EmailNotification NewEmail = new EmailNotification();
      //
      // Getting Html Files for Email
      if (_Db==null) {
				_Db = db;
			}
			 
			var StatusMasters = _Db.StatusMasters.Where(e => e.ID == permitRequest.StatusID).FirstOrDefault();
			var FileForUserEmail = StatusMasters.UserEmailFileLink;
			var FileForClientEmail = StatusMasters.ClientEmailFileLink;


			//Data Required ApplicantName, PermitID, statusName
			var StatusName = StatusMasters.Description;
			string ApplicantName = permitRequest.ApplicantName;
			int permitID = permitRequest.ID;
			string ActivationUrl = string.Format("{0}://{1}{2}",HttpContext.Current. Request.Url.Scheme,HttpContext.Current. Request.Url.Authority,"/");


			/// Setting Up Email for Client
			if (FileForClientEmail != "" && fileexist( FileForClientEmail) == true) {
				var ClientMasters = _Db.Users.Where(e => e.ID == permitRequest.UserID).FirstOrDefault(); // Getting Client Data For emailing

				string EmailHtmlBody = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(@"~/Assets/mails/" + FileForClientEmail)); //Getting Html

				string EmailBody = EmailHtmlBody.Replace("{StatusName}", StatusName);
				EmailBody = EmailBody.Replace("{ApplicantName}", ApplicantName);
				EmailBody = EmailBody.Replace("{ActivationUrl}", ActivationUrl);
				EmailBody = EmailBody.Replace("{permitID}", permitID.ToString());
				NewEmail.Send(ClientMasters.EmailID, "Status Changed To " + StatusName, EmailBody, true, "", permitID); //New User verification Email
			}

			/// Setting Up Email for Beea'h User
			if (FileForUserEmail != "" && fileexist(FileForUserEmail) == true) {
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
        System.Text.StringBuilder query = new System.Text.StringBuilder();

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
						EmailHtmlBody = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(@"~/Assets/mails/" + FileForUserEmail)); //Getting Html

						EmailBody = EmailHtmlBody.Replace("{StatusName}", StatusName);
						EmailBody = EmailBody.Replace("{ApplicantName}", ApplicantName);
						EmailBody = EmailBody.Replace("{ActivationUrl}", ActivationUrl);
						EmailBody = EmailBody.Replace("{Name}", item.Name);
						EmailBody = EmailBody.Replace("{permitID}", permitID.ToString());
						NewEmail.Send(item.EmailID, "Status Changed To " + StatusName, EmailBody, true, "", permitID); //New User verification Email
					}
				} catch (Exception ex) {


				}



			}

			return true;

			return true;
    }


		public bool fileexist(string link) {

		
		
      if (System.IO.File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(@"~/Assets/mails/" + link))) {  
				return true;
      } else {
				return false;
      }
			//
   //   HttpWebResponse response = null;
			//var request = (HttpWebRequest)WebRequest.Create(url);
			//request.Method = "HEAD";
			//bool resp = false;

			//try {
			//	response = (HttpWebResponse)request.GetResponse();
			//	resp = true;
			//} catch (WebException ex) {
			//	resp = false;
			//	/* A WebException will be thrown if the status of the response is not `200 OK` */
			//} finally {
			//	// Don't forget to close your response.
			//	if (response != null) {
			//		response.Close();
			//	}
			//}
			//return resp;
		}
	}
}