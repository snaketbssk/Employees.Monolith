using Employees.Monolith.DataLayer.Models.Tables;
using Employees.Monolith.DataLayer.Models.Tables.Entities;

namespace Employees.Monolith.LogicLayer.Models.Creaters
{
    public interface IUserTableCreater : ICreater<UserTable>
    {
        string Login { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Phone { get; set; }
        RoleUserTable Role { get; set; }
        LanguageUserTable Language { get; set; }
    }
}
