/***********************************************************************
 * Module:  IngredientRepository.cs
 * Purpose: Definition of the Class Repository.ManagerRepository.IngredientRepository
 ***********************************************************************/

using System;
using System.Runtime.Serialization;

namespace Repository.ManagerRepository
{
    [Serializable]
    internal class NotUniqueException : Exception
    {
        public NotUniqueException()
        {
        }

        public NotUniqueException(string message) : base(message)
        {
        }

        public NotUniqueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}