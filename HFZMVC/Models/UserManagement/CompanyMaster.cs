using HFZMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFZMVC.Models.UserManagement
{
    public class CompanyMaster
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public string LicenseNumber { get; set; }
        public string VATNumber { get; set; }
        public string Status { get; set; }
    }

    public class Permitlist
    {
        public int ID { get; set; }
        public string statusName { get; set; }
    }
    public class CompanyView 
    {
        public User Users { get; set; }
        public User Admindetail { get; set; }
        public List<Permitlist> permitlist { get; set; }
    }

}