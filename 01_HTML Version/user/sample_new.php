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
          <div class="col-md-8">
            <div id="smartwizard">
              <ul>
                <li><a href="#step-1"><i style="font-size: 40px" class="fa fa-info-circle"></i><br />
                  <small>Request Information</small></a></li>
                <li><a href="#step-4"><i style="font-size: 40px" class="fa fa-shopping-cart"></i><br />
                  <small>Summary</small></a></li>
              </ul>
              <div>
                <div id="step-1" class="">
                  <div class="panel-body">
                    <div class="row">
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group" style="margin-bottom: 0;">
                        <hr>
                        <h6 class="wizard-title">New Lab Analysis & Sampling Request:</h6>
                        <hr>
                      </div>
                            <div class="col-sm-12">
                            <div class="btn-group mo-mb-2 wt-btn">
                                                <button class="btn btn-secondary btn-lg dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    Associated Permit No
                                                </button>
                                                <div class="dropdown-menu" x-placement="bottom-start" style="position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 41px, 0px);">
                                                    <a class="dropdown-item" href="#">1418020001</a>
                                                    <a class="dropdown-item" href="#">1418020001</a>
                                                    <a class="dropdown-item" href="#">1418020001</a>
                                                    <a class="dropdown-item" href="#">1418020001</a>
                                                    <a class="dropdown-item" href="#">1418020001</a>
                                                </div>
                                            </div>
                            </div>
                      </div>
                      
                       <div class="row">
                      <div class="col-xs-12 col-sm-12 col-md-12">
                             <hr><br></div>
                        </div>
                      
                      <div class="row">
                          
                          <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                        <label>Waste Description</label>
                        <textarea disabled class="form-control" cols="20" data-val="true" data-val-length="Additional Remarks cannot be longer than 500 characters" data-val-length-max="500" id="Remarks" name="Remarks" rows="3"></textarea>
                        <span class="field-validation-valid text-danger" data-valmsg-for="Remarks" data-valmsg-replace="true"></span> </div>
                          
                      </div>
                  </div>
                </div>
                
                
                <div id="step-4" class="">
                  <div class="panel-body">
                    <div class="row">
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                        <hr>
                        <h6 class="wizard-title">Please review your submitted information below:</h6>
                        <hr>
                      </div>
                      <div class="col-lg-12">
                        <div class="card" style="min-height: 100%;">
                          <div class="card-body charges">
                            <h4 class="mt-0 header-title chargestitle"><i class="ti-receipt"></i> <span>Request Information</span></h4>
                            <div class="table-responsive">
                              <table class="table mb-0">
                                <tbody>
                                  <tr>
                                    <th scope="row"><i class="ti-zip charges-icons"></i> Associated Permit No</th>
                                    <td>1418020001</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-settings charges-icons"></i>Waste Description</th>
                                    <td>Lab Analysis & Sampling</td>
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
                      <button type="button" class="btn btn-success btn-lg waves-effect checkoutbtn" style="margin: 0 auto;">Check Out</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="creditcard">
              <iframe width="100%" height="550px" src="include/card/index.html" frameborder="0"></iframe>
              <div class="col-sm-12" style="background: #f9f9f9; margin-top: -10px; padding-bottom: 30px;">
                <div class="button-items" style="display: table; margin: 0 auto;">
                  <button type="button" class="btn btn-secondary waves-effect cancelcheckout">Back</button>
                  <a href="invoice.php">
                  <button type="button" class="btn btn-success waves-effect waves-light submitcheckout">Submit</button>
                  </a> </div>
              </div>
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
