using Employees.Monolith.DataLayer.Models.Tables.Entities;
using System;

namespace Employees.Monolith.LogicLayer.Models.Creaters.Entities
{
    public class PermissionTableCreater : IdCreater<PermissionTable>
    {
        public PermissionTableCreater() : base(0)
        {
        }
        public PermissionTableCreater(int id) : base(id)
        {
        }
        public override PermissionTable Create()
        {
            var nowAt = DateTime.UtcNow;
            var result = new PermissionTable
            {
                UserId = Id,
                CreatedAt = nowAt,
                UpdateAt = nowAt
            };
            return result;
        }

        public override PermissionTable Update(PermissionTable result)
        {
            result.UpdateAt = DateTime.UtcNow;
            return result;
        }
    }
}
