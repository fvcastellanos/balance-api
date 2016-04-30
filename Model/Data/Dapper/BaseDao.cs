using System;
using System.Data;
using System.Data.SqlClient;

using Configuration;

namespace Model.Data.Dapper
{
    public abstract class BaseDao
    {
        protected AppSettingsHelper settingsHelper;
        
        protected IDbConnection getConnection() 
        {
            try
            {
                return new SqlConnection(settingsHelper.getConnectionString());
            }
            catch(Exception ex)
            {
                ex.ToString();
            } 
            
            return null;
        }
    }
}