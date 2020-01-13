using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shepherd.Models
{
    public class Locations
    {
        public string TUL { get; set; }
        public string MTY { get; set; }
        public string WIT { get; set; }
        public string EGY { get; set; }
        public string ARG { get; set; }
        public string BRA { get; set; }
        public string IND { get; set; }
        public string FRP { get; set; }
        public string UK { get; set; }
        public string RPI { get; set; }
        public string DCK { get; set; }
        public string CHN { get; set; }

        public Locations() 
        {
            TUL = "Tulsa";
            MTY = "RPSA";
            WIT = "Witten";
            ARG = "";
            BRA = "Brazil";
            IND = "India";
            FRP = "Metals";
            UK  = "UK";
            RPI = "RPI";
            DCK = "Witten";
            CHN = "China";
        }
    }
}