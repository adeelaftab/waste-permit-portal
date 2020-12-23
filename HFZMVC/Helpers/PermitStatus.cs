using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFZMVC.Helpers
{
	public enum PermitStatus
	{
		PermitRequestSubmitted=1,
		SamplingRequested,
		SampleUnderReview,
		MoreSamplingequired
	}
}