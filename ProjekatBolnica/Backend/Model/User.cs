/***********************************************************************
 * Module:  User.cs
 * Purpose: Definition of the Class User
 ***********************************************************************/

using Model.UserModel;
using ProjekatBolnica.Backend.Repository;
using System;

namespace Model
{
    public class User : IIdentifiable<long>
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public Boolean Active { get; set; }
        public TypeOfUser Role { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }
        public String PersonalIDnumber { get; set; }
        public String Telephone { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }
        public Language Language { get; set; }

        public User() { }
        public User(long id, String username, Boolean active, TypeOfUser role, String password, String name,
            String surname, String email, String personalIDnumber, String telephone, Gender gender, DateTime dateOfBirth,
            Address address, Language language)
        {
            Id = id;
            Username = username;
            Active = active;
            Role = role;
            Password = password;
            Name = name;
            Surname = surname;
            Email = email;
            PersonalIDnumber = personalIDnumber;
            Telephone = telephone;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Address = address;
            Language = language;

        }

        public User( String username, Boolean active, TypeOfUser role, String password, String name,
            String surname, String email, String personalIDnumber, String telephone, Gender gender, DateTime dateOfBirth,
            Address address, Language language)
        {
            Username = username;
            Active = active;
            Role = role;
            Password = password;
            Name = name;
            Surname = surname;
            Email = email;
            PersonalIDnumber = personalIDnumber;
            Telephone = telephone;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Address = address;
            Language = language;

        }


        public User(long id)
        {
            Id = id;
        }

        public User(String username)
        {
            Username = username;
        }

        public long GetId() => Id;
        public void SetId(long id) => Id = id;

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}