﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shepherd.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class APQP_betaEntities : DbContext
    {
        public APQP_betaEntities()
            : base("name=APQP_betaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Assembly_Commitment> Assembly_Commitment { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<PlanInfo> PlanInfo { get; set; }
        public virtual DbSet<PlanInfo_Jobs> PlanInfo_Jobs { get; set; }
        public virtual DbSet<ViewGetSalesOrderReadyToShip> ViewGetSalesOrderReadyToShip { get; set; }
        public virtual DbSet<Sales_Orders> Sales_Orders { get; set; }
    }
}
