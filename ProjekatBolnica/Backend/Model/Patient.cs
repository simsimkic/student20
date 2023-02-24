
using Model.UserModel;
using ProjekatBolnica.Backend.Repository;
using System;
using System.Security.RightsManagement;

namespace Model
{
   public class Patient : User
    {
        public String Allergy { get; set; }
        public Boolean IsGuest { get; set; }

        public Patient(long id,String username, Boolean active,  String password, String name, 
            String surname, String email, String personalIDnumber, String telephone, Gender gender, DateTime dateOfBirth,
            Address address, Language language, String allergy, Boolean isGuest) : base(id,username,active,TypeOfUser.Patient,password,name,surname,email,personalIDnumber,telephone,
                gender,dateOfBirth,address,language)
        {
            Allergy = allergy;
            IsGuest = IsGuest;
        }

        public Patient( String username, Boolean active, String password, String name,
          String surname, String email, String personalIDnumber, String telephone, Gender gender, DateTime dateOfBirth,
          Address address, Language language, String allergy, Boolean isGuest) : base(username, active, TypeOfUser.Patient, password, name, surname, email, personalIDnumber, telephone,
              gender, dateOfBirth, address, language)
        {
            Allergy = allergy;
            IsGuest = IsGuest;
        }

        public Patient() { }

        public Patient(long id) {
            Id = id;
        }


        public Patient(String username)
        {
            Username = username;
        }
    }
}