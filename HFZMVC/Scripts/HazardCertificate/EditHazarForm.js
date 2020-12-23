var imgUpload = document.getElementById('upload_imgs')
  , imgPreview = document.getElementById('img_preview')
  , imgUploadForm = document.getElementById('img-upload-form')
  , totalFiles
  , previewTitle
  , previewTitleText
  , img
  , permitImages = []
  , permitMSDS = []
  , imageidCounter = 0
  , rowButton = document.getElementById("btnMoreRows")
  , hazardcounter = 0;

//rowButton.addEventListener('click', function (e) {
//  e.preventDefault();
//  hazardcounter++;
//  $("#tblHazardItems").append(`  <tr id="hazard_` + hazardcounter + `">
//                  <td>
//                    <input id="hazard[`+ hazardcounter + `][name]" name="hazard[` + hazardcounter + `][name]" class="form-control" type="text" placeholder="Waste Name">
//                  </td>
//                  <td>
//                    <input class="form-control" type="text" required placeholder="Waste Quantity" id="hazard[`+ hazardcounter + `][quantityItem]" name="hazard[` + hazardcounter + `][quantityItem]" style="width: 50%; float: left;">
//                    <select id="hazard[`+ hazardcounter + `][quantity]" name="hazard[` + hazardcounter + `][quantity]" class="form-control" style="width: 50%">
//                      <option value="KG">KG</option>
//                      <option value="TON">TON</option>
//                    </select>
//                  </td>
//                  <td>
//                    <select id="hazard[`+ hazardcounter + `][state]" name="hazard[` + hazardcounter + `][state]" class="form-control">
//                      <option value="Liquid">Liquid</option>
//                      <option value="Solid">Solid</option>
//                      <option value="Semi-Solid">Semi-Solid</option>
//                      <option value="Gas">Gas</option>
//                    </select>
//                  </td>
//                  <td>
//                    <select id="hazard[`+ hazardcounter + `][ContainerType]" name="hazard[` + hazardcounter + `][ContainerType]" class="form-control">
//                      <option value="Type 1">Type 1</option>
//                      <option value="Type 2">Type 2</option>
//                      <option value="Type 3">Type 3</option>
//                      <option value="Type 4">Type 4</option>
//                    </select>
//                  </td>
//                </tr>`)

//});






function UploadPermitFiles() {
  // Checking whether FormData is available in browser  
  if (window.FormData !== undefined) {

    var fileUpload = $("#upload_imgs").get(0);
    var files = fileUpload.files;

    // Create FormData object  
    var fileData = new FormData();

    // Looping over all files and add it to FormData object  
    for (var i = 0; i < files.length; i++) {
      let file = files[i].name.split('\\').pop().trim();

      var regex = /(?:\.([^.]+))?$/;
      var ext = regex.exec(file)[1].toLocaleLowerCase();
      console.log(ext);
      if (ext == "jpg" || ext == "jpeg" || ext == "png") {
        fileData.append(files[i].name, files[i]);
      }
      else {
        alert("File with extension \"" + ext + "\" is not allowed. Only JPG and PNG format are acceptable.");
        return;
      }

    }

    // Adding one more key to FormData object  
    if (permitImages.length > 20) {
      alert("Please upload 15 only!");
    }
    permitImages.length = 0;
    $.ajax({
      url: '/HazardCertificate/UploadHazardFiles?type=photos',
      type: "POST",
      contentType: false, // Not to set any content header  
      processData: false, // Not to process data  
      data: fileData,
      success: function (result) {
        if (result.includes("error")) {
          alert(result);
        } else {

          
          for (var i = 0; i < result.length; i++) {
            imageidCounter++;
            permitImages.push(result[i]);


          }

          PopulateImageHiddenForm();

        }

      },
      error: function (err) {
        alert(err.statusText);
      }
    });
  } else {
    alert("FormData is not supported.");
  }

  $("#PermitFiles").val(null);
}





function UploadMSDSFiles() {
  // Checking whether FormData is available in browser  
  if (window.FormData !== undefined) {

    var fileUpload = $("#upmsds").get(0);
    var files = fileUpload.files;

    // Create FormData object  
    var fileData = new FormData();

    // Looping over all files and add it to FormData object  
    for (var i = 0; i < files.length; i++) {
      let file = files[i].name.split('\\').pop().trim();

      var regex = /(?:\.([^.]+))?$/;
      var ext = regex.exec(file)[1].toLocaleLowerCase();
      console.log(ext);
      if (ext == "pdf" ) {
        fileData.append(files[i].name, files[i]);
      }
      else {
        alert("Kindly upload .PDF file format only.");
        return;
      }

    }

    // Adding one more key to FormData object  
    if (permitMSDS.length > 20) {
      alert("Please upload 20 files only!");
      return;
    }
    permitMSDS.length = 0;
    $.ajax({
      url: '/HazardCertificate/UploadHazardFiles?type=msds',
      type: "POST",
      contentType: false, // Not to set any content header  
      processData: false, // Not to process data  
      data: fileData,
      success: function (result) {
        if (result.includes("error")) {
          alert(result);
        } else {

          //var generatedHtml = '';
          //imgPreview.classList.remove('quote-imgs-thumbs--hidden');
          //previewTitle = document.createElement('p');
          //previewTitle.style.fontWeight = 'bold';
          //previewTitleText = document.createTextNode(result.length + ' Total Images Selected');
          //previewTitle.appendChild(previewTitleText);
          //imgPreview.appendChild(previewTitle);


          for (var i = 0; i < result.length; i++) {
            imageidCounter++;
            permitMSDS.push(result[i]);

            //img = document.createElement('img');
            //img.src = "/UploadedFiles/Temp/" + result[i] + "";
            //img.classList.add('img-preview-thumb');
            //imgPreview.appendChild(img);


          }

          PopulateMSDSHiddenForm();

        }

      },
      error: function (err) {
        alert(err.statusText);
      }
    });
  } else {
    alert("FormData is not supported.");
  }

  $("#PermitFiles").val(null);
}


function PopulateImageHiddenForm() {


  for (var i = 0; i < permitImages.length; i++) {

    $("#permitmsdsform").append('<input type="hidden" name="HPhotos[' + hazardImageCounter + '].Id" id="HPhotos[' + hazardImageCounter + '].Id" value="110" />');
    $("#permitmsdsform").append(`<input type="hidden" name="HPhotos[` + hazardImageCounter + `].FilePath" id="HPhotos[` + hazardImageCounter + `].FilePath" value="` + permitImages[i] + `" />`);
    $("#permitmsdsform").append('<input type="hidden" name="HPhotos[' + hazardImageCounter + '].IsDeleted" id="HPhotos[' + hazardImageCounter + '].IsDeleted" value="false" />');
    $("#permitmsdsform").append('<input type="hidden" id="HPhotos_New_' + hazardImageCounter + '" value="' + hazardImageCounter + '" />');

    $("#ulHPhotos").append(` <li id="li_HPhotos_New_` + hazardImageCounter + `"> 
                    <a target="_blank" href="/UploadedFiles/Temp/`+ permitImages[i] + `"  class="btn btn-primary">View Photo</a>
                    <a href="javascript:void(0)"  onclick="deleteFile('New_`+ hazardImageCounter + `','hphoto')" style="text-decoration:underline;color:blue">x Remove</a>
                  </li>`);
    hazardImageCounter++;
  } 

}


function PopulateMSDSHiddenForm() {

 
  
  for (var i = 0; i < permitMSDS.length; i++) {

    $("#permitmsdsform").append('<input type="hidden" name="HMSDS[' + hazardMSDSCoutner + '].Id" id="HMSDS[' + hazardMSDSCoutner + '].Id" value="110" />');
    $("#permitmsdsform").append(`<input type="hidden" name="HMSDS[` + hazardMSDSCoutner + `].FilePath" id="HMSDS[` + hazardMSDSCoutner + `].FilePath" value="` + permitMSDS[i] +`" />`);
    $("#permitmsdsform").append('<input type="hidden" name="HMSDS[' + hazardMSDSCoutner + '].IsDeleted" id="HMSDS[' + hazardMSDSCoutner + '].IsDeleted" value="false" />');
    $("#permitmsdsform").append('<input type="hidden" id="HMSDS_New_' + hazardMSDSCoutner + '" value="' + hazardMSDSCoutner + '" />');

    $("#ulHazardMSDS").append(` <li id="li_HMSDS_New_` + hazardMSDSCoutner + `"> 
                    <a target="_blank" href="/UploadedFiles/Temp/`+ permitMSDS[i] + `"  class="btn btn-primary">View File</a>
                    <a href="javascript:void(0)"  onclick="deleteFile('New_`+ hazardMSDSCoutner +`','msds')" style="text-decoration:underline;color:blue">x Remove</a>
                  </li>`);
    hazardMSDSCoutner++;
  }
}


///Form Validation
function checkform() {
  var validatoinerror = document.getElementById('ulErorlist');
  $("#validationErrors").hide();
  validatoinerror.textContent = "";
  var isValid = true;
  if (!document.getElementById('chkAcceptTerms').checked) {
    var errormessage = "<li>You did not accept Hazard Waste Guidelines</li>";

    validatoinerror.innerHTML += errormessage;


    isValid = false;
  }

  if ($("#WasteDescription").val() == "") {
    var errormessage = "<li>Please add Description of waste generation</li>";
    validatoinerror.innerHTML += errormessage;
    $("#WasteDescription").addClass("errorForm")
    isValid = false;
  }
  if ($('input[name="transporter"]:checked').val() == undefined || $('input[name="transporter"]:checked').val() == "") {
    var errormessage = "<li>Please select transporter</li>";
    $("#spTransporterRequired").show();
    validatoinerror.innerHTML += errormessage;


    isValid = false;
  }

  if ($('input[name="transporter"]:checked').val() == "Other") {
    if ($("#transporterDetails").val() == "") {
      var errormessage = "<li>Please fill Transporter details</li>";

      validatoinerror.innerHTML += errormessage;
      $("#transporterDetails").addClass("errorForm")

      isValid = false;
    }
  }
  if (hazardImageCounter == 0) {
    var errormessage = "<li>Please add Hazard Waste photos</li>";

    validatoinerror.innerHTML += errormessage;


    isValid = false;
  }
  if (hazardMSDSCoutner == 0) {
    var errormessage = "<li>Please upload Material Safety Data Sheet</li>";

    validatoinerror.innerHTML += errormessage;


    isValid = false;
  }
  if (isValid) {
    //toggleLoading();
    $('#dvSubmitControl').show();
    $('#dvPreSubmitControl').hide();
    return true;
  } else {
    $("#validationErrors").show();
    return false;
  }

}


function deleteFile(id, type) {


  if (type == "msds") {
    var indexMSDS = $(jq("HMSDS_" + id)).val();
    if (indexMSDS !== undefined) {

      $(jq('HMSDS[' + indexMSDS + '].IsDeleted')).val("true");
    }
    $(jq("li_HMSDS_" + id)).remove();
    hazardMSDSCoutner--;
  }
  else if (type == "hphoto") {
    var indexImages = $(jq("HPhotos_" + id)).val();
    if (indexImages !== undefined) {

      $(jq('HPhotos[' + indexImages + '].IsDeleted')).val("true");
    }
    $(jq("li_HPhotos_" + id)).remove();
    hazardImageCounter--;
  }

}


function jq(myid) {

  return "#" + myid.replace(/(:|\.|\[|\]|,|=|@)/g, "\\$1");

}