<? include('include/header.php'); ?>
  
  <!-- ============================================================== --> 
  <!-- Start right Content here --> 
  <!-- ============================================================== -->
  <div class="content-page"> 
    <!-- Start content -->
    <div class="content">
      <div class="container-fluid mt-6 bg-white">
        <div class="row">
          <div class="col-sm-12">
            <div class="page-title-box">
              <h3 class="page-title"><i class="ti-write"></i> <span>New Permits Request</span>
                <div class="pull-right p-3 slide-number">
                  <div class="pull-left no-one">1&nbsp;</div>
                    <div class="pull-left no-two">2&nbsp;</div>
                    <div class="pull-left no-three">3&nbsp;</div>
                    <div class="pull-left no-four">4&nbsp;</div>
                  / 4</div>
              </h3>
              <div class="separator"></div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-12">
            <div class="card m-t-90 m-b-120">
              <div class="card-body pt-0 pb-0 pl-0 pr-0">
                <div class="">
                  <div class="progress" style="height: 10px;">
                    <div class="progress-bar bg-success companyinfo activeprogress" role="progressbar" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100" style="width: 25%">
                      <div class="companiinfo-icon timeline-icon"><i class="fa fa-info-circle activetl-icon"></i></div>
                    </div>
                    <div class="progress-bar bg-info waste-description deactiveprogress" role="progressbar" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100" style="width: 25%">
                      <div class="companiinfo-icon timeline-icon"><i class="fa fa-prescription-bottle "></i></div>
                    </div>
                    <div class="progress-bar bg-info purpose-sampling deactiveprogress" role="progressbar" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100" style="width: 25%">
                      <div class="companiinfo-icon timeline-icon"><i class="fa fa-sitemap "></i></div>
                    </div>
                    <div class="progress-bar bg-info disclaimer deactiveprogress" role="progressbar" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100" style="width: 25%">
                      <div class="companiinfo-icon timeline-icon"><i class="fa fa-shopping-cart"></i></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!-- end col --> 
        </div>
        <div class="row">
          <div class="col-12">
            <div class="card m-b-20">
              <div class="card-body pt-0 pb-0 pl-0 pr-0">
                <div class="">
                  <div class="progress" style="height: 50px;">
                    <div class="progress-bar pb-title bg-success companyinfo activeprogress" role="progressbar" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100">Company Information</div>
                    <div class="progress-bar pb-title bg-info waste-description deactiveprogress" role="progressbar" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100">Waste Description</div>
                    <div class="progress-bar pb-title bg-info purpose-sampling deactiveprogress" role="progressbar" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100">Purpose of Sampling</div>
                    <div class="progress-bar pb-title bg-info disclaimer deactiveprogress" role="progressbar" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100">Summary</div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!-- end col --> 
        </div>
        <div class="row">
          <div class="col-8">
            <div class="card m-b-20" style="min-height: 95%;">
              <div class="card-body companyinfo-div tl-div">
                  <div class="col-sm-12"><h4 class="mt-0 header-title">&#45; Please fill the Company Information Below: </h4></div>
                <div class="row">
                  <div class="form-group col-sm-6">
                    <label for="example-text-input" class="col-sm-2 col-form-label">Company Name</label>
                    <div class="col-sm-12">
                      <input class="form-control" type="text" value="" placeholder="Enter Company Name" id="example-text-input">
                    </div>
                  </div>
                  <div class="form-group col-sm-6">
                    <label for="example-number-input" class="col-sm-2 col-form-label">Trade License Number</label>
                    <div class="col-sm-12">
                      <input class="form-control" type="number" value="" placeholder="Trade License Number" id="example-number-input">
                    </div>
                  </div>
                  <div class="form-group col-6">
                    <label for="example-text-input" class="col-sm-2 col-form-label">Manager Name</label>
                    <div class="col-sm-12">
                      <input class="form-control" type="text" value="" placeholder="Enter Manager Name" id="example-text-input">
                    </div>
                  </div>
                  <div class="form-group col-6">
                    <label for="example-number-input" class="col-sm-2 col-form-label">Phone</label>
                    <div class="col-sm-12">
                      <input class="form-control" type="number" value="" placeholder="Trade Phone Number" id="example-number-input">
                    </div>
                  </div>
                  <div class="form-group col-12">
                    <label for="example-email-input" class="col-sm-2 col-form-label">Email</label>
                    <div class="col-sm-12">
                      <input class="form-control" type="email" value="bootstrap@example.com" id="example-email-input">
                    </div>
                  </div>
                </div>
                <div class="row nxtprv-row">
                  <div class="col-6">
                    <div class="prev-btn companyinfo-prev"><i class="fas fa-angle-double-left"></i> PREV</div>
                  </div>
                  <div class="col-6">
                    <div class="next-btn companyinfo-next">NEXT <i class="fas fa-angle-double-right"></i></div>
                  </div>
                </div>
              </div>
              <div class="card-body wastedescription-div tl-div">
                  <div class="col-sm-12"><h4 class="mt-0 header-title">&#45; Please fill the Waste Description information Below: </h4></div>
                <div class="row">
                  <div class="form-group col-sm-6">
                    <label class="col-sm-2 col-form-label">Waste Category</label>
                    <div class="col-sm-10">
                      <select class="custom-select">
                        <option selected="selected">Select Waste Category</option>
                        <option value="1">Municipal Solid Waste</option>
                        <option value="2">Industrial Non-hazardous Waste</option>
                        <option value="3">Hazardous Waste</option>
                        <option value="3">Expired Chemical/Products</option>
                      </select>
                    </div>
                  </div>
                  <div class="form-group col-sm-6">
                    <label class="col-sm-2 col-form-label">Waste Type</label>
                    <div class="col-sm-10">
                      <select class="custom-select">
                        <option selected="selected">Select Waste Type</option>
                        <option value="1">Municipal Solid Waste</option>
                        <option value="2">Industrial Non-hazardous Waste</option>
                        <option value="3">Hazardous Waste</option>
                        <option value="3">Expired Chemical/Products</option>
                        <option value="3">Lab Analysis &amp; Sampling</option>
                      </select>
                    </div>
                  </div>
                  <div class="form-group col-sm-6">
                    <label for="example-text-input" class="col-sm-2 col-form-label">Waste Location</label>
                    <div class="col-sm-10">
                      <input class="form-control" type="text" value="" placeholder="Enter Location" id="example-text-input">
                    </div>
                  </div>
                  <div class="form-group col-sm-6">
                    <label for="example-text-input" class="col-sm-2 col-form-label">Total Weight / Volume</label>
                    <div class="col-sm-10">
                      <input class="form-control" type="text" value="" placeholder="Total Weight / Volume" id="example-text-input">
                    </div>
                  </div>
                  <div class="form-group col-sm-6">
                    <label for="example-number-input" class="col-sm-2 col-form-label">Source Process</label>
                    <div class="col-sm-10">
                      <input class="form-control" type="text" value="" placeholder="Source Process" id="example-text-input">
                    </div>
                  </div>
                  <div class="form-group col-sm-6">
                    <label for="example-email-input" class="col-sm-2 col-form-label">Packaging/Storage</label>
                    <div class="col-sm-10">
                      <input class="form-control" type="text" value="" placeholder="Packaging/Storage" id="example-text-input">
                    </div>
                  </div>
                  <div class="form-group col-sm-12">
                    <label for="example-email-input" class="col-sm-2 col-form-label">Additional Remarks</label>
                    <div class="col-sm-10">
                      <input class="form-control" type="text" value="" placeholder="Additional Remarks" id="example-text-input">
                    </div>
                  </div>
                </div>
                <div class="row nxtprv-row">
                  <div class="col-6">
                    <div class="prev-btn wastedescription-prev"><i class="fas fa-angle-double-left"></i> PREV</div>
                  </div>
                  <div class="col-6">
                    <div class="next-btn wastedescription-next">NEXT <i class="fas fa-angle-double-right"></i></div>
                  </div>
                </div>
              </div>
              <div class="card-body purposesample-div tl-div">
                  <div class="col-sm-12"><h4 class="mt-0 header-title">&#45; Please submit the Purpose of Sampling Below: </h4></div>
                <div class="row">
                  <div class="form-group col-sm-12">
                    <label class="col-sm-2 col-form-label">Purpose of Sampling</label>
                    <div class="col-sm-12">
                      <select class="custom-select">
                        <option selected="selected">Select Purpose of Sampling</option>
                        <option value="1">Final Disposal</option>
                        <option value="2">Private - Gov</option>
                        <option value="3">Routine</option>
                        <option value="3">Private</option>
                        <option value="3">Survey</option>
                        <option value="3">Illegal Disposal</option>
                      </select>
                    </div>
                  </div>
                  <div class="form-group  col-sm-12">
                    <label class="col-sm-2 col-form-label">Source of Sample</label>
                    <div class="col-sm-12">
                      <select class="custom-select">
                        <option selected="selected">Effluent/Sewage</option>
                        <option value="1">Raw Material</option>
                        <option value="2">Final Product</option>
                        <option value="3">Industrial Solid Waste</option>
                        <option value="3">Industrial Liquid Waste</option>
                        <option value="3">Groundwater Depth</option>
                        <option value="3">Other</option>
                      </select>
                    </div>
                  </div>
                </div>
                <div class="row nxtprv-row">
                  <div class="col-6">
                    <div class="prev-btn purposesample-prev"><i class="fas fa-angle-double-left"></i> PREV</div>
                  </div>
                  <div class="col-6">
                    <div class="next-btn purposesample-next">NEXT <i class="fas fa-angle-double-right"></i></div>
                  </div>
                </div>
              </div>
              <div class="card-body disclaimer-div tl-div">
                  <div class="col-sm-12"><h4 class="mt-0 header-title">&#45; Please review your data Below: </h4></div>
                <div class="row">
                  <div class="form-group col-sm-12">
                    <label class="switch">
                      <input type="checkbox">
                      <span class="slider round"></span> </label>
                    <div class="col-sm-10 fa-pull-left">
                      <p style="line-height: 2.7;">I certify that my answers are true and complete to the best of my knowledge</p>
                    </div>
                  </div>
                  <div class="form-group  col-sm-12">
                    <label class="switch">
                      <input type="checkbox">
                      <span class="slider round"></span> </label>
                    <div class="col-sm-10 fa-pull-left">
                      <p style="line-height: 2.7;">I agree to the Terms and Conditions</p>
                    </div>
                  </div>
                </div>
                <div class="row nxtprv-row">
                  <div class="col-6">
                    <div class="prev-btn disclaimer-prev"><i class="fas fa-angle-double-left"></i> PREV</div>
                  </div>
                  <div class="col-6">
                    <div class="next-btn disclaimer-next">Checkout <i class="fas fa-angle-double-right"></i></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!-- end col -->
          <div class="col-lg-4">
            <div class="card" style="min-height: 95%;">
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
        </div>
        
        <!-- end row --> 
      </div>
      <!-- container-fluid --> 
    </div>
    <!-- content -->
    
    <? include('include/footer.php'); ?>