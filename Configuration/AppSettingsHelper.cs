
using System.Configuration;

namespace Configuration
{
    public class AppSettingsHelper 
    {
        
        private static string DB_CN_STRING = "Data.ConnectionString";
        
        public string getConnectionString() {
            return ConfigurationManager.AppSettings.Get(DB_CN_STRING);
        }
    }
}