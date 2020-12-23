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
                        <div class="state-information d-none d-sm-block" style="opacity: 0;">
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
                    <div class="card mini-stat bg-primary" style="background-color: #007A3E !important">
                        <div class="card-body mini-stat-img">
                            <div class="mini-stat-icon"><i class="mdi mdi-buffer float-right"></i></div>
                            <div class="text-white">
                                <h6 class="text-uppercase mb-3">Total Permits</h6>
                                <h4 class="mb-4">1,587</h4><span class="badge badge-info">+11% </span><span class="ml-2">From previous period</span></div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-md-6">
                    <div class="card mini-stat bg-primary" style="background-color: #97999B !important">
                        <div class="card-body mini-stat-img">
                            <div class="mini-stat-icon"><i class="mdi mdi-briefcase-check float-right"></i></div>
                            <div class="text-white">
                                <h6 class="text-uppercase mb-3">Processed Permits</h6>
                                <h4 class="mb-4">1,200</h4><span class="badge badge-danger">-29% </span><span class="ml-2">From previous period</span></div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-md-6">
                    <div class="card mini-stat bg-primary" style="background-color: #CF4520 !important">
                        <div class="card-body mini-stat-img">
                            <div class="mini-stat-icon"><i class="mdi mdi-tag-text-outline float-right"></i></div>
                            <div class="text-white">
                                <h6 class="text-uppercase mb-3">Pending Permits</h6>
                                <h4 class="mb-4">387</h4><span class="badge badge-warning">0% </span><span class="ml-2">From previous period</span></div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-3 col-md-6">
                    <div class="card mini-stat bg-primary" style="background-color: #0095C8 !important">
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
                            <div class="swipe-notification">
								<svg class="pull-left" style="width:40px; margin-top:5px;" id="Layer_1" data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 491.5 491.5"><defs><style>.cls-1{fill:#424242;}.cls-2{fill:none;}.cls-3{fill:#555;}</style></defs><title>swipe1</title><g id="Layer_1-2" data-name="Layer_1"><polygon class="cls-1" points="386.8 421.4 386.8 421.4 391.1 421.4 391.1 421.4 386.8 421.4"></polygon><polygon class="cls-1" points="275.4 421.4 275.4 421.4 275.4 421.4 275.4 421.4"></polygon><path class="cls-1" d="M275.4,421.4Z"></path><rect class="cls-2" width="491.5" height="491.5"></rect><path class="cls-3" d="M403.7,309.8c2.7,35.9,3.8,50-22.1,94.6a7.71,7.71,0,0,0-.8,1.9c-.1.5-.2,1.1-.3,1.6v.6h0l-.3,12.5v.5h0v.1h0a11.52,11.52,0,0,1-2.4,6.6,5.72,5.72,0,0,1-.7.8l-5.4-5.5,5.4,5.5a10.67,10.67,0,0,1-7.5,3.1h-116a10.78,10.78,0,0,1-7.4-3h0l-.1-.1-.1-.1h0a10.85,10.85,0,0,1-3.1-7.5h0v-.3h0l.3-7.9v-.5h0v-.2a7.17,7.17,0,0,0-.6-2.9,7.82,7.82,0,0,0-1.7-2.6c-48.6-47.3-49.6-50.1-66.8-98.4-2.3-6.6-5-13.9-7.6-21.2l-8-9.2a26.56,26.56,0,0,1,.7-35.6,25.91,25.91,0,0,1,17.9-8.3,26.67,26.67,0,0,1,18.7,6.2c4.4,3.7,14.9,21,23.2,34.7V123a25.9,25.9,0,0,1,51.8,0v54.6l.1-.1a26.31,26.31,0,0,1,37.2,0,25.8,25.8,0,0,1,6.1,9.7,26.64,26.64,0,0,1,44,17.9,26.48,26.48,0,0,1,16.5-5.8h0a26.29,26.29,0,0,1,26.3,26l.5,39.6c.2,18.4,1.3,32.9,2.2,44.9Z"></path><polygon class="cls-3" points="174.8 130.6 116.4 130.6 116.4 147.8 86.5 117.9 116.4 88 116.4 105.2 174.8 105.2 174.8 130.6"></polygon><path class="cls-3" d="M211.8,181.9a66.2,66.2,0,1,1,87.4-21.3c-1,1.5-2.1,3-3.2,4.4a31.79,31.79,0,0,0-6.2-.6,32.8,32.8,0,0,0-10.9,1.8V149.5c.4-.6.8-1.1,1.2-1.7a43,43,0,1,0-71.5.6,35.71,35.71,0,0,0,3.4,4.3l-.2,29.2Z"></path><polygon class="cls-3" points="313.3 130.6 371.7 130.6 371.7 147.8 401.6 117.9 371.7 88 371.7 105.2 313.3 105.2 313.3 130.6"></polygon></g></svg>
								
								<p class="text-muted m-b-0 pull-left m-t-15 font-12 m-t-20">Swipe <strong style="text-transform: uppercase">left</strong> and <strong style="text-transform: uppercase">right</strong> to see the full details</p></div>

                            <table class="table mb-0 table-responsive">
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
                                        <td><button type="button" class="btn btn-primary waves-effect waves-light" style="background-color: #0095C8;border:1px #0095C8 solid"><i class="fas fa-eye"></i></button></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">1</th>
                                        <td>1418010335</td>
                                        <td>02/08/2018</td>
                                        <td>Lab Analysis & Sampling</td>
                                        <td>AED 735</td>
                                        <td>Unpaid</td>
                                        <td><button type="button" class="btn btn-primary waves-effect waves-light" style="background-color: #0095C8;border:1px #0095C8 solid"><i class="fas fa-eye"></i></button></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">1</th>
                                        <td>1418010335</td>
                                        <td>02/08/2018</td>
                                        <td>Lab Analysis & Sampling</td>
                                        <td>AED 735</td>
                                        <td>Unpaid</td>
                                        <td><button type="button" class="btn btn-primary waves-effect waves-light" style="background-color: #0095C8;border:1px #0095C8 solid"><i class="fas fa-eye"></i></button></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">1</th>
                                        <td>1418010335</td>
                                        <td>02/08/2018</td>
                                        <td>Lab Analysis & Sampling</td>
                                        <td>AED 735</td>
                                        <td>Unpaid</td>
                                        <td><button type="button" class="btn btn-primary waves-effect waves-light" style="background-color: #0095C8;border:1px #0095C8 solid"><i class="fas fa-eye"></i></button></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">1</th>
                                        <td>1418010335</td>
                                        <td>02/08/2018</td>
                                        <td>Lab Analysis & Sampling</td>
                                        <td>AED 735</td>
                                        <td>Unpaid</td>
                                        <td><button type="button" class="btn btn-primary waves-effect waves-light" style="background-color: #0095C8;border:1px #0095C8 solid"><i class="fas fa-eye"></i></button></td>
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

