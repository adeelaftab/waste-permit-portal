/..........................Jquery....................../
$(document).ready(function () {




  $("#ddlFieldGroup").change(function () {
    var value = $("#ddlFieldGroup").val();

    if (value != "") {

      $('.typeContainer').hide();
      if (value === "MasterItems" || value === "PermitTypeMaster") {
        $("#dvMasterItem").show();
      }
      MasterDataVM.Type = value;

    }

  });

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
    var value = $("#ddlFieldGroup").val();
    if (value=="") {
      alert("Please select Field!");
      return;
    }
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

          window.location.href = "/MasterData/Index";


        }
      },
      failure: function (errMsg) {
        alert(errMsg);
      }
    });
  }
 
}



