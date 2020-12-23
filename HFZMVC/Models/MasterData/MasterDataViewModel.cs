using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFZMVC.Models
{
  public class MasterDataCreateEditVM
  {
    public string Name { get; set; }

    public bool isEdit { get; set; }
    public bool isNew { get; set; }
    public int Id { get; set; }
    public string Type { get; set; }
    public bool isActive { get; set; }

    //For Master item
    public string ItemCode { get; set; } = "";
    public Decimal Amount { get; set; } = 0;

  }

  public class MasterDataIndexVM {

    public string Category { get; set; }
    public string CategoryLabel { get; set; }
    public string Category_Data { get; set; }
    public int RecordId { get; set; }

    public bool IsActive { get; set; }

  }

}