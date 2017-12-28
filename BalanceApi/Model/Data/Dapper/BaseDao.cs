using System.Data;
using System.Linq;
using BalanceApi.Domain;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace BalanceApi.Model.Data.Dapper
{
    public abstract class BaseDao
    {
        private const string LastInsertId = "select LAST_INSERT_ID()";

        private AppSettings Settings { get; }

        private readonly ILogger _logger; 
        
        protected BaseDao(IOptions<AppSettings> appSettings, ILogger logger)
        {
            Settings = appSettings.Value;
            _logger = logger;
        }
        
        protected IDbConnection GetConnection()
        {
            _logger.LogInformation("Getting DB connection");
            return new MySqlConnection(Settings.ConnectionString);
        }

        protected long GetLasInsertedId() {
            _logger.LogInformation("Getting last inserted Id");
            return GetConnection().Query<long>(LastInsertId).Single();
        }
    }
}