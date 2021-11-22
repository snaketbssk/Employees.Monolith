using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Models.Creaters;
using System.ComponentModel.DataAnnotations;

namespace Employees.Monolith.Api.Controllers.SkillControllers.Requests.Entities
{
    public class PutSkillRequest : ICreater<SkillTable>
    {
        [StringLength(32)]
        public string Title { get; set; }
        public SkillTable Create()
        {
            var result = new SkillTable
            {
                Title = Title
            };
            return result;
        }

        public SkillTable Update(SkillTable result)
        {
            result.Title = Title;
            return result;
        }
    }
}
