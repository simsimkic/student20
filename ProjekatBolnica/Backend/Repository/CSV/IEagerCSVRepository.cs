using System;
using System.Collections.Generic;

namespace ProjekatBolnica.Backend.Repository.CSV
{
    public interface IEagerCSVRepository<E, ID>
        where E : IIdentifiable<ID>
        where ID : IComparable
    {
        E GetEager(ID id);
        IEnumerable<E> GetAllEager();
    }
}
