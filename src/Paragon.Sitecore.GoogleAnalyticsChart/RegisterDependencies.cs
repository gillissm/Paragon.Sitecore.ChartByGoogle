using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Paragon.GoogleAnalytics.Repository.IFactory.Authorization;
using Paragon.GoogleAnalytics.Repository.IFactory.Reporting;
using Paragon.GoogleAnalytics.Factory.AuthorizationFactory;
using Paragon.GoogleAnalytics.Factory.ReportingFactory;

namespace Paragon.Sitecore.GoogleAnalyticsChart
{
    //http://kamsar.net/index.php/2016/08/Dependency-Injection-in-Sitecore-8-2/
    //http://www.sitecorenutsbolts.net/2016/09/17/Habitat-Dependency-Injection-with-Sitecore-8-2/
    public class RegisterDependencies : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {           
            //serviceCollection.AddTransient<IGoogleAuthorizationFactory, GoogleAuthorizationFactory>();
            //serviceCollection.AddTransient<IGoogleReportingFactory, GoogleReportingFactory>();

            serviceCollection.AddTransient<IGoogleAuthorizationFactory, GoogleAuthorizationFactory>();
            serviceCollection.AddTransient<IGoogleReportingFactory, GoogleReportingFactory>();

        }
    }
}