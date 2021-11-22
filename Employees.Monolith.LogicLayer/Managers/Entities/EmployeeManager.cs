using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Models.Creaters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Monolith.LogicLayer.Managers.Entities
{
    public class EmployeeManager : Manager<DatabaseContext>
    {
        public EmployeeManager(DatabaseContext context) : base(context)
        {
        }

        public async Task<EmployeeTable> CreateAsync(IEmployeeTableCreater employeeTableCreater)
        {
            var employee = employeeTableCreater.Create();
            var department = await _context.Departments.FirstAsync(v => v.Guid.Equals(employeeTableCreater.GuidDepartment));
            employee.DepartmentId = department.Id;
            employee.Skills = new List<SkillTable>();
            for (int i = 0, count = employeeTableCreater.GuidSkills?.Count ?? default; i < count; i++)
            {
                var skill = await _context.Skills.FirstOrDefaultAsync(v => v.Guid.Equals(employeeTableCreater.GuidSkills[i]));
                if (skill != null) employee.Skills.Add(skill);
            }
            var result = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<EmployeeTable> UpdateAsync(Guid guid, IEmployeeTableCreater employeeTableCreater)
        {
            var employee = await _context.Employees
                .Include(v => v.Department)
                .Include(v => v.Skills)
                .FirstOrDefaultAsync(v => v.Guid.Equals(guid));
            employee = employeeTableCreater.Update(employee);
            var department = await _context.Departments.FirstAsync(v => v.Guid.Equals(employeeTableCreater.GuidDepartment));
            employee.DepartmentId = department.Id;
            employee.Skills = new List<SkillTable>();
            for (int i = 0, count = employeeTableCreater.GuidSkills?.Count ?? default; i < count; i++)
            {
                var skill = await _context.Skills.FirstOrDefaultAsync(v => v.Guid.Equals(employeeTableCreater.GuidSkills[i]));
                if (skill != null) employee.Skills.Add(skill);
            }
            var result = _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
