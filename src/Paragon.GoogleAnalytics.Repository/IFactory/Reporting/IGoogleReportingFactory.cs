using Google.Apis.Analytics.v3;
using Google.Apis.AnalyticsReporting.v4;
using Google.Apis.AnalyticsReporting.v4.Data;
using Google.Apis.Auth.OAuth2;
using Paragon.GoogleAnalytics.Repository.Model;

namespace Paragon.GoogleAnalytics.Repository.IFactory.Reporting
{
    public interface IGoogleReportingFactory
    {
        IGoogleReportOptions RequestOptionalValues { get; set; }

        void SetOptionalValues(string Dimensions = "", string Filter = "", string Sort = "", int maxResults = 100, string profileid = "", string metrics = "");

        #region Google v3 Calls
        /// <summary>
        /// You query the Core Reporting API for Google Analytics report data. 
        /// Documentation: https://developers.google.com/analytics/devguides/reporting/core/v3/reference
        /// 
        /// Dimension and metric reference : https://developers.google.com/analytics/devguides/reporting/core/dimsmets
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        GoogleReportData RetrieveGoogleReport_V3(AnalyticsService service);

        GoogleReportData ProcessResults_V3(DataResource.GaResource.GetRequest request);

        AnalyticsService GetV3Service(ICredential creds, string applicationname = "Report v3");
        #endregion

        #region Google v4 Calls
        GetReportsRequest Create_GetReportRequest();

        GoogleReportData RetrieveGoogleReport_V4(AnalyticsReportingService service);
        GoogleReportData ProcessResults_v4(ReportsResource.BatchGetRequest rptRequest);

        AnalyticsReportingService GetV4Service(ServiceCredential creds, string applicationname = "Report v4");
        #endregion
    }
}
