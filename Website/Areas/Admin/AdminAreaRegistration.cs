﻿using System.Web.Mvc;
using NuGetGallery.Areas.Admin.DynamicData;
using Ninject;

namespace NuGetGallery.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            var config = Container.Kernel.Get<IConfiguration>();

            context.Routes.Ignore("Admin/Errors.axd/{*pathInfo}"); // ELMAH owns this root
            context.Routes.Ignore("Admin/Glimpse/{*pathInfo}"); // Glimpse owns this root
            DynamicDataManager.Register(context.Routes, "Admin/Database", config);

            context.MapRoute(
                "Admin_import",
                "Admin/Import/{action}/{id}",
                new { controller = "Import", action = "Search", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
