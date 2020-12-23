using Aspose.Pdf;
using HFZMVC.AppLogics;
using HFZMVC.Models.EntityFramework;
using HFZMVC.Models.PermitRequest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HFZMVC.Controllers
{
	[HFZMVC.CustomBinders.HFZAuthorization]
	public class HazardCertificateController : Controller
	{
		WasteManageEntities _Db;
		// GET: HazardCertificate
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult Create(int id)
		{
			ViewBag.id = id;
			PermitRequestIndexViewModel permitRequests = new PermitRequestIndexViewModel();
			var HazardViewModel = new HazardCertificateModel();
			using (_Db = new WasteManageEntities())
			{
				string query = @"select pr.ID,pr.ApplyDate,pt.PermitType PermitType,
				0.0 TotalAmount,st.statusName Status, getDate() ValidTill, np.Remarks, UserID
				From PermitRequest pr 
				inner join NormalPermit np
					on pr.ID= np.PermitId

					left join PermitTypeMaster pt on pt.ID= np.WasteType
					left join StatusMaster st on pr.StatusID=st.ID

				where pr.ID = " + id;
				permitRequests = _Db.Database.SqlQuery<PermitRequestIndexViewModel>(query).FirstOrDefault();
				HazardViewModel.PermitId = permitRequests.ID;
				var userInfo = _Db.Users.Where(e => e.ID == permitRequests.UserID).FirstOrDefault();
				ViewBag.company = userInfo;
			}


			return View(HazardViewModel);

		}

		public async Task<ActionResult> SaveData(HazardCertificateModel data)
		{
			using (_Db = new WasteManageEntities())
			{
				
        using (var scope= _Db.Database.BeginTransaction()) {
					try {
					
						var permitId = data.PermitId;
						var PermitRequests =await _Db.PermitRequests.Where(e => e.ID == permitId).FirstOrDefaultAsync();
						var userinfo = await _Db.Users.Where(e => e.ID == PermitRequests.EnteredBy).FirstOrDefaultAsync();
						PermitRequests.FrequencyOfDisposal = data.DisposalFrequency;
						PermitRequests.WasteGenerationDescription = data.WasteDescription;
						PermitRequests.Transporter = data.transporter;
						PermitRequests.UpdatedOn = DateTime.Now;
						PermitRequests.Industry = data.TypeOfIndustry;
						PermitRequests.HazardTransporter = data.transporterDetails;
						await _Db.SaveChangesAsync();
						foreach (var item in data.hazard) {
							if (!string.IsNullOrEmpty(item.name)) {
								string query = @"INSERT INTO [dbo].[HazardContractItems]
						   ([PermitID]
						   ,[Name]
						   ,[Quantity]
						   ,[State]
						   ,[ContainerType]
							 ,[QuantityItem])
					 VALUES
						   (" + permitId +
									 ",'" + item.name + "'" +
									 ",'" + item.quantity + "'" +
									 ",'" + item.state + "'" +
									 ",'" + item.ContainerType + "'" +
									 ",'" + item.quantityItem + "')";
								var queryResult =
									 await _Db.Database.ExecuteSqlCommandAsync(query);
							}

							//_Db.Database.SqlQuery<PermitRequest>(query).First();
						}
						await SaveHazardFilesAsync(data);

						var stringcontent = GeneratePDFString(data.HPhotos, data.PermitId);
						var response =
							 SaveHazardImagePDF(stringcontent, data.PermitId);
						var certificateResponse =
							GenerateHazardCertificatePDF(new HazardCertificateModel_Certificate() {
								PermitId = data.PermitId,
								Company = userinfo,
								ID = data.PermitId,
								DisposalFrequency = data.DisposalFrequency,
								hazard = data.hazard,
								HazardSAPNo = " ",
								transporter = data.transporter,
								transporterDetails = data.transporterDetails,
								TypeOfIndustry = data.TypeOfIndustry,
								WasteDescription = data.WasteDescription
							});

						var pdfToConvert = new List<string>();
						if (!String.IsNullOrEmpty(response)) {
							pdfToConvert.Add(response);
						}
						foreach (var path in data.HMSDS) {
							pdfToConvert.Add(path);
						}
            if (!String.IsNullOrEmpty(certificateResponse)) {
							pdfToConvert.Add(certificateResponse);
            }
						var rawStringsForPDFS =
							 GetRawStrings(pdfToConvert, data.PermitId);
						var customer =
						 await _Db.Users.Where(e => e.ID == PermitRequests.EnteredBy)
						 .FirstOrDefaultAsync();
						var SapModel = new HazardSAPModel() {
							APPLICATION = rawStringsForPDFS.Count >= 3 ? rawStringsForPDFS[2] : "",
							KUNNR = String.IsNullOrEmpty(customer.SAPCustomerId) ? customer.ID.ToString() : customer.SAPCustomerId,
							MSDS = rawStringsForPDFS.Count >= 2 ? rawStringsForPDFS[1] : "",
							PICTUE = rawStringsForPDFS.Count >= 1 ? rawStringsForPDFS[0] : "",
							WDC_APPDT = DateTime.Now,
							WDC_APPNO = " ",
							WDC_EMAIL=customer.EmailID,
							WDC_MOBILE = customer.PhoneNumber,
							WDC_NAME = customer.CompanyName,
							WDC_VALIDIY = "219",



						};
						var appNo =
							await CallinSAPI.AddHazardCertificate(SapModel);
						PermitRequests.HazardSAP = appNo;

						_Db.Entry(PermitRequests).State = System.Data.Entity.EntityState.Modified;
						await _Db.SaveChangesAsync();
						scope.Commit();
						return RedirectToAction("ChangePermitStatus", "Permit", new { id = permitId, statusId = 11 });
					} catch (Exception ex) {
						scope.Dispose();
						TempData["actual"] = ex.ToString();
						;
						AppUtil.ExceptionLog(ex);
						TempData["message"] = "3";
						return RedirectToAction("Index","Permit");
						
					}
				}
      
			}
		}
		public ActionResult View(int id)
		{
			ViewBag.id = id;
			HazardCertificateModel permitRequests = new HazardCertificateModel();
			using (_Db = new WasteManageEntities())
			{
				string query = @"select UserID, WasteGenerationDescription, FrequencyOfDisposal, Transporter From PermitRequest where ID = " + id;
				permitRequests = _Db.Database.SqlQuery<HazardCertificateModel>(query).FirstOrDefault();

				string haz = @"select Name as name, Quantity as quantity, State as state, ContainerType From HazardContractItems where PermitID = " + id;
				ViewBag.hazards = _Db.Database.SqlQuery<HazardItems>(haz).ToList();

				var userInfo = _Db.Users.Where(e => e.ID == permitRequests.UserID).FirstOrDefault();
				ViewBag.company = userInfo;
			}
			return View(permitRequests);
		}


		public  async Task<ActionResult> Edit(int id) {
			ViewBag.id = id;
			PermitRequest permitRequests = new PermitRequest();
			var HazardViewModel = new HazardCertificateEditModel();
			using (_Db = new WasteManageEntities()) {
			
				permitRequests = await _Db.PermitRequests.Where(e=>e.ID==id).FirstOrDefaultAsync();
				HazardViewModel.PermitId = permitRequests.ID;
				var userInfo = await _Db.Users.Where(e => e.ID == permitRequests.UserID).FirstOrDefaultAsync();

				HazardViewModel.WasteDescription = permitRequests.WasteGenerationDescription;
				HazardViewModel.DisposalFrequency = permitRequests.FrequencyOfDisposal;
				HazardViewModel.transporter = permitRequests.Transporter;
				HazardViewModel.TypeOfIndustry = permitRequests.Industry;
				HazardViewModel.HazardSAPNo = permitRequests.HazardSAP;
				HazardViewModel.transporterDetails = permitRequests.HazardTransporter;

			var hazardlist =
				await	 _Db.HazardContractItems.Where(e=>e.PermitID==id).ToListAsync();
				HazardViewModel.hazard = new List<HazardItems>();

				foreach (var item in hazardlist) {
					HazardViewModel.hazard.Add(new HazardItems() {
						ContainerType = item.ContainerType,
						ID = item.ID,
						name = item.Name,
						quantity = item.Quantity,
						state = item.State,
						quantityItem=item.QuantityItem,
					});
        }
				var hazarddocuments
					= await _Db.PermitDocumentDetails.Where(e => e.PermitID == id).ToListAsync();
        if (hazarddocuments.Any()) {
					HazardViewModel.HPhotos = new List<FileModel>();
					HazardViewModel.HMSDS = new List<FileModel>();

					foreach (var item in hazarddocuments) {
						if (item.DocTypeID == 6) {
							HazardViewModel.HPhotos.Add(new FileModel() {Id=item.ID, FilePath = item.DocumentPath }); ;

						} else if (item.DocTypeID == 7) {
							HazardViewModel.HMSDS.Add(new FileModel() {Id=item.ID, FilePath = item.DocumentPath });
						}
					}
				}
        

				ViewBag.company = userInfo;
			}

			PopulateViewBags();
			return View(HazardViewModel);

		}
		[HttpPost]
		public async Task<ActionResult> Edit(HazardCertificateEditModel data) {
			using (_Db = new WasteManageEntities()) {
				using (var scope = _Db.Database.BeginTransaction()) {
					try {
						var permitId = data.PermitId;
						var PermitRequests =await _Db.PermitRequests.Where(e => e.ID == permitId).FirstOrDefaultAsync();
						var userinfo = await _Db.Users.Where(e => e.ID == PermitRequests.EnteredBy).FirstOrDefaultAsync();
						PermitRequests.FrequencyOfDisposal = data.DisposalFrequency;
						PermitRequests.WasteGenerationDescription = data.WasteDescription;
						PermitRequests.Transporter = data.transporter;
						PermitRequests.UpdatedOn = DateTime.Now;
						PermitRequests.Industry = data.TypeOfIndustry;
						PermitRequests.HazardTransporter = data.transporterDetails;
						_Db.Entry(PermitRequests).State = System.Data.Entity.EntityState.Modified;
						await _Db.SaveChangesAsync();
						var oldHazards = await _Db.HazardContractItems.Where(e => e.PermitID == data.PermitId)
							.ToListAsync();
						_Db.HazardContractItems.RemoveRange(oldHazards);

						foreach (var item in data.hazard) {
							if (!string.IsNullOrEmpty(item.name)) {
								string query = @"INSERT INTO [dbo].[HazardContractItems]
						   ([PermitID]
						   ,[Name]
						   ,[Quantity]
						   ,[State]
						   ,[ContainerType]
							 ,[QuantityItem])
					 VALUES
						   (" + permitId +
									 ",'" + item.name + "'" +
									 ",'" + item.quantity + "'" +
									 ",'" + item.state + "'" +
									 ",'" + item.ContainerType + "'" +
									 ",'" + item.quantityItem + "')";
								var queryResult =
									 await _Db.Database.ExecuteSqlCommandAsync(query);
							}

							//_Db.Database.SqlQuery<PermitRequest>(query).First();
						}
						var oldfiles =
							 await _Db.PermitDocumentDetails.Where(e => e.PermitID == data.PermitId && (e.DocTypeID == 6 || e.DocTypeID == 7)).ToListAsync();
						_Db.PermitDocumentDetails.RemoveRange(oldfiles);
						await _Db.SaveChangesAsync();

						
						await SaveHazardFilesEditAsync(data);
						//scope.Commit();
						var stringcontent = GeneratePDFString(data.HPhotos.Where(e => e.IsDeleted == false).Select(e => e.FilePath).ToList(), data.PermitId);
						var response =
							 SaveHazardImagePDF(stringcontent,data.PermitId);

						var certificateResponse =
					GenerateHazardCertificatePDF(new HazardCertificateModel_Certificate() {
						PermitId = data.PermitId,
						Company = userinfo,
						ID = data.PermitId,
						DisposalFrequency = data.DisposalFrequency,
						hazard = data.hazard,
						HazardSAPNo = PermitRequests.HazardSAP,
						transporter = data.transporter,
						transporterDetails = data.transporterDetails,
						TypeOfIndustry = data.TypeOfIndustry,
						WasteDescription = data.WasteDescription
					});
						var pdfToConvert = new List<string>();
            if (!String.IsNullOrEmpty( response)) {
							pdfToConvert.Add(response);
            }
            foreach (var path in data.HMSDS.Where(e => e.IsDeleted == false).Select(e => e.FilePath).ToList()) {
							pdfToConvert.Add(path);
            }
						if (!String.IsNullOrEmpty(certificateResponse)) {
							pdfToConvert.Add(certificateResponse);
						}
						var rawStringsForPDFS =
							 GetRawStrings(pdfToConvert,data.PermitId);
						var customer =
							 await _Db.Users.Where(e => e.ID == PermitRequests.EnteredBy)
							 .FirstOrDefaultAsync();
						var SapModel = new HazardSAPModel() { 
						APPLICATION= rawStringsForPDFS.Count >= 3 ? rawStringsForPDFS[2] : "",
						KUNNR=String.IsNullOrEmpty( customer.SAPCustomerId)? customer.ID.ToString():customer.SAPCustomerId,
						MSDS=rawStringsForPDFS.Count>=2?rawStringsForPDFS[1]:"",
						PICTUE= rawStringsForPDFS.Count >= 1 ? rawStringsForPDFS[0] : "",
						WDC_APPDT=DateTime.Now,
						WDC_APPNO=String.IsNullOrEmpty(PermitRequests.HazardSAP)?" ": PermitRequests.HazardSAP,
						WDC_MOBILE=customer.PhoneNumber,
							WDC_EMAIL = customer.EmailID,
						
							WDC_NAME =customer.CompanyName,
						 WDC_VALIDIY="219",
						 


						};
						var appNo =
						await	 CallinSAPI.AddHazardCertificate(SapModel);

						scope.Commit();
						return RedirectToAction("ChangePermitStatus", "Permit", new { id = permitId, statusId = 11 });
					} catch (Exception ex) {
						scope.Dispose();
						TempData["actual"] = ex.ToString();
						;
						AppUtil.ExceptionLog(ex);
						TempData["message"] = "3";
						return RedirectToAction("Index", "Permit");

					}
				}

			}
		}


		public ActionResult HazardousGuidelines() {

			return View();
		}
		//[HttpPost]
		//public async Task<ActionResult> Edit(HazardCertificateEditModel data) {
		//	using (_Db = new WasteManageEntities()) {
		//		var permitId = data.PermitId;
		//		var PermitRequests = _Db.PermitRequests.Where(e => e.ID == permitId).FirstOrDefault();
		//		PermitRequests.FrequencyOfDisposal = data.DisposalFrequency;
		//		PermitRequests.WasteGenerationDescription = data.WasteDescription;
		//		PermitRequests.Transporter = data.transporter;
		//		PermitRequests.UpdatedOn = DateTime.Now;
		//		_Db.Entry(PermitRequests).State = System.Data.Entity.EntityState.Modified;
		//		await _Db.SaveChangesAsync();

		//		var oldHazards =
		//			 await _Db.HazardContractItems.Where(e => e.PermitID == permitId).ToListAsync();
		//		 _Db.HazardContractItems.RemoveRange(oldHazards);

		//		foreach (var item in data.hazard) {
		//			if (!string.IsNullOrEmpty(item.name)) {
		//				string query = @"INSERT INTO [dbo].[HazardContractItems]
		//				   ([PermitID]
		//				   ,[Name]
		//				   ,[Quantity]
		//				   ,[State]
		//				   ,[ContainerType]
		//						,[HazardApplicationID])
		//			 VALUES
		//				   (" + permitId +
		//					 ",'" + item.name + "'" +
		//					 ",'" + item.quantity + "'" +
		//					 ",'" + item.state + "'" +
		//					 ",'" + item.ContainerType + "'" +
		//					 ",0)";
		//				var queryResult =
		//					 await _Db.Database.ExecuteSqlCommandAsync(query);
		//			}

		//			//_Db.Database.SqlQuery<PermitRequest>(query).First();
		//		}
		//	//	await SaveHazardFilesAsync(data);

		//		return RedirectToAction("ChangePermitStatus", "Permit", new { id = permitId, statusId = 11 });
		//	}
		//}


		#region PDF Generation


		public string GeneratePDFString(List<string> imagelist,int id) {

      try {
        ViewBag.Id = id;
        ViewData.Model = imagelist;
        using (var sw = new StringWriter()) {
          var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "~/Views/HazardCertificate/GenerateHazardPDFView.cshtml");
          var viewContext = new ViewContext(ControllerContext, viewResult.View,
                   ViewData, TempData, sw);
          viewResult.View.Render(viewContext, sw);
          viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
          return sw.GetStringBuilder().ToString();

        }
      } catch (Exception ex) {

        
      }

			return "";

    }

		public string SaveHazardImagePDF(string contents,int id) {

      try {
        var options = new HtmlLoadOptions();
        options.PageInfo.Margin.Left = 5;
        options.PageInfo.Margin.Right = 5;
        options.PageInfo.Margin.Top = 5;
        options.PageInfo.Margin.Bottom = 5;

        var attchmentString = contents;
        var byteArray = Encoding.UTF8.GetBytes(attchmentString);
        var ms = new MemoryStream(byteArray);

        var liscencePath = Server.MapPath("~/Helpers/Aspose.Total.lic");

        License objlicense = new License();

        objlicense.SetLicense(liscencePath);

        var pdfDocument = new Document(ms, options);


        var savePath = $"~/UploadedFiles/Hazard/{id}/HazardImages_" + id + ".pdf";
        pdfDocument.FitWindow = true;
        //using (FileStream fs =new FileStream(Server.MapPath())) {

        //}
        pdfDocument.Save(Server.MapPath( savePath), SaveFormat.Pdf);
        return "HazardImages_" + id + ".pdf";
      } catch (Exception ex) {

				return "";
      }
					
		}
		public List<string> GetRawStrings(List<string> locations,int id) {
			var returnString = new List<string>();
      foreach (var item in locations) {
				var file = System.IO.File.ReadAllBytes(Server.MapPath($"~/UploadedFiles/Hazard/{id}/{item}"));
				var rawstring =
				Convert.ToBase64String(file);
				returnString.Add(rawstring);
			

			}
			return returnString;

			///Read bytes form PDF File

			//var rawstring = System.Text.Encoding.UTF8.GetString(file);
			//var checkArray =
			//	new System.Collections.BitArray(file);
			//convert to base64 representation
			

		}

		public string SaveToSAP() {

			//var APPID =
			// CallinSAPI.AddHazardCertificate(new HazardCertificateModel(), rawstring);
			return "";
		}


		public async Task<ActionResult> GenerateHazardPDF(int id) {

			ViewBag.Id = id;
			_Db =new WasteManageEntities();
			var permitDocuments =
						await  _Db.PermitDocumentDetails.Where(e => e.PermitID == id && e.DocTypeID==6).Select(e=>e.DocumentPath).ToListAsync();
		
				 
			return View("GenerateHazardPDFView", permitDocuments);
		}
		#endregion



		#region Uplaod HazardFile part

		[HttpPost]
		public ActionResult UploadHazardFiles(string type) {
			//
			//Code credits: https://www.c-sharpcorner.com/UploadFile/manas1/upload-files-through-jquery-ajax-in-Asp-Net-mvc/
			if (Request.Files.Count > 0) {
				try {
					//  Get all files from Request object  
					HttpFileCollectionBase files = Request.Files;
					List<string> FilePaths = new List<string>();
					for (int i = 0; i < files.Count; i++) {
						HttpPostedFileBase file = files[i];
						string fname;
						// Checking for Internet Explorer  
						if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER") {
							string[] testfiles = file.FileName.Split(new char[] { '\\' });
							fname = testfiles[testfiles.Length - 1];
						} else {
							fname = file.FileName;
						}
						string prependFileName = type == "photos" ? "hazardphotos" : "hazardmsds";
						fname = prependFileName + DateTimeOffset.UtcNow.ToUnixTimeSeconds() + "_" + fname;
						var fullname = Path.Combine(Server.MapPath("~/UploadedFiles/Temp"), fname);
						file.SaveAs(fullname);
						FilePaths.Add(fname);
					}

					return Json(FilePaths);

				} catch (Exception ex) {

					return Json("error-" + ex.ToString());
				}
			

			} else {
				return Json("error-No files found");
			}

		}
		private async Task<bool> SaveHazardFilesAsync(HazardCertificateModel viewModel) {
			try {
				List<string> documentToMove = new List<string>();
				if (viewModel.HPhotos != null) {
					foreach (var item in viewModel.HPhotos) {	
						documentToMove.Add(item);
						var documentDetail = new PermitDocumentDetail() {

							CreatedBy = AppUtil.checkLogin(),
							CreatedOn = DateTime.Now,
							DocTypeID = 6,
							DocumentPath = item,
							PermitID = viewModel.PermitId,
							UpdatedBy = AppUtil.checkLogin(),
							UpdatedOn = DateTime.Now
						};


						_Db.PermitDocumentDetails.Add(documentDetail);
						await _Db.SaveChangesAsync();

					}
				}

				if (viewModel.HMSDS!=null) {
          foreach (var item in viewModel.HMSDS) {
						var documentDetail = new PermitDocumentDetail() {

							CreatedBy = AppUtil.checkLogin(),
							CreatedOn = DateTime.Now,
							DocTypeID = 7,
							DocumentPath = item,
							PermitID = viewModel.PermitId,
							UpdatedBy = AppUtil.checkLogin(),
							UpdatedOn = DateTime.Now
						};
						documentToMove.Add(item);
						_Db.PermitDocumentDetails.Add(documentDetail);
						await _Db.SaveChangesAsync();
					}
					
				
					
				}

				foreach (var item in documentToMove) {
					string Destinationdirectory = Server.MapPath(string.Format("~/UploadedFiles/Hazard/{0}/", viewModel.PermitId));

					if (!Directory.Exists(Destinationdirectory)) {
						Directory.CreateDirectory(Destinationdirectory);
					}
					string source = Path.Combine(Server.MapPath("~/UploadedFiles/Temp"), item);
					string destination = Path.Combine(Destinationdirectory, item);

					if (!System.IO.File.Exists(destination)) {
						System.IO.File.Move(source, destination);
					}
				}
				return true;
			} catch (Exception ex) {

				return false;
			}
		}

		private async Task<bool> SaveHazardFilesEditAsync(HazardCertificateEditModel viewModel) {
			try {
				List<string> documentToMove = new List<string>();
				if (viewModel.HPhotos != null) {
					foreach (var item in viewModel.HPhotos.Where(e=>e.IsDeleted==false)) {
						documentToMove.Add(item.FilePath);
						var documentDetail = new PermitDocumentDetail() {

							CreatedBy = AppUtil.checkLogin(),
							CreatedOn = DateTime.Now,
							DocTypeID = 6,
							DocumentPath = item.FilePath,
							PermitID = viewModel.PermitId,
							UpdatedBy = AppUtil.checkLogin(),
							UpdatedOn = DateTime.Now
						};


						_Db.PermitDocumentDetails.Add(documentDetail);
						await _Db.SaveChangesAsync();

					}
				}

				if (viewModel.HMSDS != null) {
					foreach (var item in viewModel.HMSDS.Where(e=>e.IsDeleted==false)) {
						var documentDetail = new PermitDocumentDetail() {

							CreatedBy = AppUtil.checkLogin(),
							CreatedOn = DateTime.Now,
							DocTypeID = 7,
							DocumentPath = item.FilePath,
							PermitID = viewModel.PermitId,
							UpdatedBy = AppUtil.checkLogin(),
							UpdatedOn = DateTime.Now
						};
						documentToMove.Add(item.FilePath);
						_Db.PermitDocumentDetails.Add(documentDetail);
						await _Db.SaveChangesAsync();
					}



				}

				foreach (var item in documentToMove) {
					string Destinationdirectory = Server.MapPath(string.Format("~/UploadedFiles/Hazard/{0}/", viewModel.PermitId));

					if (!Directory.Exists(Destinationdirectory)) {
						Directory.CreateDirectory(Destinationdirectory);
					}
					string source = Path.Combine(Server.MapPath("~/UploadedFiles/Temp"), item);
					string destination = Path.Combine(Destinationdirectory, item);

					if (!System.IO.File.Exists(destination)) {
						System.IO.File.Move(source, destination);
					}
				}
				return true;
			} catch (Exception ex) {

				return false;
			}
		}
		#endregion





		#region HazardFinalCertificate

		public string GenerateHazardCertificatePDF(HazardCertificateModel_Certificate model) {

			using (var sw = new StringWriter()) {
				var HazardViewModel = model;
				ViewData.Model = HazardViewModel;
				var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "~/Views/HazardCertificate/GenerateHazardCertificatePDF.cshtml");
				var viewContext = new ViewContext(ControllerContext, viewResult.View,
								 ViewData, TempData, sw);
				viewResult.View.Render(viewContext, sw);
				viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
				var pdfcontent= sw.GetStringBuilder().ToString();
				try {
					var options = new HtmlLoadOptions();
					//options.PageInfo.Margin.Left = 5;
					//options.PageInfo.Margin.Right = 5;
					options.PageInfo.Margin.Top = 1;
					options.PageInfo.Margin.Bottom = 0;

					var attchmentString = pdfcontent;
					var byteArray = Encoding.UTF8.GetBytes(attchmentString);
					var ms = new MemoryStream(byteArray);

					var liscencePath = Server.MapPath("~/Helpers/Aspose.Total.lic");

					License objlicense = new License();

					objlicense.SetLicense(liscencePath);

					var pdfDocument = new Document(ms, options);


					var savePath = $"~/UploadedFiles/Hazard/{model.PermitId}/HazardCertificate_{model.PermitId}.pdf";
					pdfDocument.FitWindow = true;
					//using (FileStream fs =new FileStream(Server.MapPath())) {

					//}
					pdfDocument.Save(Server.MapPath(savePath), SaveFormat.Pdf);
					return $"HazardCertificate_{model.PermitId}.pdf";
				} catch (Exception ex) {

					return "";
				}
			}
			//return View();
			
		}
		#endregion



		#region Endpoint for SAP
		public string HWCertificate(object pdfstring, int clientid,string clientsecret) {

			byte[] result;
			try {
				result = Convert.FromBase64String(pdfstring.ToString());

			} catch (Exception ex) {

				return "PDF is not valid Base64 binary string";
      }
			var
				check =
				 Convert.ToBase64String(pdfstring as byte[]);

			return "ok";
		}

		#endregion

		public void PopulateViewBags() {

			ViewBag.TypeOfIndustryList = new List<SelectListItem>() {
			new SelectListItem(){Text="Accommodation and food service activities",Value="Accommodation and food service activities"},
			new SelectListItem(){ Text="Administrative and support service activities",Value="Administrative and support service activities"},
			new SelectListItem(){ Text="Agriculture & Farms Industry",Value="Agriculture & Farms Industry"},
			new SelectListItem(){ Text="Arts, entertainment and recreation",Value="Arts, entertainment and recreation"},
			new SelectListItem(){ Text="Cargo & Transportation",Value="Cargo & Transportation"},
			new SelectListItem(){ Text="Chemicals and pharmaceutical Industry",Value="Chemicals and pharmaceutical Industry"},
				new SelectListItem(){ Text="Construction and demolition",Value="Construction and demolition"},
				new SelectListItem(){ Text="Education",Value="Education"},
				new SelectListItem(){ Text="Electronic products",Value="Electronic products"},
				new SelectListItem(){ Text="Energy Industry",Value="Energy Industry"},
				new SelectListItem(){ Text="Financial and insurance activities",Value="Financial and insurance activities"},
				new SelectListItem(){ Text="Food & Beverages products Industry",Value="Food & Beverages products Industry"},
				new SelectListItem(){ Text="Health Care Industry",Value="Health Care Industry"},
				new SelectListItem(){ Text="HVAC Industry",Value="HVAC Industry"},
				new SelectListItem(){ Text="Information and communication",Value="Information and communication"},
				new SelectListItem(){ Text="Maintenance and technical Activites",Value="Maintenance and technical Activites"},
				new SelectListItem(){ Text="Manufacturing Industry",Value="Manufacturing Industry"},
				new SelectListItem(){ Text="Marine services",Value="Marine services"},
				new SelectListItem(){ Text="Metals and metal works Industry",Value="Metals and metal works Industry"},
				new SelectListItem(){ Text="Mining and Quarrying",Value="Mining and Quarrying"},
				new SelectListItem(){ Text="Oil & Gas",Value="Oil & Gas"},
				new SelectListItem(){ Text="Paints Industry",Value="Paints Industry"},
				new SelectListItem(){ Text="Petroleum and lubricants industry",Value="Petroleum and lubricants industry"},
				new SelectListItem(){ Text="Printing press",Value="Printing press"},
				new SelectListItem(){ Text="Public admin and defence, compulsory social security",Value="Public admin and defence, compulsory social security"},
				new SelectListItem(){ Text="Pulp & Paper Industry",Value="Pulp & Paper Industry"},
				new SelectListItem(){ Text="Vehicles service and repairing",Value="Vehicles service and repairing"},
				new SelectListItem(){ Text="Water Treatment Industry",Value="Water Treatment Industry"},
				new SelectListItem(){ Text="Wood Industry",Value="Wood Industry"},
				new SelectListItem(){ Text="Other Services",Value="Other Services"},
		};

			ViewBag.DisposalFrequencyList = new List<SelectListItem>() {
			new SelectListItem(){Text="Once",Value="Once"},
			new SelectListItem(){ Text="Weekly",Value="Weekly"},
			new SelectListItem(){ Text="Monthly",Value="Monthly"},
			new SelectListItem(){ Text="Quarterly",Value="Quarterly"},
			new SelectListItem(){ Text="Every 6 months",Value="Every 6 months"},
			new SelectListItem(){ Text="Annually",Value="Annually"},
		};

			ViewBag.stateList = new List<SelectListItem>() {
			new SelectListItem(){Text="Liquid",Value="Liquid"},
			new SelectListItem(){ Text="Solid",Value="Solid"},
			new SelectListItem(){ Text="Semi-Solid",Value="Semi-Solid"},
			new SelectListItem(){ Text="Gas",Value="Gas"},

		};
			ViewBag.ContainerTypeList = new List<SelectListItem>() {
			new SelectListItem(){Text="Type 1",Value="Type 1"},
			new SelectListItem(){ Text="Type 2",Value="Type 2"},
			new SelectListItem(){ Text="Type 3",Value="Type 3"},
			new SelectListItem(){ Text="Type 3",Value="Type "},

		};



		}
	}
}