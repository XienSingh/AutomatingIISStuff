using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.Administration;
using Site = Microsoft.Web.Administration.Site;

namespace AutomateIIS.Logic
{
	public class CoreIISFeatures
	{
		public IEnumerable<Site> GetAllSites()
		{
			ServerManager iisManager = new ServerManager();
			SiteCollection sites = iisManager.Sites;

			return sites;
		}
		public static bool ConfirmDir(string Path)
		{
			var exists = System.IO.Directory.Exists(Path);

			if (!exists)
			{
				System.IO.Directory.CreateDirectory(Path);
				ConfirmDir(Path); //Some recursion never hurt;
			}
			return exists;
		}

		public static bool CheckIfSiteExists(string siteName, int siteId = 0)
		{

			ServerManager iisManager = new ServerManager();
			var site = iisManager.Sites[siteName];
			if (site == null)
			{
				return false;

			}
			else
			{
				return true;

			}

		}
		public static string CreateSite(string siteName, string protocol, string binding, string path)
		{
			if (CheckIfSiteExists(siteName))
			{
				return "Site Exsits";
			}
			ConfirmDir(path);
			var iisManager = new ServerManager();
			Site site = iisManager.Sites.Add(siteName, protocol, binding, path);
			iisManager.CommitChanges();
			AddAppPool(site, path);
			return "Created Site";
		}

		public static void AddAppPool(Site siteobj, string Path)
		{
			ServerManager serverManager = new ServerManager();
			Site site = serverManager.Sites[siteobj.Name];
			site.Name = siteobj.Name;
			site.Applications[0].VirtualDirectories[0].PhysicalPath = Path;
			serverManager.ApplicationPools.Add(siteobj.Name.Replace(" ", "") + "- ApplicationPool");
			serverManager.Sites[siteobj.Name].Applications[0].ApplicationPoolName = siteobj.Name.Replace(" ", "") + "- ApplicationPool";
			ApplicationPool apppool = serverManager.ApplicationPools[siteobj.Name.Replace(" ", "") + "- ApplicationPool"];
			apppool.ManagedPipelineMode = ManagedPipelineMode.Integrated;
			serverManager.CommitChanges();

		}
		public static string AddBindings(string siteToAddBinding, string BindingToAdd, string Proto)
		{

			ServerManager iisManager = new ServerManager();
			var site = iisManager.Sites[siteToAddBinding];

			BindingCollection biningCollection = site.Bindings;
			Binding binding = site.Bindings.CreateElement("binding");
			binding["protocol"] = Proto;
			var port = 80;
			if (Proto.ToLower() == "https")
				port = 443;

			binding["bindingInformation"] = $"*:{port}:{BindingToAdd}";
			foreach (var bindingInfo in biningCollection)
			{
				if (bindingInfo.BindingInformation == binding["bindingInformation"].ToString())
				{
					return "Binding Exists: " + binding["bindingInformation"].ToString();
				}
			}

			biningCollection.Add(binding);
			iisManager.CommitChanges();
			return "Binding added: " + binding["bindingInformation"].ToString();
		}

		
		public static void RemoveIISWebsite(string sitename, bool RemoveSite, int SiteId = 0)
		{

			ServerManager iisManager = new ServerManager();
			Site site = null;
			if (SiteId > 0)
			{
				site = iisManager.Sites[SiteId];
			}
			else
			{
				site = iisManager.Sites[sitename];
			}
			bool isActive = false;
			isActive = iisManager.Sites[sitename] == null ? false : true;
			if (isActive)
			{
				iisManager.Sites.Remove(site);
				iisManager.CommitChanges();
				DeleteApplicationPool((sitename.Replace(" ", "") + "- ApplicationPool").ToString(), RemoveSite);
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
