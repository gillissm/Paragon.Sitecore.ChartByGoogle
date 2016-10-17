namespace Paragon.GoogleAnalytics.Repository.Model
{
    public class GoogleReportOptions : IGoogleReportOptions
    {
        /// <summary>
        /// Metrics must be comma seperated list.
        /// ex: ga:sessions,ga:users
        /// https://developers.google.com/analytics/devguides/reporting/core/dimsmets
        /// </summary>
        public string Metrics { get; set; }
        /// <summary>
        /// The unique table ID of the form XXXX, where XXXX is the Analytics view (profile) ID for which the query will retrieve the data.
        /// </summary>
        public string ProfileID { get; set; }

        /// <summary>
        /// Start date for fetching Analytics data.
        /// 
        /// Requests can specify a start date formatted as YYYY-MM-DD, 
        /// or as a relative date (e.g., today, yesterday, or NdaysAgo where N is a positive integer).
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// End date for fetching Analytics data.
        /// 
        /// Request can specify an end date formatted as YYYY-MM-DD,
        /// or as a relative date (e.g., today, yesterday, or NdaysAgo where N is a positive integer).
        /// </summary>
        public string EndDate { get; set; }

        public string Dimensions { get; set; }
        public string Filter { get; set; }
        public string Sort { get; set; }
        public string Segment { get; set; }
        public int MaxResults { get; set; }
    }
}
