using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shepherd.Models
{
    public class Constraints
    {
        public string businessUnit { get; set; }
        public string series { get; set; }
        public string mostepid { get; set; }
        public string mfOrdr { get; set; }
        public string OperationCode { get; set; }
        public string PlannedStart { get; set; }
        public string PlannedEnd { get; set; }
        public string SchStart { get; set; }
        public string SchEnd { get; set; }
        public string mfgItemCode { get; set; }
        public string mfgItemDescription { get; set; }
        public string RealDelay { get; set; }
        public string Operation { get; set; }
        public string TaskDescription { get; set; }
        public string Machine { get; set; }
        public string sequenceNum { get; set; }
        public string addtnlDelay { get; set; }
        public string ConstraintType { get; set; }
        public string resourceCode { get; set; }
        public string resourceName { get; set; }
        public string predecessorCode { get; set; }
        public string ComponentCode { get; set; }
        public string ComponentDescription { get; set; }
        public string SchEndWeek { get; set; }
        public string SchdYear { get; set; }
        public string SchdMonth { get; set; }
        public string SchdWeek { get; set; }
        public string totalQty { get; set; }
        public string ConfirmedDelivery { get; set; }
        public string Area { get; set; }
    }
}