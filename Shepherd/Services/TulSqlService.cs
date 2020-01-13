using Shepherd.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Shepherd.Services
{
    public class TulSqlService
    {
        string connectionString = "Server=tulbaansql;Initial Catalog=baanlivedb;User ID=xlsmty;Password=read";
        string queryString = "";

        public List<Errors> getMultipleStartOperations(string locationSelected, string company = null)
        {
            queryString = "exec [dbo].[rmmMultipleStartOperations]";
            try
            {
                List<Errors> info = new List<Errors>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cm = new SqlCommand(queryString, conn);
                    conn.Open();
                    SqlDataReader rd = cm.ExecuteReader();
                    Errors fm;

                    while (rd.Read())
                    {
                        fm = new Errors();
                        fm.Company = rd.GetValue(0).ToString();
                        fm.Order = rd.GetValue(1).ToString();
                        fm.Item = rd.GetValue(2).ToString();
                        fm.Description = rd.GetValue(3).ToString();
                        if (locationSelected == fm.Company)
                        {
                            if (locationSelected == "105")
                            {
                                if ((fm.Order.IndexOf("11", 0, 2) != -1 || fm.Order.IndexOf("21", 0, 2) != -1))
                                {
                                    if (!startWith1192(fm.Order) && !startWith21(fm.Order) && company == "MTY")
                                        info.Add(fm);
                                }

                                if (isRPIorder(fm.Order) && company == "RPI")
                                    info.Add(fm);

                                if (isFRPorder(fm.Order) && company == "FRP")
                                    info.Add(fm);
                            }
                            else
                            {
                                if (!startWith1192(fm.Order) && !startWith21(fm.Order))
                                    info.Add(fm);
                            }
                        }
                        else if (locationSelected == "ALL")
                        {
                            if (!startWith1192(fm.Order) && !startWith21(fm.Order))
                                info.Add(fm);
                        }
                    }
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Errors> getOperationLessOrders(string locationSelected, string company = null)
        {
            queryString = "exec [dbo].[rmmOperationLessOrders]";
            try
            {
                List<Errors> info = new List<Errors>();

                using(SqlConnection conn = new SqlConnection(connectionString))
	            {
		            SqlCommand cm = new SqlCommand(queryString, conn);
                    conn.Open();
                    SqlDataReader rd = cm.ExecuteReader();
                    Errors fm;

                    while (rd.Read())
	                {
                        fm = new Errors();
	                    fm.Company = rd.GetValue(0).ToString();
                        fm.Order = rd.GetValue(1).ToString();
                        fm.Item = rd.GetValue(2).ToString();
                        fm.Description = rd.GetValue(3).ToString();
                        if (locationSelected == fm.Company)
                        {
                            if (locationSelected == "105")
                            {
                                if ((fm.Order.IndexOf("11", 0, 2) != -1 || fm.Order.IndexOf("21", 0, 2) != -1))
                                {
                                    if (!startWith1192(fm.Order) && !startWith21(fm.Order) && company == "MTY" )
                                        info.Add(fm);
                                }

                                if (isRPIorder(fm.Order) && company == "RPI")
                                    info.Add(fm);

                                if (isFRPorder(fm.Order) && company == "FRP")
                                    info.Add(fm);
                            }
                            else
                            {
                                if (!startWith1192(fm.Order) && !startWith21(fm.Order))
                                    info.Add(fm);
                            }
                        }
                        else if (locationSelected == "ALL")
                        {
                            if (!startWith1192(fm.Order) && !startWith21(fm.Order))
                                info.Add(fm);
                        }
	                }
	            }

                return info;
            }
            catch
            {
                throw;
            }
        }

        public List<Errors> getSchedulerScrambledSequences(string locationSelected, string company = null)
        {
            queryString = "exec [dbo].[rmmSchedulerScrambledSequences]";
            try
            {
                List<Errors> info = new List<Errors>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cm = new SqlCommand(queryString, conn);
                    conn.Open();
                    SqlDataReader rd = cm.ExecuteReader();
                    Errors fm;

                    while (rd.Read())
                    {
                        fm = new Errors();
                        fm.Type = rd.GetValue(0).ToString();
                        fm.Company = rd.GetValue(1).ToString();
                        fm.OrderProduction  = rd.GetValue(2).ToString();
                        fm.OrderOperation = rd.GetValue(3).ToString();
                        fm.Status = rd.GetValue(4).ToString();
                        if (locationSelected == fm.Company)
                        {
                            if (locationSelected == "105")
                            {
                                if ((fm.OrderProduction.IndexOf("11", 0, 2) != -1 || fm.OrderProduction.IndexOf("21", 0, 2) != -1))
                                {
                                    if (!startWith1192(fm.OrderProduction) && !startWith21(fm.OrderProduction) && company == "MTY")
                                        info.Add(fm);
                                }

                                if (isRPIorder(fm.OrderProduction) && company == "RPI")
                                    info.Add(fm);

                                if (isFRPorder(fm.OrderProduction) && company == "FRP")
                                    info.Add(fm);
                            }
                            else
                            {
                                if (!startWith1192(fm.OrderProduction) && !startWith21(fm.OrderProduction))
                                    info.Add(fm);
                            }
                        }
                        else if (locationSelected == "ALL")
                        {
                            if (!startWith1192(fm.OrderProduction) && !startWith21(fm.OrderProduction))
                                info.Add(fm);
                        }
                    }
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Errors> getTimeLessOperations(string locationSelected, string company = null)
        {
            queryString = "exec [dbo].[rmmTimeLessOperations]";
            try
            {
                List<Errors> info = new List<Errors>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cm = new SqlCommand(queryString, conn);
                    conn.Open();
                    SqlDataReader rd = cm.ExecuteReader();
                    Errors fm;

                    while (rd.Read())
                    {
                        fm = new Errors();
                        fm.Company = rd.GetValue(0).ToString();
                        fm.Order = rd.GetValue(1).ToString();
                        fm.Operation = rd.GetValue(2).ToString();
                        fm.Item = rd.GetValue(3).ToString();
                        fm.Description = rd.GetValue(4).ToString();
                        fm.TaskDescription = rd.GetValue(5).ToString();
                        if (locationSelected == fm.Company && fm.TaskDescription != "Subcontract Machining")
                        {
                            if ( locationSelected == "105")
                            {
                                if ((fm.Order.IndexOf("11", 0, 2) != -1 || fm.Order.IndexOf("21", 0, 2) != -1))
                                {
                                    if (!startWith1192(fm.Order) && !startWith21(fm.Order) && !startWith1112AndOperationOne(fm.Order, fm.Operation) && company == "MTY")
                                        info.Add(fm);
                                }

                                if (isRPIorder(fm.Order) && company == "RPI")
                                    info.Add(fm);

                                if (isFRPorder(fm.Order) && company == "FRP")
                                    info.Add(fm);
                            }
                            else
                            {
                                if (!startWith1192(fm.Order) && !startWith21(fm.Order) && !startWith1112AndOperationOne(fm.Order, fm.Operation))
                                    info.Add(fm);
                            }
                            
                        }
                        else if (locationSelected == "ALL" && fm.TaskDescription != "Subcontract Machining")
                        {
                            if (!startWith1192(fm.Order) && !startWith21(fm.Order) && !startWith1112AndOperationOne(fm.Order, fm.Operation))
                                info.Add(fm);
                        }
                    }
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Return true if order numbers starting with '1192'
        /// </summary>
        /// <param name="order">Order Number</param>
        /// <returns></returns>
        public bool startWith1192(string order)
        {
            if (order.IndexOf("1192", 0, 4) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Return trut if order numbers starting with '12'
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool startWith21(string order) 
        {
            if (order.IndexOf("21", 0, 2) == 0)
                return true;
            else
                return false;
        }

        public bool startWith1112AndOperationOne(string order, string operation) 
        {
            if (order.IndexOf("1112", 0, 4) == 0 && operation == "1")
                return true;
            else
                return false;
        }

        public bool isRPIorder(string order)
        {
            if (order.IndexOf("17", 0, 4) == 0)
                return true;
            else
                return false;
        }

        public bool isFRPorder(string order) 
        {
            if (order.IndexOf("36", 0, 4) == 0)
                return true;
            else
                return false;
        }

    }
}