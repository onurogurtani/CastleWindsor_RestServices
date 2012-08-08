using System;
using System.Web;
using Castle.Core;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CastleWindsor_RestServices.Aspects;
using Rest.Service.Contract;
using Rest.Services;

namespace CastleWindsor_RestServices
{
    public class Global : HttpApplication
    {
        private IWindsorContainer _container;
        public IWindsorContainer Container { get; private set; }

        protected void Application_Start(object sender, EventArgs e)
        {
            BootCastle();
        }

        private void BootCastle()
        {
            _container = new WindsorContainer();

            _container.AddFacility<WcfFacility>()
                .Register
                (
                    Component.For<Logging>().LifestyleTransient(),
                    Component.For<Performance>().LifestyleTransient(),
                    Component.For<Authorized>().LifestyleTransient(),
                    Component.For<ExceptionHandling>().LifestyleTransient(),
                    Component.For<IDemoService>().ImplementedBy<DemoService>().Named("DemoService").
                        LifestyleTransient()
                        .Interceptors(InterceptorReference.ForType<ExceptionHandling>()).First
                        .Interceptors(InterceptorReference.ForType<Performance>()).First
                        .Interceptors(InterceptorReference.ForType<Logging>()).First
                        .Interceptors(InterceptorReference.ForType<Authorized>()).First
                );
        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (_container != null)
            {
                _container.Dispose();
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST,PUT,DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers",
                                                       "Content-Type, Accept, x-requested-with");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }
    }
}