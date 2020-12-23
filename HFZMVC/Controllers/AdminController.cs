using HFZMVC.AppLogics;
using HFZMVC.Models.EntityFramework;
using HFZMVC.Models.Finance;
using HFZMVC.Models.PermitRequest;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HFZMVC.Controllers
{
  [HFZMVC.CustomBinders.HFZAuthorization]
  public class AdminController : Controller
  {
    #region Properties
    WasteManageEntities _Db = new WasteManageEntities();
    #endregion

    // GET: Admin
    public async Task<ActionResult> SalesOrderIndex() {
      var sql = @" select pr.ID,pr.ApplyDate,pt.PermitType PermitType,pr.ID permitID,wc.WasteType as Category, pr.StatusID,
				(select top 1 TotalAmount from Invoices inv where inv.PermitID=pr.ID and Invoice_Type='Permit')  TotalAmount,st.statusName Status, getDate() ValidTill, PermitreferenceiD,
ur.CompanyName CompanyName,np.Remarks,cast('false' as bit) IsSampling
				From PermitRequest pr 
				inner join NormalPermit np
					on pr.ID= np.PermitId

					left join PermitTypeMaster pt on pt.ID= np.WasteType
					left join PermitWasteMapping wc on wc.ID= np.WasteCategory
					left join StatusMaster st on pr.StatusID=st.ID
left join Users ur on ur.Id=pr.UserId
					where pr.StatusID=7 and  pr.id  in (
					select  permitid from invoices 

					where Invoice_Type='Permit'
					 and SalesOrder is   null
					)";
      var resutl =
         await _Db.Database.SqlQuery<PermitRequestIndexViewModel>(sql).ToListAsync();
      return View(resutl);
    }

    public async Task<ActionResult> PushSalesOrder(int id) {

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
          throw new Exception("Unable to store sales order in SAP, record returned is null; Permit Item Code: "+Data.ProductCode +", Customer ID: "+Data.SAPCustomerID);
        } else {
          var inv = _Db.Invoices.Where(x => x.PermitID == id && x.Invoice_Type == "Permit").OrderByDescending(e => e.ID).FirstOrDefault();
          inv.SalesOrder = SalesOrder;
          _Db.Entry(inv).State = EntityState.Modified;
          await _Db.SaveChangesAsync();
        }
        TempData["success"] = "Error! Something went wrong";
        return RedirectToAction("SalesOrderIndex");
      } catch (Exception ex) {
        TempData["error"] = ex.Message;
        AppUtil.ExceptionLog(ex);
        return RedirectToAction("SalesOrderIndex");
      }

      TempData["error"] = "Error! Something went wrong";
      return RedirectToAction("SalesOrderIndex");
    }
  }


}