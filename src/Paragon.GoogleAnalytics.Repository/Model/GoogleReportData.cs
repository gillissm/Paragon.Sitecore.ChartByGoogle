using Google.Apis.Analytics.v3.Data;
using System.Collections.Generic;
using System.Linq;

namespace Paragon.GoogleAnalytics.Repository.Model
{
    public class GoogleReportData
    {

        public string AdditionalMessages { get; set; }
        public string ErrorMessages { get; set; }
        public List<string> ColumnHeaders { get; set; }
        public List<IList<string>> DataRows { get; set; }
        public string NextPageToken { get; set; }
        public string AccountId { get; set; }
        public string ProfileId { get; set; }
        public string ProfileName { get; set; }

        public GoogleReportData()
        {
            ColumnHeaders = new List<string>();
            DataRows = new List<IList<string>>();
        }

        public void MapFromGaData(GaData allData)
        {
            if (allData.ColumnHeaders != null)
            {
                foreach (var c in allData.ColumnHeaders)
                    ColumnHeaders.Add(c.Name);
            }

            if (allData.Rows != null)
                DataRows.AddRange(allData.Rows.ToList());

            AccountId = allData.ProfileInfo.AccountId;
            ProfileId = allData.ProfileInfo.ProfileId;
            ProfileName = allData.ProfileInfo.ProfileName;
        }
    }

}
