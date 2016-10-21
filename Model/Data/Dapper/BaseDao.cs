using System;
using System.Data;
using MySql.Data.MySqlClient;

using Microsoft.Extensions.Logging;
using BalanceApi.Domain;
using Microsoft.Extensions.Options;

namespace BalanceApi.Model.Data.Dapper
{
    public abstract class BaseDao
    {
        protected AppSettings Settings { get; }

        protected ILogger Logger; 
        protected BaseDao(IOptions<AppSettings> appSettings, ILogger Logger)
        {
            this.Settings = appSettings.Value;
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
                Logger.LogError("Unable to create db connection", ex);
                throw ex;
            } 
        }
    }
}