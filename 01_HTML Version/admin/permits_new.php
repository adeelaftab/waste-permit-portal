<? include('include/header.php'); ?>
  
  <!-- ============================================================== --> 
  <!-- Start right Content here --> 
  <!-- ============================================================== -->
  <div class="content-page"> 
    <!-- Start content -->
    <div class="content">
      <div class="container-fluid">
        <div class="row">
          <div class="col-sm-12">
            <div class="page-title-box">
              <h4 class="page-title">Permits Management</h4>
              <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="javascript:void(0);">Dashoard</a></li>
                <li class="breadcrumb-item"><a href="javascript:void(0);">Permits</a></li>
              </ol>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-8">
            <div id="smartwizard">
              <ul>
                <li><a href="#step-1"><i style="font-size: 40px" class="fa fa-info-circle"></i><br />
                  <small>Company Information</small></a></li>
                <li><a href="#step-2"><i style="font-size: 40px" class="fa fa-prescription-bottle "></i><br />
                  <small>Waste Description</small></a></li>
                <li><a href="#step-3"><i style="font-size: 40px" class="fa fa-sitemap "></i><br />
                  <small>Purpose of Sampling</small></a></li>
                <li><a href="#step-4"><i style="font-size: 40px" class="fa fa-shopping-cart"></i><br />
                  <small>Documents and Disclaimer</small></a></li>
              </ul>
              <div>
                <div id="step-1" class="">
                  <div class="panel-body">
                    <div class="row">
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Company Name</label>
                        <input class="form-control"  value="PLAN A Agency" type="text">
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Trade License Number</label>
                        <input class="form-control"  value="773330" type="text">
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Manager Name</label>
                        <input class="form-control"  value="jarkas" type="text">
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Phone</label>
                        <input class="form-control"  value="123456" type="text">
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Email</label>
                        <input class="form-control"  value="info@plana.ae" type="text">
                      </div>
                    </div>
                  </div>
                </div>
                <div id="step-2" class="">
                  <div class="panel-body">
                    <div class="row">
                      <div class="col-md-6 col-xs-12 form-group">
                        <label>Waste Category</label>
                        <select class="form-control" data-val="true" data-val-number="The field PermitTypeID must be a number." data-val-required="Waste category is required" id="PermitTypeIDChange" name="PermitTypeID">
                          <option value="">Select Waste Category</option>
                          <option value="1">Municipal Solid Waste</option>
                          <option value="2">Industrial Non-hazardous Waste</option>
                          <option value="3">Hazardous Waste</option>
                          <option value="4">Expired Chemical/Products</option>
                        </select>
                        <input id="PermitTypeID" name="PermitTypeID" value="0" type="hidden">
                      </div>
                      <div class="col-md-6 col-xs-12 form-group">
                        <label>Waste Type</label>
                        <select class="form-control" data-val="true" data-val-number="The field WasteTypeId must be a number." data-val-required="Waste type is required" id="WasteTypeIDChange" name="WasteTypeId">
                          <option value="">Select Waste Type</option>
                          <option value="1">Municipal Solid Waste</option>
                          <option value="2">Industrial Non-hazardous Waste</option>
                          <option value="3">Hazardous Waste</option>
                          <option value="4">Expired Chemical/Products</option>
                          <option value="5">Lab Analysis &amp; Sampling</option>
                        </select>
                        <input id="WasteTypeId" name="WasteTypeId" value="" type="hidden">
                        <span class="field-validation-valid text-danger" data-valmsg-for="WasteTypeId" data-valmsg-replace="true"></span> </div>
                      <div class="col-xs-12 col-sm-12 col-md-12">
                        <div class="row">
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                            <label>Waste Location</label>
                            <input autofocus="geolocate()" class="form-control text-box single-line" data-val="true" data-val-length="Waste Location cannot be longer than 200 characters" data-val-length-max="200" data-val-required="Waste Location is required" id="WasteLocation" maxlength="10" name="WasteLocation" placeholder="Auto Detect Current Location" value="" autocomplete="off" type="text">
                            <span class="field-validation-valid text-danger" data-valmsg-for="WasteLocation" data-valmsg-replace="true"></span> </div>
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                            <label>Total Weight / Volume</label>
                            <input class="form-control text-box single-line" data-val="true" data-val-maxlength="Total Weight / Volume cannot be longer than 10 characters" data-val-maxlength-max="10" data-val-minlength="The field TotalVolume must be a string or array type with a minimum length of '0'." data-val-minlength-min="0" data-val-regex="Total Weight / Volume must be numeric" data-val-regex-pattern="^[0-9]*$" data-val-required="Total Weight / Volume is required" id="TotalVolume" maxlength="10" name="TotalVolume" placeholder="Total Weight / Volume" value="" type="text">
                            <span class="field-validation-valid text-danger" data-valmsg-for="TotalVolume" data-valmsg-replace="true"></span> </div>
                        </div>
                      </div>
                      <div class="col-xs-12 col-sm-12 col-md-12">
                        <div class="row">
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                            <label class="control-label" for="SourceProcess">Source Process</label>
                            <textarea class="form-control" cols="20" data-val="true" data-val-length="Source Process cannot be longer than 500 characters" data-val-length-max="500" data-val-required="Source Process is required" id="SourceProcess" name="SourceProcess" rows="3"></textarea>
                            <span class="field-validation-valid text-danger" data-valmsg-for="SourceProcess" data-valmsg-replace="true"></span> </div>
                          <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 form-group">
                            <label>Packaging/Storage</label>
                            <textarea class="form-control" cols="20" data-val="true" data-val-length="Packaging/Storage cannot be longer than 500 characters" data-val-length-max="500" data-val-required="Packaging/Storage is required" id="Storage" name="Storage" rows="3"></textarea>
                            <span class="field-validation-valid text-danger" data-valmsg-for="Storage" data-valmsg-replace="true"></span> </div>
                        </div>
                      </div>
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                        <label>Additional Remarks</label>
                        <textarea class="form-control" cols="20" data-val="true" data-val-length="Additional Remarks cannot be longer than 500 characters" data-val-length-max="500" id="Remarks" name="Remarks" rows="3"></textarea>
                        <span class="field-validation-valid text-danger" data-valmsg-for="Remarks" data-valmsg-replace="true"></span> </div>
                    </div>
                  </div>
                </div>
                <div id="step-3" class="">
                  <div class="panel-body">
                    <div class="row">
                      <div class="col-md-6 col-xs-12 form-group">
                        <label>Purpose of Sampling</label>
                        <select class="form-control" data-val="true" data-val-number="The field PurposeOfSampleingID must be a number." data-val-required="Purpose Of Sampling is required" id="PurposeOfSampleingID" name="PurposeOfSampleingID">
                          <option value="">Select Purpose of Sampling</option>
                          <option value="1">Final Disposal</option>
                          <option value="2">Private - Gov</option>
                          <option value="3">Routine</option>
                          <option value="4">Private</option>
                          <option value="5">Survey</option>
                          <option value="6">Illegal Disposal</option>
                        </select>
                        <span class="field-validation-valid text-danger" data-valmsg-for="PurposeOfSampleingID" data-valmsg-replace="true"></span> </div>
                      <div class="col-md-6 col-xs-12 form-group"></div>
                      <div class="col-md-6 col-xs-12 form-group">
                        <label>Source of Sample</label>
                        <input id="txtSourceOfSampleMasterIDs" name="SourceOfSampleMasterIDs" value="" type="hidden">
                        <select id="SourceOfSampleMasterIDs" name="SourceOfSampleMasterIDs[]" multiple="" class="form-control">
                          <option value="1" selected="">Effluent/Sewage</option>
                          <option value="2">Raw Material</option>
                          <option value="3">Final Product</option>
                          <option value="4">Industrial Solid Waste</option>
                          <option value="5">Industrial Liquid Waste</option>
                          <option value="6">Groundwater Depth</option>
                          <option value="7">Other</option>
                        </select>
                      </div>
                      <div class="col-md-6 col-xs-12 form-group"></div>
                      <div class="col-md-6 col-xs-12 form-group">
                        <label>Other Source of Sample</label>
                        <input class="form-control" data-val="true" data-val-length="Other Source of Sample cannot be longer than 200 characters" data-val-length-max="200" data-val-required="Other Source of Sample is required" id="OtherSourceOfSample" name="OtherSourceOfSample" rows="3" value="" type="text">
                        <span class="field-validation-valid text-danger" data-valmsg-for="OtherSourceOfSample" data-valmsg-replace="true"></span> </div>
                      <div class="col-md-6 col-xs-12 form-group"></div>
                    </div>
                  </div>
                </div>
                <div id="step-4" class="">
                  <div class="panel-body">
                    <div class="row">
                      <div class="col-lg-6">
                        <div class="card" style="min-height: 100%;">
                          <div class="card-body charges">
                            <h4 class="mt-0 header-title chargestitle"><i class="ti-receipt"></i> <span>Company Iformation</span></h4>
                            <div class="table-responsive">
                              <table class="table mb-0">
                                <tbody>
                                  <tr>
                                    <th scope="row"><i class="ti-zip charges-icons"></i> Company Name</th>
                                    <td>Plan A</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-settings charges-icons"></i>Trade License Number</th>
                                    <td>773330</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-ruler-pencil charges-icons"></i>Manager Name</th>
                                    <td>jarkas</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-bookmark-alt charges-icons"></i>Phone</th>
                                    <td>123456</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i>Email</th>
                                    <td>info@plana.ae</td>
                                  </tr>
                                </tbody>
                              </table>
                            </div>
                          </div>
                        </div>
                      </div>
                      <div class="col-lg-6">
                        <div class="card" style="min-height: 100%;">
                          <div class="card-body charges">
                            <h4 class="mt-0 header-title chargestitle"><i class="ti-receipt"></i> <span>Waste Description</span></h4>
                            <div class="table-responsive">
                              <table class="table mb-0">
                                <tbody>
                                  <tr>
                                    <th scope="row"><i class="ti-zip charges-icons"></i> Waste Category</th>
                                    <td>Municipal Solid Waste</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-settings charges-icons"></i>Waste Type</th>
                                    <td>Municipal Solid Waste</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-ruler-pencil charges-icons"></i>Waste Location</th>
                                    <td><a href="https://goo.gl/maps/hfBWQFGYEZ9gjwq59">Open the Map</a></td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-bookmark-alt charges-icons"></i>Total Weight / Volume</th>
                                    <td>50 TON</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i>Source Process</th>
                                    <td>Lorem Ipsum</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i>Packaging/Storage</th>
                                    <td>Lorem Ipsum</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i>Additional Remarks</th>
                                    <td>Lorem Ipsum</td>
                                  </tr>
                                </tbody>
                              </table>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <hr>
                    </div>
                    <div class="row">
                      <div class="col-lg-12">
                        <div class="card" style="min-height: 100%;">
                          <div class="card-body charges">
                            <h4 class="mt-0 header-title chargestitle"><i class="ti-receipt"></i> <span>Purpose of Sampling</span></h4>
                            <div class="table-responsive">
                              <table class="table mb-0">
                                <tbody>
                                  <tr>
                                    <th scope="row"><i class="ti-zip charges-icons"></i> Purpose of Sampling</th>
                                    <td>Private- Gov</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-settings charges-icons"></i>Source of Sample</th>
                                    <td>Effluent/Sewage</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-ruler-pencil charges-icons"></i>Other Source of Sample</th>
                                    <td><a href="https://goo.gl/maps/hfBWQFGYEZ9gjwq59">Lorem Ipsum</a></td>
                                  </tr>
                                </tbody>
                              </table>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <hr>
                    </div>
                    <div class="row">
                      <div class="col-md-12 col-xs-12 form-group">
                        <div class="checkbox">
                          <label>
                            <input style="margin-right: 10px;" data-val="true" data-val-range="You must agree with the Disclaimer" data-val-range-max="True" data-val-range-min="True" data-val-required="The Disclaimer field is required." id="IsDisclaimerCertified" name="IsDisclaimerCertified" value="true" type="checkbox">
                            <input name="IsDisclaimerCertified" value="false" type="hidden">
                            I certify that my answers are true and complete to the best of my knowledge <span class="field-validation-valid text-danger" data-valmsg-for="IsDisclaimerCertified" data-valmsg-replace="true"></span> </label>
                        </div>
                        <div class="checkbox">
                          <label>
                            <input style="margin-right: 10px;" data-val="true" data-val-range="You must agree with the Terms and Conditions" data-val-range-max="True" data-val-range-min="True" data-val-required="Disclaimer is required" id="IsIAgreeTermsAndCondition" name="IsIAgreeTermsAndCondition" value="true" type="checkbox">
                            <input name="IsIAgreeTermsAndCondition" value="false" type="hidden">
                            I agree to the Terms and Conditions. <span class="field-validation-valid text-danger" data-valmsg-for="IsIAgreeTermsAndCondition" data-valmsg-replace="true"></span> </label>
                        </div>
                      </div>
                      <div class="col-md-12 col-xs-12 form-group"></div>
                      <div class="col-md-6 col-xs-12 form-group">
                        <label>Name</label>
                        <input class="form-control text-box single-line" data-val="true" data-val-length="Name cannot be longer than 200 characters" data-val-length-max="200" data-val-regex="Use characters only" data-val-regex-pattern="[A-Za-z ]*" data-val-required="Disclaimer Name is required" id="DisclaimerName" name="DisclaimerName" value="" type="text">
                        <span class="field-validation-valid text-danger" data-valmsg-for="DisclaimerName" data-valmsg-replace="true"></span> </div>
                      <div class="col-md-6 col-xs-12 form-group">
                        <label>Date</label>
                        <label class="form-control" for="">30/09/2018</label>
                      </div>
                    </div>
                    <div class="row">
                      <button type="button" class="btn btn-success btn-lg waves-effect checkoutbtn" style="margin: 0 auto;">Check Out</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="creditcard">
              <iframe width="100%" height="550px" src="include/card/index.html" frameborder="0"></iframe>
              <div class="col-sm-12" style="background: #f9f9f9; margin-top: -10px; padding-bottom: 30px;"><div class="button-items" style="display: table; margin: 0 auto;">
                <button type="button" class="btn btn-secondary waves-effect cancelcheckout">Back</button>
                  <a href="invoice.php"><button type="button" class="btn btn-success waves-effect waves-light submitcheckout">Submit</button></a>
                  </div></div>
            </div>
          </div>
          <div class="col-lg-4">
            <div class="card" style="min-height: 100%;">
              <div class="card-body charges">
                <h4 class="mt-0 header-title chargestitle"><i class="ti-receipt"></i> <span>Charges</span></h4>
                <div class="table-responsive">
                  <table class="table mb-0">
                    <tbody>
                      <tr>
                        <th scope="row"><i class="ti-zip charges-icons"></i> Permit Fees</th>
                        <td>AED</td>
                        <td>00.00</td>
                      </tr>
                      <tr>
                        <th scope="row"><i class="ti-settings charges-icons"></i> Service Fees</th>
                        <td>AED</td>
                        <td>00.00</td>
                      </tr>
                      <tr>
                        <th scope="row"><i class="ti-ruler-pencil charges-icons"></i> R&amp;D Fees</th>
                        <td>AED</td>
                        <td>00.00</td>
                      </tr>
                      <tr>
                        <th scope="row"><i class="ti-bookmark-alt charges-icons"></i> VAT</th>
                        <td>AED</td>
                        <td>00.00</td>
                      </tr>
                      <tr class="totalcharges">
                        <th scope="row"><i class="ti-files charges-icons"></i> Total</th>
                        <td>AED</td>
                        <td>00.00</td>
                      </tr>
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          </div>
          <!-- end col --> 
        </div>
        <!-- end row --> 
      </div>
      <!-- container-fluid --> 
    </div>
    <!-- content -->
    
    <? include('include/footer.php'); ?>
