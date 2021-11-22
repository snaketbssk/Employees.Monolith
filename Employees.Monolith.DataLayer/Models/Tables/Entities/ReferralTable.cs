using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Employees.Monolith.DataLayer.Models.Tables.Entities
{
    public class ReferralTable : ConnectionUserTable, IUpdateAtTable
    {
        [JsonIgnore]
        public ICollection<UserTable> Users { get; set; }
        public int UsersCount
        {
            get
            {
                var result = 0;
                if (Users != null) result = Users.Count;
                return result;
            }
        }
        public DateTime UpdateAt { get; set; }
    }
}
