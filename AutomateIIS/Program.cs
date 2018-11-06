using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.Administration;
using Site = Microsoft.Web.Administration.Site;

namespace AutomateIIS
{
	class Program
	{
		static void Main(string[] args)
		{
			var Name = "Gateway - Keegan";
			var Proto = "http";
			var Binding = "*:80:keegan.p4.qa.ix.co.za";
			var BindingAdd = "*:80:";
			var Path = @"E:\iX-Websites\Gateway - Keegan";
			var iisManager = new ServerManager();
			var RemoveSite = false;

			RemoveIISWebsite(Name, RemoveSite);
			Console.WriteLine("Removed site if true");
			Site site = iisManager.Sites.Add(Name, Proto, Binding, Path);
			Console.WriteLine("Site Added");
			iisManager.CommitChanges();
			AddAppPool(site, Path);
			Console.WriteLine("Added AppPool For Site");
			//AddBindings(site, BindingAdd, Proto);

			Console.WriteLine("Site : " + Name + " Created");

			Console.ReadLine();
		}

		public static void AddAppPool(Site siteobj, string Path)
		{
			ServerManager serverManager = new ServerManager();
			Site site = serverManager.Sites[siteobj.Name];
			site.Name = siteobj.Name;
			site.Applications[0].VirtualDirectories[0].PhysicalPath = Path;
			serverManager.ApplicationPools.Add(siteobj.Name.Replace(" ", "") + "ApplicationPool");
			serverManager.Sites[siteobj.Name].Applications[0].ApplicationPoolName = siteobj.Name.Replace(" ", "") + "ApplicationPool";
			ApplicationPool apppool = serverManager.ApplicationPools[siteobj.Name.Replace(" ", "") + "ApplicationPool"];
			apppool.ManagedPipelineMode = ManagedPipelineMode.Integrated;
			serverManager.CommitChanges();

		}
		public static void AddBindings(Site siteObj, string AddBinding, string Proto)
		{
			ServerManager serverMgr = new ServerManager();
			Site Site = serverMgr.Sites[siteObj.Name];
			Site.Bindings.Add(Proto, "http");
			Site.ServerAutoStart = true;
			serverMgr.CommitChanges();
		}
		public static void RemoveIISWebsite(string sitename,bool RemoveSite)
		{

			ServerManager iisManager = new ServerManager();
			Site site = iisManager.Sites[sitename];
			bool isActive = false;
			isActive = iisManager.Sites[sitename] == null ? false : true;
			if (isActive)
			{
				iisManager.Sites.Remove(site);
				iisManager.CommitChanges();
				DeleteApplicationPool((sitename.Replace(" ", "") + "ApplicationPool").ToString(), RemoveSite);
			}
		}
		public static void DeleteApplicationPool(String applicationPoolName, bool RemoveSite)
		{
			if (RemoveSite)
			{
				if (string.IsNullOrEmpty(applicationPoolName))
					throw new ArgumentNullException("applicationPoolName", "DeleteApplicationPool: applicationPoolName is null or empty.");

				ServerManager iisManager = new ServerManager();
				ApplicationPool AppPool = iisManager.ApplicationPools[applicationPoolName];
				ApplicationPoolCollection AppPoolColl = iisManager.ApplicationPools;
				AppPoolColl.Remove(AppPool);
				iisManager.CommitChanges();
			}
		}

	}
}
