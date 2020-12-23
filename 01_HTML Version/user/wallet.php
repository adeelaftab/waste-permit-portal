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
                        <h4 class="page-title">Wallet</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0);">Dashobard</a></li>
                            <li class="breadcrumb-item"><a href="javascript:void(0);">Wallet</a></li>
                        </ol>
                        <div class="state-information d-none d-sm-block">
                            <button onclick="myFunction()" type="button" class="btn btn-primary waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-center"><i class="fa fa-plus-circle"></i> Add Credit</button>
                        </div>
                    </div>
                </div>
        </div>
        <div class="row">
          <div class="col-xl-3">
            <div class="card m-b-20">
              <div class="card-body">
                <h4 class="mt-0 header-title">Balance</h4>
                <div class="row text-center m-t-20">
                  <div class="col-6">
                    <h5 class="">AED 56241</h5>
                    <p class="text-muted">Avilable</p>
                  </div>
                  <div class="col-6">
                    <h5 class="">AED 23651</h5>
                    <p class="text-muted">Total Spent</p>
                  </div>
                </div>
                <div id="morris-donut-example" class="dashboard-charts morris-charts"></div>
				  <div class="state-information d-none d-sm-block">
                            <button style="width: 100%; padding: 15px;" onclick="myFunction()" type="button" class="btn btn-primary waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-center"><i class="fa fa-plus-circle"></i> Add Credit</button>
                        </div>
              </div>
            </div>
          </div>
          
          <div class="col-xl-3">
            <div class="card m-b-20">
              <div class="card-body">
                <h4 class="mt-0 header-title">Monthly Transactions</h4>
                <div class="row text-center m-t-20">
                  <div class="col-6">
                    <h5 class="">AED 2548</h5>
                    <p class="text-muted">Marketplace</p>
                  </div>
                  <div class="col-6">
                    <h5 class="">AED 6985</h5>
                    <p class="text-muted">Total Spent</p>
                  </div>
                </div>
                <div id="morris-bar-stacked" class="dashboard-charts morris-charts"></div>
				  <div class="state-information d-none d-sm-block">
                            <button style="width: 100%; padding: 15px;"  onclick="myFunction()" type="button" class="btn btn-primary waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-center"><i class="fa fa-plus-circle"></i> Export Report</button>
                        </div>
              </div>
            </div>
          </div>
			<div class="col-xl-6">
            <div class="card m-b-20">
              <div class="card-body">
                <h4 class="mt-0 m-b-30 header-title">Latest Transactions</h4>
                <div class="table-responsive">
                  <table class="table table-vertical">
                    <tbody>
                      <tr>
                        <td>Municipal Solid Waste</td>
                        <td><i class="mdi mdi-checkbox-blank-circle text-success"></i> Confirm</td>
                        <td>AED 14,584
                          </td>
                        <td>5/12/2016
                          </td>
                        <td><button type="button" class="btn btn-secondary btn-sm waves-effect waves-light">Edit</button></td>
                      </tr>
                      <tr>
                        <td>Municipal Solid Waste</td>
                        <td><i class="mdi mdi-checkbox-blank-circle text-warning"></i> Waiting payment</td>
                        <td>AED 8,541
                          </td>
                        <td>10/11/2016
                          </td>
                        <td><button type="button" class="btn btn-secondary btn-sm waves-effect waves-light">Edit</button></td>
                      </tr>
                      <tr>
                        <td>Municipal Solid Waste</td>
                        <td><i class="mdi mdi-checkbox-blank-circle text-success"></i> Confirm</td>
                        <td>AED 954
                          </td>
                        <td>8/11/2016
                          </td>
                        <td><button type="button" class="btn btn-secondary btn-sm waves-effect waves-light">Edit</button></td>
                      </tr>
                      <tr>
                        <td>Municipal Solid Waste</td>
                        <td><i class="mdi mdi-checkbox-blank-circle text-danger"></i> Payment expired</td>
                        <td>AED 44,584
                          </td>
                        <td>7/11/2016
                          </td>
                        <td><button type="button" class="btn btn-secondary btn-sm waves-effect waves-light">Edit</button></td>
                      </tr>
                      <tr>
                        <td>Municipal Solid Waste</td>
                        <td><i class="mdi mdi-checkbox-blank-circle text-success"></i> Confirm</td>
                        <td>AED 8,844
                          </td>
                        <td>1/11/2016
                          </td>
                        <td><button type="button" class="btn btn-secondary btn-sm waves-effect waves-light">Edit</button></td>
                      </tr>
                      <tr>
                        <td>Municipal Solid Waste</td>
                        <td><i class="mdi mdi-checkbox-blank-circle text-success"></i> Confirm</td>
                        <td>AED 8,844
                          </td>
                        <td>1/11/2016
                          </td>
                        <td><button type="button" class="btn btn-secondary btn-sm waves-effect waves-light">Edit</button></td>
                      </tr>
					 <tr>
                        <td>Municipal Solid Waste</td>
                        <td><i class="mdi mdi-checkbox-blank-circle text-success"></i> Confirm</td>
                        <td>AED 8,844
                          </td>
                        <td>1/11/2016
                          </td>
                        <td><button type="button" class="btn btn-secondary btn-sm waves-effect waves-light">Edit</button></td>
                      </tr>
					  </tbody>
                  </table>
                </div>
              </div>
            </div>
          </div>
        </div>
		  <div class="row hideit">
          
			  <div class="col-xl-6">
            <div class="card m-b-20">
              <div class="card-body">
                <h4 class="mt-0 header-title">Email Sent</h4>
                <div class="row text-center m-t-20">
                  <div class="col-4">
                    <h5 class="">AED  89425</h5>
                    <p class="text-muted">Marketplace</p>
                  </div>
                  <div class="col-4">
                    <h5 class="">AED  56210</h5>
                    <p class="text-muted">Total Transactions</p>
                  </div>
                  <div class="col-4">
                    <h5 class="">AED  8974</h5>
                    <p class="text-muted">Last Month</p>
                  </div>
                </div>
                <div id="morris-area-example" class="dashboard-charts morris-charts"></div>
              </div>
            </div>
          </div>
          <div class="col-xl-6">
            <div class="card m-b-20">
              <div class="card-body">
                <h4 class="mt-0 m-b-30 header-title">Latest Orders</h4>
                <div class="table-responsive">
                  <table class="table table-vertical mb-1">
                    <tbody>
                      <tr>
                        <td>#12354781</td>
                        <td>Municipal Solid Waste</td>
                        <td><span class="badge badge-pill badge-success">Delivered</span></td>
                        <td>AED 185</td>
                        <td>5/12/2016</td>
                        <td><button type="button" class="btn btn-secondary btn-sm waves-effect waves-light">Edit</button></td>
                      </tr>
                      <tr>
                        <td>#52140300</td>
                        <td>Municipal Solid Waste</td>
                        <td><span class="badge badge-pill badge-success">Delivered</span></td>
                        <td>AED 1,024</td>
                        <td>5/12/2016</td>
                        <td><button type="button" class="btn btn-secondary btn-sm waves-effect waves-light">Edit</button></td>
                      </tr>
                      <tr>
                        <td>#96254137</td>
                        <td>Municipal Solid Waste</td>
                        <td><span class="badge badge-pill badge-danger">Cancel</span></td>
                        <td>AED 657</td>
                        <td>5/12/2016</td>
                        <td><button type="button" class="btn btn-secondary btn-sm waves-effect waves-light">Edit</button></td>
                      </tr>
                      <tr>
                        <td>#12365474</td>
                        <td>Municipal Solid Waste</td>
                        <td><span class="badge badge-pill badge-warning">Shipped</span></td>
                        <td>AED 8451</td>
                        <td>5/12/2016</td>
                        <td><button type="button" class="btn btn-secondary btn-sm waves-effect waves-light">Edit</button></td>
                      </tr>
                      <tr>
                        <td>#85214796</td>
                        <td>Municipal Solid Waste</td>
                        <td><span class="badge badge-pill badge-success">Delivered</span></td>
                        <td>AED 584</td>
                        <td>5/12/2016</td>
                        <td><button type="button" class="btn btn-secondary btn-sm waves-effect waves-light">Edit</button></td>
                      </tr>
                      
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          </div>
        </div>
		  
		  <div id="myDIV">
		    <iframe width="100%" height="550px" src="include/card/index.html" frameborder="0"></iframe>
		  </div>
        <!-- end row --> 
      </div>
      <!-- container-fluid --> 
    </div>
    <!-- content -->
    
    <? include('include/footer.php'); ?>
