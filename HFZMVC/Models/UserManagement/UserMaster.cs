using HFZMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFZMVC.Models.UserManagement
{
    public class UserMaster
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string EmailID { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}