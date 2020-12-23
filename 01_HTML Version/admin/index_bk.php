<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0,minimal-ui">
    <title>Login - Bee'ah Waste Disposal Management Service.</title>
    <link href="https://fonts.googleapis.com/css?family=Roboto:100,100i,300,300i,400,400i,500,500i,700,700i,900,900i" rel="stylesheet">
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/icons.css" rel="stylesheet" type="text/css">
    <link href="assets/css/style.css" rel="stylesheet" type="text/css">
</head>

<body style="background-image: url(assets/images/bg.jpg);background-size: cover;background-position: center;">
    <!-- Begin page -->
    <div class="wrapper-page" style="position: absolute;max-width: 500px;height: 100%;display: inline-block;background-color: #2B3A4A;margin: 0px auto;left:100px">
        <!--
    	<div class="row">
    		<div class="col-12 text-center"><a href="index.html" class="logo logo-admin"><img src="assets/images/logo3.png" height="80" alt="logo"></a></div>
    		<br />
    	</div>
        -->
        <div class="card">
            <div class="card-body">
                <h3 class="text-center m-0"><a href="index.php" class="logo logo-admin"><img style="margin-top: 100px" src="assets/images/logo.png" height="60" alt="Beeah"></a></h3>
                <div class="p-3">
                    <h4 class="text-muted font-18 m-b-5 text-center">Welcome Back !</h4>
                    <p class="text-muted text-center">Sign in to continue to Bee'ah Waste Disposal Management Service.</p>
                    <form class="form-horizontal m-t-30" action="dashboard.php">
                        <div class="form-group">
                            <label for="username">Username</label>
                            <input type="text" class="form-control" id="username" placeholder="Enter username">
                        </div>
                        <div class="form-group">
                            <label for="userpassword">Password</label>
                            <input type="password" class="form-control" id="userpassword" placeholder="Enter password">
                        </div>
                        <div class="form-group row m-t-20">
                            <div class="col-6">
                                <div class="custom-control custom-checkbox">
                                    <input type="checkbox" class="custom-control-input" id="customControlInline">
                                    <label class="custom-control-label" for="customControlInline">Remember me</label>
                                </div>
                            </div>
                            <div class="col-6 text-right">
                                <button class="btn btn-primary w-md waves-effect waves-light" type="submit">Log In</button>
                            </div>
                        </div>
                        <div class="form-group m-t-10 mb-0 row">
                            <div class="col-12 m-t-20"><a href="pages-recoverpw.html" class="text-muted"><i class="mdi mdi-lock"></i> Forgot your password?</a></div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
        <div class="m-t-40 text-center" style="color: #fff">
            <p>Don't have an account ? <a href="register.php" class="text-primary">Signup Now</a></p>
            <p>© 2018 BEEAH</p>
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
</body>

</html>