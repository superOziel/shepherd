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
    
    public partial class PlanInfo
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public Nullable<bool> Locked { get; set; }
        public string LockedBy { get; set; }
        public string LockingModule { get; set; }
        public Nullable<int> CalId { get; set; }
        public Nullable<bool> IsCompassPlan { get; set; }
        public Nullable<int> CurrentVersion { get; set; }
        public Nullable<int> BaselineVer { get; set; }
        public Nullable<int> temp_BLV { get; set; }
        public Nullable<bool> xEngSched { get; set; }
        public string Status { get; set; }
        public Nullable<int> LocationId { get; set; }
        public Nullable<bool> UseHrs { get; set; }
        public Nullable<bool> MultiResponsible { get; set; }
        public Nullable<int> CostTplId { get; set; }
        public Nullable<bool> isShown { get; set; }
        public string activeDirectoryPM { get; set; }
        public Nullable<bool> showDashboard { get; set; }
    }
}
