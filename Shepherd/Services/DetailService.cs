using Newtonsoft.Json;
using Shepherd.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Shepherd.Services
{
    public class DetailService
    {
        string connectionString = "Server=tulbaansql;Initial Catalog=baanlivedb;User ID=xlsmty;Password=read";
        string queryString = "";
        APQP_betaEntities db = new APQP_betaEntities();
        public List<Detail> detailInformation(Report item) 
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    List<Detail> info = new List<Detail>();
                    if (item.OrderType == "PO")
                    {
                        queryString = "exec [dbo].[Get_Pegging_Upstream_by_Company_Production_Order] @BaanCompany, @ProdOrderNr" ;
                    }
                    else if (item.OrderType == "MRP")
                    {
                        queryString = "exec [dbo].[Get_Pegging_Upstream_by_Company_PlannedOrder] @ProdOrderNr, @BaanCompany";
                    }
                    else if (item.OrderType == "SO")
                    {
                        queryString = "exec [dbo].[Get_PeggingbyOrderLineSeqComp_NEW] @ProdOrderNr, @SO_Ln, @SO_Sq, @BaanCompany ";
                    }

                    SqlCommand cm = new SqlCommand(queryString, conn);
                    cm.CommandTimeout = 9999;
                    cm.Parameters.Add("@BaanCompany", SqlDbType.Int);
                    cm.Parameters["@BaanCompany"].Value = item.BaanCompany;

                    if (item.OrderType == "SO")
                    {
                        string[] parameters = item.ProdOrderNr.Split(null);
                        string prodorder = parameters[0];
                        string lin = parameters[1];
                        string sq = parameters[2];


                        cm.Parameters.Add("@ProdOrderNr", SqlDbType.VarChar );
                        cm.Parameters["@ProdOrderNr"].Value = prodorder;

                        cm.Parameters.Add("@SO_Ln", SqlDbType.VarChar);
                        cm.Parameters["@SO_Ln"].Value = lin;

                        cm.Parameters.Add("@SO_Sq", SqlDbType.VarChar);
                        cm.Parameters["@SO_Sq"].Value = sq;
                    }
                    else
                    {
                        cm.Parameters.Add("@ProdOrderNr", SqlDbType.VarChar);
                        cm.Parameters["@ProdOrderNr"].Value = item.ProdOrderNr;
                    }

                    conn.Open();
                    SqlDataReader rd = cm.ExecuteReader();

                    while (rd.Read())
                    {
                        Detail dt = new Detail();

                        if (item.OrderType == "SO")
                        {
                            dt.OrderType = rd.IsDBNull(0) ? null : rd.GetValue(0).ToString();
                            dt.OrderWrhs = rd.IsDBNull(1) ? null : rd.GetValue(1).ToString();
                            dt.Line = rd.IsDBNull(2) ? null : rd.GetValue(2).ToString();
                            //db.cluster = "3";
                            dt.Item = rd.IsDBNull(4) ? null : rd.GetValue(4).ToString();
                            dt.Description = rd.IsDBNull(5) ? null : rd.GetValue(5).ToString();
                            dt.Qty = rd.IsDBNull(6) ? null : rd.GetValue(6).ToString();
                            dt.ReceiptDate = rd.IsDBNull(21) ? null : rd.GetDateTime(21).ToString("MM/dd/yyyy");
                            dt.ReceiptDateOriginal = rd.IsDBNull(21) ? new Nullable<DateTime>() : rd.GetDateTime(21);
                            dt.ItemGroup = rd.IsDBNull(8) ? null : rd.GetValue(8).ToString();
                            dt.ProdType = rd.IsDBNull(9) ? null : rd.GetValue(9).ToString();
                            dt.ItemGroupDescription = rd.IsDBNull(10) ? null : rd.GetValue(10).ToString();
                            //dt.koor = rd.GetValue(11);
                            //dt.level = rd.GetValue(12);
                            //dt.kotr = rd.GetValue(13);
                            //db.kotrDesc = rd.GetValue(14);
                            dt.CatID = rd.IsDBNull(15) ? null : rd.GetValue(15).ToString();
                            //dt.itemtype = rd.GetValue(6);
                            dt.SelCode = rd.IsDBNull(17) ? null : rd.GetValue(17).ToString();
                            dt.Status = rd.IsDBNull(18) ? null : rd.GetValue(18).ToString();
                            dt.RequestDeliveryDate = rd.IsDBNull(19) ? null : rd.GetDateTime(19).ToString("MM/dd/yyyy");
                            dt.PlannedDeliveryDate = rd.IsDBNull(20) ? null : rd.GetDateTime(20).ToString("MM/dd/yyyy");
                            dt.Bullet = "*";
                        }
                        else 
                        {
                            dt.Status = rd.IsDBNull(0) ? null : rd.GetValue(0).ToString();
                            dt.OrderType = rd.IsDBNull(1) ? null : rd.GetValue(1).ToString();
                            dt.OrderWrhs = rd.IsDBNull(2) ? null : rd.GetValue(2).ToString();
                            dt.Line = rd.IsDBNull(3) ? null : rd.GetValue(3).ToString();
                            dt.Item = rd.IsDBNull(4) ? null : rd.GetValue(4).ToString();
                            dt.Description = rd.IsDBNull(5) ? null : rd.GetValue(5).ToString();
                            dt.Qty = rd.IsDBNull(6) ? null : rd.GetValue(6).ToString();
                            dt.ReceiptDateOriginal = rd.IsDBNull(15) ? new Nullable<DateTime>() : rd.GetDateTime(15);
                            dt.ReceiptDate = rd.IsDBNull(15) ? null : rd.GetValue(15).ToString();
                            dt.ItemGroup = rd.IsDBNull(8) ? null : rd.GetValue(8).ToString();
                            dt.ProdType = rd.IsDBNull(9) ? null : rd.GetValue(9).ToString();
                            dt.ItemGroupDescription = rd.IsDBNull(10) ? null : rd.GetValue(10).ToString();
                            dt.CatID = rd.IsDBNull(11) ? null : rd.GetValue(11).ToString();
                            dt.SelCode = rd.IsDBNull(12) ? null : rd.GetValue(12).ToString();
                            dt.RequestDeliveryDate = rd.IsDBNull(13) ? null : rd.GetDateTime(13).ToString("MM/dd/yyyy");
                            dt.PlannedDeliveryDate = rd.IsDBNull(14) ? null : rd.GetDateTime(14).ToString("MM/dd/yyyy");
                            dt.Bullet = "*";
                        }
                        
                        info.Add(dt);
                    }


                    return info.OrderByDescending(x => x.ReceiptDateOriginal).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Buy> getBuyConstraint(string order, string line, string location) 
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    List<Buy> info = new List<Buy>();
                    queryString = "exec [dbo].[ocgTimPlan_Get_PO_Extra_Information] @order, @line, @location";

                    SqlCommand cm = new SqlCommand(queryString, conn);
                    cm.CommandTimeout = 9999;
                    cm.Parameters.Add("@order", SqlDbType.Int);
                    cm.Parameters["@order"].Value = order;

                    cm.Parameters.Add("@line", SqlDbType.Int);
                    cm.Parameters["@line"].Value = line;

                    cm.Parameters.Add("@location", SqlDbType.Int);
                    cm.Parameters["@location"].Value = location;

                    conn.Open();
                    SqlDataReader rd = cm.ExecuteReader();

                    while (rd.Read())
                    {
                        Buy dt = new Buy();
                        dt.Order_Nr = rd.IsDBNull(0) ? null : rd.GetValue(0).ToString();
                        dt.Order_Date = rd.IsDBNull(1) ? null : rd.GetValue(1).ToString();
                        dt.BP_Code = rd.IsDBNull(2) ? null : rd.GetValue(2).ToString();
                        dt.BP_Name = rd.IsDBNull(3) ? null : rd.GetValue(3).ToString();
                        dt.Buyer_Code = rd.IsDBNull(4) ? null : rd.GetValue(4).ToString();
                        dt.Buyer_Name = rd.IsDBNull(5) ? null : rd.GetValue(5).ToString();
                        dt.LastConfirmationDate_Update = rd.IsDBNull(6) ? null : rd.GetValue(6).ToString();
                        dt.Other_Supplier = rd.IsDBNull(7) ? null : rd.GetValue(7).ToString();
                        dt.Other_Supplier_Name = rd.IsDBNull(8) ? null : rd.GetValue(8).ToString();

                        info.Add(dt);
                    }


                    return info;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Get the Task Information for this proyect plan
        /// </summary>
        /// <param name="projplan"></param>
        /// <returns></returns>
        public List<Task> getTasks(int projplan) 
        {
            return db.Tasks.Where(t => t.PlanId == projplan).ToList();
        }
       
        public List<TaskCategory> getTasksCat()
        {
            return db.TaskCategories.ToList();
        }
    }
}