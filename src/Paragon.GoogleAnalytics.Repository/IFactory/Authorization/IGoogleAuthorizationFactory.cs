
using Google.Apis.Auth.OAuth2;

namespace Paragon.GoogleAnalytics.Repository.IFactory.Authorization
{
    public interface IGoogleAuthorizationFactory
    {

        #region Properties
        string pathJsonService { get; set; }
        string pathP12Service { get; set; }
        string serviceAccountEmail { get; set; }
        #endregion


        #region Methods
        ServiceAccountCredential GetServiceAccountAuthorization();
        UserCredential GetUserAuthorization(string clientId, string clientSecret);
        
       
        string GetAuthorizationToken();                
        #endregion
    }
}
