using HFZMVC.Models;
using HFZMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HFZMVC.Controllers
{

  public struct MasterDataName
  {
    public const string SourceOfSampleMaster = "SourceOfSampleMaster";
    public const string PurposeOfSamplingMaster = "PurposeOfSamplingMaster";
    public const string PermitTypeMaster = "PermitTypeMaster";
    public const string LocationMaster = "LocationMaster";

    public const string MasterItems = "MasterItems";
  }
  [HFZMVC.CustomBinders.HFZAuthorization]
  public class MasterDataController : Controller
  {

    private WasteManageEntities DB = new WasteManageEntities();
    // GET: MasterData
    public async Task<ActionResult> Index() {

      using (DB = new WasteManageEntities()) {
        var MasterDataList = await DB.Database.SqlQuery<MasterDataIndexVM>(@"
select 'SourceOfSampleMaster' Category,'Source of Sampling'CategoryLabel, SourceOfSample Category_Data,status IsActive,ID RecordId  from SourceOfSampleMaster
union 
select 'PurposeOfSamplingMaster' Category,'Purpose of Sampling'CategoryLabel, PurposeOfSampling Category_Data,status IsActive,ID RecordId from 
PurposeOfSamplingMaster 
union
select 'PermitTypeMaster' Category,'Permit Type'CategoryLabel, PermitType Category_Data,status IsActive,ID RecordId From PermitTypeMaster
union
select 'LocationMaster' Category,'Location Master'CategoryLabel, Location Category_Data,status IsActive,ID RecordId From LocationMaster
union
select 'MasterItems' Category,'Items for FDC'CategoryLabel, ItemName Category_Data,status IsActive,ID RecordId From MasterItems
").ToListAsync();




        return View(MasterDataList);
      }

    }

    public ActionResult MasterDataTable(String type) {

      if (!String.IsNullOrEmpty(type)) {
        StringBuilder query = new
          StringBuilder();
        switch (type) {
          case MasterDataName.SourceOfSampleMaster:
            query.Append("select Id,SourceOfSample Name,status isActive,'SourceOfSampleMaster' as 'Type',cast ('false' as bit) isEdit,cast ('true' as bit) isNew from SourceOfSampleMaster  order by id desc");

            break;

          case MasterDataName.PurposeOfSamplingMaster:
            query.Append("select Id, PurposeOfSampling Name,status isActive,'PurposeOfSamplingMaster' as 'Type',cast ('false' as bit) isEdit,cast ('true' as bit) isNew from PurposeOfSamplingMaster order by id desc");
            break;
          case MasterDataName.PermitTypeMaster:
            query.Append("select Id, PermitType Name,status isActive,'PermitTypeMaster' as 'Type',cast ('false' as bit) isEdit,cast ('true' as bit) isNew from PermitTypeMaster  order by id desc");
            break;
          case MasterDataName.LocationMaster:
            query.Append("select Id, Location Name,status isActive,'LocationMaster' as 'Type',cast ('false' as bit) isEdit,cast ('true' as bit) isNew from LocationMaster order by id desc");
            break;
          case MasterDataName.MasterItems:
            query.Append("select Id, ItemName Name,status isActive,'MasterItems' as 'Type',cast ('false' as bit) isEdit,cast ('true' as bit) isNew from MasterItems  order by id desc");
            break;



          default:
            break;
        }

        using (DB = new WasteManageEntities()) {

          var result =
            DB.Database.SqlQuery<MasterDataCreateEditVM>(query.ToString())
            .ToList();

          return PartialView(result);
        }
      } else {
        return null;
      }
    }

    #region --------------------------Create Masterdaata---------------------------
    [HttpGet]
    public ActionResult CreateMasterData() {


      return View();


    }

    [HttpPost]
    public JsonResult CreateMasterData(MasterDataCreateEditVM masterdata) {
      try {
        bool isAdded = false;
        using (DB = new WasteManageEntities()) {
          switch (masterdata.Type) {
            case MasterDataName.SourceOfSampleMaster:
              var data =
                new SourceOfSampleMaster();

              if (data != null) {
                data.status = masterdata.isActive;
                data.SourceOfSample = masterdata.Name;
                data.CreatedBy = AppUtil.checkLogin();
                data.UpdatedBy = AppUtil.checkLogin();
                data.CreatedOn = DateTime.Now;
                data.UpdatedOn = DateTime.Now;

                DB.SourceOfSampleMasters.Add(data);


              }
              break;

            case MasterDataName.PurposeOfSamplingMaster:
              var pusrposeofsample =
              new PurposeOfSamplingMaster();

              if (pusrposeofsample != null) {
                pusrposeofsample.status = masterdata.isActive;
                pusrposeofsample.PurposeOfSampling = masterdata.Name;
                pusrposeofsample.UpdatedBy = AppUtil.checkLogin();
                pusrposeofsample.UpdatedOn = DateTime.Now;
                pusrposeofsample.CreatedBy = AppUtil.checkLogin();
                pusrposeofsample.CreatedOn = DateTime.Now;

                DB.PurposeOfSamplingMasters.Add(pusrposeofsample);


              }
              break;
            case MasterDataName.PermitTypeMaster:
              var permittype =
                new PermitTypeMaster();

              if (permittype != null) {
                permittype.status = masterdata.isActive;
                permittype.PermitType = masterdata.Name;
                permittype.UpdatedBy = AppUtil.checkLogin();
                permittype.UpdatedOn = DateTime.Now;
                permittype.CreatedBy = AppUtil.checkLogin();
                permittype.CreatedOn = DateTime.Now;
                permittype.Amount = masterdata.Amount;
                permittype.ItemCode = masterdata.ItemCode;
                DB.PermitTypeMasters.Add(permittype);
              }
              break;
            case MasterDataName.LocationMaster:
              var locationmaster = new LocationMaster();

              if (locationmaster != null) {
                locationmaster.status = masterdata.isActive;
                locationmaster.Location = masterdata.Name;
                locationmaster.UpdatedBy = AppUtil.checkLogin();
                locationmaster.UpdatedOn = DateTime.Now;
                locationmaster.CreatedBy = AppUtil.checkLogin();
                locationmaster.CreatedOn = DateTime.Now;

                DB.LocationMasters.Add(locationmaster);


              }
              break;
            case MasterDataName.MasterItems:
              var masteritem =
              new MasterItem();

              if (masteritem != null) {
                masteritem.status = masterdata.isActive;
                masteritem.ItemName = masterdata.Name;
                masteritem.UpdatedBy = AppUtil.checkLogin();
                masteritem.UpdatedOn = DateTime.Now;
                masteritem.EnteredBy = AppUtil.checkLogin();
                masteritem.EnteredOn = DateTime.Now;
                masteritem.ItemCode = masterdata.ItemCode;
                masteritem.Amount = masterdata.Amount;

                DB.MasterItems.Add(masteritem);

              }
              break;



            default:
              break;
          }


          DB.SaveChanges();
          isAdded = true;
          ///Signal that data is added
          if (isAdded) {
            return Json("1");
          } else {
            return Json("0");
          }
        }
      } catch (Exception) {
        return Json("0");
      }


    }
    #endregion





    #region -----------------Edit Master data---------------------------------
    [HttpGet]
    public async Task<JsonResult> EditMasterData(int id, string type) {
      try {
        StringBuilder query = new
          StringBuilder();
        switch (type) {
          case MasterDataName.SourceOfSampleMaster:
            query.Append("select Id,SourceOfSample Name,status isActive,'SourceOfSampleMaster' as 'Type',cast ('false' as bit) isEdit,cast ('true' as bit) isNew from SourceOfSampleMaster  where Id=@id");

            break;

          case MasterDataName.PurposeOfSamplingMaster:
            query.Append("select Id, PurposeOfSampling Name,status isActive,'PurposeOfSamplingMaster' as 'Type',cast ('false' as bit) isEdit,cast ('true' as bit) isNew from PurposeOfSamplingMaster where Id=@id ");
            break;
          case MasterDataName.PermitTypeMaster:
            query.Append("select Id, PermitType Name,status isActive,'PermitTypeMaster' as 'Type',cast ('false' as bit) isEdit,cast ('true' as bit) isNew,ItemCode,Amount from PermitTypeMaster  where Id=@id ");
            break;
          case MasterDataName.LocationMaster:
            query.Append("select Id, Location Name,status isActive,'LocationMaster' as 'Type',cast ('false' as bit) isEdit,cast ('true' as bit) isNew from LocationMaster where Id=@id ");
            break;
          case MasterDataName.MasterItems:
            query.Append("select Id, ItemName Name,status isActive,'MasterItems' as 'Type',cast ('false' as bit) isEdit,cast ('true' as bit) isNew," +
              "ItemCode,Amount  from MasterItems  where Id=@id ");
            break;



          default:
            break;
        }

        using (DB = new WasteManageEntities()) {

          var result = await
            DB.Database.SqlQuery<MasterDataCreateEditVM>(query.ToString(), new SqlParameter("@id", id))
            .FirstOrDefaultAsync();

          if (result != null) {
            return Json(result, JsonRequestBehavior.AllowGet);
          } else
            return Json("notfound", JsonRequestBehavior.AllowGet);

        }
      } catch (Exception) {

        return Json("notfound", JsonRequestBehavior.AllowGet);
      }

    }

    [HttpPost]
    public JsonResult EditMasterData(MasterDataCreateEditVM masterdata) {

      try {
        bool isAdded = false;
        using (DB = new WasteManageEntities()) {


          switch (masterdata.Type) {
            case MasterDataName.SourceOfSampleMaster:
              var data =
               DB.SourceOfSampleMasters.Where(e => e.ID == masterdata.Id).FirstOrDefault();

              if (data != null) {
                data.status = masterdata.isActive;
                data.SourceOfSample = masterdata.Name;
                data.UpdatedBy = AppUtil.checkLogin();
                data.UpdatedOn = DateTime.Now;
                DB.Entry(data).State =
                  System.Data.Entity.EntityState.Modified;

              }
              break;

            case MasterDataName.PurposeOfSamplingMaster:
              var pusrposeofsample =
              DB.PurposeOfSamplingMasters.Where(e => e.ID == masterdata.Id).FirstOrDefault();

              if (pusrposeofsample != null) {
                pusrposeofsample.status = masterdata.isActive;
                pusrposeofsample.PurposeOfSampling = masterdata.Name;
                pusrposeofsample.UpdatedBy = AppUtil.checkLogin();
                pusrposeofsample.UpdatedOn = DateTime.Now;
                DB.Entry(pusrposeofsample).State =
                  System.Data.Entity.EntityState.Modified;

              }
              break;
            case MasterDataName.PermitTypeMaster:
              var permittype =
               DB.PermitTypeMasters.Where(e => e.ID == masterdata.Id).FirstOrDefault();

              if (permittype != null) {
                permittype.status = masterdata.isActive;
                permittype.PermitType = masterdata.Name;
                permittype.UpdatedBy = AppUtil.checkLogin();
                permittype.UpdatedOn = DateTime.Now;
                permittype.Amount = masterdata.Amount;
                permittype.ItemCode = masterdata.ItemCode;
                DB.Entry(permittype).State =
                  System.Data.Entity.EntityState.Modified;

              }
              break;
            case MasterDataName.LocationMaster:
              var locationmaster =
            DB.LocationMasters.Where(e => e.ID == masterdata.Id).FirstOrDefault();

              if (locationmaster != null) {
                locationmaster.status = masterdata.isActive;
                locationmaster.Location = masterdata.Name;
                locationmaster.UpdatedBy = AppUtil.checkLogin();
                locationmaster.UpdatedOn = DateTime.Now;
                DB.Entry(locationmaster).State =
                  System.Data.Entity.EntityState.Modified;

              }
              break;
            case MasterDataName.MasterItems:
              var masteritem =
              DB.MasterItems.Where(e => e.ID == masterdata.Id).FirstOrDefault();

              if (masteritem != null) {
                masteritem.status = masterdata.isActive;
                masteritem.ItemName = masterdata.Name;
                masteritem.UpdatedBy = AppUtil.checkLogin();
                masteritem.UpdatedOn = DateTime.Now;
                masteritem.ItemCode = masterdata.ItemCode;
                masteritem.Amount = masterdata.Amount;
                DB.Entry(masteritem).State =
                  System.Data.Entity.EntityState.Modified;

              }
              break;



            default:
              break;
          }


          DB.SaveChanges();
          isAdded = true;
          ///Signal that data is added
          if (isAdded) {
            return Json("1");
          } else {
            return Json("0");
          }
        }
      } catch (Exception) {
        return Json("0");
      }
    }
    #endregion


    #region ---------------- Delete Master data----------------

    [HttpDelete]
    public JsonResult DeleteMasterData(int id, string type) {

      try {
        bool isAdded = false;
        using (DB = new WasteManageEntities()) {


          switch (type) {
            case MasterDataName.SourceOfSampleMaster:
              var data =
               DB.SourceOfSampleMasters.Where(e => e.ID == id).FirstOrDefault();

              if (data != null) {
                DB.SourceOfSampleMasters.Remove(data);

                DB.Entry(data).State =
                  System.Data.Entity.EntityState.Deleted;

              }
              break;

            case MasterDataName.PurposeOfSamplingMaster:
              var pusrposeofsample =
              DB.PurposeOfSamplingMasters.Where(e => e.ID == id).FirstOrDefault();

              if (pusrposeofsample != null) {
                DB.PurposeOfSamplingMasters.Remove(pusrposeofsample);

                DB.Entry(pusrposeofsample).State =
                  System.Data.Entity.EntityState.Deleted;

              }
              break;
            case MasterDataName.PermitTypeMaster:
              var permittype =
               DB.PermitTypeMasters.Where(e => e.ID == id).FirstOrDefault();

              if (permittype != null) {
                DB.PermitTypeMasters.Remove(permittype);

                DB.Entry(permittype).State =
                  System.Data.Entity.EntityState.Deleted;

              }
              break;
            case MasterDataName.LocationMaster:
              var locationmaster =
            DB.LocationMasters.Where(e => e.ID == id).FirstOrDefault();

              if (locationmaster != null) {
                DB.LocationMasters.Remove(locationmaster);

                DB.Entry(locationmaster).State =
                  System.Data.Entity.EntityState.Deleted;

              }
              break;
            case MasterDataName.MasterItems:
              var masteritem =
              DB.MasterItems.Where(e => e.ID == id).FirstOrDefault();

              if (masteritem != null) {
                DB.MasterItems.Remove(masteritem);
                DB.Entry(masteritem).State =
                  System.Data.Entity.EntityState.Deleted;

              }
              break;



            default:
              break;
          }


          DB.SaveChanges();
          isAdded = true;
          ///Signal that data is added
          if (isAdded) {
            return Json("1");
          } else {
            return Json("0");
          }
        }
      } catch (Exception) {
        return Json("0");
      }
    }
    #endregion





    #region ------------------- Source of Sampling -------------------------

    public ActionResult SSIndex() {
      var model = DB.SourceOfSampleMasters.OrderByDescending(e=>e.ID).ToList();
      return View(model);
    }
    public ActionResult SSCreate() {
      
      return View(new SourceOfSampleMaster());
    }
    [HttpPost]
    public ActionResult SSCreate(SourceOfSampleMaster master) {
      var valid = true;
      string invalidMessage = "Please enter data";
      if (String.IsNullOrEmpty( master.SourceOfSample)) {
        valid = false;
        ViewBag.error = invalidMessage;
      }
      if (master.status==null) {
        valid = false;
        ViewBag.error = "Please select status";
      }
      if (!valid) {
        return View(master);
      } else {
        master.CreatedBy = AppUtil.checkLogin();
        master.CreatedOn = DateTime.Now;
        master.UpdatedOn = DateTime.Now;
        master.UpdatedBy = AppUtil.checkLogin();
        DB.SourceOfSampleMasters.Add(master);
        DB.SaveChanges();

        TempData["success"] = "Record created successfully!";
        return RedirectToAction("SSIndex");
      }
      
    }

    public ActionResult SSEdit(int id) {
      var master = DB.SourceOfSampleMasters.Where(E => E.ID == id)
        .FirstOrDefault();
      if (master == null) {
        TempData["error"] = "Record cannot be found!";
        return RedirectToAction("SSIndex");
      } else {
        return View(master);
      }
     
    }
    [HttpPost]
    public ActionResult SSEdit(SourceOfSampleMaster master) {
      var valid = true;
      string invalidMessage = "Please enter data";
      if (String.IsNullOrEmpty(master.SourceOfSample)) {
        valid = false;
        ViewBag.error = invalidMessage;
      }
      if (master.status == null) {
        valid = false;
        ViewBag.error = "Please select status";
      }
      if (!valid) {
        return View(master);
      } else {
        
        master.UpdatedOn = DateTime.Now;
        master.UpdatedBy = AppUtil.checkLogin();
        DB.SourceOfSampleMasters.Attach(master);
        DB.Entry(master).State = System.Data.Entity.EntityState.Modified;
        DB.SaveChanges();
        TempData["success"] = "Record updated successfully!";
        return RedirectToAction("SSIndex");
      }
    }
    public ActionResult SSDelete(int id) {

      var master = DB.SourceOfSampleMasters.Where(E => E.ID == id)
         .FirstOrDefault();
      if (master == null) {
        TempData["error"] = "Record cannot be found!";
        return RedirectToAction("SSIndex");
      } else {
        DB.SourceOfSampleMasters.Remove(master);
        DB.Entry(master).State = System.Data.Entity.EntityState.Deleted;
        DB.SaveChanges();
        TempData["success"] = "Record deleted successfully!";
        return RedirectToAction("SSIndex");
      }
    }
    #endregion


    #region ------------- Purpose of Sampling --------------------------------
    public ActionResult PSIndex() {
      var model = DB.PurposeOfSamplingMasters.OrderByDescending(e => e.ID).ToList();
      return View(model);
    }
    public ActionResult PSCreate() {

      return View(new PurposeOfSamplingMaster());
    }
    [HttpPost]
    public ActionResult PSCreate(PurposeOfSamplingMaster  master) {
      var valid = true;
      string invalidMessage = "Please enter data";
      if (String.IsNullOrEmpty(master.PurposeOfSampling)) {
        valid = false;
        ViewBag.error = invalidMessage;
      }
      if (master.status == null) {
        valid = false;
        ViewBag.error = "Please select status";
      }
      if (!valid) {
        return View(master);
      } else {
        master.CreatedBy = AppUtil.checkLogin();
        master.CreatedOn = DateTime.Now;
        master.UpdatedOn = DateTime.Now;
        master.UpdatedBy = AppUtil.checkLogin();
        DB.PurposeOfSamplingMasters.Add(master);
        DB.SaveChanges();

        TempData["success"] = "Record created successfully!";
        return RedirectToAction("PSIndex");
      }

    }

    public ActionResult PSEdit(int id) {
      var master = DB.PurposeOfSamplingMasters.Where(E => E.ID == id)
        .FirstOrDefault();
      if (master == null) {
        TempData["error"] = "Record cannot be found!";
        return RedirectToAction("PSIndex");
      } else {
        return View(master);
      }

    }
    [HttpPost]
    public ActionResult PSEdit(PurposeOfSamplingMaster master) {
      var valid = true;
      string invalidMessage = "Please enter data";
      if (String.IsNullOrEmpty(master.PurposeOfSampling)) {
        valid = false;
        ViewBag.error = invalidMessage;
      }
      if (master.status == null) {
        valid = false;
        ViewBag.error = "Please select status";
      }
      if (!valid) {
        return View(master);
      } else {

        master.UpdatedOn = DateTime.Now;
        master.UpdatedBy = AppUtil.checkLogin();
        DB.PurposeOfSamplingMasters.Attach(master);
        DB.Entry(master).State = System.Data.Entity.EntityState.Modified;
        DB.SaveChanges();
        TempData["success"] = "Record updated successfully!";
        return RedirectToAction("PSIndex");
      }
    }
    public ActionResult PSDelete(int id) {

      var master = DB.PurposeOfSamplingMasters.Where(E => E.ID == id)
         .FirstOrDefault();
      if (master == null) {
        TempData["error"] = "Record cannot be found!";
        return RedirectToAction("PSIndex");
      } else {
        DB.PurposeOfSamplingMasters.Remove(master);
        DB.Entry(master).State = System.Data.Entity.EntityState.Deleted;
        DB.SaveChanges();
        TempData["success"] = "Record deleted successfully!";
        return RedirectToAction("PSIndex");
      }
    }
    #endregion

    #region ---------------- Permit Type Master----------------------------------
    public ActionResult PTIndex() {
      var model = DB.PermitTypeMasters.OrderByDescending(e => e.ID).ToList();
      return View(model);
    }
    public ActionResult PTCreate() {

      return View(new PermitTypeMaster());
    }
    [HttpPost]
    public ActionResult PTCreate(PermitTypeMaster master) {
      var valid = true;
      string invalidMessage = "Please enter data";
      if (String.IsNullOrEmpty(master.PermitType)) {
        valid = false;
        ViewBag.error = invalidMessage;
      }
      if (!master.Amount.HasValue) {
        valid = false;
        ViewBag.error = "Please enter amount";
      }
      if (master.status == null) {
        valid = false;
        ViewBag.error = "Please select status";
      }
      if (!valid) {
        return View(master);
      } else {
        master.CreatedBy = AppUtil.checkLogin();
        master.CreatedOn = DateTime.Now;
        master.UpdatedOn = DateTime.Now;
        master.UpdatedBy = AppUtil.checkLogin();
        DB.PermitTypeMasters.Add(master);
        DB.SaveChanges();

        TempData["success"] = "Record created successfully!";
        return RedirectToAction("PTIndex");
      }

    }

    public ActionResult PTEdit(int id) {
      var master = DB.PermitTypeMasters.Where(E => E.ID == id)
        .FirstOrDefault();
      if (master == null) {
        TempData["error"] = "Record cannot be found!";
        return RedirectToAction("PTIndex");
      } else {
        return View(master);
      }

    }
    [HttpPost]
    public ActionResult PTEdit(PermitTypeMaster master) {
      var valid = true;
      string invalidMessage = "Please enter data";
      if (String.IsNullOrEmpty(master.PermitType)) {
        valid = false;
        ViewBag.error = invalidMessage;
      }
      if (!master.Amount.HasValue) {
        valid = false;
        ViewBag.error = "Please enter amount";
      }
      if (master.status == null) {
        valid = false;
        ViewBag.error = "Please select status";
      }
      if (!valid) {
        return View(master);
      } else {

        master.UpdatedOn = DateTime.Now;
        master.UpdatedBy = AppUtil.checkLogin();
        DB.PermitTypeMasters.Attach(master);
        DB.Entry(master).State = System.Data.Entity.EntityState.Modified;
        DB.SaveChanges();
        TempData["success"] = "Record updated successfully!";
        return RedirectToAction("PTIndex");
      }
    }
    public ActionResult PTDelete(int id) {

      var master = DB.PermitTypeMasters.Where(E => E.ID == id)
         .FirstOrDefault();
      var isPermits =
       DB.NormalPermits.Where(e=>e.WasteType==master.ID).ToList();
      if (isPermits.Count>0) {
        TempData["error"] = "There are permits assosiated with this Category!";
        return RedirectToAction("PTIndex");
      }
      var isPermitwast = master.PermitWasteMappings.ToList();
      if (isPermitwast.Count > 0) {
        TempData["error"] = "There are Permit Waste Mappings assosiated with this Category!";
        return RedirectToAction("PTIndex");
      }
      if (master == null) {
        TempData["error"] = "Record cannot be found!";
        return RedirectToAction("PTIndex");
      } else {
        DB.PermitTypeMasters.Remove(master);
        DB.Entry(master).State = System.Data.Entity.EntityState.Deleted;
        try {
          DB.SaveChanges();
          TempData["success"] = "Record deleted successfully!";
          return RedirectToAction("PTIndex");
        } catch (Exception ex) {

          TempData["error"] = "Record cannot be deleted, please contact admin!";
          return RedirectToAction("PTIndex");
        }
      }
    }
    #endregion

    #region -------------------------Permit WasteMapping ----------------------------
    public ActionResult PWIndex() {
      var model = DB.PermitWasteMappings.OrderByDescending(e => e.ID).ToList();
      return View(model);
    }
    public ActionResult PWCreate() {
      ViewBag.PermitType =
         DB.PermitTypeMasters.Select(e => 

           new SelectListItem() {
             Value = e.ID.ToString(),
             Text = e.PermitType
           }
         ).ToList();

      return View(new PermitWasteMapping());
    }
    [HttpPost]
    public ActionResult PWCreate(PermitWasteMapping master) {
      var valid = true;
      string invalidMessage = "Please enter data";
      if (String.IsNullOrEmpty(master.WasteType)) {
        valid = false;
        ViewBag.error = invalidMessage;
      }
      if (master.PermitTypeMasterID == 0) {
        valid = false;
        ViewBag.error = "Please select permit type master";
      }

      if (!valid) {
        ViewBag.PermitType =
       DB.PermitTypeMasters.Select(e =>

         new SelectListItem() {
           Value = e.ID.ToString(),
           Text = e.PermitType
         }
       ).ToList();
        return View(master);
      } else {
        master.CreatedBy = AppUtil.checkLogin();
        master.CreatedOn = DateTime.Now;
        master.UpdatedOn = DateTime.Now;
        master.UpdatedBy = AppUtil.checkLogin();
        DB.PermitWasteMappings.Add(master);
        try {
          DB.SaveChanges();

          TempData["success"] = "Record created successfully!";
          return RedirectToAction("PWIndex");
        } catch (Exception ex) {
          TempData["error"] = "Error inserting record!";
          return RedirectToAction("PWIndex");

        }
      }

    }

    public ActionResult PWEdit(int id) {
      var master = DB.PermitWasteMappings.Where(E => E.ID == id)
        .FirstOrDefault();
      if (master == null) {
        TempData["error"] = "Record cannot be found!";
        return RedirectToAction("PWIndex");
      } else {
        ViewBag.PermitType =
       DB.PermitTypeMasters.Select(e =>

         new SelectListItem() {
           Value = e.ID.ToString(),
           Text = e.PermitType
         }
       ).ToList();
        return View(master);
      }

    }
    [HttpPost]
    public ActionResult PWEdit(PermitWasteMapping master) {
      var valid = true;
      string invalidMessage = "Please enter data";
      if (String.IsNullOrEmpty(master.WasteType)) {
        valid = false;
        ViewBag.error = invalidMessage;
      }
      if (master.PermitTypeMasterID==0) {
        valid = false;
        ViewBag.error = "Please select permit type master";
      }
    
      if (!valid) {
        ViewBag.PermitType =
       DB.PermitTypeMasters.Select(e =>

         new SelectListItem() {
           Value = e.ID.ToString(),
           Text = e.PermitType
         }
       ).ToList();
        return View(master);
      } else {

        master.UpdatedOn = DateTime.Now;
        master.UpdatedBy = AppUtil.checkLogin();
        DB.PermitWasteMappings.Attach(master);
        DB.Entry(master).State = System.Data.Entity.EntityState.Modified;
        try {
          DB.SaveChanges();
          TempData["success"] = "Record updated successfully!";
          return RedirectToAction("PWIndex");
        } catch (Exception ex) {
          TempData["error"] = "Error updating record!";
          return RedirectToAction("PWIndex");

        }
      }
    }
    public ActionResult PWDelete(int id) {

      var master = DB.PermitWasteMappings.Where(E => E.ID == id)
         .FirstOrDefault();
      var isPermits =
       DB.NormalPermits.Where(e=>e.WasteCategory==master.ID).ToList();
      if (isPermits.Count > 0) {
        TempData["error"] = "There are permits assosiated with this Category!";
        return RedirectToAction("PWIndex");
      }
      
      if (master == null) {
        TempData["error"] = "Record cannot be found!";
        return RedirectToAction("PWIndex");
      } else {
        DB.PermitWasteMappings.Remove(master);
        DB.Entry(master).State = System.Data.Entity.EntityState.Deleted;
        try {
          DB.SaveChanges();
          TempData["success"] = "Record deleted successfully!";
          return RedirectToAction("PWIndex");
        } catch (Exception ex) {

          TempData["error"] = "Record cannot be deleted, please contact admin!";
          return RedirectToAction("PWIndex");
        }
      }
    }
    #endregion

    #region ------------------------ Location Master----------------------------------
    public ActionResult LMIndex() {
      var model = DB.LocationMasters.OrderByDescending(e => e.ID).ToList();
      return View(model);
    }
    public ActionResult LMCreate() {

      return View(new LocationMaster());
    }
    [HttpPost]
    public ActionResult LMCreate(LocationMaster master) {
      var valid = true;
      string invalidMessage = "Please enter data";
      if (String.IsNullOrEmpty(master.Location)) {
        valid = false;
        ViewBag.error = invalidMessage;
      }
      if (master.status == null) {
        valid = false;
        ViewBag.error = "Please select status";
      }
      if (!valid) {
        return View(master);
      } else {
        master.CreatedBy = AppUtil.checkLogin();
        master.CreatedOn = DateTime.Now;
        master.UpdatedOn = DateTime.Now;
        master.UpdatedBy = AppUtil.checkLogin();
        DB.LocationMasters.Add(master);
        DB.SaveChanges();

        TempData["success"] = "Record created successfully!";
        return RedirectToAction("LMIndex");
      }

    }

    public ActionResult LMEdit(int id) {
      var master = DB.LocationMasters.Where(E => E.ID == id)
        .FirstOrDefault();
      if (master == null) {
        TempData["error"] = "Record cannot be found!";
        return RedirectToAction("LMIndex");
      } else {
        return View(master);
      }

    }
    [HttpPost]
    public ActionResult LMEdit(LocationMaster master) {
      var valid = true;
      string invalidMessage = "Please enter data";
      if (String.IsNullOrEmpty(master.Location)) {
        valid = false;
        ViewBag.error = invalidMessage;
      }
      if (master.status == null) {
        valid = false;
        ViewBag.error = "Please select status";
      }
      if (!valid) {
        return View(master);
      } else {

        master.UpdatedOn = DateTime.Now;
        master.UpdatedBy = AppUtil.checkLogin();
        DB.LocationMasters.Attach(master);
        DB.Entry(master).State = System.Data.Entity.EntityState.Modified;
        DB.SaveChanges();
        TempData["success"] = "Record updated successfully!";
        return RedirectToAction("LMIndex");
      }
    }
    public ActionResult LMDelete(int id) {

      var master = DB.LocationMasters.Where(E => E.ID == id)
         .FirstOrDefault();
      var ispermit = master.FDCPermits.Any();
      if (ispermit) {
        TempData["error"] = "Error! There is FDC record assosiated with this location.";
        return RedirectToAction("LMIndex");
      }
      if (master == null) {
        TempData["error"] = "Record cannot be found!";
        return RedirectToAction("LMIndex");
      } else {
        DB.LocationMasters.Remove(master);
        DB.Entry(master).State = System.Data.Entity.EntityState.Deleted;
        DB.SaveChanges();
        TempData["success"] = "Record deleted successfully!";
        return RedirectToAction("LMIndex");
      }
    }
    #endregion


    #region Master Items
    public ActionResult ItemIndex() {
      var model = DB.MasterItems.OrderByDescending(e => e.ID).ToList();
      return View(model);
    }
    public ActionResult ItemCreate() {
     

      return View(new MasterItem());
    }
    [HttpPost]
    public ActionResult ItemCreate(MasterItem master) {
      var valid = true;
      string invalidMessage = "Please enter data";
      if (String.IsNullOrEmpty(master.ItemName)) {
        valid = false;
        ViewBag.error = invalidMessage;
      }
      if (!master.Amount.HasValue) {
        valid = false;
        ViewBag.error = "Please enter amount";
      }
      if (!master.status.HasValue) {
        valid = false;
        ViewBag.error = "Please enter status";
      }

      if (!valid) {
       
        return View(master);
      } else {
        master.EnteredBy = AppUtil.checkLogin();
        master.EnteredOn= DateTime.Now;
        master.UpdatedOn = DateTime.Now;
        master.UpdatedBy = AppUtil.checkLogin();
        DB.MasterItems.Add(master);
        try {
          DB.SaveChanges();

          TempData["success"] = "Record created successfully!";
          return RedirectToAction("ItemIndex");
        } catch (Exception ex) {
          TempData["error"] = "Error inserting record!";
          return RedirectToAction("ItemIndex");

        }
      }

    }

    public ActionResult ItemEdit(int id) {
      var master = DB.MasterItems.Where(E => E.ID == id)
        .FirstOrDefault();
      if (master == null) {
        TempData["error"] = "Record cannot be found!";
        return RedirectToAction("ItemIndex");
      } else {

        return View(master);
      }

    }
    [HttpPost]
    public ActionResult ItemEdit(MasterItem master) {
      var valid = true;
      string invalidMessage = "Please enter data";
      if (String.IsNullOrEmpty(master.ItemName)) {
        valid = false;
        ViewBag.error = invalidMessage;
      }
      if (!master.Amount.HasValue) {
        valid = false;
        ViewBag.error = "Please enter amount";
      }
      if (!master.status.HasValue) {
        valid = false;
        ViewBag.error = "Please enter status";
      }

      if (!valid) {
       
        return View(master);
      } else {

        master.UpdatedOn = DateTime.Now;
        master.UpdatedBy = AppUtil.checkLogin();
        DB.MasterItems.Attach(master);
        DB.Entry(master).State = System.Data.Entity.EntityState.Modified;
        try {
          DB.SaveChanges();
          TempData["success"] = "Record updated successfully!";
          return RedirectToAction("ItemIndex");
        } catch (Exception ex) {
          TempData["error"] = "Error updating record!";
          return RedirectToAction("ItemIndex");

        }
      }
    }
    public ActionResult ItemDelete(int id) {

      var master = DB.MasterItems.Where(E => E.ID == id)
         .FirstOrDefault();
      var isPermits =
       master.PermitItems.ToList();
      if (isPermits.Any()) {
        TempData["error"] = "Error! There are permits assosiated with this Category.";
        return RedirectToAction("PWIndex");
      }

      if (master == null) {
        TempData["error"] = "Record cannot be found!";
        return RedirectToAction("ItemIndex");
      } else {
        DB.MasterItems.Remove(master);
        DB.Entry(master).State = System.Data.Entity.EntityState.Deleted;
        try {
          DB.SaveChanges();
          TempData["success"] = "Record deleted successfully!";
          return RedirectToAction("ItemIndex");
        } catch (Exception ex) {

          TempData["error"] = "Record cannot be deleted, please contact admin!";
          return RedirectToAction("ItemIndex");
        }
      }
    }

    #endregion

    #region ------------------------ Packaging Master----------------------------------
    public ActionResult PackStorageIndex() {
      var model = DB.PackagingStorageMasters.OrderByDescending(e => e.ID).ToList();
      return View(model);
    }
    public ActionResult PackStorageCreate() {

      return View(new PackagingStorageMaster());
    }
    [HttpPost]
    public ActionResult PackStorageCreate(PackagingStorageMaster master) {
      var valid = true;
      string invalidMessage = "Please enter data";
      if (String.IsNullOrEmpty(master.PackagingStorageMaster1)) {
        valid = false;
        ViewBag.error = invalidMessage;
      }
      if (master.status == null) {
        valid = false;
        ViewBag.error = "Please select status";
      }
      if (!valid) {
        return View(master);
      } else {
        master.CreatedBy = AppUtil.checkLogin();
        master.CreatedOn = DateTime.Now;
        master.UpdatedOn = DateTime.Now;
        master.UpdatedBy = AppUtil.checkLogin();
        DB.PackagingStorageMasters.Add(master);
        DB.SaveChanges();

        TempData["success"] = "Record created successfully!";
        return RedirectToAction("PackStorageIndex");
      }

    }

    public ActionResult PackStorageEdit(int id) {
      var master = DB.PackagingStorageMasters.Where(E => E.ID == id)
        .FirstOrDefault();
      if (master == null) {
        TempData["error"] = "Record cannot be found!";
        return RedirectToAction("PackStorageIndex");
      } else {
        return View(master);
      }

    }
    [HttpPost]
    public ActionResult PackStorageEdit(PackagingStorageMaster master) {
      var valid = true;
      string invalidMessage = "Please enter data";
      if (String.IsNullOrEmpty(master.PackagingStorageMaster1)) {
        valid = false;
        ViewBag.error = invalidMessage;
      }
      if (master.status == null) {
        valid = false;
        ViewBag.error = "Please select status";
      }
      if (!valid) {
        return View(master);
      } else {

        master.UpdatedOn = DateTime.Now;
        master.UpdatedBy = AppUtil.checkLogin();
        DB.PackagingStorageMasters.Attach(master);
        DB.Entry(master).State = System.Data.Entity.EntityState.Modified;
        DB.SaveChanges();
        TempData["success"] = "Record updated successfully!";
        return RedirectToAction("PackStorageIndex");
      }
    }
    public ActionResult PackStorageDelete(int id) {

      var master = DB.PackagingStorageMasters.Where(E => E.ID == id)
         .FirstOrDefault();

      var ispermit = DB.NormalPermits.Where(e => e.PackagingStorage == master.PackagingStorageMaster1).Any();
      if (ispermit) {
        TempData["error"] = "Error! There is FDC record assosiated with this location.";
        return RedirectToAction("PackStorageIndex");
      }
      if (master == null) {
        TempData["error"] = "Record cannot be found!";
        return RedirectToAction("PackStorageIndex");
      } else {
        DB.PackagingStorageMasters.Remove(master);
        DB.Entry(master).State = System.Data.Entity.EntityState.Deleted;
        DB.SaveChanges();
        TempData["success"] = "Record deleted successfully!";
        return RedirectToAction("PackStorageIndex");
      }
    }
    #endregion

  }
}