﻿using AutomateIIS.Logic;
using IISManagmentSite.ViewModels;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IISManagementSite.Controllers
{
    public class ManageSitesController : Controller
	{
		// GET: ManageSites
		public ActionResult Index()
		{
			return RedirectToAction("ListAllSites");
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
        public JsonResult AddSSLBindings(int SiteID ,string[] BindingToAdd)
        {
            var AddSSLBindingToSite = CoreIISFeatures.AddSSLCertAndBindings(SiteID,BindingToAdd);
            return Json(AddSSLBindingToSite);
        }
        public JsonResult DeleteBindings(string hostname, string sitename)
		{
		
			var DeleteSiteBinding = CoreIISFeatures.RemoveBinding(hostname,sitename);
			return Json(DeleteSiteBinding);

		}
		

	}
}