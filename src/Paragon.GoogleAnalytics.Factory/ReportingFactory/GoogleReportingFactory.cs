using Paragon.GoogleAnalytics.Repository.IFactory.Reporting;
using Paragon.GoogleAnalytics.Repository.Model;
using Google.Apis.AnalyticsReporting.v4;
using Google.Apis.AnalyticsReporting.v4.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Analytics.v3;
using Google.Apis.Analytics.v3.Data;

namespace Paragon.GoogleAnalytics.Factory.ReportingFactory
{
    public class GoogleReportingFactory : IGoogleReportingFactory
    {

        public IGoogleReportOptions RequestOptionalValues
        {
            get; set;
        }

        public void SetOptionalValues(string Dimensions = "", string Filter = "", string Sort = "", int maxResults = 100, string profileid = "", string metrics = "")
        {
            RequestOptionalValues = new GoogleReportOptions();
            this.RequestOptionalValues.Dimensions = Dimensions;
            this.RequestOptionalValues.Filter = Filter;
            this.RequestOptionalValues.Sort = Sort;
            this.RequestOptionalValues.MaxResults = maxResults;
            RequestOptionalValues.Metrics = metrics;
            RequestOptionalValues.ProfileID = profileid;
        }

        #region V4 Calls
        public GoogleReportData RetrieveGoogleReport_V4(AnalyticsReportingService service)
        {
            try
            {
                // string pid = RequestOptionalValues.ProfileID.StartsWith("ga:") ? RequestOptionalValues.ProfileID : String.Format("ga:{0}", RequestOptionalValues.ProfileID);

                GetReportsRequest getRptRequest = Create_GetReportRequest();
                if (getRptRequest == null)
                {
                    Sitecore.Diagnostics.Log.Audit("GoogleReportingFactory::RetrieveGoogleReport_V4 -- NO rpt request returned.", this);
                    return null;
                }

                ReportsResource.BatchGetRequest batchRequest = service.Reports.BatchGet(getRptRequest);
                GoogleReportData grd = ProcessResults_v4(batchRequest);

                return grd;
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("GoogleReportingFactory::RetrieveGoogleReport_V4", ex, this);
                return null;
            }
        }

        public GoogleReportData ProcessResults_v4(ReportsResource.BatchGetRequest rptRequest)
        {
            GoogleReportData grd = new GoogleReportData();
            try
            {
                GetReportsResponse rptResponse = rptRequest.Execute();
                if (rptResponse != null && rptResponse.Reports.FirstOrDefault() != null)
                {
                    Report r = rptResponse.Reports.FirstOrDefault();
                    //if headers have not been set do that first
                    if (!grd.ColumnHeaders.Any() && r.ColumnHeader != null)
                    {
                        grd.ColumnHeaders = r.ColumnHeader.Dimensions.ToList();
                    }

                    if (r.Data.Rows != null && r.Data.Rows.Any())
                    {
                        foreach (var row in r.Data.Rows)
                        {
                            grd.DataRows.Add(row.Dimensions.ToList());
                        }
                    }
                    grd.NextPageToken = !string.IsNullOrWhiteSpace(r.NextPageToken) ? r.NextPageToken : "";
                    ////I DON"T THINK THIS IS RIGHT
                    //if (!string.IsNullOrWhiteSpace(r.NextPageToken))
                    //{
                    //    rptResponse = rptRequest.Execute();
                    //}
                    //else
                    //    rptRequest = null;
                }



                return grd;
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("GoogleReportingFactory::ProcessResults_v4", ex, this);
                grd.ErrorMessages = ex.ToString();
                return grd;
            }
        }

        public GetReportsRequest Create_GetReportRequest()
        {
            try
            {
                // Create the DateRange object.
                DateRange dateRange = new DateRange();
                dateRange.StartDate = RequestOptionalValues.StartDate;
                dateRange.EndDate = RequestOptionalValues.EndDate;

                // Create the Metrics object.
                List<Metric> allMetrics = new List<Metric>();
                foreach (var m in RequestOptionalValues.Metrics.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    allMetrics.Add(new Metric { Expression = m.Trim() });

                //sessions.Alias = "sessions";

                //Create the Dimensions object.
                List<Google.Apis.AnalyticsReporting.v4.Data.Dimension> allDimensions = new List<Google.Apis.AnalyticsReporting.v4.Data.Dimension>();
                foreach (var d in RequestOptionalValues.Dimensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    allDimensions.Add(new Google.Apis.AnalyticsReporting.v4.Data.Dimension { Name = d.Trim() });

                // Create the ReportRequest object.
                ReportRequest request = new ReportRequest();
                //request.ViewId = "63307962";//Paragon Consulting
                request.ViewId = RequestOptionalValues.ProfileID.Replace("ga:", ""); //"126011915"; //Coffeehouse All Site Traffic
                request.DateRanges = new List<DateRange>() { dateRange };
                request.Dimensions = allDimensions;
                request.Metrics = allMetrics;

                // Create the GetReportsRequest object.
                GetReportsRequest getReport = new GetReportsRequest();
                getReport.ReportRequests = new List<ReportRequest>() { request };
                return getReport;
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("GoogleReportingFactory::GetReportRequest", ex, this);
                return null;
            }
        }

        public AnalyticsReportingService GetV4Service(ServiceCredential creds, string applicationname = "Report v4")
        {
            if (creds == null)
            {
                Sitecore.Diagnostics.Log.Info("GoogleAuthorizationFactory::GetV4Service-no service credential provided", this);
                return null;// ServiceCall: No Creds";
            }


            try
            {
                AnalyticsReportingService ars = new AnalyticsReportingService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = creds,
                    ApplicationName = applicationname
                });
                return ars;
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("GoogelAuthorizationFactory::GetV4Service", ex, this);
                return null;
            }
        }


        #endregion

        #region V3 Calls
        /// <summary>
        /// You query the Core Reporting API for Google Analytics report data. 
        /// Documentation: https://developers.google.com/analytics/devguides/reporting/core/v3/reference
        /// 
        /// Dimension and metric reference : https://developers.google.com/analytics/devguides/reporting/core/dimsmets
        /// </summary>
        /// <param name="service">Valid Authenticated AnalyticsServicve </param>
        /// <param name="ProfileId">The unique table ID of the form XXXX, where XXXX is the Analytics view (profile) ID for which the query will retrieve the data. </param>
        /// <param name="StartDate">Start date for fetching Analytics data. Requests can specify a start date formatted as YYYY-MM-DD, or as a relative date (e.g., today, yesterday, or NdaysAgo where N is a positive integer). </param>
        /// <param name="EndDate">End date for fetching Analytics data. Request can specify an end date formatted as YYYY-MM-DD, or as a relative date (e.g., today, yesterday, or NdaysAgo where N is a positive integer). </param>
        /// <param name="Metrics">A list of comma-separated metrics, such as ga:sessions,ga:bounces. </param>
        /// <param name="optionalValues">Optional values can be null </param>
        /// <returns></returns>
        public GoogleReportData RetrieveGoogleReport_V3(AnalyticsService service)
        {
            try
            {
                if (RequestOptionalValues != null)
                {
                    string pid = RequestOptionalValues.ProfileID.StartsWith("ga:") ? RequestOptionalValues.ProfileID : String.Format("ga:{0}", RequestOptionalValues.ProfileID);
                    DataResource.GaResource.GetRequest request = service.Data.Ga.Get(pid, RequestOptionalValues.StartDate, RequestOptionalValues.EndDate, RequestOptionalValues.Metrics);


                    if (!string.IsNullOrWhiteSpace(RequestOptionalValues.Dimensions))
                        request.Dimensions = RequestOptionalValues.Dimensions;
                    if (!string.IsNullOrWhiteSpace(RequestOptionalValues.Sort))
                        request.Sort = RequestOptionalValues.Sort;
                    if (!string.IsNullOrWhiteSpace(RequestOptionalValues.Filter))
                        request.Filters = RequestOptionalValues.Filter;

                    request.MaxResults = RequestOptionalValues.MaxResults <= 0 ? 100 : RequestOptionalValues.MaxResults;
                    request.StartIndex = 1;
                    return ProcessResults_V3(request);
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("GoogleReportingFactory::RetrieveGoogleReport_V3", ex, this);
            }
            return null;
        }


        // Just loops though getting all the rows.  
        public GoogleReportData ProcessResults_V3(DataResource.GaResource.GetRequest request)
        {
            GoogleReportData grd = new GoogleReportData();
            try
            {
                GaData result = request.Execute();
                List<IList<string>> allRows = new List<IList<string>>();

                //// Loop through until we arrive at an empty page
                while (result.Rows != null)// && request.StartIndex > request.MaxResults)
                {
                    //Add the rows to the final list
                    allRows.AddRange(result.Rows);

                    // We will know we are on the last page when the next page token is
                    // null.
                    // If this is the case, break.
                    if (result.NextLink == null || request.StartIndex + request.MaxResults >= request.MaxResults)
                    {
                        break;
                    }

                    // Prepare the next page of results             
                    request.StartIndex = request.StartIndex + request.MaxResults;
                    // Execute and process the next page request
                    result = request.Execute();

                }
                GaData allData = result;
                allData.Rows = (List<IList<string>>)allRows;
                grd.MapFromGaData(allData);
                return grd;
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("GoogleReportingFactory::ProcessResults", ex, this);
                grd.ErrorMessages = "error: " + ex.ToString();

                return grd;
            }




        }


        public AnalyticsService GetV3Service(ICredential creds, string applicationname = "Report v3")
        {
            if (creds == null)
            {
                Sitecore.Diagnostics.Log.Info("GoogleAuthorizationFactory::GetV3Service-no service credential provided", this);
                return null;// ServiceCall: No Creds";
            }

            try
            {
                AnalyticsService ars = new AnalyticsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = creds,
                    ApplicationName = applicationname
                });
                return ars;
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("GoogelAuthorizationFactory::GetV3Service", ex, this);
                return null;
            }
        }
        
        #endregion
    }
}
