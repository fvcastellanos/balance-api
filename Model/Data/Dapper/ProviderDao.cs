
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
        private static string FIND_PROVIDER = "select * from provider where name = @Name and country = @Country";
        private static string NEW = "insert into provider (name, country) values (@Name, @Country)";

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

        public List<Provider> GetByCountry(string country)
        {
            try {
                return getConnection().Query<Provider>(GET_BY_COUNTRY, new { Country = country }).AsList();
            } catch(Exception ex) {
                throw ex;
            }            
        }

        public Provider GetById(long id)
        {
            try {
                return getConnection().Query<Provider>(GET_BY_ID, new { Id = id }).SingleOrDefault();
            } catch(Exception ex) {
                throw ex;
            }
        }

        public Provider FindProvider(string name, string country) {
            try {
                return getConnection().Query<Provider>(FIND_PROVIDER, new { Name = name, Country = country }).SingleOrDefault();
            } catch(Exception ex) {
                throw ex;
            }
        }

        public long New(string name, string country) {
            try {
                long id = 0;
                int rows = getConnection().Execute(NEW, new { Name = name, Country = country});
                if(rows > 0) {
                    id = GetLasInsertedId();
                }

                return id;
            } catch(Exception ex) {
                throw ex;
            }
        }

        
    }
}