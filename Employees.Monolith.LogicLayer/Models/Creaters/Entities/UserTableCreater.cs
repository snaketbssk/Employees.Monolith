using Employees.Monolith.DataLayer.Models.Tables;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Extensions;
using System;

namespace Employees.Monolith.LogicLayer.Models.Creaters.Entities
{
    public class UserTableCreater : Creater<UserTable>, IUserTableCreater
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public RoleUserTable Role { get; set; }
        public LanguageUserTable Language { get; set; }
        public UserTableCreater(
            string login,
            string email,
            string password,
            string firstName,
            string middleName,
            string lastName,
            string phone,
            RoleUserTable role,
            LanguageUserTable language)
            :
            base()
        {
            Login = login;
            Email = email;
            Password = password;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Phone = phone;
            Role = role;
            Language = language;
        }

        public override UserTable Create()
        {
            var nowAt = DateTime.UtcNow;
            var result = new UserTable
            {
                Login = Login,
                Email = Email,
                Password = Password,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                Role = Role,
                Phone = Phone.ToPhoneFormat(),
                Language = Language,
                CreatedAt = nowAt,
                UpdateAt = nowAt
            };
            return result;
        }

        public override UserTable Update(UserTable result)
        {
            throw new NotImplementedException();
        }
    }
}
