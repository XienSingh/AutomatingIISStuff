using AutomateIIS.Logic;
using IISManagmentSite.ViewModels;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IISManagmentSite.Controllers
{
	public class ManageSitesController : Controller
	{
		// GET: ManageSites
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult ListAllSites()
		{
			CoreIISFeatures iISFeatures = new CoreIISFeatures();
			var result = iISFeatures.GetIISSite();
			ViewBag.SiteList = result;
			return View();
		}

		public JsonResult AddBindings(string siteToAddBinding, string BindingToAdd, string Proto)
		{
			var AddBindingToSite = CoreIISFeatures.AddBindings(siteToAddBinding,BindingToAdd,Proto);
			return Json(AddBindingToSite);
		}
		public JsonResult DeleteBindings(string hostname, string sitename)
		{
		
			var DeleteSiteBinding = CoreIISFeatures.RemoveBinding(hostname,sitename);
			return Json(DeleteSiteBinding);

		}
		

	}
}