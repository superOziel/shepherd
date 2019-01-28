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
                salesOrderes = db.ViewGetSalesOrderReadyToShip.ToList();
            }
            else
            {
                salesOrderes = db.ViewGetSalesOrderReadyToShip.Where(so => so.BaanCompany == job).ToList();
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
                report.isBetweenMaxDateAndPRD = isBetweenMaxDateAndPRD(report.SO_Ln_PlnRecDate, report.SO_Ln_PlnRecDate, report.ReadyToShip);
                report.isBiggerThanPRD = compareMaxDateAndPRD(report.SO_Ln_PlnRecDate, report.SO_Ln_PlnRecDate);
                report.BaanCompany = reader.BaanCompany;
                report.BaanProjectNum = reader.BaanProjectNum;
                report.PlanName = reader.PlanName;
                report.projplan = reader.projplan;
                
                info.Add(report);
            }

            return info;
        }        

        public List<Report> getSalesOrders() 
        {
            string queryString = "exec [dbo].[ocgTimPlan_SalesOrders_Backlog_RPSA]";
            string connectionString = "Server=tulbaansql;Initial Catalog=baanlivedb;User ID=xlsmty;Password=read";
            DataSet tabla = new DataSet();
            List<Report> info = new List<Report>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Report rp;

                while (reader.Read())
                {   
                    rp = new Report();
                    rp.ProdOrderNr = reader.GetValue(30).ToString();
                    rp.SO_QtyOpen = reader.GetValue(18).ToString();
                    rp.SO_Project = reader.GetValue(5).ToString();
                    rp.SO_CustomerName = reader.GetValue(4).ToString();
                    rp.SO_Ln_OpenAmount = reader.GetDecimal(21);
                    rp.SO_Ln_OpenAmount_Str = string.Format("{0:C}", rp.SO_Ln_OpenAmount);
                    rp.SO_Ln_PlnRecDate = reader.GetDateTime(12);
                    rp.SO_Ln_PlnRecDateStr = rp.SO_Ln_PlnRecDate.ToString("MM/dd/yyyy");
                    if (reader.GetDateTime(44).Year == 1900)
                    {
                        rp.MinStartDate = rp.SO_Ln_PlnRecDate.AddDays(-21);
                        rp.MaxDelivDate = rp.SO_Ln_PlnRecDate.AddDays(-15);
                    }
                    else
                    {
                        rp.MinStartDate = reader.GetDateTime(44);
                        rp.MaxDelivDate = reader.GetDateTime(45);
                    }
                    rp.MinStartDateStr = rp.MinStartDate.ToString("MM/dd/yyyy");
                    rp.MaxDelivDateStr = rp.MaxDelivDate.ToString("MM/dd/yyyy");
                    rp.SO_Item = reader.GetValue(6).ToString();
                    rp.Item_Description = reader.GetValue(7).ToString();
                    rp.SO_PLS_Name = reader.GetValue(26).ToString();
                    rp.Item_PumpLine = reader.GetValue(8).ToString();
                    rp.Item_PumpType = reader.GetValue(9).ToString();
                    rp.Oper_ComplTotal = reader.GetValue(40).ToString();

                    var ready = db.Assembly_Commitment.Where(c => c.assembly_number == rp.ProdOrderNr).FirstOrDefault();
                    rp.ReadyToShip = ready != null ? ready.readytoship : null;
                    rp.isBetweenMaxDateAndPRD = isBetweenMaxDateAndPRD(rp.SO_Ln_PlnRecDate, rp.SO_Ln_PlnRecDate, rp.ReadyToShip);
                    rp.isBiggerThanPRD = compareMaxDateAndPRD(rp.SO_Ln_PlnRecDate, rp.SO_Ln_PlnRecDate);
                    info.Add(rp);
                }              
            }

            return info;
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

        public Boolean isBetweenMaxDateAndPRD(DateTime MaxDelivDateStr, DateTime PRDate, DateTime? ReadyToShip)
        {
            if (ReadyToShip.HasValue) 
            {
                if (ReadyToShip > MaxDelivDateStr && MaxDelivDateStr < PRDate)
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
