<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0,minimal-ui">
    <title>Login - Bee'ah Waste Disposal Management Service.</title>
    <link href="https://fonts.googleapis.com/css?family=Raleway:100,200,300,400,500,600,700,800" rel="stylesheet">
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/icons.css" rel="stylesheet" type="text/css">
    <link href="assets/css/style_home.css" rel="stylesheet" type="text/css">
	    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">

    <!-- Include SmartWizard CSS -->
    <link href="../dist/css/smart_wizard.css" rel="stylesheet" type="text/css" />

    <!-- Optional SmartWizard theme -->
    <link href="../dist/css/smart_wizard_theme_circles.css" rel="stylesheet" type="text/css" />
	<style>
		#smartwizard .sw-toolbar-top {
			display: none!important;
		}
		table i {
		float: left;
		margin: 4px 10px 0 0;
		}
		table p{
			margin: 0;
		}
		.sw-theme-circles > ul.step-anchor > li > a i {
    font-size: 35px!important;
    float: none;
    display: table;
    margin: 6px auto;
}
		
		.sw-theme-circles > ul.step-anchor > li.done > a:hover{
			color: #f5f5f5;
		}
	</style>
</head>

<body bgcolor="#002365" style="background-color: #002365">
    <div class="main sign-in-main">
        <section class="content-area">
            <div class="rt-block-bg"></div>                
            <div class="rt-block-bg-fix-scroll"></div>                
            <div class="rt-block">                    
                <a href="#" class="logo">Beeah Logo</a>
                <h2>Login</h2>
                <p class="top-para">Welcome back to HFZ Portal</p>
                <div class="form-area">
                    <form action="dashboard.php" id="login" class="" novalidate="novalidate" method="post" accept-charset="utf-8">
                        <div class="field user-name">
                            <input type="email" id="email" name="email_id" data-rule-required="true" placeholder="E-mail" autocomplete="off" required />
                            <span class="validate-img"></span>
                        </div>
                        <div class="field pass-word">
                            <input type="password" id="password" name="password" data-rule-required="true" placeholder="Password" autocomplete="off" required />
                            <span class="validate-img"></span>
                        </div>
                        <div><input type="submit" value="Login" /></div>
                    </form>                                            
                </div>
                <a href="forgot_password.php" class="forget-pwd-link">Forgot your password ?</a>
                <div class="sign-link">
                    <span style="color: white;">Need an account?</span> &nbsp;
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
					 Sign Up
					</button>
                </div>
            </div>
            <div class="lt-block">                    
                <div class="lt-content">
                    <div class="whiteoverlay"></div>
                    <a href="#" class="logo">Beeah Logo</a>
					
										<img src="assets/images/hfzlogo.png" alt="">

                    <span class="top-msg">Introducing Bee'ah Waste Disposal Management Service</span>
					

                    <h1 style="color: #fff;">Hamriyah Free<br/> Zone Sharjah</h1>
                </div>
                <footer class="footer">
                    <div class="footer-content">
                      <p>Copyrights © 2019, Beeah. All Rights Reserved.</p>
                    </div>
                </footer>
            </div>
        </section>
    </div>
    <!--<video autoplay loop id="video-background" muted plays-inline>
      <source src="assets/images/bg.mp4" type="video/mp4">
    </video>-->
	<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="exampleModalLabel">Create New Account</h5>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">

              <!-- Smart Wizard HTML -->
              <div id="smartwizard">
                  <ul>
                      <li><a href="#step-1"><i style="font-size: 40px" class="fa fa-info-circle"></i><small>General Information</small></a></li>
                      <li><a href="#step-2"><i style="font-size: 40px" class="fa fa-sitemap "></i><small>Company Information</small></a></li>
                      <li><a href="#step-3"><i style="font-size: 40px" class="fa fa-shopping-cart"></i><small>Summary</small></a></li>
                  </ul>

                  <div>
                      <div id="step-1" class="">
					  <div class="row">
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                        <hr>
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Full Name</label>
                        <input class="form-control" value="Enter Full Name" type="text">
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Company Name</label>
                        <input class="form-control" value="Enter Company Name" type="text">
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Email Address</label>
                        <input class="form-control" value="Enter Email" type="text">
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Phone Number</label>
                        <input class="form-control" value="Enter Phone" type="text">
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>User Name</label>
                        <input class="form-control" value="Enter User Name" type="text">
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Password</label>
                        <input class="form-control" value="Enter Password" type="text">
                      </div>
                    </div>
                      </div>
                      <div id="step-2" class="">
                          <div class="row">
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                        <hr>
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Hamriah Office Address</label>
                        <input class="form-control" value="Enter Office Address" type="text">
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Trade License Number</label>
                        <input class="form-control" value="Enter Trade License Number" type="text">
						  <div class="uploaddocument-btn"><i class="fa fa-upload"></i><input type="file" name="myFile"></div>
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Company License</label>
                        <input class="form-control" value="Enter Company License" type="text">
						  <div class="uploaddocument-btn"><i class="fa fa-upload"></i><input type="file" name="myFile"></div>
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>VAT Certificate</label>
                        <input class="form-control" value="Enter VAT Certificate" type="text">
						  <div class="uploaddocument-btn"><i class="fa fa-upload"></i><input type="file" name="myFile"></div>
                      </div>
                      <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 form-group">
                        <label>Second Address</label>
                        <input class="form-control" value="Enter Second Address" type="text">
                      </div>
                     
                    </div>
                      </div>
                      <div id="step-3" class="">
                          <div class="row">
                      <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group">
                        <hr>
                        <h6 class="wizard-title">Please review your submitted information below:</h6>
                        <hr>
                      </div>
                      <div class="col-lg-12">
                        <div class="card" style="min-height: 100%;">
                          <div class="card-body charges summary">
                            <h4 class="mt-0 header-title chargestitle"><i class="ti-receipt"></i> <span>General Information</span></h4>
                            <div class="table-responsive">
                              <table class="table mb-0">
                                <tbody>
                                  <tr>
                                    <th scope="row"><i class="ti-zip charges-icons"></i> <p>Full Name</p></th>
                                    <td>Isam Rihan</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-settings charges-icons"></i> <p>Company Name</p></th>
                                    <td>Plan A</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-ruler-pencil charges-icons"></i> <p>Email Address</p></th>
                                    <td>isam@plana.ae</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-bookmark-alt charges-icons"></i> <p>Phone number</p></th>
                                    <td>123456</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i> <p>User Name</p></th>
                                    <td>isamrihan</td>
                                  </tr>
									 <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i> <p>password</p></th>
                                    <td>qefqe34$%#mf</td>
                                  </tr>
                                </tbody>
                              </table>
                            </div>
                          </div>
                          <div class="card-body charges summary">
                            <h4 class="mt-0 header-title chargestitle"><i class="ti-receipt"></i> <span>Company Information</span></h4>
                            <div class="table-responsive">
                              <table class="table mb-0">
                                <tbody>
                                  <tr>
                                    <th scope="row"><i class="ti-zip charges-icons"></i> <p>Hamriah Office Address</p></th>
                                    <td>Uae, Sharjah, Hamriah Free zone</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-settings charges-icons"></i> <p>Trade License Number</p></th>
                                    <td>230492384324</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-ruler-pencil charges-icons"></i> <p>Company License</p></th>
                                    <td>qdf343241</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-bookmark-alt charges-icons"></i> <p>VAT Certificate</p></th>
                                    <td>1341bkdfk</td>
                                  </tr>
                                  <tr>
                                    <th scope="row"><i class="ti-files charges-icons"></i> <p>Second Address</p></th>
                                    <td>Uae, SHarjah</td>
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
              </div>

            </div>
         
          </div>
        </div>
      </div>
    <!-- jQuery  -->
    <script src="assets/js/jquery.min.js"></script>
    <script src="assets/js/bootstrap.bundle.min.js"></script>
    <script src="assets/js/jquery.slimscroll.js"></script>
    <script src="assets/js/waves.min.js"></script>
    <script src="../plugins/jquery-sparkline/jquery.sparkline.min.js"></script>
    <!-- App js -->
    <script src="assets/js/app.js"></script>
	
	<script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>

    <!-- Bootstrap JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>

    <!-- Include SmartWizard JavaScript source -->
    <script type="text/javascript" src="../dist/js/jquery.smartWizard.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function(){

            // Step show event
            $("#smartwizard").on("showStep", function(e, anchorObject, stepNumber, stepDirection, stepPosition) {
               //alert("You are on step "+stepNumber+" now");
               if(stepPosition === 'first'){
                   $("#prev-btn").addClass('disabled');
               }else if(stepPosition === 'final'){
                   $("#next-btn").addClass('disabled');
               }else{
                   $("#prev-btn").removeClass('disabled');
                   $("#next-btn").removeClass('disabled');
               }
            });

            // Toolbar extra buttons
            var btnFinish = $('<button></button>').text('Finish')
                                             .addClass('btn btn-success')
                                             .on('click', function(){ alert('Registration Successful, A verification email will be sent to registered email.'); });
            var btnCancel = $('<button></button>').text('Cancel')
                                             .addClass('btn btn-danger')
                                             .on('click', function(){ $('#smartwizard').smartWizard("reset"); });

            // Smart Wizard 1
            $('#smartwizard').smartWizard({
                    selected: 0,
                    theme: 'circles',
                    transitionEffect:'fade',
                    showStepURLhash: false,
                    toolbarSettings: {toolbarPosition: 'both',
                                      toolbarExtraButtons: [btnFinish, btnCancel]
                                    }
            });

        });
    </script>
	
</body>

</html>