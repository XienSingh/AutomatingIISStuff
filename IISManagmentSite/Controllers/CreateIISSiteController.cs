using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IISManagmentSite.Models;
using AutomateIIS.Logic;

namespace IISManagmentSite.Controllers
{
	public class CreateIISSiteController : Controller
	{
		// GET: CreateIISSite
		public ActionResult Index()
		{
			return RedirectToAction("CreateSite");
		}
		public ActionResult CreateSite()
		{
			return View();
		}

		[HttpPost]
		public JsonResult CreateSite(IISSiteModel iISSite)
		{
			var CreateSite = CoreIISFeatures.CreateSite(iISSite.SiteName, iISSite.Proto, iISSite.Bindings, iISSite.PathToSite);
			return Json(CreateSite);
		}
	}
}