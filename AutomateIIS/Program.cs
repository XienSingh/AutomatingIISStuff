using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.Administration;
using Site = Microsoft.Web.Administration.Site;
using AutomateIIS.Logic;

namespace AutomateIIS
{
	public class Program
	{
		static void Main(string[] args)
		{

			var Name = "TestSite Attempt1";
			var Proto = "http";
			var Binding = "*:80:TestUrl.co.za";
			var BindingAddProto = "HTTPS";
			var BindingToAdd = "Test.co.za";
			var Path = @"DirToSite"
			var RemoveSite = true;
			CoreIISFeatures cis = new CoreIISFeatures();
			var sites = cis.GetAllSites();
			foreach(Site site in sites)
			{
				Console.WriteLine("Site Id : "+site.Id + " Site Name : " + site.Name);

			}

			CoreIISFeatures.RemoveIISWebsite(Name, RemoveSite);

			var CreateSite = CoreIISFeatures.CreateSite(Name, Proto, Binding, Path);
			Console.WriteLine(CreateSite);

			var Bindings = CoreIISFeatures.AddBindings(Name, BindingToAdd, BindingAddProto);
			Console.WriteLine(Bindings);


		}



	}
}
