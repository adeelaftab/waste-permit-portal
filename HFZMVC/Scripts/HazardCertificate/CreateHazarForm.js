var imgUpload = document.getElementById('upload_imgs')
  , imgPreview = document.getElementById('img_preview')
  , imgUploadForm = document.getElementById('img-upload-form')
  , totalFiles
  , previewTitle
  , previewTitleText
  , img
  , permitImages = []
  ,permitMSDS=[]
  , imageidCounter = 0
  , rowButton = document.getElementById("btnMoreRows")
  , hazardcounter = 0;

rowButton.addEventListener('click', function (e) {
  e.preventDefault();
  hazardcounter++;
  $("#tblHazardItems").append(`  <tr id="hazard_`+ hazardcounter+`">
                  <td>
                    <input id="hazard[`+ hazardcounter +`][name]" name="hazard[`+ hazardcounter+`][name]" class="form-control" type="text" placeholder="Waste Name">
                  </td>
                  <td>
                    <input class="form-control" type="text" required placeholder="Waste Quantity" id="hazard[`+ hazardcounter + `][quantityItem]" name="hazard[` + hazardcounter +`][quantityItem]" style="width: 50%; float: left;">
                    <select id="hazard[`+ hazardcounter +`][quantity]" name="hazard[`+ hazardcounter +`][quantity]" class="form-control" style="width: 50%">
                      <option value="KG">KG</option>
                      <option value="TON">TON</option>
                    </select>
                  </td>
                  <td>
                    <select id="hazard[`+ hazardcounter +`][state]" name="hazard[`+ hazardcounter +`][state]" class="form-control">
                      <option value="Liquid">Liquid</option>
                      <option value="Solid">Solid</option>
                      <option value="Semi-Solid">Semi-Solid</option>
                      <option value="Gas">Gas</option>
                    </select>
                  </td>
                  <td>
                    <select id="hazard[`+ hazardcounter + `][ContainerType]" name="hazard[` + hazardcounter +`][ContainerType]" class="form-control">
                      <option value="Drums/Barrels">Drums/Barrels</option>
                      <option value="Jerrycans">Jerrycans</option>
                      <option value="Buckets">Buckets</option>
                      <option value="Cans">Cans</option>
                      <option value="Boxes">Boxes</option>
                      <option value="IBC">IBC</option>
                      <option value="Bags/Jumbo Bags">Bags/Jumbo Bags</option>
                      <option value="Loose/NA">Loose/NA</option>
                    </select>
                  </td>
                </tr>`)

});
//imgUpload.addEventListener('change', previewImgs, false);
//imgUploadForm.addEventListener('submit', function (e) {
//  e.preventDefault();
//  alert('Images Uploaded! (successfully)');
//}, false);

//function previewImgs(event) {
//  totalFiles = imgUpload.files.length;

//  if (!!totalFiles) {
//    imgPreview.classList.remove('quote-imgs-thumbs--hidden');
//    previewTitle = document.createElement('p');
//    previewTitle.style.fontWeight = 'bold';
//    previewTitleText = document.createTextNode(totalFiles + ' Total Images Selected');
//    previewTitle.appendChild(previewTitleText);
//    imgPreview.appendChild(previewTitle);
//  }

//  for (var i = 0; i < totalFiles; i++) {
//    img = document.createElement('img');
//    img.src = URL.createObjectURL(event.target.files[i]);
//    img.classList.add('img-preview-thumb');
//    imgPreview.appendChild(img);
//  }
//}




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

          var generatedHtml = '';
          imgPreview.classList.remove('quote-imgs-thumbs--hidden');
          previewTitle = document.createElement('p');
          previewTitle.style.fontWeight = 'bold';
          previewTitleText = document.createTextNode(result.length + ' Total Images Selected');
          previewTitle.appendChild(previewTitleText);
          imgPreview.appendChild(previewTitle);


          for (var i = 0; i < result.length; i++) {
            imageidCounter++;
            permitImages.push(result[i]);

            img = document.createElement('img');
            img.src = "/UploadedFiles/Temp/"+ result[i] + "";
            img.classList.add('img-preview-thumb');
            imgPreview.appendChild(img);
            

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
  $("#permitimagesform").html('');
  let counter = 0;
  for (var i = 0; i < permitImages.length; i++) {

    $("#permitimagesform").append('<input type="hidden" name="HPhotos" id="HPhotos_' + counter + '" value="' + permitImages[i] + '" />');
    counter++;
  }
}


function PopulateMSDSHiddenForm() {
  $("#permitmsdsform").html('');
  let counter = 0;
  for (var i = 0; i < permitMSDS.length; i++) {

    $("#permitmsdsform").append('<input type="hidden" name="HMSDS" id="HMSDS_' + counter + '" value="' + permitMSDS[i] + '" />');
    counter++;
  }
}


///Form Validation
function checkform() {
  var validatoinerror = document.getElementById('ulErorlist');
  $("#validationErrors").hide();
  validatoinerror.textContent="";
  var isValid = true;
  if (!document.getElementById('chkAcceptTerms').checked) {
    var errormessage = "<li>You did not accept Hazard Waste Guidelines</li>";
   
      validatoinerror.innerHTML+=errormessage;
    
    
    isValid = false;
  }
  if ($("#WasteDescription").val()=="") {
    var errormessage = "<li>Please add Description of waste generation</li>";
    validatoinerror.innerHTML += errormessage;
    $("#WasteDescription").addClass("errorForm")
    isValid = false;
  }

  if ($('input[name="transporter"]:checked').val() =="Other") {
    if ($("#transporterDetails").val()=="") {
      var errormessage = "<li>Please fill Transporter details</li>";

      validatoinerror.innerHTML += errormessage;
      $("#transporterDetails").addClass("errorForm")

      isValid = false;
    }
  }
  if (permitImages.length==0) {
    var errormessage = "<li>Please add Hazard Waste photos</li>";

    validatoinerror.innerHTML += errormessage;
    

    isValid = false;
  }
  if (permitMSDS.length==0) {
    var errormessage = "<li>Please upload Material Safety Data Sheet</li>";

    validatoinerror.innerHTML += errormessage;


    isValid = false;
  }
  if ($('input[name="transporter"]:checked').val() == undefined || $('input[name="transporter"]:checked').val() == "") {
    var errormessage = "<li>Please select transporter</li>";
    $("#spTransporterRequired").show();
    validatoinerror.innerHTML += errormessage;


    isValid = false;
  }
  if (isValid) {
    $('#dvSubmitControl').show();
    $('#dvPreSubmitControl').hide();
    return true;
  } else {
    $("#validationErrors").show();
    return false;
  }
  
}