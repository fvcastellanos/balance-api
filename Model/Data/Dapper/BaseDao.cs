using System;
using System.Data;
using MySql.Data.MySqlClient;

using BalanceApi.Configuration;

namespace BalanceApi.Model.Data.Dapper
{
    public abstract class BaseDao
    {
        protected AppSettingsHelper settingsHelper;

        protected BaseDao(AppSettingsHelper settingsHelper)
        {
            this.settingsHelper = settingsHelper;
        }
        
        protected IDbConnection getConnection() 
        {
            try
            {
                return new MySqlConnection("Server=localhost;Database=account_balance;Uid=root;Pwd=r00t;");
                
            }
            catch (Exception ex)
            {
                ex.ToString();
            } 
            
            return null;
        }
    }
}