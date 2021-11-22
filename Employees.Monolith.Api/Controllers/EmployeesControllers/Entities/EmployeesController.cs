using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.Api.Controllers.EmployeesControllers.Requests.Entities;
using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.DataLayer.Models.Tables;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Models.Constants;
using Employees.Monolith.LogicLayer.Models.Requests.Entities;
using Employees.Monolith.LogicLayer.Models.Responses.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Monolith.Api.Controllers.EmployeesControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly DatabaseContext _context;
        public EmployeesController(ILogger<EmployeesController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployeeTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _context.Employees
                .Include(v => v.Skills)
                .ToListAsync();
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<EmployeeTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> PostAsync([FromBody] PostEmployeesRequest request)
        {
            IQueryable<EmployeeTable> query = _context.Employees.Include(v => v.Skills);
            if (request.GuidDepartments != null && request.GuidDepartments.Count > 0)
            {
                var guidDepartaments = new HashSet<Guid>(request.GuidDepartments);
                var departments = await _context.Departments
                    .Where(v => guidDepartaments.Contains(v.Guid.Value))
                    .ToListAsync();
                var idDepartments = departments.Select(v => v.Id).ToHashSet();
                query = query.Where(v => idDepartments.Contains(v.DepartmentId));
            }
            if (request.GuidSkills != null && request.GuidSkills.Count > 0)
            {
                var guidSkills = new HashSet<Guid>(request.GuidSkills);
                var skills = await _context.Skills
                    .Where(v => guidSkills.Contains(v.Guid.Value))
                    .ToListAsync();
                var idSkills = skills.Select(v => v.Id).ToHashSet();
                query = query.Where(v => v.Skills.Any(s => idSkills.Contains(s.Id)));
            }
            if (request.MinSalary.HasValue && request.MinSalary != 0m)
            {
                query = query.Where(v => v.Salary >= request.MinSalary.Value);
            }
            if (request.MaxSalary.HasValue && request.MaxSalary != 0m)
            {
                query = query.Where(v => v.Salary <= request.MaxSalary.Value);
            }
            if (!string.IsNullOrWhiteSpace(request.FirstName))
            {
                query = query.Where(v => v.FirstName.Contains(request.FirstName));
            }
            if (!string.IsNullOrWhiteSpace(request.MiddleName))
            {
                query = query.Where(v => v.MiddleName.Contains(request.MiddleName));
            }
            if (!string.IsNullOrWhiteSpace(request.LastName))
            {
                query = query.Where(v => v.LastName.Contains(request.LastName));
            }
            if (!string.IsNullOrWhiteSpace(request.PatronymicName))
            {
                query = query.Where(v => v.PatronymicName.Contains(request.PatronymicName));
            }
            var result = await query.ToListAsync();
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="paginationRequest"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{From}/{To}")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> PostAsync([FromRoute] PaginationRequest paginationRequest, [FromBody] PostEmployeesRequest request)
        {
            IQueryable<EmployeeTable> query = _context.Employees.Include(v => v.Skills);
            if (request.GuidDepartments != null && request.GuidDepartments.Count > 0)
            {
                var guidDepartaments = new HashSet<Guid>(request.GuidDepartments);
                var departments = await _context.Departments
                    .Where(v => guidDepartaments.Contains(v.Guid.Value))
                    .ToListAsync();
                var idDepartments = departments.Select(v => v.Id).ToHashSet();
                query = query.Where(v => idDepartments.Contains(v.DepartmentId));
            }
            if (request.GuidSkills != null && request.GuidSkills.Count > 0)
            {
                var guidSkills = new HashSet<Guid>(request.GuidSkills);
                var skills = await _context.Skills
                    .Where(v => guidSkills.Contains(v.Guid.Value))
                    .ToListAsync();
                var idSkills = skills.Select(v => v.Id).ToHashSet();
                query = query.Where(v => v.Skills.Any(s => idSkills.Contains(s.Id)));
            }
            if (request.MinSalary.HasValue && request.MinSalary != 0m)
            {
                query = query.Where(v => v.Salary >= request.MinSalary.Value);
            }
            if (request.MaxSalary.HasValue && request.MaxSalary != 0m)
            {
                query = query.Where(v => v.Salary <= request.MaxSalary.Value);
            }
            if (!string.IsNullOrWhiteSpace(request.FirstName))
            {
                query = query.Where(v => v.FirstName.Contains(request.FirstName));
            }
            if (!string.IsNullOrWhiteSpace(request.MiddleName))
            {
                query = query.Where(v => v.MiddleName.Contains(request.MiddleName));
            }
            if (!string.IsNullOrWhiteSpace(request.LastName))
            {
                query = query.Where(v => v.LastName.Contains(request.LastName));
            }
            if (!string.IsNullOrWhiteSpace(request.PatronymicName))
            {
                query = query.Where(v => v.PatronymicName.Contains(request.PatronymicName));
            }
            var take = paginationRequest.GetTake();
            var totalCount = await query.CountAsync();
            var employees = await query
                .OrderBy(v => v.Id)
                .Skip(paginationRequest.From)
                .Take(take)
                .ToListAsync();
            var result = new PaginationResponse<EmployeeTable>(totalCount);
            result.Values.AddRange(employees);
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{From}/{To}")]
        [ProducesResponseType(typeof(PaginationResponse<EmployeeTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAsync([FromRoute] PaginationRequest request)
        {
            var take = request.GetTake();
            var totalCount = await _context.Employees.CountAsync();
            var employees = await _context.Employees
                .OrderBy(v => v.Id)
                .Skip(request.From)
                .Take(take)
                .ToListAsync();
            var result = new PaginationResponse<EmployeeTable>(totalCount);
            result.Values.AddRange(employees);
            return Ok(result);
        }
        /// <summary>
        /// [ADMINISTRATORS]
        /// </summary>
        /// <param name="guidRequest"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("Department/{Guid}/{From}/{To}")]
        [ProducesResponseType(typeof(PaginationResponse<EmployeeTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetAsync([FromRoute] GuidRequest guidRequest, [FromRoute] PaginationRequest request)
        {
            var take = request.GetTake();
            var department = await _context.Departments.FirstAsync(v => v.Guid.Equals(guidRequest.Guid));
            var totalCount = await _context.Employees.CountAsync();
            var employees = await _context.Employees
                .Where(v => v.DepartmentId.Equals(department.Id))
                .OrderBy(v => v.Id)
                .Skip(request.From)
                .Take(take)
                .ToListAsync();
            var result = new PaginationResponse<EmployeeTable>(totalCount);
            result.Values.AddRange(employees);
            return Ok(result);
        }
    }
}
