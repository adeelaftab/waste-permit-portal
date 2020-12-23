
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFZMVC.Models.PermitRequest
{
    public class TimelineViewModel
    {

        public string Old_Value { get; set; }
        public string New_Value { get; set; }
        public string Action_Type { get; set; }
        public string Description { get; set; }
        public DateTime? Created_Timestamp { get; set; }
        public string UserName { get; set; }
    public bool LatestOne { get; set; }

    public HFZMVC.Models.EntityFramework.SamplingRequest attachedSampling { get; set; }
    }

    
}