//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class ViewGetSalesOrderReadyToShip
    {
        public string ProdOrderNr { get; set; }
        public string SO_QtyOpen { get; set; }
        public string SO_Project { get; set; }
        public string SO_CustomerName { get; set; }
        public decimal SO_Ln_OpenAmount { get; set; }
        public System.DateTime MinStartDate { get; set; }
        public System.DateTime MaxDelivDate { get; set; }
        public string SO_Item { get; set; }
        public string Item_Description { get; set; }
        public string SO_PLS_Name { get; set; }
        public string Item_PumpLine { get; set; }
        public string Item_PumpType { get; set; }
        public string Oper_ComplTotal { get; set; }
        public Nullable<System.DateTime> readytoship { get; set; }
        public string BaanProjectNum { get; set; }
        public string PlanName { get; set; }
        public Nullable<int> projplan { get; set; }
        public string BaanCompany { get; set; }
        public System.DateTime SO_Ln_PlnRecDate { get; set; }
    }
}
