using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shepherd.Models
{
    public class Buy: Detail
    {
        public string Order_Nr { get; set; }
        public string Order_Date { get; set; }
        public string BP_Code { get; set; }
        public string BP_Name { get; set; }
        public string Buyer_Code { get; set; }
        public string Buyer_Name { get; set; }
        public string LastConfirmationDate_Update { get; set; }
        public string Other_Supplier { get; set; }
        public string Other_Supplier_Name { get; set; }

    }
}