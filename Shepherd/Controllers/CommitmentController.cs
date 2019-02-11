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
            ViewBag.SO_Nr = item.SO_Nr;
            ViewBag.SO_Ln = item.SO_Ln;
            ViewBag.SO_Sq = item.SO_Sq;

            return View();
        }

        [HttpGet]
        public ActionResult GetCommitment(Report item) {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var commitment = db.Assembly_Commitment.Where(s => s.SO_Nr == item.SO_Nr && s.SO_Ln == item.SO_Ln && 
                                                       s.SO_Sq == item.SO_Sq && s.ProdOrderNr == item.ProdOrderNr ).FirstOrDefault();
                return Json(commitment, JsonRequestBehavior.AllowGet);
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
                return Json(db.ViewGetSalesOrderReadyToShips.Where(v => v.ProdOrderNr == assembly_number).FirstOrDefault(), JsonRequestBehavior.AllowGet);
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
            Sales_Orders so = new Sales_Orders();
            Boolean post = false;
            try
            {
                var commitment = db.Assembly_Commitment.Where(s => s.SO_Nr == asco.SO_Nr && s.SO_Ln == asco.SO_Ln &&
                                                              s.SO_Sq == asco.SO_Sq && s.ProdOrderNr == asco.ProdOrderNr).FirstOrDefault();
                if (commitment != null)
                {
                    commitment.final_buffer = asco.final_buffer;
                    commitment.assembly_days = asco.assembly_days;
                    commitment.machining_days = asco.machining_days;
                    commitment.bom_date = asco.bom_date;
                    commitment.casting_date = asco.casting_date;
                    commitment.readytoship = asco.readytoship;
                    commitment.SO_Nr = asco.SO_Nr;
                    commitment.SO_Sq = asco.SO_Sq;
                    commitment.SO_Ln = asco.SO_Ln;
                    commitment.ProdOrderNr = asco.ProdOrderNr;
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
                    acomm.SO_Nr = asco.SO_Nr;
                    acomm.SO_Sq = asco.SO_Sq;
                    acomm.SO_Ln = asco.SO_Ln;
                    acomm.ProdOrderNr = asco.ProdOrderNr;
                    db.Assembly_Commitment.Add(acomm);
                    post = true;
                }

                var sale_order = db.Sales_Orders.Where(s => s.SO_Nr == asco.SO_Nr && s.SO_Ln == asco.SO_Ln &&
                                                               s.SO_Sq == asco.SO_Sq && s.ProdOrderNr == asco.ProdOrderNr).FirstOrDefault();

                if (sale_order.HasChangedPRD == true)
                {
                    sale_order.HasChangedPRD = false;
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
