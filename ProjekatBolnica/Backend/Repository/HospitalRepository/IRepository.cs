

using ProjekatBolnica.Backend.Repository;
using System;
using System.Collections.Generic;

namespace Repository.HospitalRepository
{
    public interface IRepository<E, ID>
        where E : IIdentifiable<ID>
        where ID : IComparable
    {
        void Set(E obj);
        E Get(ID id);
        IEnumerable<E> GetAll();
        void New(E obj);
        void Delete(E obj);
        void OpenFile(String myPath);
        void CloseFile(String myPath);
        IEnumerable<E> Find(Func<E, bool> predicate);

    }
}