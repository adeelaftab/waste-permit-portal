using HFZMVC.Models;
using HFZMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace HFZMVC.Helpers
{
	public class EmailNotification
	{
		//private WasteManagementEntities _context = new WasteManagementEntities();

		public void Send(string EmailId, string subject, string body, bool priority, string Path = "", int? PermitID = 0)
		{
			Task.Factory.StartNew(() => SendEmail(EmailId, subject, body, priority, Path, PermitID), TaskCreationOptions.LongRunning);
		}

		private void SendEmail(string EmailId, string subject, string body, bool priority, string Path = "", int? PermitID = 0)
		{
			try
			{

        //string fromAddress = "wastepermit@beeah.ae";
        //string serverName = "smtp.office365.com";
        //int? port = 587;
        //string userName = "wastepermit@beeah.ae";
        //string password = "Beeah@123";
        string fromAddress = "beeahdemo@gmail.com";
        string serverName = "smtp.gmail.com";
        int? port = 587;
        string userName = "beeahdemo@gmail.com";
        string password = "PlanA1234";

        bool? SSL = true;


				//var MailSettings = _context.tbb_SystemSetting.Where(x => (x.SettingTag ?? "").Trim() == "MailSetting".Trim()).ToList();

				#region  IF Mail settings Available then start send process

				if (EmailId != null)
				{
					//if (MailSettings.Count > 0)
					//{
					//fromAddress = MailSettings.Where(x => x.SettingName == "SendingEmail").Select(x => x.SettingValue).FirstOrDefault();
					//serverName = MailSettings.Where(x => x.SettingName == "HostName").Select(x => x.SettingValue).FirstOrDefault();
					//port = Convert.ToInt32(MailSettings.Where(x => x.SettingName == "Port").Select(x => x.SettingValue).FirstOrDefault());
					//userName = MailSettings.Where(x => x.SettingName == "UserName").Select(x => x.SettingValue).FirstOrDefault();
					//password = MailSettings.Where(x => x.SettingName == "Password").Select(x => x.SettingValue).FirstOrDefault();
					//SSL = Convert.ToBoolean(MailSettings.Where(x => x.SettingName == "EnableSSL").Select(x => x.SettingValue).FirstOrDefault());

					//////
					using (WasteManageEntities _Db = new WasteManageEntities())
					{
						Email saveemail = new Email();
						if (PermitID != 0)
						{
							saveemail.PermitID = PermitID;
						}
						saveemail.UserID = AppUtil.checkLogin();
						saveemail.FromEmail = fromAddress;  
						saveemail.FromName = "";
						saveemail.ToEmail = EmailId;
						saveemail.Subject = subject;
						saveemail.Body = body;
						if (AppUtil.checkLogin()>0)
						{
							saveemail.EnteredBy = AppUtil.checkLogin();
						}
						else
						{
							saveemail.EnteredBy = 1;
						}
						saveemail.EnteredOn = DateTime.Now;
						_Db.Emails.Add(saveemail);
						_Db.SaveChanges();
					}
					//////

					var message = new MailMessage(fromAddress, EmailId);
					message.Subject = subject;
					message.Body = body;
					if (Path != "") message.Attachments.Add(new Attachment(Path));
					message.IsBodyHtml = true;
					message.HeadersEncoding = Encoding.UTF8;
					message.SubjectEncoding = Encoding.UTF8;
					message.BodyEncoding = Encoding.UTF8;
					if (priority) message.Priority = MailPriority.High;

				//	Thread.Sleep(1000);

					SmtpClient client = new SmtpClient(serverName, (int)port);
					client.DeliveryMethod = SmtpDeliveryMethod.Network;
					client.EnableSsl = (bool)SSL;// Convert.ToBoolean(WebConfigurationManager.AppSettings["SmtpSsl"]);
					client.UseDefaultCredentials = false;

					NetworkCredential smtpUserInfo = new NetworkCredential(userName, password);
					client.Credentials = smtpUserInfo;

					client.Send(message);

					client.Dispose();
					message.Dispose();


					//}
				}

				#endregion

			}
			catch (DbEntityValidationException ex)
			{
				foreach (var errors in ex.EntityValidationErrors)
				{
					foreach (var validationError in errors.ValidationErrors)
					{
						// get the error message 
						string errorMessage = validationError.ErrorMessage;
					}
				}
			} 
			catch (Exception ex) {

				AppUtil.ExceptionLog(ex);
      }
    }

		public void SendTestEmail(string EmailId, string subject, string body, bool priority, string Path = "", int? PermitID = 0) {
			try {

				string fromAddress = "wastepermit@beeah.ae";
				string serverName = "smtp.office365.com";
				int? port = 587;
				string userName = "wastepermit@beeah.ae";
				string password = "Beeah@123";
				//string fromAddress = "beeahdemo@gmail.com";
				//string serverName = "smtp.gmail.com";
				//int? port = 587;
				//string userName = "beeahdemo@gmail.com";
				//string password = "PlanA1234";

				bool? SSL = true;


				//var MailSettings = _context.tbb_SystemSetting.Where(x => (x.SettingTag ?? "").Trim() == "MailSetting".Trim()).ToList();

				#region  IF Mail settings Available then start send process

				if (EmailId != null) {
					//if (MailSettings.Count > 0)
					//{
					//fromAddress = MailSettings.Where(x => x.SettingName == "SendingEmail").Select(x => x.SettingValue).FirstOrDefault();
					//serverName = MailSettings.Where(x => x.SettingName == "HostName").Select(x => x.SettingValue).FirstOrDefault();
					//port = Convert.ToInt32(MailSettings.Where(x => x.SettingName == "Port").Select(x => x.SettingValue).FirstOrDefault());
					//userName = MailSettings.Where(x => x.SettingName == "UserName").Select(x => x.SettingValue).FirstOrDefault();
					//password = MailSettings.Where(x => x.SettingName == "Password").Select(x => x.SettingValue).FirstOrDefault();
					//SSL = Convert.ToBoolean(MailSettings.Where(x => x.SettingName == "EnableSSL").Select(x => x.SettingValue).FirstOrDefault());

					//////
					using (WasteManageEntities _Db = new WasteManageEntities()) {
						Email saveemail = new Email();
						if (PermitID != 0) {
							saveemail.PermitID = PermitID;
						}
						saveemail.UserID = AppUtil.checkLogin();
						saveemail.FromEmail = fromAddress;
						saveemail.FromName = "";
						saveemail.ToEmail = EmailId;
						saveemail.Subject = subject;
						saveemail.Body = body;
						if (AppUtil.checkLogin() > 0) {
							saveemail.EnteredBy = AppUtil.checkLogin();
						} else {
							saveemail.EnteredBy = 1;
						}
						saveemail.EnteredOn = DateTime.Now;
						_Db.Emails.Add(saveemail);
						_Db.SaveChanges();
					}
					//////

					var message = new MailMessage(fromAddress, EmailId);
					message.Subject = subject;
					message.Body = body;
					if (Path != "")
						message.Attachments.Add(new Attachment(Path));
					message.IsBodyHtml = true;
					message.HeadersEncoding = Encoding.UTF8;
					message.SubjectEncoding = Encoding.UTF8;
					message.BodyEncoding = Encoding.UTF8;
					if (priority)
						message.Priority = MailPriority.High;

					//	Thread.Sleep(1000);

					SmtpClient client = new SmtpClient(serverName, (int)port);
					client.DeliveryMethod = SmtpDeliveryMethod.Network;
					client.EnableSsl = (bool)SSL;// Convert.ToBoolean(WebConfigurationManager.AppSettings["SmtpSsl"]);
					client.UseDefaultCredentials = false;

					NetworkCredential smtpUserInfo = new NetworkCredential(userName, password);
					client.Credentials = smtpUserInfo;

					client.Send(message);

					client.Dispose();
					message.Dispose();


					//}
				}

				#endregion

			} catch (DbEntityValidationException ex) {
				foreach (var errors in ex.EntityValidationErrors) {
					foreach (var validationError in errors.ValidationErrors) {
						// get the error message 
						string errorMessage = validationError.ErrorMessage;
					}
				}
			} catch (Exception ex) {

				throw;
			}
		}


		public void SendMultirecipient(string To, string Cc, string subject, string body, bool priority, string Path = "")
		{
			Task.Factory.StartNew(() => SendEmailMultirecipient(To, Cc, subject, body, priority, Path), TaskCreationOptions.LongRunning);

		}

		private void SendEmailMultirecipient(string To, string Cc, string subject, string body, bool priority, string Path = "")
		{
			#region just fro testing i have commented this 
			try
			{
				string fromAddress = WebConfigurationManager.AppSettings["FromMailId"];
				var message = new MailMessage();
				message.From = new MailAddress(fromAddress);

				string serverName = WebConfigurationManager.AppSettings["Host"];
				int port = Convert.ToInt32(WebConfigurationManager.AppSettings["Port"]);
				string userName = WebConfigurationManager.AppSettings["UserId"];
				string password = WebConfigurationManager.AppSettings["Password"];



				//Adding multiple To Addresses
				if (To.Contains('+'))
				{
					var tolist = To.Split('+');
					foreach (var item in tolist)
						message.To.Add(item);
				}
				else
				{
					message.To.Add(To);
				}

				if ((Cc ?? "").Trim() != "")
				{
					if (Cc.Contains('+'))
					{
						var cclist = Cc.Split('+');
						foreach (var item in cclist)
							message.CC.Add(item);

					}
					else
					{
						message.CC.Add(Cc);

					}
				}

				message.Subject = subject;
				message.Body = body;
				if (Path != "") message.Attachments.Add(new Attachment(Path));
				message.IsBodyHtml = true;
				message.HeadersEncoding = Encoding.UTF8;
				message.SubjectEncoding = Encoding.UTF8;
				message.BodyEncoding = Encoding.UTF8;
				if (priority) message.Priority = MailPriority.High;

				Thread.Sleep(1000);

				SmtpClient client = new SmtpClient(serverName, port);
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.EnableSsl = true;// Convert.ToBoolean(WebConfigurationManager.AppSettings["SmtpSsl"]);
				client.UseDefaultCredentials = false;

				NetworkCredential smtpUserInfo = new NetworkCredential(userName, password);
				client.Credentials = smtpUserInfo;

				client.Send(message);

				client.Dispose();
				message.Dispose();
			}
			catch (Exception ex)
			{

				throw ex;
			}
			#endregion





		}



	}
}