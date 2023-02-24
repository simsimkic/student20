/***********************************************************************
 * Module:  IController.cs
 * Purpose: Definition of the Interface Controller.UserController.IController
 ***********************************************************************/

using System.Collections.Generic;

namespace Controller.HospitalController
{
    public interface IController<E, ID> where E : class
    {
        void Set(E obj);
        E Get(ID id);
        IEnumerable<E> GetAll();
        void New(E obj);
        void Delete(E obj);
    }
}