using System;

namespace Employees.Monolith.DataLayer.Models.Tables.Entities
{
    public class TokenTable : ConnectionUserTable, IExpiredAtTable, IUpdateAtTable
    {
        public bool IsActive { get; set; }
        public string FamilyOS { get; set; }
        public string MajorOS { get; set; }
        public string FamilyUserAgent { get; set; }
        public string MajorUserAgent { get; set; }
        public string FamilyDevice { get; set; }
        public string BrandDevice { get; set; }
        public string ModelDevice { get; set; }
        public string IpAddress { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
