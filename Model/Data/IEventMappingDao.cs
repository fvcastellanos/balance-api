
using System.Collections.Generic;

namespace Model.Data
{
    public interface IEventMappingDao
    {
        IEnumerable<long> findAll();
    }
}