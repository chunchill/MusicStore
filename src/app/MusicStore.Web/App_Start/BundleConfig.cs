using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MusicStore.Web.App_Start
{
   public class BundleConfig
   {
      public static void RegisterBundles(BundleCollection bundles)
      {
         //Modernizr loads first
         bundles.Add(new ScriptBundle("~/bundles/modernizr")
            .Include("~/Scripts/lib/common/modernizr-{version}.min.js"));

         //jQuery
         bundles.Add(new ScriptBundle("~/bundles/jquery")
            .Include("~/Scripts/lib/jquery/jquery-{version}.js"));
         //3rd Party JS Files

         // All application JS files

         //3rd Party css file

         //Custom LESS files


      }
   }
}