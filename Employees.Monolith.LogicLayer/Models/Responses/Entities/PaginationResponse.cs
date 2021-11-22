using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Monolith.LogicLayer.Models.Responses.Entities
{
    public class PaginationResponse<T>
    {
        public int TotalCount { get; set; }
        public List<T> Values { get; set; }
        public PaginationResponse(int totalCount)
        {
            TotalCount = totalCount;
            Values = new List<T>();
        }
    }
}
