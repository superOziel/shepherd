using Shepherd.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shepherd.Services
{
    public class ReportService : Controller
    {
        //
        // GET: /ReportService/
        APQP_betaEntities db = new APQP_betaEntities();
        public ActionResult Index()
        {
            return View();
        }

        public List<Report> getSalesOrderes(string job) 
        {
            var salesOrderes = new List<ViewGetSalesOrderReadyToShip>();
            List<Report> info = new List<Report>();
            if (job == "ALL" )
            {
                salesOrderes = db.ViewGetSalesOrderReadyToShips.ToList();
            }
            else
            {
                salesOrderes = db.ViewGetSalesOrderReadyToShips.Where(so => so.Location == job).ToList();
            }
            foreach (ViewGetSalesOrderReadyToShip reader in salesOrderes)
            {
                Report report = new Report();
                report.ProdOrderNr = reader.ProdOrderNr;
                report.SO_QtyOpen = reader.SO_QtyOpen;
                report.SO_Project = reader.SO_Project;
                report.SO_CustomerName = reader.SO_CustomerName;
                report.SO_Ln_OpenAmount = reader.SO_Ln_OpenAmount;
                report.SO_Ln_OpenAmount_Str = string.Format("{0:C}", report.SO_Ln_OpenAmount);
                report.SO_Ln_PlnRecDate = reader.SO_Ln_PlnRecDate;
                report.SO_Ln_PlnRecDateStr = report.SO_Ln_PlnRecDate.ToString("MM/dd/yyyy");
                if (reader.MinStartDate.Year == 1900)
                {
                    report.MinStartDate = report.SO_Ln_PlnRecDate.AddDays(-21);
                    report.MaxDelivDate = report.SO_Ln_PlnRecDate.AddDays(-15);
                }
                else
                {
                    report.MinStartDate = reader.MinStartDate;
                    report.MaxDelivDate = reader.MaxDelivDate;
                }
                report.MinStartDateStr = report.MinStartDate.ToString("MM/dd/yyyy");
                report.MaxDelivDateStr = report.MaxDelivDate.ToString("MM/dd/yyyy");
                report.SO_Item = reader.SO_Item;
                report.Item_Description = reader.Item_Description;
                report.SO_PLS_Name = reader.SO_PLS_Name;
                report.Item_PumpLine = reader.Item_PumpLine;
                report.Item_PumpType = reader.Item_PumpType;
                report.Oper_ComplTotal = reader.Oper_ComplTotal;
                report.ReadyToShip = reader.readytoship;
                report.isBetweenMaxDateAndPRD = isBetweenMaxDateAndPRD(report.MaxDelivDate, report.ReadyToShip);
                report.isBiggerThanPRD = compareMaxDateAndPRD(report.MaxDelivDate, report.SO_Ln_PlnRecDate);
                report.BaanCompany = reader.BaanCompany;
                report.BaanProjectNum = reader.BaanProjectNum;
                report.PlanName = reader.PlanName;
                report.projplan = reader.projplan;
                report.HasChangedPRD = reader.HasChangedPRD;
                report.beforeToday = beforeToday(report.MaxDelivDate);
                report.PRDIsOut = PRDIsOut(report.SO_Ln_PlnRecDate);

                report.SO_Ln = reader.SO_Ln;
                report.SO_Nr = reader.SO_Nr;
                report.SO_Sq = reader.SO_Sq;

                info.Add(report);
            }

            return info;
        }

        public Boolean PRDIsOut(DateTime PRD)
        {
            if (PRD < DateTime.Now)
            {
                return true;
            } else
	        {
                return false;
	        }
        }

        public Boolean beforeToday(DateTime MaxDelivDateStr) 
        {
            if (MaxDelivDateStr < DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean compareMaxDateAndPRD(DateTime MaxDelivDateStr, DateTime PRDate) 
        {
            if ( MaxDelivDateStr >= PRDate )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean isBetweenMaxDateAndPRD(DateTime MaxDelivDateStr, DateTime? ReadyToShip)
        {
            if (ReadyToShip.HasValue) 
            {
                if (MaxDelivDateStr > ReadyToShip)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public int getCommitmentDate(string ProdOrderNr)
        {
            var dbb = db.Assembly_Commitment.Where(c => c.assembly_number == ProdOrderNr).FirstOrDefault();
            return 1;
        }
    }
}
