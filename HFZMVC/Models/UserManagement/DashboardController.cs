using HFZMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HFZMVC.Models.UserManagement
{

    public class DashboardController : Controller
    {
        private WasteManageEntities dbContext = new WasteManageEntities();
        bool IsUser = AppUtil.getUserRole()==3;
        public class Count
        {
            public int ID { get; set; }
        }

        public class LatestPermit
        {
            public int ID { get; set; }
      public string Company { get; set; }
      public DateTime ApplyDate { get; set; }
            public string PType { get; set; }
            public string PaymentStatus { get; set; }
            public string PermitStatus { get; set; }
        }

        // GET: Dashboard
        #region --------------------------Methods------------------------------
        public ActionResult Index()
        {
            int userid = AppUtil.checkLogin();

            if (userid == 0)
            {
                return RedirectToAction("../");
            }
            else
            {
                //var role = AppUtil.getUserRole();                
                //if (role == 3)

                if(IsUser==true)
                {
                    string query = "Select ID from PermitRequest where UserID = " + userid + "";
                    var TotalPermit = dbContext.Database.SqlQuery<Count>(query).Count();

                    string query1 = "Select ID from PermitRequest where statusId = '7' and UserID = " + userid + "";
                    var ProcessedPermit = dbContext.Database.SqlQuery<Count>(query1).Count();

                    string query2 = "Select ID, * from PermitRequest where statusId not in ('7','5','8','9') and UserID = " + userid + "";
                    var PendingPermit = dbContext.Database.SqlQuery<Count>(query2).Count();
                    ViewBag.TotalPermit = TotalPermit.ToString();
                    ViewBag.ProcessedPermit = ProcessedPermit.ToString();
                    ViewBag.PendingPermit = PendingPermit.ToString();
                }
                else
                {
                    string query = "Select ID from PermitRequest";
                    var TotalPermit = dbContext.Database.SqlQuery<Count>(query).Count();

                    string query1 = "Select ID from PermitRequest where statusId = '7'";
                    var ProcessedPermit = dbContext.Database.SqlQuery<Count>(query1).Count();

                    string query2 = "Select ID, * from PermitRequest where statusId not in ('7','5','8','9')";
                    var PendingPermit = dbContext.Database.SqlQuery<Count>(query2).Count();
                    
                    string query3 = "Select * from AccountTransactions where Status = 1";
                    var PermitRevenue = dbContext.Database.SqlQuery<AccountTransaction>(query3).ToList();
                    decimal sum = 0;
                    foreach (var item in PermitRevenue)
                    {
                        sum = sum + (decimal)item.AmountPaid;
                    }
                    ViewBag.TotalPermit = TotalPermit.ToString();
                    ViewBag.ProcessedPermit = ProcessedPermit.ToString();
                    ViewBag.PendingPermit = PendingPermit.ToString();
                    ViewBag.PermitRevenue = sum.ToString("0.##");
                }
                return View();
            }
        }
        #endregion

        [HttpGet]
        [ChildActionOnly]
        public string GetTopFivePermit()
        {
            List<string> results = new List<string>();
            //var role = AppUtil.getUserRole();
            var loginuserid = AppUtil.checkLogin() ;
      var ishazardApprover =
          dbContext.UserRoleMappings.Where(e => e.UserID == loginuserid && e.RoleID == 1003)
          .Any();
            //if (role == 3)
            if (IsUser == true)
            {
                string query = "Select Top 5 ID , ApplyDate, (CASE When PermitType = 1 Then 'Normal' When PermitType != 1 Then 'FDC' End ) as PType," +
                    "CASE When IsPaymentDone = 1 Then 'Paid' When IsPaymentDone != 1 Then 'UnPaid' End as PaymentStatus, " +
                    "(Select statusName from StatusMaster where ID = StatusID) as PermitStatus " +
                    "from PermitRequest where UserID = " + loginuserid + " order by UpdatedOn desc";
                var result =  dbContext.Database.SqlQuery<LatestPermit>(query);
                //var result = db.tbLeads.Take(5).OrderByDescending(e => e.AssignedDate);
                StringBuilder _html =  LeadHTMLResult(result);

                return _html.ToString();
            }
            else
            {
                string query = "Select Top 5  ID, (select top 1 Name from Users where Users.Id=Userid ) Company , ApplyDate, (CASE When PermitType = 1 Then 'Normal' When PermitType != 1 Then 'FDC' End ) as PType," +
                    "CASE When IsPaymentDone = 1 Then 'Paid' When IsPaymentDone != 1 Then 'UnPaid' End as PaymentStatus, " +
                    "(Select statusName from StatusMaster where ID = StatusID) as PermitStatus " +
                    "from PermitRequest where 1=1 ";
        if (ishazardApprover) {
          query += " and statusId=11" ;
        }
        query += "order by UpdatedOn desc ";
                var result = dbContext.Database.SqlQuery<LatestPermit>(query);
                //var result = db.tbLeads.Take(5).OrderByDescending(e => e.AssignedDate);
                StringBuilder _html =  LeadHTMLResult(result);
                return _html.ToString();
            }
        }

        //private StringBuilder LeadHTMLResult(DbRawSqlQuery<LatestPermit> result)
        //{
        //    throw new NotImplementedException();
        //}

        private StringBuilder LeadHTMLResult(DbRawSqlQuery<LatestPermit> result)
        {
            StringBuilder html = new StringBuilder();
    
            foreach (var item in result)
            {
                html.Append(" <tr>");
               if(IsUser) html.Append(" <td>" + item.ID + "</td>");
               else
                html.Append(" <td>" + item.Company + "</td>");
                html.Append("<td>" + item.ApplyDate + "</td>");
                html.Append(" <td>" + item.PType + "</td>");
                html.Append("<td><span class=\"label label-success\">" + item.PaymentStatus + "</span></td>");
                html.Append(" <td>" + item.PermitStatus + "</td>");
        if (item.PType.ToLower()=="fdc") {
          html.Append(" <td><a href=\"/Permit/FDCDetail/" + item.ID + "\"><button type=\"button\" class=\"btn btn - primary waves - effect waves - light\" style=\"background - color: #0084d6;border:1px #0084d6 solid\"><i class=\"fas fa-eye\"></i></button></a></td>");
        } else {
          html.Append(" <td><a href=\"/Permit/Detail/" + item.ID + "\"><button type=\"button\" class=\"btn btn - primary waves - effect waves - light\" style=\"background - color: #0084d6;border:1px #0084d6 solid\"><i class=\"fas fa-eye\"></i></button></a></td>");
        }
              
                html.Append("</tr>");
            }
            return html;
        }



    }
}