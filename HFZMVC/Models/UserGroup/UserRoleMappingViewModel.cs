using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFZMVC.Models.UserGroup
{
	public class UserRoleMappingViewModel
	{
		public string PageName { get; set; }
		public string strPagePath { get; set; }
		public  int? RoleId { get; set; }
		public int? MenuId { get; set; }
		public int? ParentId { get; set; }
		public bool? IsPermission { get; set; }

		public bool? isEdit { get; set; }
		public bool? isDelete { get; set; }
		public bool? isView { get; set; }
		public bool? isNew { get; set; }


	}
}