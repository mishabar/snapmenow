using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using MongoDB.Driver;
using SMN.Data.Repositories;
using SMN.Services;

namespace SMN.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            // initializing dependency injection  container
            var builder = new ContainerBuilder();

            // registrating all the existing controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            MongoClient mongoClient = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString);
            builder.Register(c => mongoClient.GetServer().GetDatabase(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString.Split('/').Last())).AsSelf();

            builder.Register(c => new ProductsRepository(c.Resolve<MongoDatabase>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new SalesRepository(c.Resolve<MongoDatabase>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new UserSnapsRepository(c.Resolve<MongoDatabase>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new InvoicesRepository(c.Resolve<MongoDatabase>())).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Register(c => new ProductsService(c.Resolve<IProductsRepository>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new SalesService(c.Resolve<ISalesRepository>(), c.Resolve<IUserSnapsRepository>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new CheckoutService(c.Resolve<IUserSnapsRepository>(), c.Resolve<IInvoicesRepository>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new SMN.Services.EmailService()).AsImplementedInterfaces().InstancePerLifetimeScope();

            // build the dependencies
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
