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
                        <h4 class="page-title">Dashboard</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item active">Welcome to Bee'ah Waste Disposal Management Service</li>
                        </ol>
                        <div class="state-information d-none d-sm-block">
                            <div class="state-graph">
                                <div id="header-chart-1"></div>
                                <div class="info">Balance AED2,317</div>
                            </div>
                            <div class="state-graph">
                                <div id="header-chart-2"></div>
                                <div class="info">Item Sold 1230</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end row -->
            <div class="row">
                <div class="col-xl-3 col-md-6">
                    <div class="card mini-stat bg-primary" style="background-color: #008A1A !important">
                        <div class="card-body mini-stat-img">
                            <div class="mini-stat-icon"><i class="mdi mdi-buffer float-right"></i></div>
                            <div class="text-white">
                                <h6 class="text-uppercase mb-3">Total Permits</h6>
                                <h4 class="mb-4">1,587</h4><span class="badge badge-info">+11% </span><span class="ml-2">From previous period</span></div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-md-6">
                    <div class="card mini-stat bg-primary" style="background-color: #94989B !important">
                        <div class="card-body mini-stat-img">
                            <div class="mini-stat-icon"><i class="mdi mdi-briefcase-check float-right"></i></div>
                            <div class="text-white">
                                <h6 class="text-uppercase mb-3">Processed Permits</h6>
                                <h4 class="mb-4">1,200</h4><span class="badge badge-danger">-29% </span><span class="ml-2">From previous period</span></div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-md-6">
                    <div class="card mini-stat bg-primary" style="background-color: #ff8300 !important">
                        <div class="card-body mini-stat-img">
                            <div class="mini-stat-icon"><i class="mdi mdi-tag-text-outline float-right"></i></div>
                            <div class="text-white">
                                <h6 class="text-uppercase mb-3">Pending Permits</h6>
                                <h4 class="mb-4">387</h4><span class="badge badge-warning">0% </span><span class="ml-2">From previous period</span></div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-md-6">
                    <div class="card mini-stat bg-primary" style="background-color: #00205c !important">
                        <div class="card-body mini-stat-img">
                            <div class="mini-stat-icon"><i class="mdi mdi-coin float-right"></i></div>
                            <div class="text-white">
                                <h6 class="text-uppercase mb-3">Revenue</h6>
                                <h4 class="mb-4">AED 46,782</h4><span class="badge badge-info">+89% </span><span class="ml-2">From previous period</span></div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end row -->
            
            <!-- end row -->
            <div class="row">
                <div class="col-xl-5">
                    <div class="card m-b-20">
                        <div class="card-body" style="padding-bottom:68px">
                            <!--
                            <h4 class="mt-0 header-title">Total Permits</h4>
                            <div class="row text-center m-t-20">
                                <div class="col-4">
                                    <h5 class="">9,463</h5>
                                    <p class="text-muted">July</p>
                                </div>
                                <div class="col-4">
                                    <h5 class="">6,219</h5>
                                    <p class="text-muted">August</p>
                                </div>
                                <div class="col-4">
                                    <h5 class="">2,439</h5>
                                    <p class="text-muted">September</p>
                                </div>
                            </div>
                            <div id="morris-area-example" class="dashboard-charts morris-charts"></div>
                            -->
                            <div id="threedChart" style="height: 400px"></div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-7">
                    <div class="card m-b-20">
                        <div class="card-body">
                            <h4 class="mt-0 header-title">
                            	Latest Permits Requests 
                            	<button type="button" class="btn btn-primary waves-effect waves-light float-right">View All</button>
                            </h4>
                            <p class="text-muted m-b-30">You can find below latest permits requests</p>

                            <table class="table mb-0">
                                <thead class="thead-default">
                                    <tr>
                                        <th>#</th>
                                        <th>Request Date</th>
                                        <th>Type</th>
                                        <th>Amount</th>
                                        <th>Payment</th>
                                        <th>Status</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th scope="row">1</th>
                                        <td>1418010335</td>
                                        <td>02/08/2018</td>
                                        <td>Lab Analysis & Sampling</td>
                                        <td>AED 735</td>
                                        <td>Unpaid</td>
                                        <td><button type="button" class="btn btn-primary waves-effect waves-light" style="background-color: #0084d6;border:1px #0084d6 solid"><i class="fas fa-eye"></i></button></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">1</th>
                                        <td>1418010335</td>
                                        <td>02/08/2018</td>
                                        <td>Lab Analysis & Sampling</td>
                                        <td>AED 735</td>
                                        <td>Unpaid</td>
                                        <td><button type="button" class="btn btn-primary waves-effect waves-light" style="background-color: #0084d6;border:1px #0084d6 solid"><i class="fas fa-eye"></i></button></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">1</th>
                                        <td>1418010335</td>
                                        <td>02/08/2018</td>
                                        <td>Lab Analysis & Sampling</td>
                                        <td>AED 735</td>
                                        <td>Unpaid</td>
                                        <td><button type="button" class="btn btn-primary waves-effect waves-light" style="background-color: #0084d6;border:1px #0084d6 solid"><i class="fas fa-eye"></i></button></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">1</th>
                                        <td>1418010335</td>
                                        <td>02/08/2018</td>
                                        <td>Lab Analysis & Sampling</td>
                                        <td>AED 735</td>
                                        <td>Unpaid</td>
                                        <td><button type="button" class="btn btn-primary waves-effect waves-light" style="background-color: #0084d6;border:1px #0084d6 solid"><i class="fas fa-eye"></i></button></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">1</th>
                                        <td>1418010335</td>
                                        <td>02/08/2018</td>
                                        <td>Lab Analysis & Sampling</td>
                                        <td>AED 735</td>
                                        <td>Unpaid</td>
                                        <td><button type="button" class="btn btn-primary waves-effect waves-light" style="background-color: #0084d6;border:1px #0084d6 solid"><i class="fas fa-eye"></i></button></td>
                                    </tr>
                                </tbody>
                            </table>
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

