using Shepherd.Models;
using Shepherd.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Shepherd.Controllers
{
    public class ReportController : Controller
    {
        APQP_betaEntities db = new APQP_betaEntities();
        ReportService reportService = new ReportService();
        TulSqlService tulService = new TulSqlService();


        // GET: /Report/
        public ActionResult Index(string id = null)        
        {
            var user_decrypted = DecodeUser(System.Web.HttpContext.Current.Request.QueryString["id"]);
            ViewBag.User = user_decrypted;


            return View();
        }

        public ActionResult getSalesOrders(string job = null, bool mrp = false, string planner = null, bool plannerEmpty = false, string locationId = null) 
        {
            var JsonResult = Json(reportService.getSalesOrderes(job, mrp, planner, plannerEmpty, locationId), JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        public ActionResult getPlanners(string job = null, string planner = null) 
        {
            var JsonResult = Json(reportService.getPlanners(job), JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        public ActionResult getLocations()
        {
            return Json(reportService.getLocations(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getMultipleStartOperations(string locationSelected, string company = null) 
        {
            var JsonResult = Json(tulService.getMultipleStartOperations(locationSelected, company), JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        public ActionResult getOperationLessOrders(string locationSelected, string company =  null) 
        {
            var JsonResult = Json(tulService.getOperationLessOrders(locationSelected, company), JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        public ActionResult getSchedulerScrambledSequences(string locationSelected, string company = null)
        {
            var JsonResult = Json(tulService.getSchedulerScrambledSequences(locationSelected, company), JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        public ActionResult getTimeLessOperations(string locationSelected, string company = null)
        {
            var JsonResult = Json(tulService.getTimeLessOperations(locationSelected, company), JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        public string DecodeUser(string user_encode)
        {
            try
            {
                Byte[] b = Convert.FromBase64String(user_encode);
                string decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
                return decrypted;
            }
            catch
            {
                return null;
            }

        } 
    }
}
