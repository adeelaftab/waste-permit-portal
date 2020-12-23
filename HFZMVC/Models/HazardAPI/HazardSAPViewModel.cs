using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFZMVC.Models.HazardAPI
{
  public class HazardSAPViewModel
  {
    public string pdfstring { get; set; }
    public int clientid { get; set; }
    public string clientsecret { get; set; }
    public string appno { get; set; }
    public string landfill_user { get; set; } = "landfill";
  }
 
}