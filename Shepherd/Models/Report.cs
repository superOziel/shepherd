using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shepherd.Models
{
    public class Report
    {
        public string SO_Nr { get; set; } //0
        public string SO_Ln { get; set; } //1
        public string SO_Sq { get; set; } //2
        public string SO_CustomerCode { get; set; } //3
        public string SO_CustomerName { get; set; } //4
        public string SO_Project { get; set; } //5
        public string SO_Item { get; set; } //6

        public string Item_Description { get; set; } //7
        public string Item_PumpLine { get; set; } //8
        public string Item_PumpType { get; set; } //9

        public string SO_Date { get; set; } //10
        public string SO_Ln_PlnDlvDate { get; set; } //11
        public DateTime SO_Ln_PlnRecDate { get; set; } //12
        public string SO_Ln_OrigExpShpDate { get; set; } //13
        public string SO_Ln_ExpShpDate { get; set; } //14
        public string SO_Ln_ExpRevDate { get; set; } //15
        public string SO_QtyOrd { get; set; } //16
        public string SO_QtyDlv { get; set; } //17
        public string SO_QtyOpen { get; set; } //18
        public string SO_Unit { get; set; } //19
        public string SO_Price { get; set; } //20
        public Decimal SO_Ln_OpenAmount { get; set; } //21
        public string SO_Ln_OpenAmount_Str { get; set; }
        public string SO_Currency { get; set; } //22
        public string SO_CustomerPO { get; set; } //23
        public string SO_CustomerPO_Date { get; set; } //24
        public string SO_PM_Name { get; set; } //25
        public string SO_PLS_Name { get; set; } //26
        public string SO_GPS_Quote { get; set; } //27
        public string SO_Reference_A { get; set; } //28
        public string SO_Reference_B { get; set; } //29

        public string ProdOrderNr { get; set; } //30
        public string ProdOrder_Status { get; set; } //31
        public string ProdOrder_Route { get; set; } //32
        public string ProdOrder_PlnStrDate { get; set; } //33
        public string ProdOrder_PlnDlvDate { get; set; } //34
        public string ProdOrder_ConfDate { get; set; } //35
        public string ProdOrder_QtyOrd { get; set; } //36
        public string ProdOrder_QtyDlv { get; set; } //37
        public string ProdOrder_QtyRjc { get; set; } //38
        public string ProdOrder_QtyBal { get; set; } //39

        public string Oper_ComplTotal { get; set; } //40
        public string FirstInProcessOperationMachine { get; set; } //41
        public string FirstInProcessOperationMachineName { get; set; } //42
        public string FirstInProcessOperation { get; set; } //43
        public DateTime MinStartDate { get; set; } //44
        public string MinStartDateStr { get; set; }
        public DateTime MaxDelivDate { get; set; } //45
        public string MaxDelivDateStr { get; set; } //46
        public string SO_Ln_PlnRecDateStr { get; set; } //47

        public Boolean PRDDate { get; set; }//48
        public Boolean isBiggerThanPRD { get; set; }//49
        public Nullable<System.DateTime> ReadyToShip { get; set; }//50
        public Boolean isBetweenMaxDateAndPRD { get; set; }//51
        public string BaanCompany { get; set; }
        public string BaanProjectNum { get; set; }
        public string PlanName { get; set; }
        public int? projplan { get; set; }
    }
}