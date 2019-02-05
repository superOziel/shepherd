using Shepherd.Models;
using Shepherd.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Shepherd.Controllers
{
    public class ReportController : Controller
    {
        APQP_betaEntities db = new APQP_betaEntities();
        ReportService reportService = new ReportService();

        // GET: /Report/
        public ActionResult Index()        
        {
            return View();
        }

        public ActionResult getSalesOrders(string job = null) 
        {
            return Json(reportService.getSalesOrderes(job), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getLocations() 
        {
            return Json(db.Locations.ToList() , JsonRequestBehavior.AllowGet);
        }
    }
}
