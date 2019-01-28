using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Shepherd.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles) 
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-3.3.1.js"));
        }
    }
}