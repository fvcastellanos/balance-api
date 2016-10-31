
using System;
using BalanceApi.Domain;
using BalanceApi.Model.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Dapper;
using System.Collections.Generic;

namespace BalanceApi.Model.Data.Dapper {

    public class ProviderDao : BaseDao, IProviderDao
    {
        private ILogger logger;
        public ProviderDao(IOptions<AppSettings> settings,  ILogger<ProviderDao> logger) : base(settings, logger)
        {
            this.logger = logger;
        }

        public List<Provider> GetAll()
        {
            try {
                return getConnection().Query<Provider>("select id, name, country from provider").AsList();
            } catch(Exception ex) {
                throw ex;
            }
        }
    }
}