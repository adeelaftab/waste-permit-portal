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
                        <h4 class="page-title pull-left">Payments Management</h4>
                        <h4 class="page-title pull-right">Total Payments : 10</h4>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-12">
                    <div class="card m-b-20">
                        <div class="card-body top-filter">
                            <div class="form-group col-sm-2 fa-pull-left">
                                <div class=" mo-mb-2"><input type="text" class="colorpicker-default form-control colorpicker-element" value="Reference No"></div>
                            </div>
                            <div class="form-group col-sm-2 fa-pull-left">
                                <div>
                                    <div class="input-group  mo-mb-2">
                                        <input type="text" class="form-control" placeholder="From [ mm/dd/yyyy ]" id="datepicker">
                                        <div class="input-group-append"><span class="input-group-text filter-bar-icons"><i class="mdi mdi-calendar"></i></span></div>
                                    </div>
                                    <!-- input-group -->
                                </div>
                            </div>
                            <div class="form-group col-sm-2 fa-pull-left">
                                <div>
                                    <div class="input-group  mo-mb-2">
                                        <input type="text" class="form-control" placeholder="To [ mm/dd/yyyy ]" id="datepicker">
                                        <div class="input-group-append"><span class="input-group-text filter-bar-icons"><i class="mdi mdi-calendar"></i></span></div>
                                    </div>
                                    <!-- input-group -->
                                </div>
                            </div>
                            <div class="col-sm-2 fa-pull-left">
                                <div class="dropdown mo-mb-2">
                                    <button class="btn btn-secondary filter-btn border-0 btn-lg dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        Payment Type
                                    </button>
                                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton" x-placement="bottom-start" style="position: absolute; transform: translate3d(0px, 33px, 0px); top: 0px; left: 0px; will-change: transform;">
                                        <a class="dropdown-item" href="#">All</a>
                                        <a class="dropdown-item" href="#">Card</a>
                                        <a class="dropdown-item" href="#">Cheque</a>
                                        <a class="dropdown-item" href="#">Transfer</a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2 fa-pull-left">
                                <div class="dropdown mo-mb-2">
                                    <button class="btn btn-secondary filter-btn border-0 btn-lg dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        Status
                                    </button>
                                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton" x-placement="bottom-start" style="position: absolute; transform: translate3d(0px, 33px, 0px); top: 0px; left: 0px; will-change: transform;">
                                        <a class="dropdown-item" href="#">Verified</a>
                                        <a class="dropdown-item" href="#">Rejected</a>
                                        <a class="dropdown-item" href="#">Pending</a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2 fa-pull-left">
                            <button type="button" class="btn btn-lg btn-success waves-effect waves-light filter-btn-search">Search</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end col -->
            </div>
            
            <div class="row">
                <div class="col-12">
                    <div class="card m-b-20">
                        <div class="card-body permits-managment-box">
							<div class="swipe-notification">
								<svg class="pull-left" style="width:40px; margin-top:5px;" id="Layer_1" data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 491.5 491.5"><defs><style>.cls-1{fill:#424242;}.cls-2{fill:none;}.cls-3{fill:#555;}</style></defs><title>swipe1</title><g id="Layer_1-2" data-name="Layer_1"><polygon class="cls-1" points="386.8 421.4 386.8 421.4 391.1 421.4 391.1 421.4 386.8 421.4"></polygon><polygon class="cls-1" points="275.4 421.4 275.4 421.4 275.4 421.4 275.4 421.4"></polygon><path class="cls-1" d="M275.4,421.4Z"></path><rect class="cls-2" width="491.5" height="491.5"></rect><path class="cls-3" d="M403.7,309.8c2.7,35.9,3.8,50-22.1,94.6a7.71,7.71,0,0,0-.8,1.9c-.1.5-.2,1.1-.3,1.6v.6h0l-.3,12.5v.5h0v.1h0a11.52,11.52,0,0,1-2.4,6.6,5.72,5.72,0,0,1-.7.8l-5.4-5.5,5.4,5.5a10.67,10.67,0,0,1-7.5,3.1h-116a10.78,10.78,0,0,1-7.4-3h0l-.1-.1-.1-.1h0a10.85,10.85,0,0,1-3.1-7.5h0v-.3h0l.3-7.9v-.5h0v-.2a7.17,7.17,0,0,0-.6-2.9,7.82,7.82,0,0,0-1.7-2.6c-48.6-47.3-49.6-50.1-66.8-98.4-2.3-6.6-5-13.9-7.6-21.2l-8-9.2a26.56,26.56,0,0,1,.7-35.6,25.91,25.91,0,0,1,17.9-8.3,26.67,26.67,0,0,1,18.7,6.2c4.4,3.7,14.9,21,23.2,34.7V123a25.9,25.9,0,0,1,51.8,0v54.6l.1-.1a26.31,26.31,0,0,1,37.2,0,25.8,25.8,0,0,1,6.1,9.7,26.64,26.64,0,0,1,44,17.9,26.48,26.48,0,0,1,16.5-5.8h0a26.29,26.29,0,0,1,26.3,26l.5,39.6c.2,18.4,1.3,32.9,2.2,44.9Z"></path><polygon class="cls-3" points="174.8 130.6 116.4 130.6 116.4 147.8 86.5 117.9 116.4 88 116.4 105.2 174.8 105.2 174.8 130.6"></polygon><path class="cls-3" d="M211.8,181.9a66.2,66.2,0,1,1,87.4-21.3c-1,1.5-2.1,3-3.2,4.4a31.79,31.79,0,0,0-6.2-.6,32.8,32.8,0,0,0-10.9,1.8V149.5c.4-.6.8-1.1,1.2-1.7a43,43,0,1,0-71.5.6,35.71,35.71,0,0,0,3.4,4.3l-.2,29.2Z"></path><polygon class="cls-3" points="313.3 130.6 371.7 130.6 371.7 147.8 401.6 117.9 371.7 88 371.7 105.2 313.3 105.2 313.3 130.6"></polygon></g></svg>
								
								<p class="text-muted m-b-0 pull-left m-t-15 font-12 m-t-20">Swipe <strong style="text-transform: uppercase">left</strong> and <strong style="text-transform: uppercase">right</strong> to see the full details</p></div>
                            <p class="text-muted m-b-20"></p>
							
                            <table id="datatable-buttons" class="table table-striped table-bordered dt-responsive nowrap table-responsive" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                <thead>
                                    <tr>
                                        <th>Reference No#</th>
                                        <th>Date</th>
                                        <th>Type</th>
                                        <th>Amount</th>
                                        <th>Status</th>
                                        <th>Notes</th>
                                        <th style="width:200px">Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>1418020002</td>
                                        <td>29/09/2019</td>
                                        <td>Cheque</td>
                                        <td>AED 735</td>
                                        <td>Pending</td>
                                        <td></td>
                                        <td>
                                            <a href="#"><button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button></a>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-download"></i></button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>1418020002</td>
                                        <td>12/09/2019</td>
                                        <td>Card</td>
                                        <td>AED 1575</td>
                                        <td>Approved</td>
                                        <td></td>
                                        <td>
                                            <a href="#"><button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button></a>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-download"></i></button>
                                        </td>
                                    </tr>
                                    
                                </tbody>
                            </table>
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