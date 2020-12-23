using HFZMVC.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HFZMVC.Controllers
{
  public class TestViewModel {
    public string Message { get; set; }

    [Required]
    public string To { get; set; }
  }
  [HFZMVC.CustomBinders.HFZAuthorization]
  public class TestController : Controller
    {
        // GET: Test
        public ActionResult TestEmail()
        {
            return View();
        }
      [HttpPost]
    public ActionResult TestEmail(TestViewModel tm) {
      try {
        EmailNotification NewEmail = new EmailNotification();
        string EmailBody = tm.Message;
         NewEmail.SendTestEmail(tm.To, "This is test email", EmailBody, true, "", 0); //New User verification Email
        return View();
      } catch (Exception ex) {

        ViewBag.Error = ex.ToString();
        return View(tm);
      }
    }

  }
}