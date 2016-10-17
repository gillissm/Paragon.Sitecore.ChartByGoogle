using System;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Util.Store;
using System.Threading.Tasks;
using System.Threading;
using Paragon.GoogleAnalytics.Repository.Constants;
using Paragon.GoogleAnalytics.Repository.IFactory.Authorization;
using Google.Apis.AnalyticsReporting.v4;

namespace Paragon.GoogleAnalytics.Factory.AuthorizationFactory
{
    public class GoogleAuthorizationFactory : IGoogleAuthorizationFactory
    {

        public string pathJsonService
        {
            get
            {
                return Path.Combine(Sitecore.Configuration.Settings.DataFolder, ConfigurationConstants.JSONServiceValue);
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string pathP12Service
        {
            get
            {
                return Path.Combine(Sitecore.Configuration.Settings.DataFolder, ConfigurationConstants.P12ServiceValue);
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string serviceAccountEmail
        {
            get
            {
                return ConfigurationConstants.AccountEmail;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// https://developers.google.com/api-client-library/dotnet/guide/aaa_oauth#service-account
        /// </summary>
        public ServiceAccountCredential GetServiceAccountAuthorization()
        {
            Sitecore.Diagnostics.Log.Audit("DataFolder: " + Sitecore.Configuration.Settings.DataFolder, this);
            Sitecore.Diagnostics.Log.Audit("p12: " + ConfigurationConstants.P12ServiceValue, this);
            Sitecore.Diagnostics.Log.Audit("json: " + ConfigurationConstants.JSONServiceValue, this);
            Sitecore.Diagnostics.Log.Audit("combined p12: " + this.pathP12Service, this);
            Sitecore.Diagnostics.Log.Audit("combined json: " + this.pathJsonService, this);
            Sitecore.Diagnostics.Log.Audit("email: " + this.serviceAccountEmail, this);

            try
            {
                var certificate = new X509Certificate2(this.pathP12Service, "notasecret", X509KeyStorageFlags.Exportable);

                ServiceAccountCredential credential = new ServiceAccountCredential(
                   new ServiceAccountCredential.Initializer(serviceAccountEmail)
                   {
                       Scopes = new[] { AnalyticsReportingService.Scope.AnalyticsReadonly }
                   }.FromCertificate(certificate));

                return credential;
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("GetServiceAuthorization", ex, this);
                //return "Error: GetServiceAuthorization";
                return null;
            }
        }

        public UserCredential GetUserAuthorization(string clientId, string clientSecret)
        {

            try
            {
                FileDataStore fds = new FileDataStore(Sitecore.Configuration.Settings.DataFolder, true);
                ClientSecrets cs = new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                };


                Task<UserCredential> uc = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    cs,
                    new[] { AnalyticsReportingService.Scope.Analytics },
                                    "ForAggregation",
                                    CancellationToken.None,
                                    fds);

                uc.Wait();
                if (uc.IsCompleted && !uc.IsFaulted)
                {
                    return uc.Result;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("GoogleAuthorizationFactory::GetUserAuthorization", ex, this);
                return null;
            }
        }


        

     



        public string GetAuthorizationToken()
        {
            return this.GetServiceAccountAuthorization().GetAccessTokenForRequestAsync().Result;
        }
        
    }
}
