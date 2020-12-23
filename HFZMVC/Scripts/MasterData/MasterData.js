/..........................Jquery....................../
$(document).ready(function () {


  $("#qViewTable").DataTable({
    "iDisplayLength": 5,
    "dom": '<"dataTables_wrapper"<"dataTables_head"lf>t<"dataTables_footer"p>>',
    "order": [[0, "desc"]],

  });

  //$("#ddlFieldGroup").change(function () {
  //  var value = $("#ddlFieldGroup").val();
    
  //  if (value != "") {
      
  //    $('.typeContainer').hide();
  //    if (value === "MasterItems" || value === "PermitTypeMaster") {
  //      $("#dvMasterItem").show();
  //    }
  //    MasterDataVM.Type = value;
     
  //  }

  //});

});





/.......................Jquery......................./







var MasterDataVM = {
  Id: 0,
  Name: "",
  Type: "",
  isEdit: false,
  isNew: true,
  isActive: true,
  Amount: 0,
  ItemCode: 0
};
function SetDefault(isdisableProp) {
  MasterDataVM.Id = 0;
  if (MasterDataVM.isEdit) {
    $("#btnCreate").prop('value', 'Create');
  }
  MasterDataVM.Name = "";
  MasterDataVM.isEdit = false;

  MasterDataVM.isNew = true
    ;
  MasterDataVM.Amount = 0;
  MasterDataVM.ItemCode = "";
  $("#Fields").val("");
  $("#Amount").val(0);
  $("#ItemCode").val("");
  $("#isActive").prop("checked", false);
  if (isdisableProp) {
    $("#Fields").prop('disabled', true);
  }

}
function Submit() {

  if (MasterDataVM.isNew) {
    MasterDataVM.Name = $("#Fields").val();
    MasterDataVM.isActive = $('#isActive').is(":checked");
    if (MasterDataVM.Name === "") {
      alert("Please enter value");
      return;
    }
    MasterDataVM.Amount = $("#Amount").val();
    MasterDataVM.ItemCode = $("#ItemCode").val();

    $.ajax({
      type: "POST",
      url: "/MasterData/CreateMasterData",
      // The key needs to match your method's input parameter (case-sensitive).
      data: JSON.stringify({ masterdata: MasterDataVM }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        if (data == "1") {
          alert("Record inserted succesfully!");
      
          SetDefault(false);


        }
      },
      failure: function (errMsg) {
        alert(errMsg);
      }
    });
  }
  else if (MasterDataVM.isEdit) {
    MasterDataVM.Name = $("#Fields").val();
    MasterDataVM.isActive = $('#isActive').is(":checked");
    MasterDataVM.Amount = $("#Amount").val();
    MasterDataVM.ItemCode = $("#ItemCode").val();
    $.ajax({
      type: "POST",
      url: "/MasterData/EditMasterData",
      // The key needs to match your method's input parameter (case-sensitive).
      data: JSON.stringify({ masterdata: MasterDataVM }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        if (data == "1") {
          alert("Record updated succesfully!");
          window.location.reload();
          //SetDefault(false);

        }

      },
      failure: function (errMsg) {
        alert(errMsg);
      }
    });
  }
}

function Edit(id, type) {
  $('.typeContainer').hide();
  $("#btnCreate").prop("disabled", "");
  $.get("EditMasterData/" + id + "?type=" + type, function (data) {

    if (data == "notfound") {
      alert("Error! Something went wrong, please try again");

      return;
    }
    MasterDataVM.isEdit = true;
    MasterDataVM.Id = id;
    MasterDataVM.Type = data.Type;
    MasterDataVM.Amount = data.Amount;
    $("#Amount").val(MasterDataVM.Amount);
    if (type == "PermitTypeMaster" || type =="MasterItems") $("#dvMasterItem").show();
    MasterDataVM.ItemCode = data.ItemCode;
    $("#ItemCode").val(MasterDataVM.ItemCode);
    MasterDataVM.isNew = false
    MasterDataVM.Name = data.Name;
    MasterDataVM.isActive = data.isActive;
    $("#btnCreate").prop('value', 'Update');
    $("#isActive").prop("checked", MasterDataVM.isActive);
    $("#Fields").prop('disabled', false);
    $("#Fields").val(data.Name);


  });
  //SetDefault();
}
function Delete(id, type) {
  if (confirm("Are you sure you want to Delete?")) {
    $.ajax({
      type: "DELETE",
      url: "/MasterData/DeleteMasterData",
      // The key needs to match your method's input parameter (case-sensitive).
      data: JSON.stringify({ id: id, type: type }),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (data) {
        if (data == "1") {
          alert("Record Deleted succesfully!");
          window.location.reload();

        }
      },
      failure: function (errMsg) {
        alert(errMsg);
      }
    });
  }


}

