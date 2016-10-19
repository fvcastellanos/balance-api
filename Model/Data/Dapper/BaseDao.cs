using System;
using System.Data;
using MySql.Data.MySqlClient;

using Microsoft.Extensions.Logging;
using BalanceApi.Domain;

namespace BalanceApi.Model.Data.Dapper
{
    public abstract class BaseDao
    {
        protected AppSettings Settings;

        protected ILogger Logger; 
        protected BaseDao(AppSettings Settings, ILogger Logger)
        {
            this.Settings = Settings;
            this.Logger = Logger;
        }
        
        protected IDbConnection getConnection() 
        {
            try
            {
                Logger.LogInformation("Getting DB connection");
                return new MySqlConnection(Settings.ConnectionString);
                // return new MySqlConnection("Server=localhost;Database=account_balance;Uid=root;Pwd=r00t;");
                
            }
            catch (Exception ex)
            {
                ex.ToString();
            } 
            
            return null;
        }
    }
}