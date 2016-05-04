
using System.Configuration;

namespace BalanceApi.Configuration
{
    public class AppSettingsHelper 
    {
        
        private static string DB_CN_STRING = "Data:AccountDb:ConnectionString";
        
        public string getConnectionString() {
            return ConfigurationManager.AppSettings.Get(DB_CN_STRING);
        }
    }
}