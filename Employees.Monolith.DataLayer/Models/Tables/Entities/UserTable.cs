using System;
using System.Text.Json.Serialization;

namespace Employees.Monolith.DataLayer.Models.Tables.Entities
{
    public class UserTable : BaseTable, IUpdateAtTable
    {
        public string Login { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public RoleUserTable Role { get; set; }
        public LanguageUserTable Language { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool ConfirmEmail { get; set; }
        public bool ConfirmTFA { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
