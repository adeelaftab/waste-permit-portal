using HFZMVC.Models.EntityFramework;
using HFZMVC.Models.UserGroup;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace HFZMVC.Controllers
{
    public class UserGroupController : Controller
    {
        WasteManageEntities _Db;
        // GET: UserGroup
        public ActionResult Index()
        {
            using (_Db=new WasteManageEntities())
            {
                var usergroups = _Db.RoleMasters.Where(e => e.RoleName != "--").ToList();
                return View(usergroups);
            }
            
        }

        public ActionResult Create() 
        {        
            using (_Db = new WasteManageEntities())
            {
                var RoleMasterinsert = new RoleMaster
                {
                    RoleName = "--",
                    CreatedBy = (int)Session[AppVariables.SessionUserId],
                    CreatedOn = DateTime.Now
                };

                _Db.RoleMasters.Add(RoleMasterinsert);
                _Db.SaveChanges();

                int id = RoleMasterinsert.ID;
                
                ViewBag.roleid = id;
                var role = _Db.RoleMasters.Where(e => e.ID == id).FirstOrDefault();
                var viewmodel = new List<UserRoleMappingViewModel>();
                if (role != null)
                {
                    ViewBag.rolename = role.RoleName;
                    var menuList = _Db.eMenu_Master.ToList();
                    foreach (var item in menuList)
                    {
                        var permission = _Db.RoleMenuMappings.Where(e => e.RoleID == id &&
                         e.MenuID == item.nMenuID).FirstOrDefault();
                        if (permission == null)
                        {
                            viewmodel.Add(new UserRoleMappingViewModel()
                            {
                                MenuId = item.nMenuID,
                                isDelete = false,
                                isEdit = false,
                                isNew = false,
                                IsPermission = item.bIsPermission ?? false,
                                isView = false,
                                PageName = item.strPageName,
                                strPagePath = item.strPagePath,
                                ParentId = item.nParentID,
                                RoleId = id
                            });

                        }
                        else
                        {
                            viewmodel.Add(new UserRoleMappingViewModel()
                            {
                                MenuId = item.nMenuID,
                                isDelete = permission.IsDelete,
                                isEdit = permission.IsEdit,
                                isNew = permission.IsNew,
                                IsPermission = item.bIsPermission,
                                isView = permission.IsView,
                                PageName = item.strPageName,
                                strPagePath = item.strPagePath,
                                ParentId = item.nParentID,
                                RoleId = id
                            });
                        }

                    }


                    return View(viewmodel.ToList());
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
        }
        public ActionResult EditRole(int id)
        {
            ViewBag.roleid = id;
            
            using (_Db=new WasteManageEntities())
            {
                var role = _Db.RoleMasters.Where(e=>e.ID==id).FirstOrDefault();
                var viewmodel = new List<UserRoleMappingViewModel>();
                if (role != null)
                {
                    ViewBag.rolename = role.RoleName;
                    var menuList = _Db.eMenu_Master.ToList();
                    foreach (var item in menuList)
                    {
                        var permission = _Db.RoleMenuMappings.Where(e => e.RoleID == id &&
                         e.MenuID == item.nMenuID).FirstOrDefault();
                        if (permission==null)
                        {
                            viewmodel.Add(new UserRoleMappingViewModel()
                            {
                                MenuId = item.nMenuID,
                                isDelete = false,
                                isEdit = false,
                                isNew = false,
                                IsPermission = item.bIsPermission ?? false,
                                isView = false,
                                PageName = item.strPageName,
                                strPagePath = item.strPagePath,
                                ParentId = item.nParentID,
                                RoleId = id
                            });

                        }
                        else
                        {
                            viewmodel.Add(new UserRoleMappingViewModel()
                            {
                                MenuId = item.nMenuID,
                                isDelete = permission.IsDelete,
                                isEdit = permission.IsEdit,
                                isNew = permission.IsNew,
                                IsPermission = item.bIsPermission,
                                isView = permission.IsView,
                                PageName = item.strPageName,
                                strPagePath = item.strPagePath,
                                ParentId = item.nParentID,
                                RoleId = id
                            });
                        }

                    }


                    return View(viewmodel.ToList());
                }
                else
                {
                    return RedirectToAction("Index");
                }
              
            }
            
        }
        
        [HttpPost]
        public ActionResult EditRole(IEnumerable<UserRoleMappingViewModel> userroles) {


            if (userroles!=null)
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append(" delete from RoleMenuMapping where RoleId=@roleid");

                using (TransactionScope scope= new TransactionScope())
                {
                    try
                    {
                        if (userroles.Count() > 0)
                        {
                            using (_Db = new WasteManageEntities())
                            {
                                var rec = userroles.First();
                                var usergroups = _Db.RoleMasters.Where(e => e.ID == rec.RoleId).FirstOrDefault();
                                usergroups.RoleName = rec.PageName;
                                _Db.Entry(usergroups).State = EntityState.Modified;
                                _Db.SaveChanges();

                                _Db.Database.ExecuteSqlCommand(queryBuilder.ToString(), new SqlParameter("@roleid", userroles.First().RoleId));

                                foreach (var item in userroles)
                                {

                                    _Db.RoleMenuMappings.Add(new RoleMenuMapping()
                                    {
                                        CreatedBy = AppUtil.checkLogin(),
                                        CreatedOn = DateTime.Now,
                                        RoleID = item.RoleId.Value,
                                        UpdatedBy = AppUtil.checkLogin(),
                                        UpdatedOn = DateTime.Now,
                                        IsDelete = item.isDelete.Value,
                                        IsEdit = item.isEdit.Value,
                                        IsNew = item.isNew.Value,
                                        IsView = item.isView.Value,
                                        MenuID = item.MenuId.Value
                                    });


                                }
                                _Db.Database.ExecuteSqlCommand("Delete From RoleMaster where RoleName = '--'");

                                _Db.SaveChanges();
                                scope.Complete();

                            }
                        }
                        return Json("Ok");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var errors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in errors.ValidationErrors)
                            {
                                // get the error message 
                                string errorMessage = validationError.ErrorMessage;
                            }
                        }
                    }
                }
            }
            return View(userroles);
            
        }

        public ActionResult ViewRole(int id)
        {
            ViewBag.roleid = id;
            using (_Db = new WasteManageEntities())
            {
                var role = _Db.RoleMasters.Where(e => e.ID == id).FirstOrDefault();
                var viewmodel = new List<UserRoleMappingViewModel>();
                if (role != null)
                {
                    ViewBag.rolename = role.RoleName;
                    var menuList = _Db.eMenu_Master.ToList();
                    foreach (var item in menuList)
                    {
                        var permission = _Db.RoleMenuMappings.Where(e => e.RoleID == id &&
                         e.MenuID == item.nMenuID).FirstOrDefault();
                        if (permission == null)
                        {
                            viewmodel.Add(new UserRoleMappingViewModel()
                            {
                                MenuId = item.nMenuID,
                                isDelete = false,
                                isEdit = false,
                                isNew = false,
                                IsPermission = item.bIsPermission ?? false,
                                isView = false,
                                PageName = item.strPageName,
                                strPagePath = item.strPagePath,
                                ParentId = item.nParentID,
                                RoleId = id
                            });
                        }
                        else
                        {
                            viewmodel.Add(new UserRoleMappingViewModel()
                            {
                                MenuId = item.nMenuID,
                                isDelete = permission.IsDelete,
                                isEdit = permission.IsEdit,
                                isNew = permission.IsNew,
                                IsPermission = item.bIsPermission,
                                isView = permission.IsView,
                                PageName = item.strPageName,
                                strPagePath = item.strPagePath,
                                ParentId = item.nParentID,
                                RoleId = id
                            });
                        }
                    }
                    return View(viewmodel.ToList());
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
        }
    }
}