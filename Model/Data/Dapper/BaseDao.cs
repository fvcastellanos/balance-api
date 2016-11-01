using System;
using System.Data;
using MySql.Data.MySqlClient;

using Microsoft.Extensions.Logging;
using BalanceApi.Domain;
using Microsoft.Extensions.Options;
using Dapper;
using System.Linq;

namespace BalanceApi.Model.Data.Dapper
{
    public abstract class BaseDao
    {
        protected static string LAST_INSERT_ID = "select LAST_INSERT_ID()";

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
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        protected long GetLasInsertedId() {
            try {
                Logger.LogInformation("Getting last inserted Id");
                return getConnection().Query<long>(LAST_INSERT_ID).Single();
            } catch(Exception ex) {
                throw ex;
            }
        }
    }
}