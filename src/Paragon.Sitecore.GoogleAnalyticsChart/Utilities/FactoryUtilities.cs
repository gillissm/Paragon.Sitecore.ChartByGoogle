using Paragon.GoogleAnalytics.Repository.IFactory.Reporting;
using Paragon.GoogleAnalytics.Repository.IFactory.Authorization;
using Sitecore.DependencyInjection;

namespace Paragon.Sitecore.GoogleAnalyticsChart.Utilities
{
    public static class FactoryUtilities
    {
        /// <summary>
        /// creates and returns a ContactFactory object base don the configuration "googlefactory/authorizationfactory"
        /// </summary>        
        /// <returns></returns>
        public static IGoogleAuthorizationFactory GetGoogleAuthorizationFactory()
        {
            return ServiceLocator.ServiceProvider.GetService((typeof(IGoogleAuthorizationFactory))) as IGoogleAuthorizationFactory;
            //return Sitecore.Configuration.Factory.CreateObject("googlefactory/authorizationfactory", true) as IGoogleAuthorizationFactory;
        }


        /// <summary>
        /// creates and returns a ContactFactory object base don the configuration "googlefactory/reportingfactory"
        /// </summary>        
        /// <returns></returns>
        public static IGoogleReportingFactory GetGoogleReportingFactory()
        {
            return ServiceLocator.ServiceProvider.GetService((typeof(IGoogleReportingFactory))) as IGoogleReportingFactory;
            //return GET FROM DI Container
            //return Sitecore.Configuration.Factory.CreateObject("googlefactory/reportingfactory", true) as IGoogleReportingFactory;
        }
    }
    
}