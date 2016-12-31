
using System;
using System.Collections.Generic;
using System.Linq;
using BalanceApi.Domain;
using BalanceApi.Model.Domain;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BalanceApi.Model.Data.Dapper {

    public class ProviderDao : BaseDao, IProviderDao
    {
        private readonly ILogger _logger;

        private static string GET_ALL = "select * from provider";
        private static string GET_BY_COUNTRY = "select * from provider where country = @Country";
        private static string GET_BY_ID = "select * from provider where id = @Id";
        private static string FIND_PROVIDER = "select * from provider where name = @Name and country = @Country";
        private static string NEW = "insert into provider (name, country) values (@Name, @Country)";
        private static string DELETE = "delete from provider where id = @Id";

        public ProviderDao(IOptions<AppSettings> settings,  ILogger<ProviderDao> logger) : base(settings, logger)
        {
            _logger = logger;
        }

        public List<Provider> GetAll()
        {
            return GetConnection().Query<Provider>(GET_ALL).AsList();
        }

        public List<Provider> GetByCountry(string country)
        {
            return GetConnection().Query<Provider>(GET_BY_COUNTRY, new { Country = country }).AsList();
        }

        public Provider GetById(long id)
        {
            return GetConnection().Query<Provider>(GET_BY_ID, new { Id = id }).SingleOrDefault();
        }

        public Provider FindProvider(string name, string country)
        {
            return GetConnection().Query<Provider>(FIND_PROVIDER, new { Name = name, Country = country }).SingleOrDefault();
        }

        public long New(string name, string country) {
            long id = 0;
            var rows = GetConnection().Execute(NEW, new { Name = name, Country = country});
            if(rows > 0) {
                id = GetLasInsertedId();
            }

            return id;
        }

        public int Delete(long id) {
            var rows = GetConnection().Execute(DELETE, new { Id = id });
            return rows;
        }

        public Provider Update(Provider provider) {
            GetConnection().Execute("update provider set name = @Name, country = @Country where id = @Id",
                new {provider.Name, provider.Country, provider.Id });
            return GetById(provider.Id);
        }
    }
}