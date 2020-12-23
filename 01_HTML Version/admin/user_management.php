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
                        <h4 class="page-title">List of Users</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0);">Dashobard</a></li>
                            <li class="breadcrumb-item"><a href="javascript:void(0);">Manage Users</a></li>
                        </ol>
                        <div class="state-information d-none d-sm-block">
                            <button type="button" class="btn btn-primary waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-center"><i class="fa fa-plus-circle"></i> Add New User</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-12">
                    <div class="card m-b-20">
                        <div class="card-body">

                            <div class="modal fade bs-example-modal-center" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title mt-0"><i class="fa fa-user"></i> Add New User</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                        </div>
                                        <div class="modal-body">
                                            <form class="form-horizontal" action="" style="padding: 0px 30px">
                                                <div class="form-group">
                                                    <input type="text" class="form-control" id="useremail" placeholder="Enter Full Name">
                                                </div>

                                                <div class="form-group">
                                                    <input type="text" class="form-control" id="username" placeholder="Enter username">
                                                </div>

                                                <div class="form-group">
                                                    <input type="email" class="form-control" id="username" placeholder="Enter Email">
                                                </div>

                                                <div class="form-group">
                                                    <input type="text" class="form-control" id="username" placeholder="Enter Phone">
                                                </div>

                                                <div class="form-group">
                                                    <input type="password" class="form-control" id="userpassword" placeholder="Enter password">
                                                </div>

                                                <select class="form-control">
                                                    <option>-- Select Role --</option>
                                                    <option>Admin</option>
                                                    <option>Manager</option>
                                                </select>
                                            </form>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary waves-effect" data-dismiss="modal">Close</button>
                                            <button type="button" class="btn btn-primary waves-effect waves-light">Save</button>
                                        </div>
                                    </div>
                                    <!-- /.modal-content -->
                                </div>
                                <!-- /.modal-dialog -->
                            </div>
                            <!-- /.modal -->
                            
                            <table id="datatable-buttons" class="table table-striped table-bordered dt-responsive nowrap" style="border-collapse: collapse; border-spacing: 0; width: 100%;">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Name</th>
                                        <th>Email</th>
                                        <th>Phone</th>
                                        <th>Role</th>
                                        <th>Status</th>
                                        <th style="width:200px">Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>01</td>
                                        <td>Isam Rihan</td>
                                        <td>isam@plana.ae</td>
                                        <td>+971 55 306 68999</td>
                                        <td>Manager</td>
                                        <td><span class="badge badge-danger">Inactive</span></td>
                                        <td>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></button>
                                            <button type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times"></i></button>
                                            <button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>02</td>
                                        <td>Isam Rihan</td>
                                        <td>isam@plana.ae</td>
                                        <td>+971 55 306 68999</td>
                                        <td>Manager</td>
                                        <td><span class="badge badge-success">Active</span></td>
                                        <td>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></button>
                                            <button type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times"></i></button>
                                            <button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>03</td>
                                        <td>Isam Rihan</td>
                                        <td>isam@plana.ae</td>
                                        <td>+971 55 306 68999</td>
                                        <td>Admin</td>
                                        <td><span class="badge badge-danger">Inactive</span></td>
                                        <td>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></button>
                                            <button type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times"></i></button>
                                            <button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>04</td>
                                        <td>Isam Rihan</td>
                                        <td>isam@plana.ae</td>
                                        <td>+971 55 306 68999</td>
                                        <td>Admin</td>
                                        <td><span class="badge badge-success">Active</span></td>
                                        <td>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></button>
                                            <button type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times"></i></button>
                                            <button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>05</td>
                                        <td>Isam Rihan</td>
                                        <td>isam@plana.ae</td>
                                        <td>+971 55 306 68999</td>
                                        <td>Manager</td>
                                        <td><span class="badge badge-danger">Inactive</span></td>
                                        <td>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></button>
                                            <button type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times"></i></button>
                                            <button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>06</td>
                                        <td>Isam Rihan</td>
                                        <td>isam@plana.ae</td>
                                        <td>+971 55 306 68999</td>
                                        <td>Manager</td>
                                        <td><span class="badge badge-success">Active</span></td>
                                        <td>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></button>
                                            <button type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times"></i></button>
                                            <button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>07</td>
                                        <td>Isam Rihan</td>
                                        <td>isam@plana.ae</td>
                                        <td>+971 55 306 68999</td>
                                        <td>Manager</td>
                                        <td><span class="badge badge-danger">Inactive</span></td>
                                        <td>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></button>
                                            <button type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times"></i></button>
                                            <button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>08</td>
                                        <td>Isam Rihan</td>
                                        <td>isam@plana.ae</td>
                                        <td>+971 55 306 68999</td>
                                        <td>Manager</td>
                                        <td><span class="badge badge-success">Active</span></td>
                                        <td>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></button>
                                            <button type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times"></i></button>
                                            <button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>09</td>
                                        <td>Isam Rihan</td>
                                        <td>isam@plana.ae</td>
                                        <td>+971 55 306 68999</td>
                                        <td>Manager</td>
                                        <td><span class="badge badge-danger">Inactive</span></td>
                                        <td>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></button>
                                            <button type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times"></i></button>
                                            <button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>10</td>
                                        <td>Isam Rihan</td>
                                        <td>isam@plana.ae</td>
                                        <td>+971 55 306 68999</td>
                                        <td>Manager</td>
                                        <td><span class="badge badge-success">Active</span></td>
                                        <td>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></button>
                                            <button type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times"></i></button>
                                            <button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>11</td>
                                        <td>Isam Rihan</td>
                                        <td>isam@plana.ae</td>
                                        <td>+971 55 306 68999</td>
                                        <td>Manager</td>
                                        <td><span class="badge badge-danger">Inactive</span></td>
                                        <td>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></button>
                                            <button type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times"></i></button>
                                            <button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>12</td>
                                        <td>Isam Rihan</td>
                                        <td>isam@plana.ae</td>
                                        <td>+971 55 306 68999</td>
                                        <td>Manager</td>
                                        <td><span class="badge badge-success">Active</span></td>
                                        <td>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></button>
                                            <button type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times"></i></button>
                                            <button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>13</td>
                                        <td>Isam Rihan</td>
                                        <td>isam@plana.ae</td>
                                        <td>+971 55 306 68999</td>
                                        <td>Manager</td>
                                        <td><span class="badge badge-danger">Inactive</span></td>
                                        <td>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></button>
                                            <button type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times"></i></button>
                                            <button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>14</td>
                                        <td>Isam Rihan</td>
                                        <td>isam@plana.ae</td>
                                        <td>+971 55 306 68999</td>
                                        <td>Manager</td>
                                        <td><span class="badge badge-success">Active</span></td>
                                        <td>
                                            <button type="button" class="btn btn-primary waves-effect waves-light"><i class="fa fa-edit"></i></button>
                                            <button type="button" class="btn btn-danger waves-effect waves-light"><i class="fa fa-times"></i></button>
                                            <button type="button" class="btn btn-secondary waves-effect waves-light"><i class="fa fa-eye"></i></button>
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
