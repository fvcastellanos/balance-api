using System;
using System.Collections.Generic;
using Configuration;
using Dapper;

using Model.Domain;
using Microsoft.Extensions.Logging;

namespace Model.Data.Dapper
{
    
    public class EventMappingDao : BaseDao, IEventMappingDao
    {
        ILogger<EventMappingDao> logger;

        public EventMappingDao(AppSettingsHelper settingsHelper, ILogger<EventMappingDao> logger) : base(settingsHelper)
        {
            this.logger = logger;
        }

        public List<EventMapping> findAll()
        {
            try
            {
                return getConnection().Query<EventMapping>("select id, xoom_event_name, partner from integrationdb.event_mapping").AsList();    
            } catch(Exception ex) {
                logger.LogError("Exception thrown when trying to get all the rows", ex);
            }
            
            return null;
            
        }
    }
}