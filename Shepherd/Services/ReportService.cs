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
        string connString = "Server=TULSQL;initial catalog=APQP_beta;persist security info=True;user id=ExtranetBeta;password=ateBtenartxE;";
        //
        // GET: /ReportService/
        public ActionResult Index()
        {
            return View();
        }

        public List<Report> getSalesOrderes(string job, bool mrp, string planner, bool plannerEmpty, string locationId = null) 
        {
            
            string queryString = "";
            string ordertype = "";
            string location = "";

            if (job == "ALL")
            {
                job = "";
            }

            if (!mrp)
            {
                ordertype = "PO";
            }

            if (planner == "ALL" || string.IsNullOrEmpty(planner))
            {
                planner = "";
            }

            if (locationId == null)
            {
                locationId = "";
            }
            
            using (SqlConnection con = new SqlConnection(connString))
            {
                var info = new List<Report>();
                
                queryString = "exec [dbo].[GetSalesOrders] @job, @ordertype, @planner, @plannerEmpty, @locationId";
                SqlCommand cm = new SqlCommand(queryString, con);
                cm.CommandTimeout = 999;
                cm.Parameters.AddWithValue("@job", job ?? (object)DBNull.Value);
                cm.Parameters.AddWithValue("@ordertype", ordertype ?? (object)DBNull.Value);
                cm.Parameters.AddWithValue("@planner", planner ?? (object)DBNull.Value);
                cm.Parameters.AddWithValue("@plannerEmpty", plannerEmpty);
                cm.Parameters.AddWithValue("@locationId", locationId ?? (object)DBNull.Value);

                con.Open();
                SqlDataReader reader = cm.ExecuteReader();

                while (reader.Read()) 
                {
                    Report report = new Report();                    
                    report.ProdOrderNr = reader.GetValue(0).ToString();// ProdOrderNr;
                    report.SO_QtyOpen = reader.GetValue(1).ToString(); //SO_QtyOpen;
                    report.SO_Project = reader.GetValue(2).ToString(); //SO_Project;
                    report.SO_CustomerName = reader.GetValue(3).ToString(); //SO_CustomerName;                    
                    report.SO_Item = reader.GetValue(6).ToString();// SO_Item;
                    report.Item_Description = reader.GetValue(7).ToString();// Item_Description;
                    report.SO_PLS_Name = reader.GetValue(8).ToString();//S O_PLS_Name;
                    report.Item_PumpLine = reader.GetValue(9).ToString();// Item_PumpLine;
                    report.Item_PumpType = reader.GetValue(10).ToString();// Item_PumpType;
                    report.Oper_ComplTotal = reader.GetValue(11).ToString();// Oper_ComplTotal;
                    report.ReadyToShip = reader.IsDBNull(12) ? new Nullable<DateTime>() : reader.GetDateTime(12);  // readytoship;
                    report.PlanName = reader.GetValue(13).ToString();// PlanName;
                    report.BaanCompany = reader.GetValue(14).ToString();// BaanCompany;
                    report.SO_Ln_PlnRecDate = reader.GetDateTime(15); //SO_Ln_PlnRecDate;
                    if (reader.GetDateTime(4).Year == 1900) //reader.GetDateTime(4) = MinStartDate.
                    {
                        report.MinStartDate = report.SO_Ln_PlnRecDate.AddDays(-21);
                        report.MaxDelivDate = report.SO_Ln_PlnRecDate.AddDays(-15);
                    }
                    else
                    {
                        report.MinStartDate = reader.GetDateTime(4);
                        report.MaxDelivDate = reader.GetDateTime(5);
                    }
                    report.HasChangedPRD = reader.IsDBNull(16) ? new Nullable<Boolean>() : reader.GetBoolean(16);// HasChangedPRD;                    
                    report.SO_Ln_OpenAmount = reader.GetDouble(17);  //SO_Ln_OpenAmount;
                    report.Location = reader.GetValue(18).ToString();// Location;                  
                    report.OrderType = reader.GetValue(19).ToString();// OrderType;
                    report.projplan = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);// projplan;
                    report.assembly_start = reader.IsDBNull(21) ? new Nullable<DateTime>() : reader.GetDateTime(21);
                    report.assembly_end = reader.IsDBNull(22) ? new Nullable<DateTime>() : reader.GetDateTime(22);
                    report.assembly_days = reader.GetValue(23).ToString();
                    report.ProdOrder_Status = reader.GetValue(24).ToString();
                    report.ProdOrder_QtyOrd = reader.GetValue(25).ToString();
                    report.DaysBuffer = reader.IsDBNull(26) ? 0 : reader.GetInt32(26);
                    report.ProdOrder_PlnDlvDate = reader.GetDateTime(27);
                    report.ProdOrder_ReqDlvDate = reader.IsDBNull(28) ? new Nullable<DateTime>() : reader.GetDateTime(28);

                    report.SO_Ln_OpenAmount_Str = string.Format("{0:C}", report.SO_Ln_OpenAmount);
                    report.SO_Ln_PlnRecDateStr = report.SO_Ln_PlnRecDate.ToString("MM/dd/yyyy");
                    report.MinStartDateStr = report.MinStartDate.ToString("MM/dd/yyyy");
                    report.MaxDelivDateStr = report.MaxDelivDate.ToString("MM/dd/yyyy");
                    report.ProdOrder_PlnDlvDateStr = report.ProdOrder_PlnDlvDate.ToString("MM/dd/yyyy");
                    report.assembly_startstr = report.assembly_start.HasValue ? report.assembly_start.Value.ToString("MM/dd/yyyy") : "<not available>";
                    report.assembly_endstr = report.assembly_end.HasValue ? report.assembly_end.Value.ToString("MM/dd/yyyy") : "<not available>";
                    report.beforeToday = beforeToday(report.assembly_start != null ? report.assembly_start : report.MinStartDate);
                    report.isBetweenMaxDateAndPRD = isBetweenMaxDateAndPRD(report.assembly_end != null ? report.assembly_end : report.MaxDelivDate, report.ReadyToShip);
                    report.isBiggerThanPRD = compareMaxDateAndPRD(report.assembly_end != null ? report.assembly_end : report.MaxDelivDate, report.SO_Ln_PlnRecDate);
                    report.PRDIsOut = PRDIsOut(report.SO_Ln_PlnRecDate);
                    if ( !(report.Location == "EGY" || report.Location == "ARG" || report.Location == "BRA" || report.Location == "CHL" || report.Location == "RUS") && report.ProdOrder_Status != "Closed" )
                    {
                        info.Add(report);      
                    }
                }
                con.Close();
                return info;
            }
        }

        public List<Report> getPlanners(string job = null, string planner = null) 
        {
            string queryString = "";

            if (job == "ALL" || String.IsNullOrEmpty(job))
            {
                job = "";
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                var infoplanners = new List<Report>();
                queryString = "exec [dbo].[getPlanners] @job";
                SqlCommand cm = new SqlCommand(queryString, con);
                cm.CommandTimeout = 999; 
                cm.Parameters.Add("@job", SqlDbType.VarChar);
                cm.Parameters["@job"].Value = job;
                con.Open();
                SqlDataReader reader = cm.ExecuteReader();

                while (reader.Read())
                {
                    Report report = new Report();
                    report.SO_PLS_Name = reader[0].ToString();
                    report.HasPls = reader[1].ToString();
                    infoplanners.Add(report);
                }
                con.Close();
                return infoplanners;
            }
        }

        public List<Location> getLocations() 
        {
            var db = new APQP_betaEntities();  
            return db.Locations.Where(l => l.LocationId == 1 || l.LocationId == 2 || l.LocationId == 3 || l.LocationId == 7  || l.LocationId == 8 || l.LocationId == 9 || l.LocationId == 17 || l.LocationId == 18 || l.LocationId == 12).ToList();
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

        public Boolean beforeToday(DateTime? MinStartDateStr) 
        {
            if (MinStartDateStr < DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean compareMaxDateAndPRD(DateTime? MaxDelivDateStr, DateTime PRDate) 
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

        public Boolean isBetweenMaxDateAndPRD(DateTime? MaxDelivDateStr, DateTime? ReadyToShip)
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
            var db = new APQP_betaEntities();  
            var dbb = db.Assembly_Commitment.Where(c => c.assembly_number == ProdOrderNr).FirstOrDefault();
            return 1;
        }
    }
}
