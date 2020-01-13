using Shepherd.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;
using System.Net.Mail;

namespace Shepherd.Services
{
    public class RPSAService
    {
        string connectionString = "";
        string queryString = "";
        APQP_betaEntities db = new APQP_betaEntities();

        public List<Constraints> getConstraint(string order, string location)
        {
            queryString = "SELECT * FROM APQP_beta.dbo.Orders_Constraints WHERE mfOrdr = @order and addtnlDelay > '0.00'";
            List<Constraints> info = new List<Constraints>();

            connectionString = "Server=tulsql;Initial Catalog=APQP_beta;User ID=ExtranetBeta;Password=ateBtenartxE";
            //connectionString = whereToFind(location);
            //queryString = "exec [dbo].[rmmSchConstraint] @order";


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cm = new SqlCommand(queryString, conn);
                    cm.Parameters.Add("@order", SqlDbType.VarChar);
                    cm.Parameters["@order"].Value = order;
                    conn.Open();
                    SqlDataReader rd = cm.ExecuteReader();
                    Constraints fm;

                    while (rd.Read())
                    {
                        fm = new Constraints();
                        fm.businessUnit = rd.GetValue(2).ToString();
                        fm.series = rd.GetValue(3).ToString();
                        fm.mostepid = rd.GetValue(4).ToString();
                        fm.mfOrdr = rd.GetValue(5).ToString();
                        fm.OperationCode = rd.GetValue(6).ToString();
                        fm.PlannedStart = rd.GetValue(7).ToString();
                        fm.PlannedEnd = rd.GetValue(8).ToString();
                        fm.SchStart = rd.GetValue(9).ToString();
                        fm.SchEnd = rd.GetValue(10).ToString();
                        fm.mfgItemCode = rd.GetValue(11).ToString();
                        fm.mfgItemDescription = rd.GetValue(12).ToString();
                        fm.RealDelay = rd.GetValue(13).ToString();
                        fm.Operation = rd.GetValue(14).ToString();
                        fm.TaskDescription = rd.GetValue(15).ToString();
                        fm.Machine = rd.GetValue(16).ToString();
                        fm.sequenceNum = rd.GetValue(17).ToString();
                        fm.addtnlDelay = rd.GetValue(18).ToString();
                        fm.ConstraintType = rd.GetValue(19).ToString();
                        fm.resourceCode = rd.GetValue(20).ToString();
                        fm.resourceName = rd.GetValue(21).ToString();//what!
                        fm.predecessorCode = rd.GetValue(22).ToString();//int?
                        fm.ComponentCode = rd.GetValue(23).ToString();//int!
                        fm.ComponentDescription = rd.GetValue(24).ToString();
                        fm.SchEndWeek = rd.GetValue(25).ToString();
                        fm.SchdYear = rd.GetValue(26).ToString();
                        fm.SchdMonth = rd.GetValue(27).ToString();
                        fm.SchdWeek = rd.GetValue(28).ToString();
                        fm.totalQty = rd.GetValue(29).ToString();
                        fm.ConfirmedDelivery = rd.GetValue(30).ToString();
                        fm.Area = rd.GetValue(31).ToString();//no regresa nada ;(
                        info.Add(fm);
                    }
                }
                return info;
            }
            catch (Exception)
            {
                
                throw;
            }           
        }

        public string whereToFind(string location) 
        {
            return ConfigurationManager.AppSettings[location];
        }
    }
}