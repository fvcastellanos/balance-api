using System;
using System.Collections.Generic;
using Dapper;

using Model.Domain;

namespace Model.Data.Dapper
{
    
    public class EventMappingDao : BaseDao, IEventMappingDao
    {
        public List<EventMapping> findAll()
        {
            try
            {
                return getConnection().Query<EventMapping>("select id, xoom_event_name, partner from integrationdb.event_mapping").AsList();    
            } catch(Exception ex) {
                //ex.Message;
            }
            
            return null;
            
        }
    }
}