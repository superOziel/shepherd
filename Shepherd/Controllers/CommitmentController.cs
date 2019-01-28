using Shepherd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shepherd.Controllers
{
    public class CommitmentController : Controller
    {
        //
        // GET: /Commitment/
        APQP_betaEntities db = new APQP_betaEntities();
        public ActionResult Index(Report item)
        {
            ViewBag.Assembly = item.ProdOrderNr;
            ViewBag.SO_Item = item.SO_Item;
            ViewBag.Item_Description = item.Item_Description;
            ViewBag.SO_Project = item.SO_Project;
            ViewBag.MaxDelivDateStr = item.MaxDelivDateStr;

            return View();
        }

        public ActionResult OpenModal(Report item) 
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCommitment(string assembly_number) {
            try
            {
                return Json(db.Assembly_Commitment.Where(c => c.assembly_number == assembly_number).FirstOrDefault(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult getSaleOrder(string assembly_number)
        {
            try
            {
                return Json(db.ViewGetSalesOrderReadyToShip.Where(v => v.ProdOrderNr == assembly_number).FirstOrDefault(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [HttpPost]
        public ActionResult SaveCommitment(Assembly_Commitment asco) 
        {
            Assembly_Commitment acomm = new Assembly_Commitment();
            try
            {
                var commitment = db.Assembly_Commitment.Where(c => c.assembly_number == asco.assembly_number).FirstOrDefault();
                Boolean post = false;

                if (commitment != null)
                {
                    //var comit = db.Assembly_Commitment.First<Assembly_Commitment>();
                    commitment.final_buffer = asco.final_buffer;
                    commitment.assembly_days = asco.assembly_days;
                    commitment.machining_days = asco.machining_days;
                    commitment.bom_date = asco.bom_date;
                    commitment.casting_date = asco.casting_date;
                    commitment.readytoship = asco.readytoship;
                } 
                else 
                {
                    acomm.assembly_number = asco.assembly_number;
                    acomm.final_buffer = asco.final_buffer;
                    acomm.assembly_days = asco.assembly_days;
                    acomm.machining_days = asco.machining_days;
                    acomm.bom_date = asco.bom_date;
                    acomm.casting_date = asco.casting_date;
                    acomm.readytoship = asco.readytoship;
                    post = true;
                    db.Assembly_Commitment.Add(acomm);
                }

                db.SaveChanges();

                return Json(post);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
