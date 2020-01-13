using Shepherd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shepherd.Services
{
    public class CommitmentService
    {
        APQP_betaEntities db = new APQP_betaEntities();

        public Assembly_Commitment getCommitment(Report item) 
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var commitment = db.Assembly_Commitment.Where(s => s.ProdOrderNr == item.ProdOrderNr && s.active == true).FirstOrDefault();
                return commitment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ViewGetSalesOrderReadyToShip getSaleOrder(Report item) 
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var saleorder = db.ViewGetSalesOrderReadyToShips.Where(v =>
                                                        v.ProdOrderNr == item.ProdOrderNr).FirstOrDefault();

                return saleorder;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}