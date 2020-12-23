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
                        <h4 class="page-title pull-left">Permits Management</h4>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-12">
                    <div class="card m-b-20">
                        <div class="card-body permits-managment-box">
							
							<div class="row tab">
							
								<div class="col-sm-2"></div>
								<div class="col-sm-2"><div class="tabcircle active" onclick="openCity(event, 'Information')"> Information </div></div>
								<div class="col-sm-2"><div class="tabcircle" onclick="openCity(event, 'Status')"> Status </div></div>
								<div class="col-sm-2"><div class="tabcircle" onclick="openCity(event, 'Payment')"> Payment </div></div>
								<div class="col-sm-2"><div class="tabcircle" onclick="openCity(event, 'Download')"> Download </div></div>
								<div class="col-sm-2"></div>
							
							</div>
							
							<div class="row">
							
								<div class="col-sm-12 tabcontent" id="Information">
								
									<div class="panel-body">
                    <div class="row">
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                        <hr>
                        <h6 class="wizard-title" style="text-align: center;">Please review your submitted information below:</h6>
                        <hr>
                      </div>
                      <div class="col-lg-12">
                        <div class="card" style="min-height: 100%;">
                          <div class="card-body charges summary">
                            <h4 class="mt-0 header-title chargestitle"><i class="ti-receipt"></i> <span>Company Information</span></h4>
                            <div class="table-responsive">
                              <table class="table mb-0">
                                <tbody>
                                  <tr>
                                    <th scope="row"><i class="ti-zip charges-icons"></i> <p>Company Name</p></th>
                                    <td>Plan A</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-settings charges-icons"></i> <p>Trade License Number</p></th>
                                    <td>773330</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-ruler-pencil charges-icons"></i> <p>Manager Name</p></th>
                                    <td>jarkas</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-bookmark-alt charges-icons"></i> <p>Phone</p></th>
                                    <td>123456</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i> <p>Email</p></th>
                                    <td>info@plana.ae</td>
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
                          <div class="card-body charges summary">
                            <h4 class="mt-0 header-title chargestitle"><i class="ti-receipt"></i> <p><span>Waste Description</span></p></h4>
                            <div class="table-responsive">
                              <table class="table mb-0">
                                <tbody>
                                  <tr>
                                    <th scope="row"><i class="ti-zip charges-icons"></i> <p>List of Items</p></th>
                                    <td>Municipal Solid Waste</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-settings charges-icons"></i><p>Country of Origin</p></th>
                                    <td>United Arab Emirates</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-ruler-pencil charges-icons"></i><p>Total Weight (kg\ton)</p></th>
                                    <td>500 KG</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-bookmark-alt charges-icons"></i><p>PRO</p></th>
                                    <td>4802938429</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i><p>EXP Date</p></th>
                                    <td>30 / 10 / 2019</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i><p>Type of packaging</p></th>
                                    <td>Lorem Ipsum</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i><p>No of Pakages</p></th>
                                    <td>657</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i><p>Package Size</p></th>
                                    <td>657</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i><p>Reason for Destruction</p></th>
                                    <td>Expired food items</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i><p>HFZ inspection form</p></th>
                                    <td>HFZ IF.pdf</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i><p>Location of facility</p></th>
                                    <td><a href="https://goo.gl/maps/hfBWQFGYEZ9gjwq59">Open the Map</a></td>
                                  </tr>
                                </tbody>
                              </table>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    

                  </div>
								
								</div>
								
								<div class="col-sm-12 tabcontent" id="Status">
								
									 <div class="timeline">
    <div class="timeline__group">
      <div class="timeline__box">
        <div class="timeline__date pending">
          <span class="timeline__day">15</span>
          <span class="timeline__month">March</span>
        </div>
        <div class="timeline__post tp-pending">
          <div class="timeline__content">
			  <h6>Sunday, 8 / 2 / 2020</h6>
			  <hr>
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam.</p>
          </div>
        </div>
      </div>
      <div class="timeline__box">
        <div class="timeline__date done">
          <span class="timeline__day">8</span>
          <span class="timeline__month">March</span>
        </div>
        <div class="timeline__post tp-done">
          <div class="timeline__content">
			  <h6>Sunday, 8 / 2 / 2020</h6>
			  <hr>
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam.</p>
          </div>
        </div>
      </div>
      <div class="timeline__box">
        <div class="timeline__date done">
          <span class="timeline__day">26</span>
          <span class="timeline__month">Feb</span>
        </div>
        <div class="timeline__post tp-done">
          <div class="timeline__content">
			  <h6>Wednesday, 26 / 2 / 2020</h6>
			  <hr>
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam.</p>
          </div>
        </div>
      </div>
	<div class="timeline__box">
        <div class="timeline__date done">
          <span class="timeline__day">18</span>
          <span class="timeline__month">Feb</span>
        </div>
        <div class="timeline__post tp-done">
          <div class="timeline__content">
			  <h6><strong>Tuesday, 18 / 2 / 2020</strong></h6>
			  <hr>
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam.</p>
          </div>
        </div>
      </div>
    </div>
  </div>
								
								</div>
								
								<div class="col-sm-12 tabcontent" id="Payment">
								
									<div class="panel-body">
                    <div class="row">
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                        <hr>
						  <h6 class="wizard-title"><center>Your payment refrence Number</center></h6>
                        <hr>
                      </div>
						
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
						 <center><p class="payment-status">Paid by Credit Card</p></center>
						  <br>
						  <a href="#"><i class="mdi mdi-file-document payment-ref-icon"></i></a>
                       <center> <a href="#" class="wizard-title status-title">1418020002</a></center>
                      </div>

                    </div>
                  </div>
								
								</div>
								
								<div class="col-sm-12 tabcontent" id="Download">
								
									<div class="panel-body">
                    <div class="row">
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                        <hr>
						  <h6 class="wizard-title"><center>Your permit request has been submitted successfully<br><br>You can download it via the below button</center></h6>
                        <hr>
                      </div>
						
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
						  <a href="#"><i class="dripicons-download approved"></i></a>
                        <h6 class="wizard-title status-title"><a href="#">Download</a></h6>
                      </div>

                    </div>
                  </div>
								
								</div>
							
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