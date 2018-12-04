using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IISManagmentSite.Models
{
	public class IISSiteModel
	{
		public string SiteName { get; set; }
		public string PathToSite { get; set; }
		public string Bindings { get; set; }
		public string Proto { get; set; }
		public enum BindingType {
			Http = 1,
			https = 2
			}

	}
}