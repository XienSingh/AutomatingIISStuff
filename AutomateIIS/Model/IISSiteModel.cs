using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutomateIIS.Model
{
	public class IISSiteModel
	{
		public string SiteName { get; set; }
		public string State { get; set; }
		public string Bindings { get; set; }
        public long SiteID { get;set;}

	}
}
