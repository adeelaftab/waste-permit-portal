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
              <h4 class="page-title">Pay by Card</h4>
              <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="javascript:void(0);">Dashoard</a></li>
                <li class="breadcrumb-item"><a href="javascript:void(0);">Payments</a></li>
              </ol>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-md-8">
            <div class="creditcardpayment">
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
                        <th scope="row"><i class="ti-zip charges-icons"></i> Permit ID</th>
                        <td><input id="name" maxlength="20" type="text"></td>
                      </tr>
                      <tr>
                        <th scope="row"><i class="ti-settings charges-icons"></i> Amount</th>
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
