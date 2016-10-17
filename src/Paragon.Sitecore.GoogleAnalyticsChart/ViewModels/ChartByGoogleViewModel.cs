using Sitecore.Mvc.Presentation;
using Sitecore.Speak.Components.Models;
using Paragon.Sitecore.GoogleAnalyticsChart.Utilities;


namespace Paragon.Sitecore.GoogleAnalyticsChart.ViewModels
{
    public class ChartByGoogleViewModel : ComponentRenderingModel
    {
        public string GAAuthorizationToken
        {
            get
            {
                return (string)this.Properties["GAAuthorizationToken"];
            }
            set { this.Properties["GAAuthorizationToken"] = value; }
        }


        public override void Initialize(Rendering rendering)
        {
            base.Initialize(rendering);            
            this.GAAuthorizationToken = FactoryUtilities.GetGoogleAuthorizationFactory().GetAuthorizationToken();
        }
    }
}