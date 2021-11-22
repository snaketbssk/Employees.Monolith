using Employees.Monolith.DataLayer.Models.Tables.Entities;
using System;
using System.Collections.Generic;

namespace Employees.Monolith.LogicLayer.Models.Creaters.Entities
{
    public class ReferralTableCreater : IdCreater<ReferralTable>
    {
        public ReferralTableCreater(int id) : base(id)
        {
        }
        public override ReferralTable Create()
        {
            var nowAt = DateTime.UtcNow;
            var result = new ReferralTable
            {
                UserId = Id,
                Users = new List<UserTable>(),
                CreatedAt = nowAt,
                UpdateAt = nowAt
            };
            return result;
        }

        public override ReferralTable Update(ReferralTable result)
        {
            throw new NotImplementedException();
        }
    }
}
