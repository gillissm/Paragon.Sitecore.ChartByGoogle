namespace Paragon.GoogleAnalytics.Repository.Model
{
    public interface IGoogleReportOptions
    {

        /// <summary>
        /// Metrics must be comma seperated list.
        /// ex: ga:sessions,ga:users
        /// https://developers.google.com/analytics/devguides/reporting/core/dimsmets
        /// </summary>
        string Metrics { get; set; }
        /// <summary>
        /// The unique table ID of the form XXXX, where XXXX is the Analytics view (profile) ID for which the query will retrieve the data.
        /// </summary>
        string ProfileID { get; set; }

        /// <summary>
        /// Start date for fetching Analytics data.
        /// 
        /// Requests can specify a start date formatted as YYYY-MM-DD, 
        /// or as a relative date (e.g., today, yesterday, or NdaysAgo where N is a positive integer).
        /// </summary>
        string StartDate { get; set; }
        /// <summary>
        /// End date for fetching Analytics data.
        /// 
        /// Request can specify an end date formatted as YYYY-MM-DD,
        /// or as a relative date (e.g., today, yesterday, or NdaysAgo where N is a positive integer).
        /// </summary>
        string EndDate { get; set; }
        /// <summary>
        /// A list of comma-separated dimensions for your Analytics data, such as ga:browser,ga:city. 
        /// Documentation: https://developers.google.com/analytics/devguides/reporting/core/v3/reference#dimensions
        /// </summary>            
        string Dimensions { get; set; }

        /// <summary>
        /// Dimension or metric filters that restrict the data returned for your request. 
        /// Documentation: https://developers.google.com/analytics/devguides/reporting/core/v3/reference#filters
        /// </summary>
        string Filter { get; set; }

        /// <summary>
        /// A list of comma-separated dimensions and metrics indicating the sorting order and sorting direction for the returned data. 
        /// Documentation:  https://developers.google.com/analytics/devguides/reporting/core/v3/reference#sort
        /// </summary>
        string Sort { get; set; }

        /// <summary>
        /// The maximum number of rows to include in the response. max is 10000
        /// </summary>
        int MaxResults { get; set; }
        
    }
}
