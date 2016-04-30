
using System.Collections.Generic;
using Model.Domain;

namespace Model.Data
{
    public interface IEventMappingDao
    {
        List<EventMapping> findAll();
    }
}