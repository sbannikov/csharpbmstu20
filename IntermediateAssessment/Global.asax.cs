using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;

namespace IntermediateAssessment
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Протоколирование
        /// </summary>
        private Logger log = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Error += MvcApplication_Error;
        }

        /// <summary>
        /// Обработка ошибок уровня приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MvcApplication_Error(object sender, EventArgs e)
        {
            log.Error(Server.GetLastError());
        }
    }
}
