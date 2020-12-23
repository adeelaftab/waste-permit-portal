using HFZMVC.Models.EntityFramework;
using HFZMVC.Models.PermitRequest;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HFZMVC.Controllers
{
	public class AppController : Controller
	{
		// GET: App
		public ActionResult Header()
		{
			return View("_Header");
		}

		public ActionResult Footer()
		{
			return View("_Footer");
		}

		public ActionResult UserMenu()
		{
			using (var _Db = new WasteManageEntities())
			{
				var userid = AppUtil.checkLogin();

				if (userid == 0) return PartialView("_Menu", null);
				else
				{
					int userrole = AppUtil.getUserRole();
					var menus =
						_Db.Database.SqlQuery<eMenu_Master>($"select nMenuID,strPageName,nParentID,strPagePath,strImagePath,bIsPermission,nOrder,dtAddDate, nAddBy, dtUpdateDate, nUpdateBy, dtDeleteDate, nDeleteBy, bIsDeleted From eMenu_Master em left join RoleMenuMapping  rm ON em.nMenuID = rm.MenuID where rm.RoleID = '{userrole}' and IsView='1' and em.bIsPermission=0 ");
					return PartialView("_Menu", menus.ToList());

				}
			}

		}

		public ActionResult Notifications()
		{
			using (var _Db = new WasteManageEntities())
			{
				var userid = AppUtil.checkLogin();

				if (userid == 0) return PartialView("_notification", null);
				else
				{
					List<PermitRequestIndexViewModel> permitRequests = new List<PermitRequestIndexViewModel>();
					string query = @"select TOP 5 pr.ID,pr.ApplyDate,pt.PermitType PermitType,wc.WasteType as Category, pr.StatusID,
						(select top 1 TotalAmount from Invoices inv where inv.PermitID=pr.ID) TotalAmount,st.statusName Status, getDate() ValidTill, np.Remarks
						From PermitRequest pr 
						inner join NormalPermit np
							on pr.ID= np.PermitId

							left join PermitTypeMaster pt on pt.ID= np.WasteType
							left join PermitWasteMapping wc on wc.ID= np.WasteCategory
							left join StatusMaster st on pr.StatusID=st.ID

							{WhereCondition} order by pr.UpdatedOn desc
							";
					query = query.Replace("{WhereCondition}", " where pr.UserID=" + userid + " and pr.UpdatedBy!=" + userid + " and IsRead = 0 ");
					permitRequests = _Db.Database.SqlQuery<PermitRequestIndexViewModel>(query).ToList();
					return PartialView("_notification", permitRequests);

				}
			}

		}
	}
}