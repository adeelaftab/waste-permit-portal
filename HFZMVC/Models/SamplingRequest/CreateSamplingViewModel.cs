using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFZMVC.Models.SamplingRequest
{
	public class CreateSamplingViewModel
	{
		public int PermitId { get; set; }
		//public IEnumerable<HFZMVC.Models.EntityFramework. PermitRequest> permitRequests { get; set; }
		public string WasteDescription { get; set; }
		public string SamplingAddress { get; set; }

		public int permitIdLabel { get; set; }
		public string WasteDescriptoinLabel { get; set; }
		public decimal? SampleFees { get; set; }
		public decimal? ServiceFee { get; set; }
		public decimal? rdfee { get; set; }
		public decimal? vat { get; set; }
		public decimal? grandtotal { get; set; }
    public string samplingpurpose { get; set; }
    public string samplesource { get; set; }
    public string OtherSourceOfSample { get; set; }

  }
}