using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Employees.Monolith.LogicLayer.Models.Requests.Entities
{
    public class PaginationRequest
    {
        [Range(0, int.MaxValue)]
        public int From { get; set; }
        [Range(0, int.MaxValue)]
        public int To { get; set; }
        public int GetTake()
        {
            var result = Math.Abs(To - From) + 1;
            return result;
        }
    }
}
