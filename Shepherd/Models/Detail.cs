using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shepherd.Models
{
    public class Detail
    {
        public string Status { get; set; }
        public string OrderType { get; set; }
        public string OrderWrhs { get; set; }
        public string Line { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public string Qty { get; set; }
        public Nullable<DateTime> ReceiptDateOriginal { get; set; }
        public string ReceiptDate { get; set; }
        public string ItemGroup { get; set; }
        public string ProdType { get; set; }
        public string ItemGroupDescription { get; set; }
        public string CatID { get; set; }
        public string Bullet { get; set; }
        public string SelCode { get; set; }
        public string RequestDeliveryDate { get; set; }
        public string PlannedDeliveryDate { get; set; }
    }
}