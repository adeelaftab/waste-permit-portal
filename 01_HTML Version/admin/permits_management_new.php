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
                        <h4 class="page-title pull-right">Total Permits : 13</h4>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-12">
                    <div class="card m-b-20">
                        <div class="card-body top-filter">
                            <div class="form-group col-sm-2 fa-pull-left">
                                <input type="text" class="colorpicker-default form-control colorpicker-element" value="Reference No">
                            </div>
                            <div class="form-group col-sm-2 fa-pull-left">
                                <div>
                                    <div class="input-group">
                                        <input type="text" class="form-control" placeholder="From [ mm/dd/yyyy ]" id="datepicker">
                                        <div class="input-group-append"><span class="input-group-text filter-bar-icons"><i class="mdi mdi-calendar"></i></span></div>
                                    </div>
                                    <!-- input-group -->
                                </div>
                            </div>
                            <div class="form-group col-sm-2 fa-pull-left">
                                <div>
                                    <div class="input-group">
                                        <input type="text" class="form-control" placeholder="To [ mm/dd/yyyy ]" id="datepicker">
                                        <div class="input-group-append"><span class="input-group-text filter-bar-icons"><i class="mdi mdi-calendar"></i></span></div>
                                    </div>
                                    <!-- input-group -->
                                </div>
                            </div>
                            <div class="col-sm-2 fa-pull-left">
                                <div class="dropdown mo-mb-2">
                                    <button class="btn btn-secondary filter-btn border-0 btn-lg dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        Permit Type
                                    </button>
                                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton" x-placement="bottom-start" style="position: absolute; transform: translate3d(0px, 33px, 0px); top: 0px; left: 0px; will-change: transform;">
                                        <a class="dropdown-item" href="#">All</a>
                                        <a class="dropdown-item" href="#">Municipal Solid Waste</a>
                                        <a class="dropdown-item" href="#">Industrial Non-hazardous Waste</a>
                                        <a class="dropdown-item" href="#">Hazardous Waste</a>
                                        <a class="dropdown-item" href="#">Expired Chemical/Products</a>
                                        <a class="dropdown-item" href="#">Lab Analysis &amp; Sampling</a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2 fa-pull-left">
                                <div class="dropdown mo-mb-2">
                                    <button class="btn btn-secondary filter-btn border-0 btn-lg dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        Status
                                    </button>
                                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton" x-placement="bottom-start" style="position: absolute; transform: translate3d(0px, 33px, 0px); top: 0px; left: 0px; will-change: transform;">
                                        <a class="dropdown-item" href="#">Permit Request Submitted</a>
                                        <a class="dropdown-item" href="#">Sampling Requested</a>
                                        <a class="dropdown-item" href="#">Sample Under Review</a>
                                        <a class="dropdown-item" href="#">More Sampling Required</a>
                                        <a class="dropdown-item" href="#">Permit Request Rejected</a>
                                        <a class="dropdown-item" href="#">Permit Request Accepted &amp; Under Review</a>
                                        <a class="dropdown-item" href="#">Permit Issued (Active)</a>
                                        <a class="dropdown-item" href="#">Permit Issued (Expired)</a>
                                        <a class="dropdown-item" href="#">Request Cancelled</a>
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
                        <div class="card-body">
                            <p class="text-muted m-b-20"></p>
                            <table id="datatable-buttons" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                <thead>
                                    <tr>
                                        <th>Reference No#</th>
                                        <th>Request Date</th>
                                        <th>Permit Type</th>
                                        <th>Total Amount</th>
                                        <th>Status</th>
                                        <th>Valid Till</th>
                                        <th>Notes</th>
                                        <th style="width:200px">Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>1418020002</td>
                                        <td>29/09/2018</td>
                                        <td>Lab Analysis & Sampling</td>
                                        <td>AED 735</td>
                                        <td>Sampling Requested</td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <a href="#"><button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button></a>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-download"></i></button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>1418020002</td>
                                        <td>29/09/2018</td>
                                        <td>Expired Chemical/Products</td>
                                        <td>AED 1575</td>
                                        <td>Permit Issued (Active)</td>
                                        <td></td>
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