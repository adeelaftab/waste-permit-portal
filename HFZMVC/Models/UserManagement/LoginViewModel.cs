using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFZMVC.Models.UserManagement
{
	public class LoginViewModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public bool RememberMe { get; set; }
		public int RetryCount { get; set; }
	}
}