using log4net;
using SportGoods.Domain.Entities;
using SportGoods.WebUI.App_Start;
using SportGoods.WebUI.Infrastructure.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SportGoods.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Application started.");
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();

            log.Error(ex);
        }
    }
}
    
