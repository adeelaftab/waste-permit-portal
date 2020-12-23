using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFZMVC.Models.HazardAPI
{
  public class HazardEditRequestViewModel
  {
    public string appno { get; set; }
    public string editremarks { get; set; }
    public string landfill_user { get; set; } = "hfzadmin";
    public int clientid { get; set; }
    public string clientsecret { get; set; }

    public bool isreject { get; set; } = false;

  }
}