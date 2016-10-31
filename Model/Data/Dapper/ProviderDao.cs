
using System;
using BalanceApi.Domain;
using BalanceApi.Model.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace BalanceApi.Model.Data.Dapper {

    public class ProviderDao : BaseDao, IProviderDao
    {
        private ILogger logger;

        private static string GET_ALL = "select * from provider";
        private static string GET_BY_COUNTRY = "select * from provider where country = @Country";
        private static string GET_BY_ID = "select * from provider where id = @Id";

        public ProviderDao(IOptions<AppSettings> settings,  ILogger<ProviderDao> logger) : base(settings, logger)
        {
            this.logger = logger;
        }

        public List<Provider> GetAll()
        {
            try {
                return getConnection().Query<Provider>(GET_ALL).AsList();
            } catch(Exception ex) {
                throw ex;
            }
        }

        List<Provider> IProviderDao.GetByCountry(string country)
        {
            try {
                return getConnection().Query<Provider>(GET_BY_COUNTRY, new { Country = country }).AsList();
            } catch(Exception ex) {
                throw ex;
            }            
        }

        Provider IProviderDao.GetById(long id)
        {
            try {
                return getConnection().Query<Provider>(GET_BY_ID, new { Id = id }).SingleOrDefault();
            } catch(Exception ex) {
                throw ex;
            }
        }
    }
}