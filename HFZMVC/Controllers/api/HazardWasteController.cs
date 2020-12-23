using HFZMVC.Helpers;
using HFZMVC.Models.EntityFramework;
using HFZMVC.Models.HazardAPI;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HFZMVC.Controllers
{

  public class HazardWasteController : ApiController
  {
    WasteManageEntities _db;

    public HazardWasteController() {
      _db = new WasteManageEntities();
    }

    [HttpOptions]
    public HttpResponseMessage Options() {
      return Request.CreateResponse(
        HttpStatusCode.OK, new {
          Status = "Success",
          Message = "Allow cross orgin option for access this method"
        });
    }
    //[HttpGet]
    //public IHttpActionResult Get() {

    //  return Json("sampleget");

    //}
    [Route("api/HazardWaste")]
    [HttpPost]
    public async Task<IHttpActionResult> Post(HazardSAPViewModel model) {
      byte[] byteOFPDF;
      try {
        byteOFPDF =
            Convert.FromBase64String(model.pdfstring);
      } catch (Exception ex) {

        return BadRequest("String is not in valid base64");
      }

      try {
        if (String.IsNullOrEmpty(model.appno)) {
          return BadRequest("AppNo is required");

        }
        if (model.clientid == 1001 && model.clientsecret == "137a374c60156dc2daf5e9a6f705d184&&0ff8d54c360d6854aff743d73a82cbab") {
          var permitFromAppNo =
            await _db.PermitRequests.Where(e => e.HazardSAP == model.appno).FirstOrDefaultAsync();
          if (permitFromAppNo == null) {
            return BadRequest("Cannot find permit associated with provided  App No: " + model.appno);
          }
          model.landfill_user = "hfzadmin";
          var user =
       await _db.Users.Where(e => e.UserName == model.landfill_user).FirstOrDefaultAsync();
          if (user == null) {
            return BadRequest("Landfill admin cannot be found on permit portal!");
          }
          try {
            File.WriteAllBytes(System.Web.Hosting.HostingEnvironment.MapPath("~/UploadedFiles/HFZContract_" + model.appno + ".pdf"), byteOFPDF);

            permitFromAppNo.StatusID = 15;
            permitFromAppNo.UpdatedBy = user.ID;
            permitFromAppNo.UpdatedOn = DateTime.Now;
            _db.Entry(permitFromAppNo).State = EntityState.Modified;

            var result = new PermitEmails()
              .SendPermitStatusChangeEmail(permitFromAppNo, _db);
            await _db.SaveChangesAsync();

           
          } catch (Exception ex) {

            return BadRequest("Error! Something went wrong processing, please contact support Error:Message: "+ex.Message);
            //Ignore for now
          }
          return Ok("Success! File inserted in permit portal");
        } else {
          return Unauthorized();

        }

      } catch (Exception) {

        return BadRequest("Error! Something went wrong with input, please contact support");
      }
   


    }
    [Route("api/HazardWaste/RequestEdit")]
    [HttpPost]
    //[ActionName("RequestEdit")]
    public async Task<IHttpActionResult> RequestForEdit([FromBody] HazardEditRequestViewModel model) {
      try {
        var isValid = true;
        var invalidReason = new List<string>();
        
        if (String.IsNullOrEmpty(model.appno)) {
          isValid = false;
          invalidReason.Add("appno is required");
        }
        if (String.IsNullOrEmpty(model.editremarks)) {
          isValid = false;
          invalidReason.Add("editremarks is required");
        }
        if (!(model.clientid == 1001 && model.clientsecret == "137a374c60156dc2daf5e9a6f705d184&&0ff8d54c360d6854aff743d73a82cbab")) {

          return Unauthorized();
        }
        var permitRequest =
           await _db.PermitRequests.Where(e => e.HazardSAP == model.appno).FirstOrDefaultAsync();
        if (permitRequest == null) {
          isValid = false;
          invalidReason.Add("Cannot find permit associated with provided  App No: " + model.appno);
        }
        model.landfill_user = "hfzadmin";
        var user =
        await _db.Users.Where(e => e.UserName == model.landfill_user).FirstOrDefaultAsync();
        if (user == null) {
          isValid = false;
          invalidReason.Add("Landfill admin cannot be found on permit portal!");
        }
        if (!isValid) {


          return BadRequest(
           invalidReason.First()
          );
        }
        

        permitRequest.StatusID = model.isreject? 16:17;
        permitRequest.HazardRemarks = model.editremarks;

        permitRequest.UpdatedBy = user.ID;
        permitRequest.UpdatedOn = DateTime.Now;
        
        _db.Entry(permitRequest).State = EntityState.Modified;

        await _db.SaveChangesAsync();

        try {
          //var result = new PermitEmails()
          //   .SendPermitStatusChangeEmail(permitRequest,_db);
        } catch (Exception ex) {

          //Ignore for now
        }
        return Ok("hazard waste application status changed successfully!");
      } catch (Exception ex) {

        return BadRequest("Error! Something went wrong with input, please contact support");
      }
    }

  }
}