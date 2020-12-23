<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0,minimal-ui">
    <title>Bee'ah Dashboard</title>
    <meta content="Admin Dashboard" name="description">
    <meta content="Themesbrand" name="author">
    <link rel="shortcut icon" href="assets/images/favicon.ico">
    <link rel="stylesheet" href="../plugins/morris/morris.css">

    <!-- DataTables -->
    <link href="../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css">
    <link href="../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css">
    <!-- Responsive datatable examples -->
    <link href="../plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css">
    
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/smart_wizard.css" rel="stylesheet">
    <link href="assets/css/smart_wizard_theme_circles.css" rel="stylesheet">
    <link href="assets/css/metismenu.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/icons.css" rel="stylesheet" type="text/css">
    <link href="assets/css/style.css" rel="stylesheet" type="text/css">
    
    <style type="text/css">
        #threedChart {
            height: 400px; 
            min-width: 310px; 
            max-width: 800px;
            margin: 0 auto;
        }
    </style>
</head>

<body>
    <!-- Begin page -->
    <div id="wrapper">
        <!-- Top Bar Start -->
        <div class="topbar">
            <!-- LOGO -->
            <div class="topbar-left"><a href="dashboard.php" class="logo"><span><img src="assets/images/beeah-logo.svg" alt="" height="50"> </span><i><img src="assets/images/logo-sm.svg" alt="" height="22"></i></a></div>
            <nav class="navbar-custom">
                <ul class="navbar-right d-flex list-inline float-right mb-0">
                    <li class="dropdown notification-list d-none d-sm-block">
                        <form role="search" class="app-search">
                            <div class="btn-group m-b-10">
                                                <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">New Request &nbsp;<i class="fas fa-angle-down newrequest-arrow"></i></button>
                                                <div class="dropdown-menu" x-placement="bottom-start" style="position: absolute; transform: translate3d(0px, 33px, 0px); top: 0px; left: 0px; will-change: transform;">
                                                    <a class="dropdown-item" href="fdc_new.php">Food Destruction Certificate</a>
                                                    <div class="dropdown-divider"></div>
                                                    <a class="dropdown-item" href="permits_new.php">New Permit Request</a>
                                                    <div class="dropdown-divider"></div>
                                                    <a class="dropdown-item" href="sample_new.php">New Sampling Request</a>
                                                </div>
                                            </div>
                        </form>
                    </li>
                    <li class="dropdown notification-list"><a class="nav-link dropdown-toggle arrow-none waves-effect" data-toggle="dropdown" href="#" role="button" aria-haspopup="false" aria-expanded="false"><i class="ti-bell noti-icon"></i> <span class="badge badge-pill badge-danger noti-icon-badge">3</span></a>
                        <div class="dropdown-menu dropdown-menu-right dropdown-menu-lg">
                            <!-- item-->
                            <h6 class="dropdown-item-text">Notifications (258)</h6>
                            <div class="slimscroll notification-item-list">
                                <!-- item-->
                                <a href="javascript:void(0);" class="dropdown-item notify-item active">
                                    <div class="notify-icon bg-success"><i class="mdi mdi-cart-outline"></i></div>
                                    <p class="notify-details">Your order is placed<span class="text-muted">Dummy text of the printing and typesetting industry.</span></p>
                                </a>
                                <!-- item-->
                                <a href="javascript:void(0);" class="dropdown-item notify-item">
                                    <div class="notify-icon bg-warning"><i class="mdi mdi-message"></i></div>
                                    <p class="notify-details">New Message received<span class="text-muted">You have 87 unread messages</span></p>
                                </a>
                                <!-- item-->
                                <a href="javascript:void(0);" class="dropdown-item notify-item">
                                    <div class="notify-icon bg-info"><i class="mdi mdi-martini"></i></div>
                                    <p class="notify-details">Your item is shipped<span class="text-muted">It is a long established fact that a reader will</span></p>
                                </a>
                                <!-- item-->
                                <a href="javascript:void(0);" class="dropdown-item notify-item">
                                    <div class="notify-icon bg-primary"><i class="mdi mdi-cart-outline"></i></div>
                                    <p class="notify-details">Your order is placed<span class="text-muted">Dummy text of the printing and typesetting industry.</span></p>
                                </a>
                                <!-- item-->
                                <a href="javascript:void(0);" class="dropdown-item notify-item">
                                    <div class="notify-icon bg-danger"><i class="mdi mdi-message"></i></div>
                                    <p class="notify-details">New Message received<span class="text-muted">You have 87 unread messages</span></p>
                                </a>
                            </div>
                            <!-- All--><a href="javascript:void(0);" class="dropdown-item text-center text-primary">View all <i class="fi-arrow-right"></i></a></div>
                    </li>
                    <li class="dropdown notification-list">
                        <div style="display: block;height: 100%;vertical-align: middle;margin-top: 25px"> <b>Welcome, Isam Rihan</b> </div>
                    </li>
                    <li class="dropdown notification-list">
                        <div class="dropdown notification-list nav-pro-img">
                            <a class="dropdown-toggle nav-link arrow-none waves-effect nav-user" data-toggle="dropdown" href="#" role="button" aria-haspopup="false" aria-expanded="false">
                                <img src="assets/images/users/user-6.jpg" alt="user" class="rounded-circle">
                            </a>
                            <div class="dropdown-menu dropdown-menu-right profile-dropdown">
                                <!-- item--><a class="dropdown-item" href="profile.php"><i class="mdi mdi-account-circle m-r-5"></i> Profile</a> <a class="dropdown-item" href="wallet.php"><i class="mdi mdi-wallet m-r-5"></i> My Wallet</a> <a class="dropdown-item d-block" href="#"><span class="badge badge-success float-right">11</span><i class="mdi mdi-settings m-r-5"></i> Settings</a> <a class="dropdown-item" href="#"><i class="mdi mdi-lock-open-outline m-r-5"></i> Lock screen</a>
                                <div class="dropdown-divider"></div><a class="dropdown-item text-danger" href="#"><i class="mdi mdi-power text-danger"></i> Logout</a></div>
                        </div>
                    </li>
                </ul>
                <ul class="list-inline menu-left mb-0">
                    <li class="float-left">
                        <button class="button-menu-mobile open-left waves-effect"><i class="mdi mdi-menu"></i></button>

                        <!-- <img src="assets/images/logo2.png" alt="" height="40" style="margin-right: 20px"> -->
                    </li>
                    
                </ul>
            </nav>
        </div>
        <!-- Top Bar End -->
        <!-- ========== Left Sidebar Start ========== -->
        <div class="left side-menu">
            <div class="slimscroll-menu" id="remove-scroll">
                <!--- Sidemenu -->
                <div id="sidebar-menu">
                    <!-- Left Menu Start -->
                    <ul class="metismenu" id="side-menu">
                        <li class="menu-title">Main Menu</li>
                         <li class="has-submenu"><a href="dashboard.php"><i class="ti-dashboard"></i> <span>Dashboard</span></a></li>
                        <li class="has-submenu">
                            <a href="javascript:void(0);" class="waves-effect">
                                <i class="ti-receipt"></i> <span>Manage Permits <span class="float-right menu-arrow"><i class="mdi mdi-chevron-right"></i></span></span>
                            </a>
                            <ul class="submenu">
                                <li><a href="permits_management.php">All Permits</a></li>
                                <li><a href="#">Active Permits</a></li>
                                <li><a href="#">Expired Permits</a></li>
                            </ul>
                        </li>
                        <li class="menu-title">New Request</li>
                        <li><a href="fdc_new.php"><i class="mdi mdi-food-off"></i><span> FDC Request </span></a></li>
                        <li><a href="permits_new.php"><i class="ti-receipt"></i><span> New Permit Request </span></a></li>
                        <li><a href="sample_new.php"><i class="mdi mdi-package-variant"></i><span> New Sampling Request </span></a></li>
                        <li class="menu-title">Billings</li>
                        <li><a href="#"><i class="mdi mdi-file-document"></i><span> Invoices </span></a></li>
                        <li><a href="payments_management.php"><i class="mdi mdi-currency-usd"></i><span> Payments </span></a></li>
                        <li class="menu-title">Account Settings</li>
                        <li class="has-submenu"><a href="editprofile.php"><i class="mdi mdi-settings"></i> <span>Edit Profile</span></a></li>
                        <li class="has-submenu"><a href="wallet.php"><i class="fas fa-money-check"></i> <span>Wallet</span></a></li>
                        <li class="has-submenu"><a href="#"><i class="mdi mdi-lock-open-outline"></i> <span>Change Password</span></a></li>
                        <li class="has-submenu"><a href="index.php"><i class="mdi mdi-power"></i> <span>Logout</span></a></li>
                    </ul>
                </div>
                <!-- Sidebar -->
                <div class="clearfix"></div>
            </div>
            <!-- Sidebar -left -->
        </div>
        <!-- Left Sidebar End -->
