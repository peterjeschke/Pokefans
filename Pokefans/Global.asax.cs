﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Pokefans.App_Start;
using Microsoft.Practices.Unity;
using Pokefans.Util;
using Pokefans.Caching;
using System.Configuration;

namespace Pokefans
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private bool cacheInitialized = false;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            UnityConfig.GetConfiguredContainer().RegisterInstance<IBreadcrumbs>(new Breadcrumbs(), new PerRequestLifetimeManager());
            
            if (ConfigurationManager.AppSettings["CachingBackend"].ToLower() == "native")
            {
                UnityConfig.GetConfiguredContainer().RegisterType<Cache, NativeCache>(new PerRequestLifetimeManager());
                cacheInitialized = false;
            }
            else
            {
                UnityConfig.GetConfiguredContainer().RegisterType<Cache, Memcached>(new PerRequestLifetimeManager());
            }

            if (!cacheInitialized)
            {
                UnityConfig.GetConfiguredContainer().Resolve<CacheFill>();
            }
        }
    }
}
