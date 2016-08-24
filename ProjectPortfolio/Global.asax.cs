using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity.Infrastructure.Interception;
using ProjectPortfolio.DAL;
using ProjectPortfolio.Migrations;
using System.Data.Entity.Migrations;


namespace ProjectPortfolio
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DbInterception.Add(new PortfolioInterceptorLogging());
            DbInterception.Add(new PortfolioInterceptorTransientErrors());

            // CODE FIRST MIGRATIONS - Forcing seed without database changes while deployed
            //#if !DEBUG
            //       var migrator = new DbMigrator(new Configuration());
            //       migrator.Update();
            //#endif

        }
    }
}
