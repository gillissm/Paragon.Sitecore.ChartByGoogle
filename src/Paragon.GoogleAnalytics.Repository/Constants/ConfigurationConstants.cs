namespace Paragon.GoogleAnalytics.Repository.Constants
{
    public static class ConfigurationConstants
    {
        public static string JSONServiceValue = Sitecore.Configuration.Settings.GetSetting("JsonService");
        public static string P12ServiceValue = Sitecore.Configuration.Settings.GetSetting("P12Service");
        public static string AccountEmail = Sitecore.Configuration.Settings.GetSetting("AccountEMail");
    }
}
