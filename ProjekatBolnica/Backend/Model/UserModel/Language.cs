/***********************************************************************
 * Module:  Language.cs
 * Purpose: Definition of the Class User.Language
 ***********************************************************************/

using ProjekatBolnica.Backend.Repository;
using System;

namespace Model.UserModel
{
   public class Language : IIdentifiable<long>
    {
      private String Serbian;
      private String English;
      private String Chinese;
      private String Spanish;
      private String Russian;
      private String Portuguese;
      private String Japanese;
      private String French;
      private string _token;
        public Language()
        {

        }


        public long Id { get; set; }



        public Language(string token)
        {
            _token = token;
        }

        public long GetId()
        {
            throw new NotImplementedException();
        }

        public void SetId(long id)
        {
            throw new NotImplementedException();
        }
    }
}